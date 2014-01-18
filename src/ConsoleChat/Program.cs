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
using Chat.Common;

namespace ConsoleChat
{
    class Program
    {
        static void Main(string[] args)
        {
            var listenPort = int.Parse(args[0]);
            var sendPort = int.Parse(args[1]);

            var userChannel = new ConsoleUserInterfaceChannel();
            var serverChannel = new TcpServerCommunicationChannel(listenPort, sendPort);

            var application = new ChatApplication(userChannel, serverChannel);
            application.Start();
        }
    }
}
