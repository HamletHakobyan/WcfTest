using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
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
            var service = new WcfServiceHost(args);
            if (Environment.UserInteractive)
            {
                service.RunAsConsole(args);
            }
            else
            {
            ServiceBase.Run(service);
            }
        }
    }
}
