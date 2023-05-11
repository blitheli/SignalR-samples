using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatSample.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// 本函数用于客户端调用,用于向所有客户端发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Send(string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}