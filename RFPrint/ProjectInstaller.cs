using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Diagnostics;


namespace RFPrint
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }


        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController service = new ServiceController("GhostRFPrint");
            try
            {

                if (1 == 0) //пока что автоматом не запускаем!!!
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running);
                }
            }
            catch
            {
                // ...
            }
        }

        private void serviceInstaller1_BeforeUninstall(object sender, InstallEventArgs e)
        {

        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
