using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
        }


        private void OnPreInitialize()
        {
            connectionStorageService = new ReportConnectionStorageService();
            ReplaceService(reportDesigner1, typeof(IConnectionStorageService), connectionStorageService);
            reportDesigner1.DesignPanelLoaded += OnSuscribeEvents;
        }

        private void OnInitialize()
        {
            xtraReport = new XtraReport();
            reportDesigner1.OpenReport(xtraReport);
        }


        private static void ReplaceService(IServiceContainer container, Type serviceType, object serviceInstance)
        {
            if (container.GetService(serviceType) != null)
                container.RemoveService(serviceType);
            container.AddService(serviceType, serviceInstance);
        }

       

       
    }
}