using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WorkshopAtentoBot.Dialogs;

namespace WorkshopAtentoBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var builder = new ContainerBuilder();
            builder.RegisterType<RespostaHelper>().As<IRespostaHelper>();

 
            var container = builder.Build();
            ServiceResolver.Container = container;
        }
    }
}
