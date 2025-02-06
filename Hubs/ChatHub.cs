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


        public async Task NoArgsEchoMethod()
        {
            await Clients.Client(Context.ConnectionId).SendAsync(nameof(NoArgsEchoMethod));
        }

        public Task OneArgEchoMethod(string agr1)
        {
            string message = "You invoked the " + nameof(OneArgEchoMethod) + " method with argument: " + agr1;
            return Clients.Client(Context.ConnectionId).SendAsync(nameof(OneArgEchoMethod), message);
        }

        public Task TwoArgsEchoMethod(string arg1, string arg2)
        {
            string msg1 = "You invoked the " + nameof(TwoArgsEchoMethod) + " method";
            string msg2 = "arguments: " + arg1 + " and " + arg2;
            return Clients.Client(Context.ConnectionId).SendAsync(nameof(TwoArgsEchoMethod), msg1, msg2);
        }

        public Task SendMessage(string username, string message, string recepientConnectionId)
        {
            return Clients.Client(recepientConnectionId).SendAsync("ReceiveMessage", username, message);
        }

        public Task ReceiveMessage(string username, string message)
        {
            return Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", username, message);
        }
    }
}