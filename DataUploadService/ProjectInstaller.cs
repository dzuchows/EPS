using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DataUploadService
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            // Here you can set properties on serviceProcessInstaller
            //or register event handlers
            serviceProcessInstaller.Account = ServiceAccount.LocalService;

            serviceInstaller.ServiceName = EPSDataUploadService.SERVICE_NAME;
            this.Installers.AddRange(new Installer[] {
                serviceProcessInstaller, serviceInstaller });

            EventLogInstaller installer = FindInstaller(this.Installers);
            if (installer != null)
            {
                installer.Log = EPSDataUploadService.SERVICE_NAME;
            }
        }


        private EventLogInstaller FindInstaller(InstallerCollection installers)
        {
            foreach (Installer installer in installers)
            {
                if (installer is EventLogInstaller)
                {
                    return (EventLogInstaller)installer;
                }

                EventLogInstaller eventLogInstaller = FindInstaller(installer.Installers);
                if (eventLogInstaller != null)
                {
                    return eventLogInstaller;
                }
            }
            return null;
        }

        private void InitializeComponent()
        {

        }
    }
}
