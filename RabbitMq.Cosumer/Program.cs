using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


Console.WriteLine("Connecting . . .");
var factory = new ConnectionFactory
{
    // Uri = new Uri("amqps://b-9c19991c-d773-47e2-bf34-86aba36c324a.mq.ap-northeast-2.amazonaws.com:5671"),
    Uri = new Uri("amqp://ec2-3-34-123-235.ap-northeast-2.compute.amazonaws.com:5672"),
    UserName = "admin",
    // UserName = "pf",
    Password = "Cokee"
    // Password = "123$mqTest123$"
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
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

channel.BasicConsume("demo-queue", true, consumer);

while(true)
{
    //loop for user to supply messages
}