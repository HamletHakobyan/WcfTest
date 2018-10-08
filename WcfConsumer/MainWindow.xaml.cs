using System.ServiceModel;
using System.Windows;
using WcfTest.Clinet;
using WcfTest.Clinet.Callbacks;
using WcfTest.Contracts.Data;

namespace WcfConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEventBroker _eventBroker;
        public MainWindow()
        {
            _eventBroker = new EventBroker();

            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var proxy = new MyServiceClinet();

            _eventBroker.Subscribe<TrippleReturned>(d => AgeBox.Text = d.TrippleValue.ToString());
            var age = await proxy.GetAgeAsync();
            AgeBox.Text = age.DoubledValue.ToString();
        }
    }
}
