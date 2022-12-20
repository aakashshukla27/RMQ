using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "letterbox",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = "This is my first Message";

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", "letterbox", null, body);

            Console.WriteLine($"Send message: {message}");

        }
    }
}