using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartup(typeof(MyShop.Models.StartupSignalR))]
namespace MyShop.Models
{
    public class StartupSignalR
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}