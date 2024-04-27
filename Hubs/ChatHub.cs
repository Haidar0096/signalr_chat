using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("A client has connected to the server, with connection id: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public Task InvokeReceiveMessage(string connectionId, string user, string message) =>
         Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message);


        public async Task NoArgsEchoMethod()
        {
            string message = "You invoked the " + nameof(NoArgsEchoMethod) + " method with no arguments";
            await InvokeReceiveMessage(Context.ConnectionId, "Server", message);
        }

        public Task OneArgMethod(string agr1)
        {
            string message = "You invoked the " + nameof(OneArgMethod) + " method with argument: " + agr1;
            return InvokeReceiveMessage(Context.ConnectionId, "Server", message); 
        }

        public Task TwoArgsMethod(string arg1, string arg2)
        {
            string message = "You invoked the " + nameof(TwoArgsMethod) + " method with arguments: " + arg1 + " and " + arg2;
            return InvokeReceiveMessage(Context.ConnectionId, "Server", message);
        }
    }
}