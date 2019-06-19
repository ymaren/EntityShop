using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.Hubs
{
    public class ChatHub : Hub
    {
        static List<UserSignalR> Users = new List<UserSignalR>();
        public string GetCurrentName()
        {
            return "Natasha";
        }

       
        // connect new user
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new UserSignalR { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // disconnect new user
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
        // send message
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message,"kikiki");
        }

        public void Send(string name)
        {
            string a = name;
        }

    }
}