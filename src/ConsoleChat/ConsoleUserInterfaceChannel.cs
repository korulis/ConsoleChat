using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chat.Common;

namespace ConsoleChat
{
    public class ConsoleUserInterfaceChannel : ICommunicationChannel
    {
        public void Open()
        {
            var listenerThread = new Thread(RetrieveUserInput);
            listenerThread.Start();
        }

        private void RetrieveUserInput()
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (IsAnyoneListening()) MessageReceived(line);
            }
        }

        private bool IsAnyoneListening()
        {
            return MessageReceived != null;
        }

        public void SendMessage(string line)
        {
            Console.WriteLine(line);
        }

        public event Action<string> MessageReceived;
        public event Action ConnectionEstablished;
        public event Action ConnectionLost;
    }
}
