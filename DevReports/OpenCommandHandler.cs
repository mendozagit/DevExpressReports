using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace DevReports
{
    public class OpenCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
    {
        private static XRDesignMdiController _designer;

        public OpenCommandHandler(XRDesignMdiController designer)
        {
            _designer = designer;
        }

        public void HandleCommand(ReportCommand command, object[] args)
        {
            Open();
        }

        private static void Open()
        {
            // Write your custom .

            //_designer.ActiveDesignPanel.ExecCommand(ReportCommand.Close);
            var report = ReportFromString(File.ReadAllText("Report.XML"));

            _designer.OpenReport(report);
            MessageBox.Show("Open");
        }

        public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
        {
            useNextHandler = command != ReportCommand.OpenFile;
            return !useNextHandler;
        }
        private static XtraReport ReportFromString(string s)
        {
            using var reportStream = new MemoryStream(Encoding.UTF8.GetBytes(s));
            return XtraReport.FromXmlStream(reportStream);
        }
    }
}