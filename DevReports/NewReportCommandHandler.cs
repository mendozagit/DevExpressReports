using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace DevReports
{
    public class NewReportCommandHandler : ICommandHandler
    {
        private static XRDesignMdiController _designer;

        public NewReportCommandHandler(XRDesignMdiController designer)
        {
            _designer = designer;
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
            _designer.OpenReport(new XtraReport());
        }
    }
}