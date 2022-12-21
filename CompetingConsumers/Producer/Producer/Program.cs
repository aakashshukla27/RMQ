using System.Text;
using RabbitMQ.Client;
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
                arguments: null
            );

            var messageId = 1;
            var random = new Random();

            while (true)
            {
                var message = $"Sending Message Id: {messageId}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "letterbox", null, body);
                Console.WriteLine($"Send Message: {message}");
                var waitTime = random.Next(1, 8);
                Task.Delay(TimeSpan.FromSeconds(waitTime)).Wait();
                messageId++;
            }
        }
    }
}

 