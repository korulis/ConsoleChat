using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Common;
using Xunit;
using Xunit.Extensions;

namespace ConsoleChat.Tests
{
    public class ConsoleApplicationTests
    {
        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        //public void ApplicationSendsTheMessageThatItReceivedFromUserToServer()
        public void ApplicationSendsMessageToServerThatItReceivesFromUser(string message)
        {
            var uiChannel = new FakeCommunicationChannel();
            var serverChannel = new FakeCommunicationChannel();
            var sut = new ChatApplication(uiChannel, serverChannel);
            sut.Start();

            uiChannel.SimulateMessageReceived(message);

            Assert.Equal(message, serverChannel.LastMessageSent);
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        //public void ApplicationSendsTheMessageThatItReceivedFromUserToServer()
        public void ApplicationSendsMessageToUIThatItReceivesFromServer(string message)
        {
            var uiChannel = new FakeCommunicationChannel();
            var serverChannel = new FakeCommunicationChannel();
            var sut = new ChatApplication(uiChannel, serverChannel);
            sut.Start();

            serverChannel.SimulateMessageReceived(message);

            Assert.Equal(message, uiChannel.LastMessageSent);
        }
    }

    public class FakeCommunicationChannel : ICommunicationChannel
    {
        public string LastMessageSent = null;

        public void Open()
        {
            
        }

        public void SendMessage(string line)
        {
            LastMessageSent = line;
        }

        public event Action<string> MessageReceived;
        public event Action ConnectionEstablished;
        public event Action ConnectionLost;

        public void SimulateMessageReceived(string line)
        {
            MessageReceived(line);
        }
    }

}
