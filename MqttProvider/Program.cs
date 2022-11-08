using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var factory = new ConnectionFactory
{
    Uri = new Uri("amqps://mqtt.cokee.io:5671"),
    // Uri = new Uri("amqps://b-5ffc876c-f4a1-44e5-a730-9fcd7112eee4.mq.ap-northeast-2.amazonaws.com:5671"),
    UserName = "admin",
    // UserName = "admin",
    // Password = "ejrrpdlawm1!"
    Password = "Cokee",
    Ssl = new SslOption("mqtt.cokee.io", "", enabled:true)
};

var queue = "demo-fanout-test";
// var queue = "queue_game_info";

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic_logs", ExchangeType.Fanout);

channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);


var message = new { name = "Producer", message = "Hello" };
var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

var properties = channel.CreateBasicProperties();
properties.Persistent = true;
channel.BasicPublish(exchange: "topic_logs", routingKey: "", basicProperties: properties, body: body);


channel.BasicPublish("topic_logs", "", null, body);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();