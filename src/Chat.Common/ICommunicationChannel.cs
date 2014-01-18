using System;

namespace Chat.Common
{
    public interface ICommunicationChannel
    {
        void Open();
        void SendMessage(string line);
        event Action<string> MessageReceived;
    }
}