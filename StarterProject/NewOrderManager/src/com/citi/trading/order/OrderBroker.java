package com.citi.trading.order;

import java.sql.Timestamp;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.jms.Message;
import javax.jms.MessageListener;
import javax.jms.QueueReceiver;
import javax.jms.QueueSender;
import javax.jms.TextMessage;

import com.citi.trading.Trade;
import com.citi.trading.jms.PointToPointClient;
import com.citi.trading.mq.TradeMessenger;

//puts the message on the message queue
public class OrderBroker
    implements MessageListener
{
    private static Logger LOGGER = 
        Logger.getLogger (OrderBroker.class.getName ());

    /**
    Sends the trade request to the exchange.
    Returns shares actually transacted.
    */
    public int makeTrade (Trade trade)
    {
        OrderManager.OrderResult result =
            trade.isBuy ()
                ? OrderManager.getInstance ().buyOrder 
                    (trade.getStock (), trade.getPrice (), trade.getSize ())
                : OrderManager.getInstance ().sellOrder 
                    (trade.getStock (), trade.getPrice (), trade.getSize ());
        
        return result.shares;
    }

    /**
    Take incoming trade messages, break out the Trade content,
    and {@link #makeTrade place the trade with the OrderManager}.
    */
    @Override
    public void onMessage (Message message)
    {
        LOGGER.log (Level.INFO, "Received trade request.");
        
        if (!(message instanceof TextMessage))
        {
            LOGGER.log (Level.WARNING, 
                "Received non-text message -- ignoring.");
            return;
        }
        
        Trade trade = TradeMessenger.parseTradeMessage ((TextMessage) message);
        int shares = makeTrade (trade);
        
        trade.setSize (shares);
        trade.setToNow ();
        
        PointToPointClient replyClient = null;
        try
        {
            if (message.getJMSCorrelationID () != null)
            {
            	message.setJMSCorrelationID("Quick fix for JMS: id = 1");
            }
                replyClient = 
                    new PointToPointClient (TradeMessenger.RESPONSE_QUEUE);
                QueueSender sender = replyClient.openToSend ();
                Message reply = replyClient.getSession ().createTextMessage 
                    (TradeMessenger.tradeToXML (trade));
                reply.setJMSCorrelationID (message.getJMSCorrelationID ());
                sender.send (reply);
//            }
//            else
//                LOGGER.log (Level.WARNING, "No correlation ID found in trade request; no response message sent to reply queue. Trade ID is " + trade.getId ());
        }
        catch (Exception ex)
        {
            LOGGER.log (Level.SEVERE, 
                "Failed to send response to confirm requested trade", ex);
        }
        finally
        {
            if (replyClient != null)
                replyClient.close ();
        }
    }
    
    public static void main (String[] args)
    {
        try
        {
            TradeMessenger service = new TradeMessenger ();
            QueueReceiver receiver = service.openToReceive ();
            receiver.setMessageListener (new OrderBroker ());
            System.out.println ("Listening for trade messages on OrderBroker queue ...");
            
            //creating a trade
//            Trade t = new Trade();
//            t.setBuy(true);
//            t.setStock("APPL");
//            t.setSize(200);
//            t.setId(4);
//            t.setToNow();
//            t.setPrice(20.00);
//            OrderBroker ob = new OrderBroker();
//            ob.makeTrade(t); //send to order manager, order manager will make the trade with exchange
        }
        catch (Exception ex)
        {
            LOGGER.log (Level.SEVERE, "OrderBroker crashed!", ex);
        }
    }
}
