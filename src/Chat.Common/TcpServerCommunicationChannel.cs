using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat.Common
{
    public class TcpServerCommunicationChannel : ICommunicationChannel
    {
        private readonly int _listenPort;
        private readonly int _sendPort;
        private TcpClient _client = null;

        public TcpServerCommunicationChannel(int listenPort, int sendPort)
        {
            _listenPort = listenPort;
            _sendPort = sendPort;
        }

        public void Open()
        {
            var listenerThread = new Thread(Listen);
            listenerThread.Start();
        }

        private void Listen()
        {
            var listener = new TcpListener(IPAddress.Any, _listenPort);
            listener.Start();

            var client = listener.AcceptTcpClient();

            var reader = new StreamReader(client.GetStream());

            while (true)
            {
                var message = reader.ReadLine();
                if (MessageReceived != null) MessageReceived(message);
            }
        }

        public void SendMessage(string line)
        {
            if (_client == null)
            {
                _client = new TcpClient();
                _client.Connect("localhost", _sendPort);
            }

            var lineBytes = Encoding.ASCII.GetBytes(line + "\r\n");
            _client.GetStream().Write(lineBytes, 0, lineBytes.Length);
        }

        public event Action<string> MessageReceived;
    }
}
