using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Shared.Constants;
using System.IO;
using System.Data.Common;
using System.Diagnostics;

namespace Shared
{
    public abstract class TcpConnection
    {
        public int Id;
        public TcpClient Socket;

        protected byte[] mReceivedBuffer;
        protected NetworkStream mStream;

        public virtual void Connect(TcpClient socket)
        {
            Socket = socket;
            Socket.ReceiveBufferSize = SharedConstants.DATA_BUFFER_SIZE;
            Socket.SendBufferSize = SharedConstants.DATA_BUFFER_SIZE;

            mStream = Socket.GetStream();

            mReceivedBuffer = new byte[SharedConstants.DATA_BUFFER_SIZE];

            mStream.BeginRead(mReceivedBuffer, 0, SharedConstants.DATA_BUFFER_SIZE, ReceivedDataCallback, null);
        }

        protected virtual void ReceivedDataCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = mStream.EndRead(result);
                if(byteLength == 0)
                {
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(mReceivedBuffer, data, byteLength);

                mStream.BeginRead(mReceivedBuffer, 0, SharedConstants.DATA_BUFFER_SIZE, ReceivedDataCallback, null);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error receiving TCP data: {e}");
                //DISCONNECT
            }
        }
    }
}
