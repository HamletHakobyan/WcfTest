using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using WcfTest.Contracts.Service;

namespace WcfTest.Service.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MyService>().AsSelf();
            builder.RegisterType<EventRegistrar>().AsSelf().As<IEventRegistrar>().SingleInstance();
            var container = builder.Build();
            var host = new ServiceHost(typeof(MyService));
            host.AddDependencyInjectionBehavior(typeof(MyService),container);
            host.Open();
            host = new ServiceHost(typeof(EventRegistrar));
            host.AddDependencyInjectionBehavior(typeof(EventRegistrar), container);
            host.Open();
            Console.WriteLine("Service started. Press Enter to stop the service.");
            Console.ReadLine();
        }
    }
}
