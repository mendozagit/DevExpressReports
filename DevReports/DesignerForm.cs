using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace DevReports
{
    public partial class DesignerForm : DevExpress.XtraEditors.XtraForm
    {
        private ReportConnectionStorageService connectionStorageService;
        private XtraReport xtraReport;

        public DesignerForm()
        {
            InitializeComponent();
            OnPreInitialize();
            OnInitialize();
        }

        private void OnSuscribeEvents(object sender, DesignerLoadedEventArgs e)
        {
            reportDesigner1.AddCommandHandler(new OpenCommandHandler(reportDesigner1));
            reportDesigner1.AddCommandHandler(new NewReportCommandHandler(reportDesigner1));
            reportDesigner1.AddCommandHandler(new SaveCommandHandler(reportDesigner1));
            reportDesigner1.DesignPanelLoaded += OnDesignPanelLoaded;
        }

        private void OnDesignPanelLoaded(object sender, DesignerLoadedEventArgs e)
        {
            ReplaceService(e.DesignerHost, typeof(IConnectionStorageService), connectionStorageService);
            ReplaceService(e.DesignerHost, typeof(IConnectionProviderService), new ReportConnectionProviderService());
        }


        private void OnPreInitialize()
        {
            connectionStorageService = new ReportConnectionStorageService()
            {
                FileName = "AppSecrets.Json",
            };
            ReplaceService(reportDesigner1, typeof(IConnectionStorageService), connectionStorageService);
            reportDesigner1.DesignPanelLoaded += OnSuscribeEvents;
        }

        private void OnInitialize()
        {
            //var file = Path.Combine(Application.StartupPath, $"{System.Diagnostics.Process.GetCurrentProcess().ProcessName}.dll.config");
           // AddNewConnectionString("ReportsConnection", "Server=127.0.0.1;Database=Dym;User Id=sa;Password=12345678;", file);

            xtraReport = new XtraReport();
            reportDesigner1.OpenReport(xtraReport);
        }


        private static void ReplaceService(IServiceContainer container, Type serviceType, object serviceInstance)
        {
            if (container.GetService(serviceType) != null)
                container.RemoveService(serviceType);
            container.AddService(serviceType, serviceInstance);
        }


        private static void AddNewConnectionString(string connectionName, string connectionSetting,
            string appConfigFullPath)

        {
            /* This code provides access to configuration files using OpenMappedExeConfiguration,method. You can use the OpenExeConfiguration method instead. For further informatons,consult the MSDN, it gives you more inforamtions about config files access methods*/

            var exeConfigurationFileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = appConfigFullPath
            };

            var configManager =
                ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);

            //Define a connection string settings incuding the name and the connection string
            var oConnectionSettings = new ConnectionStringSettings(connectionName, connectionSetting);

            //Adding the connection string to the oConfiguration object
            if (configManager.ConnectionStrings.ConnectionStrings[connectionName] != null) return;
            configManager.ConnectionStrings.ConnectionStrings.Add(oConnectionSettings);

            //Save the new connection string settings
            configManager.Save(ConfigurationSaveMode.Full);
            MessageBox.Show($@"Report Designer configured.");
            
        }
    }
}