﻿using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace ProductService.BLL.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
        string hostName = _configuration["RabbitMQ_HostName"]!;
        string userName = _configuration["RabbitMQ_UserName"]!;
        string password = _configuration["RabbitMQ_Password"]!;
        string port = _configuration["RabbitMQ_Port"]!;
        port = System.Environment.GetEnvironmentVariable("RabbitMQ_Port")!;

        Console.WriteLine($"RabbitMQ_HostName: {_configuration["RabbitMQ_HostName"]}");
        Console.WriteLine($"RabbitMQ_UserName: {_configuration["RabbitMQ_UserName"]}");
        Console.WriteLine($"RabbitMQ_Password: {_configuration["RabbitMQ_Password"]}");
        Console.WriteLine($"RabbitMQ_Port: {_configuration["RabbitMQ_Port"]}");

        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            Port = Convert.ToInt32(port)
        };
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }
    public void Publish<T>(Dictionary<string,object> headers, T message)
    {
        //Create Exchange
        string messageJson = JsonSerializer.Serialize(message);
        byte[] messageBodyBytes = Encoding.UTF8.GetBytes(messageJson);

        string exchangeName = _configuration["RabbitMQ_Products_Exchange"]!;
        _channel.ExchangeDeclare(exchange: exchangeName,type:ExchangeType.Headers,durable:true);

        var basicProperties=_channel.CreateBasicProperties();
        basicProperties.Headers = headers;

        //Publish message
        _channel.BasicPublish(
            exchange: exchangeName,
            routingKey: string.Empty,
            basicProperties: basicProperties,
            mandatory: true,// set false if you don't want an exception on unroutable
            body: messageBodyBytes
        );
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}
