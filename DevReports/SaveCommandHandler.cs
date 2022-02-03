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
    public class SaveCommandHandler : ICommandHandler
    {
        private static XRDesignMdiController _designer;

        public SaveCommandHandler(XRDesignMdiController designer)
        {
            _designer = designer;
        }

        public void HandleCommand(ReportCommand command, object[] args)
        {
            Save();
        }

        private static void Save()
        {
            File.WriteAllText("Report.XML", ReportToString(_designer.ActiveDesignPanel.Report));
            MessageBox.Show("Save");
        }

        public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
        {
            useNextHandler = command is not (ReportCommand.SaveFile or ReportCommand.SaveFileAs);
            return !useNextHandler;
        }

        private static string ReportToString(XtraReport rep)
        {
            using var reportStream = new MemoryStream();
            rep.SaveLayoutToXml(reportStream);
            reportStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(reportStream, Encoding.UTF8);
            var s = reader.ReadToEnd();
            return s;
        }
    }
}