using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WcfTest.Service.Host
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            using (var sc = new ServiceController(serviceInstaller1.ServiceName))
            {
                sc.Start();
            }

            base.OnAfterInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            using (var sc = new ServiceController(serviceInstaller1.ServiceName))
            {
                if (sc.Status != (ServiceControllerStatus.Stopped | ServiceControllerStatus.StopPending))
                {
                    sc.Stop();
                }
            }
            base.OnBeforeUninstall(savedState);
        }
    }
}
