using System;
using Autofac;
using Autofac.Integration.Wcf;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceProcess;
using WcfTest.Contracts.Service;
using WcfTest.Service.Infrastructure;

namespace WcfTest.Service.Host
{
    public class WcfServiceHost : ServiceBase
    {
        private readonly IContainer _container;
        private readonly List<ServiceHost> _serviceHosts;
        public WcfServiceHost(string[] args)
        {
            InitializeComponent();
            var builder = new ContainerBuilder();
            builder.RegisterType<MyService>().AsSelf().SingleInstance();
            builder.RegisterType<ImpersonationService>()
                .AsSelf()
                .SingleInstance()
                .ExternallyOwned()
                .As<IImpersonationProvider>()
                .SingleInstance()
                .ExternallyOwned()
                .As<IImpersonationService>()
                .SingleInstance()
                .ExternallyOwned();
            builder.RegisterType<EventHandlerSource>()
                .AsSelf()
                .As<IEventHandlerSource>()
                .As<IEventHandlerRegistrar>()
                .SingleInstance();
            builder.RegisterType<EventHandler>()
                .As<IEventHandler>();
            _container = builder.Build();
            _serviceHosts = new List<ServiceHost>();
        }
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            var host = new ServiceHost(typeof(MyService));
            host.AddDependencyInjectionBehavior(typeof(MyService), _container);
            host.Open();
            _serviceHosts.Add(host);
            host = new ServiceHost(typeof(EventHandlerSource));
            host.AddDependencyInjectionBehavior(typeof(EventHandlerSource), _container);
            host.Open();
            _serviceHosts.Add(host);
            host = new ServiceHost(typeof(ImpersonationService));
            host.AddDependencyInjectionBehavior(typeof(ImpersonationService), _container);
            host.Open();
            _serviceHosts.Add(host);

        }

        protected override void OnStop()
        {
            foreach(var host in _serviceHosts)
            {
                host.Close();
            }

            base.OnStop();
        }

        private void InitializeComponent()
        {
            // 
            // WcfServiceHost
            // 
            this.ServiceName = "WcfServiceHost";

        }

        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            Console.WriteLine("Service started. Press Enter to stop.");
            Console.ReadLine();
            OnStop();
        }
    }
}