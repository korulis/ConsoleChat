using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Common;

namespace ConsoleChat
{
    public class ChatApplication
    {
        private ICommunicationChannel _userInterfaceChannel;
        private ICommunicationChannel _serverChannel;

        public ChatApplication(ICommunicationChannel userInterfaceChannel, ICommunicationChannel serverChannel)
        {
            _userInterfaceChannel = userInterfaceChannel;
            _serverChannel = serverChannel;
        }

        public void Start()
        {
            _userInterfaceChannel.MessageReceived += MessageReceivedFromUser;
            _serverChannel.MessageReceived += MessageReceivedFromServer;

            _serverChannel.Open();
            _userInterfaceChannel.Open();
        }

        private void MessageReceivedFromUser(string message)
        {
            _serverChannel.SendMessage(message);
        }

        private void MessageReceivedFromServer(string message)
        {
            _userInterfaceChannel.SendMessage(message);
        }
    }
}
