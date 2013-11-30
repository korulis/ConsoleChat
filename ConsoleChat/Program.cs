using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleChat
{
    class Program
    {
        static void Main(string[] args)
        {
            var listenPort = int.Parse(args[0]);
            var sendPort = int.Parse(args[1]);

            var listenerThread = new Thread(Listen);
            listenerThread.IsBackground = true;
            listenerThread.Start(listenPort);

            Console.ReadLine();
            var client = new TcpClient("localhost", sendPort);

            while (true)
            {
                var line = Console.ReadLine();
                var lineBytes = Encoding.ASCII.GetBytes(line + "\r\n");

                client.GetStream().Write(lineBytes, 0, lineBytes.Length);
            }
        }

        private static void Listen(object listenPort)
        {
            var listener = new TcpListener(IPAddress.Any, (int)listenPort);
            listener.Start(); //degdgdsgf

            var client = listener.AcceptTcpClient();

            var reader = new StreamReader(client.GetStream());

            while (true)
            {
                var message = reader.ReadLine();
                Console.WriteLine(message);
            }
        }
    }
}
