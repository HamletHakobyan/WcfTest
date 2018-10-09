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
    }
}
