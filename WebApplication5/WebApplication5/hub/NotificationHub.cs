using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NotificationSignalR.Hubs
{
    [HubName("notifHub")]
    public class NotificationHub : Microsoft.AspNet.SignalR.Hub
    {
        public void SendMessage(string users, string message)
        {
            string curr = DateTime.Now.ToString();
            string[] userids = new JavaScriptSerializer().Deserialize<string[]>(users);

            foreach (string s in userids)
            {
                Clients.Group(s).addMessageToUser(message, curr);
            }

            Clients.All.addMessageToAdmin(userids, message, curr);

        }

        public Task AddConnectionId(string userid)
        {
            Clients.All.addUser(userid);
            return Groups.Add(Context.ConnectionId, userid);
        }

        public Task RemoveConnectionId(string userid)
        {
            return Groups.Remove(Context.ConnectionId, userid);
        }



        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}