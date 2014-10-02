using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Util;
using System.Threading;
using TREX.Entities;

namespace TREX.Utilities
{
    public class QueueManager
    {
        private static QueueManager _instance = null;
        private MessageBroker broker = null;

        // Event for every trade prepared to send
        public event Action<Trade> TradeSending;

        // Event fired every time the async consumer received trade from Active MQ
        public event Action<Trade> TradeReplied;

        private QueueManager() 
        {
            Console.WriteLine(">> Enter constructor of QueueManager");

            broker = new MessageBroker();
            MessageBroker.tradeRepliedFromExchange += new Action<string>(notifyTradeReceivedFromExchange);
        }

        // Instance to access Singleton
        public static QueueManager Instance 
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new QueueManager();
                }

                return _instance;
            }
        }

        public void sendMessage(Trade trade)
        {
            string test = MessageTranslator.ConvertTradeToXML(trade);

            // Call TradeManager to process it before sending
            TradeSending(trade);

            broker.SendOrder(test);
        }

        public void startAsyncReceiveMessage()
        {
            Thread asyncConsumerThread = new Thread(new ThreadStart(broker.StartAsyncReceiveTrade));
            asyncConsumerThread.IsBackground = true;
            asyncConsumerThread.Priority = ThreadPriority.Normal;
            asyncConsumerThread.Start();
        }

        private void notifyTradeReceivedFromExchange(string xmlTrade)
        {
            Console.WriteLine(">> [QueueManager] XML data received from Exchange");
            TradeReplied(MessageTranslator.ConvertXMLToTrade(xmlTrade));
        }
        
    }
}
