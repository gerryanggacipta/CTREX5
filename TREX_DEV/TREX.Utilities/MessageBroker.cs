using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace TREX.Utilities
{
    public class MessageBroker
    {
        static IConnection connection;
        static ISession session;
        static IDestination dest;
        static IMessageProducer producer;
        static IMessageConsumer consumer;

        private static AutoResetEvent semaphore = new AutoResetEvent(false);
        private static ITextMessage asyncMessage = null;

        private const string TO_EXCHANGE_QUEUE = "OrderBroker";
        private const string FROM_EXCHANGE_QUEUE = "OrderBroker_Reply";

        public static event Action<string> tradeRepliedFromExchange;
        public const int SLEEP_INTERVAL = 1000;

        public MessageBroker()
        {
            IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616");
            connection = factory.CreateConnection();
            connection.Start();
            session = connection.CreateSession();

            Console.WriteLine(">> Broker created");
        }      

        public void SendOrder(String xmlTrade)
        {
            dest = session.GetQueue(TO_EXCHANGE_QUEUE);

            if (producer == null)
                producer = session.CreateProducer(dest);

            ITextMessage message = producer.CreateTextMessage(xmlTrade);
            producer.Send(message);            
        }

        public void StartAsyncReceiveTrade()
        {
            AsyncReceiveTrade();
        }

        protected void AsyncReceiveTrade()
        {
            dest = session.GetQueue(FROM_EXCHANGE_QUEUE);

            if (consumer == null)
                consumer = session.CreateConsumer(dest);

            consumer.Listener += new MessageListener(OnMessage);

            // Wait for the message
            semaphore.WaitOne();

            System.Threading.Thread.Sleep(SLEEP_INTERVAL);
        }

        protected static void OnMessage(IMessage receivedMsg)
        {
            asyncMessage = receivedMsg as ITextMessage;

            if (asyncMessage == null)
            {
                Console.WriteLine("No message received!");
            }
            else
            {
                Console.WriteLine(">> [MessageBroker] Trade replied from Exchange");
                tradeRepliedFromExchange(asyncMessage.Text);
            }
            semaphore.Set();
        }

        public void Dispose()
        {
            try
            {
                connection.Close();
                Console.WriteLine(">> Connection closed");
            }
            catch (Exception e) {
                Console.WriteLine(">> " + e.Message);
            } 
        }
    }
}
