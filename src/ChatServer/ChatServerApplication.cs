using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Common;

namespace ChatServer
{
    public class ChatServerApplication
    {
        private IServerChatListener _listener;

        public ChatServerApplication(IServerChatListener listener)
        {
            _listener = listener;
        }

        public int NumberOfUsers { get { return _connectedClients.Count; } }

        public void Start()
        {
            _listener.ConnectionEstablished += HandleConnection;
            _listener.ConnectionLost += HandleDisconnection;
        }

        private List<ICommunicationChannel> _connectedClients = new List<ICommunicationChannel>();
 
        private void HandleConnection(ICommunicationChannel ch)
        {
            _connectedClients.Add(ch);
            ch.MessageReceived += HandleMessageReceived;
        }

        void HandleMessageReceived(string incomingMessage)
        {
            foreach (var client in _connectedClients.ToArray())
            {
                try
                {
                    client.SendMessage(incomingMessage);               
                }
                catch (Exception)
                {
                    HandleDisconnection(client);
                }
            }
        }
        private void HandleDisconnection(ICommunicationChannel disconnectingChannel)
        {
            _connectedClients.Remove(disconnectingChannel);
            disconnectingChannel.MessageReceived -= HandleMessageReceived;

        }
    }
}
