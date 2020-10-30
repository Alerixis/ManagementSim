using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Shared;

namespace ManagementServer
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, ServerClient> gConnectedClients = new Dictionary<int, ServerClient>(); 
        public static TcpListener gTcpListener;

        /// <summary>
        /// Initializes the server properties.
        /// </summary>
        /// <param name="maxPlayers"></param>
        /// <param name="port"></param>
        public static void Start(int maxPlayers, int port)
        {
            MaxPlayers = maxPlayers;
            Port = port;

            InitializerServerData();

            gTcpListener = new TcpListener(IPAddress.Any, port);
            gTcpListener.Start();
            gTcpListener.BeginAcceptTcpClient(new AsyncCallback(ClientConnectedCallback), null);

            Console.WriteLine($"Server started on {Port}");
        }

        private static void ClientConnectedCallback(IAsyncResult result)
        {
            TcpClient client = GetConnectedClient(result);

            Console.WriteLine($"Incoming connection from {client.Client.RemoteEndPoint}...");
            for (int i = 0; i <= MaxPlayers; i++)
            {
                if(gConnectedClients[i].Connection.Socket == null)
                {
                    gConnectedClients[i].Connection.Connect(client);
                    return;
                }
            }

            Console.WriteLine($"{client.Client.RemoteEndPoint} failed to connect due to server being at capacity.");
        }


        private static TcpClient GetConnectedClient(IAsyncResult result)
        {
            TcpClient client = gTcpListener.EndAcceptTcpClient(result);
            gTcpListener.BeginAcceptTcpClient(new AsyncCallback(ClientConnectedCallback), null);
            return client;
        }

        private static void InitializerServerData()
        {
            for(int i = 0; i < MaxPlayers; i++)
            {
                gConnectedClients.Add(i, new ServerClient(i));

            }
        }
    }
}
