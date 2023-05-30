using Autofac.Integration.Web;
using Autofac;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Service;

namespace WebForms
{
    public class Global : System.Web.HttpApplication, IContainerProviderAccessor
    {

        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            // Register EF Entities
            string connectionString = "name=Entities";
            builder.RegisterType<Entities>().AsSelf().WithParameter(new TypedParameter(typeof(string), connectionString));

            // Manually registration
             builder.RegisterType<BankAccountRepository>().As<IBankAccountRepository>().InstancePerRequest();
             builder.RegisterType<BankAccountService>().As<IBankAccountService>().InstancePerRequest();

            // Automatic registration with convention
            /*
            builder.RegisterAssemblyTypes(typeof(BankAccountRepository).Assembly).Where(t => t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(i => i.Name == "I" + t.Name)).InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(BankAccountService).Assembly).Where(t => t.Name.EndsWith("Service"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(i => i.Name == "I" + t.Name)).InstancePerRequest();
            */
            // Once you're done registering things, set the container
            // provider up with your registrations.
            var container = builder.Build();
            _containerProvider = new ContainerProvider(container);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}