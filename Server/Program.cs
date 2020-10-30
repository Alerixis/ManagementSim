using System;
using Shared.Constants;

namespace ManagementServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Management Game Server";
            Server.Start(10, SharedConstants.SERVER_PORT);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
