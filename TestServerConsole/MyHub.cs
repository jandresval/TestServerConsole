using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerConsole
{

    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }

    public class MyHub : Hub
    {

        public void Send(string name, string message)
        {
            this.AddMessage(name, message);
        }

        public void AddMessage(string name, string message)
        {
            Console.WriteLine("Hub AddMessage {0} {1}\n", name, message);
            Clients.All.addMessage(name, message);
        }

        public void Heartbeat()
        {
            Console.WriteLine("Hub Heartbeat\n");
            Clients.All.heartbeat();
        }

        public void SendHelloObject(HelloModel hello)
        {
            Console.WriteLine("Hub hello {0} {1}\n", hello.Molly, hello.Age);
            Clients.All.sendHelloObject(hello);
        }

        public override Task OnConnected()
        {

            UserHandler.ConnectedIds.Add(Context.ConnectionId);
            Console.WriteLine("Hub OnConnected {0}\n", Context.ConnectionId);
            return (base.OnConnected());
        }

        public override Task OnDisconnected()
        {
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            Console.WriteLine("Hub OnDisconnected {0}\n", Context.ConnectionId);
            return (base.OnDisconnected());
        }

        public override Task OnReconnected()
        {
            Console.WriteLine("Hub OnReconnected {0}\n", Context.ConnectionId);
            return (base.OnDisconnected());
        }

    }
}
