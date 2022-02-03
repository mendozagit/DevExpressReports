using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UserDesigner;

namespace DevReports
{
    public class NewReportCommandHandler : ICommandHandler
    {
        private XRDesignMdiController _designer;

        public NewReportCommandHandler(XRDesignMdiController designer)
        {
            this._designer = designer;
        }


        public void HandleCommand(ReportCommand command, object[] args)
        {
            NewReport();
        }


        public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
        {
            
            useNextHandler = command is not (ReportCommand.NewReport or ReportCommand.NewReportWizard);
            return !useNextHandler;
        }

        private static void NewReport()
        {
            MessageBox.Show("New Report");
        }
    }
}