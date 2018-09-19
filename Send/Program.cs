using System;
using System.Text;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RabbitMQ.Client.Framing;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var properties = new BasicProperties();

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);


                    var message = new List<int>
                        {
                            1,2,3,4,5
                        };

                    var binFormatter = new BinaryFormatter();
                    var mStream = new MemoryStream();
                    binFormatter.Serialize(mStream, message);

                    channel.BasicPublish(string.Empty, "hello", null, mStream.ToArray());



                    Console.WriteLine(" Press Enter to continue");

                    Console.ReadLine();

                }
            }
        }
    }
}
