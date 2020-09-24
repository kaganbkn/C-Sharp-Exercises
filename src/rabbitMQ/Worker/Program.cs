using System;
using System.Linq;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "task_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Or, in other words, don't dispatch a new message to a worker until it has processed and acknowledged the previous one.
            channel.BasicQos(0,1,false);


            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                int dots = message.Count();
                Thread.Sleep(dots * 1000);

                Console.WriteLine(" [x] Received {0}", message);

                //channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); //manuel ack
                // ack : if the consumer dies...
            };

            channel.BasicConsume(queue: "task_queue",
                autoAck: true,
                consumer: consumer);


            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
