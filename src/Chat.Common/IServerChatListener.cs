using System;

namespace Chat.Common
{
    public interface IServerChatListener
    {
        event Action<ICommunicationChannel> ConnectionEstablished;
        event Action<ICommunicationChannel> ConnectionLost;
    }
}