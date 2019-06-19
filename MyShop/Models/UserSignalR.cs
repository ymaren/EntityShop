using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class UserSignalR
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
    }
}