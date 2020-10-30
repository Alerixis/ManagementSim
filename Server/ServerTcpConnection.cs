using System;
using System.Collections.Generic;
using System.Text;
using Shared;
using System.Net;
using System.Net.Sockets;

namespace ManagementServer
{
    public class ServerTcpConnection : TcpConnection
    {
        public ServerTcpConnection(int id)
        {
            Id = id;
        }
    }
}
