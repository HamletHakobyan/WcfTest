using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;

namespace WcfTest.Service.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(MyService));
            host.Open();
            Console.WriteLine("Service started. Press Enter to stop the service.");
            Console.ReadLine();
        }
    }
}
