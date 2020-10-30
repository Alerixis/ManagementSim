using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Shared;

namespace ManagementServer
{
    class ServerClient
    {
        public TcpConnection Connection;
        public int Id;

        public ServerClient(int id)
        {
            Id = id;
            Connection = new ServerTcpConnection(id);
        }
    }
}
