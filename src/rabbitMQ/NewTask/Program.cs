using System;
using System.Text;
using RabbitMQ.Client;

namespace NewTask
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

            Console.WriteLine("Write something for send to message.");

            while (true)
            {
                var message = Console.ReadLine();

                if (message == "exit")
                    break;

                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true; //persist to disk

                channel.BasicPublish(exchange: "",
                    routingKey: "task_queue",
                    basicProperties: properties,
                    body: body);

                Console.WriteLine(" [x] Sent {0}", message);


                Console.WriteLine(" Press [exit] to exit.");
            }
        }
    }
}
