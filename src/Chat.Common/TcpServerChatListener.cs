using System;

namespace Chat.Common
{
    public class TcpServerChatListener : IServerChatListener
    {
        public event Action<ICommunicationChannel> ConnectionEstablished;
        public event Action<ICommunicationChannel> ConnectionLost;
    }
}