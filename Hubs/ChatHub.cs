using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // if target device is disconnected throw exception
            

            var sendDate = DateTime.Now;
            var userString = user;
            var messageString = message;
            if (user == "")
            {
                userString = "Anonymous";
            }
            if (message == "")
            {
                messageString = "Empty message";
            }
            await Clients.All.SendAsync("ReceiveMessage", "[ " + sendDate + " ] " + userString, messageString);
        }
    }
}