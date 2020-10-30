using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Shared.Constants;
using Shared;

namespace Assets
{
    public class ClientTcpConnection : TcpConnection
    {
        public void Connect()
        {
            Socket = new TcpClient
            {
                ReceiveBufferSize = SharedConstants.DATA_BUFFER_SIZE,
                SendBufferSize = SharedConstants.DATA_BUFFER_SIZE
            };

            mReceivedBuffer = new byte[SharedConstants.DATA_BUFFER_SIZE];
            Socket.BeginConnect(Client.gInstance.Ip, Client.gInstance.Port, ConnectToServerCallback, Socket);
        }

        private void ConnectToServerCallback(IAsyncResult result)
        {
            Socket.EndConnect(result);
            if (!Socket.Connected)
            {
                Console.WriteLine("Client is not connected to the server...");
                return;
            }

            mStream = Socket.GetStream();

            mStream.BeginRead(mReceivedBuffer, 0, SharedConstants.DATA_BUFFER_SIZE, ReceivedDataCallback, null);
        }
    }
}
