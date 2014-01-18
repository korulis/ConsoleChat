using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Common;
using Xunit;
using Xunit.Extensions;

namespace ChatServer.Tests
{
    public class ChatServerTests
    {
        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(3,3)]

        public void WhenFirstUserConnectsToServerChatApplicationIncreasesUserCounter(int result, int numberOfConnections)
        {
            
            var serverChannel = new FakeServerChatListener();
            var sut = new ChatServerApplication(serverChannel);
            sut.Start();
            
            for (int i = 0; i < numberOfConnections; ++i)
            {
                serverChannel.SimulateConnectToServer(new FakeCommunicationChannel());
            }

            Assert.Equal(result, sut.NumberOfUsers);
        }


        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 2)]

        public void TheNumberOfConnectionsAfterOneDisconnect(int initialNumberOfConnections, int expected)
        {
            var serverChannel = new FakeServerChatListener();
            var sut = new ChatServerApplication(serverChannel);
            sut.Start();

            var connectedClients = new List<FakeCommunicationChannel>();

            for (int i = 0; i < initialNumberOfConnections; ++i)
            {
                connectedClients.Add(new FakeCommunicationChannel());
            }

            foreach(var client in connectedClients)
            {
                serverChannel.SimulateConnectToServer(client);
            }

            serverChannel.SimulateDisconnectFromServer(connectedClients[0]);
            Assert.Equal(expected, sut.NumberOfUsers);
            
        }

        [Fact]

        public void TheNumberOfUsersInitiallyIsZero()
        {
            var serverChannel = new FakeServerChatListener();
            var sut = new ChatServerApplication(serverChannel);
            Assert.Equal(0, sut.NumberOfUsers);
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]

        public void MessageReceivedFromOneUserIsSentToTheOtherUser(string incomingMessage)
        {
            var chatListener = new FakeServerChatListener();
            var sut = new ChatServerApplication(chatListener);
            sut.Start();
            var messageSource = new FakeCommunicationChannel();
            var messageRecipient = new FakeCommunicationChannel();
            chatListener.SimulateConnectToServer(messageRecipient);
            chatListener.SimulateConnectToServer(messageSource);
            messageSource.SimulateMessageReceived(incomingMessage);

            Assert.Equal(incomingMessage, messageRecipient.LastMessageSent);
        }
        [Fact]
        public void MessagesAreNotSentToReceivingClientAfterReceivingClientDisconnects()
        {
            var chatListener = new FakeServerChatListener();
            var sut = new ChatServerApplication(chatListener);
            sut.Start();
            var messageSource = new FakeCommunicationChannel();
            var messageRecipient = new FakeCommunicationChannel();
            chatListener.SimulateConnectToServer(messageRecipient);
            chatListener.SimulateConnectToServer(messageSource);
            chatListener.SimulateDisconnectFromServer(messageRecipient);

            messageSource.SimulateMessageReceived("foo");

            Assert.Null(messageRecipient.LastMessageSent);
        }
        [Fact]
        public void ReceivedMessagesAreIgnoredAfterSourceClientDisconnects()
        {
            var chatListener = new FakeServerChatListener();
            var sut = new ChatServerApplication(chatListener);
            sut.Start();
            var messageSource = new FakeCommunicationChannel();
            var messageRecipient = new FakeCommunicationChannel();
            chatListener.SimulateConnectToServer(messageRecipient);
            chatListener.SimulateConnectToServer(messageSource);
            chatListener.SimulateDisconnectFromServer(messageSource);

            messageSource.SimulateMessageReceived("foo");

            Assert.Null(messageRecipient.LastMessageSent);
        }

        [Fact]
        public void ZinuteIssiustaNormaliemsNetJeiServakeYraDalbajobu()
        {
            var chatListener = new FakeServerChatListener();
            var sut = new ChatServerApplication(chatListener);
            sut.Start();
            var messageSource = new FakeCommunicationChannel();
            var messageLoxasRecipient = new FakeDalbajobasCommunicationChannel();
            var messageRecipient = new FakeCommunicationChannel();
            chatListener.SimulateConnectToServer(messageLoxasRecipient);
            chatListener.SimulateConnectToServer(messageRecipient);
            chatListener.SimulateConnectToServer(messageSource);

            messageSource.SimulateMessageReceived("foo");

            Assert.Equal("foo", messageRecipient.LastMessageSent);
        }
        
        [Fact]
        public void DalbajobusAtjungiamNuoServako()
        {
            var chatListener = new FakeServerChatListener();
            var sut = new ChatServerApplication(chatListener);
            sut.Start();
            var messageSource = new FakeCommunicationChannel();
            var messageLoxasRecipient = new FakeDalbajobasCommunicationChannel();
            chatListener.SimulateConnectToServer(messageLoxasRecipient);
            chatListener.SimulateConnectToServer(messageSource);

            messageSource.SimulateMessageReceived("foo");
            messageSource.SimulateMessageReceived("bar");
            Assert.Equal("foo", messageLoxasRecipient.LastMessageSent);
        }
    }

    public class FakeDalbajobasCommunicationChannel : ICommunicationChannel
    {
        public string LastMessageSent = null;

        public void Open()
        {
        }

        public void SendMessage(string line)
        {
            LastMessageSent = line;

            throw new Exception();
        }

        public event Action<string> MessageReceived;
    }

    public class FakeServerChatListener:IServerChatListener
    {


        public event Action<ICommunicationChannel> ConnectionEstablished;
        public void SimulateConnectToServer(ICommunicationChannel ch)
        {
            ConnectionEstablished(ch);
        }
        public event Action<ICommunicationChannel> ConnectionLost;
        public void SimulateDisconnectFromServer(ICommunicationChannel disconnectingChannel)
        {
            ConnectionLost(disconnectingChannel);
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

        public void SimulateMessageReceived(string line)
        {
            if (MessageReceived != null) MessageReceived(line);
        }
    }

}
