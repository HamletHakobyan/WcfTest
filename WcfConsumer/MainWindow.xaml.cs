using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.ServiceModel;
using System.Windows;
using Autofac;
using WcfTest.Clinet;
using WcfTest.Clinet.Callbacks;
using WcfTest.Contracts.Data;
using WcfTest.Contracts.Service;

namespace WcfConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEventSubscriber _eventSubscriber;
        public MainWindow()
        {
            _eventSubscriber = AutofacBootstrapper.GetContainer().Resolve<IEventSubscriber>();
            using (var proxy = new ImpersonationProviderClient())
            {
                var proxyClientCredentials = proxy.ClientCredentials;
                if (proxyClientCredentials != null)
                {
                    proxyClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
                }

                proxy.SetImpersonationContext();
            }

            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _eventSubscriber.Subscribe<TrippleReturned>(t => AgeBox.Text = t.TrippleValue.ToString());
            _eventSubscriber.Subscribe<DoubleReturned>(d => AgeBox.Text = d.DoubledValue.ToString());
            _eventSubscriber.Subscribe<DoubleReturned>(d => OtherBox.Text = d.DoubledValue.ToString() + d.DoubledValue.ToString());
            _eventSubscriber.Subscribe((NeedData d) => d.InputData + d.InputData);

            DoubleReturned age;
            using (var proxy = new MyServiceClinet())
            {
                age = await proxy.GetAgeAsync();
            }
            
            AgeBox.Text = age.DoubledValue.ToString();
        }

        private async void GetName_Click(object sender, RoutedEventArgs e)
        {
            using (var proxy = new MyServiceClinet())
            {
                AgeBox.Text = await proxy.GetName();
            }
        }

        const string KERNEL32 = "kernel32.dll";
        [DllImport(KERNEL32, CharSet = CharSet.Auto, SetLastError = true)]
        [ResourceExposure(ResourceScope.Process)]
        internal static extern uint GetCurrentProcessId();
        private async void GetImpersonatedName_Click(object sender, RoutedEventArgs e)
        {

            //using (WindowsIdentity.GetCurrent().Impersonate())
            using (var proxy = new MyServiceClinet())
            {
                OtherBox.Text = await proxy.GetImpersonatedName((int)GetCurrentProcessId());
            }
        }

        private async void WinImpersonationClick(object sender, RoutedEventArgs e)
        {
            using (var proxy = new MyServiceClinet())
            {
                try
                {
                    ImpersBox.Text = await proxy.GetAttrImpersonationData();

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    proxy.Abort();
                }
            }
        }
    }
}
