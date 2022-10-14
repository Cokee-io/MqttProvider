using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


Console.WriteLine("Connecting . . .");
var factory = new ConnectionFactory
{
    // Uri = new Uri("amqp://ec2-3-34-123-235.ap-northeast-2.compute.amazonaws.com:5672"),
    Uri = new Uri("amqps://b-5ffc876c-f4a1-44e5-a730-9fcd7112eee4.mq.ap-northeast-2.amazonaws.com:5671"),
    UserName = "admin",
    // UserName = "admin",
    Password = "ejrrpdlawm1!"
    // Password = "Cokee"
};

// var queue = "demo-queue";
var queue = "queue_game_info";

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.ConfirmSelect();
channel.WaitForConfirmsOrDie();
Console.WriteLine("Connected to " + factory.Uri);
Console.WriteLine(" [*] Ready to send messages." +
                  "To exit press CTRL+C");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) =>
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

channel.BasicConsume(queue, true, consumer);

// Queue Message Ex) {"header":{"cmd":"StartGame"},"body":{"deviceCode":10001,"gameCode":1780}}

while(true)
{
    //loop for user to supply messages
}