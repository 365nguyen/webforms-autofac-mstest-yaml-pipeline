using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Service;
using System;
using System.Linq;

namespace MSTest.Test
{
    [TestClass]
    public class BaseTest
    {
        // Provider that holds the IOC container.
        static IContainer _container;

        // Instance property that will be used by Autofac to resolve and inject dependencies.
        public IContainer IOCContainer
        {
            get { return _container; }
        }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            // Run code once per assembly, before any test in that assembly runs.
            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            // Register EF Entities
            string connectionString = "name=Entities";
            builder.RegisterType<Entities>().AsSelf().WithParameter(new TypedParameter(typeof(string), connectionString));

            // Manually registration
            // builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerRequest();
            // builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerRequest();

            // Automatic registration with convention
            builder.RegisterAssemblyTypes(typeof(BankAccountRepository).Assembly).Where(t => t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(i => i.Name == "I" + t.Name)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(BankAccountService).Assembly).Where(t => t.Name.EndsWith("Service"))
                .As(t => t.GetInterfaces()?.FirstOrDefault(i => i.Name == "I" + t.Name)).InstancePerLifetimeScope();

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _container = builder.Build();
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
