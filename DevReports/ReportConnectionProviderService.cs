using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Wizard.Services;
using Newtonsoft.Json;

namespace DevReports
{
    public class ReportConnectionProviderService : IConnectionProviderService
    {
        public SqlDataConnection LoadConnection(string connectionName)
        {
            // Specify custom connection parameters.

            var appSecrets = Program.JsonDeserialize<List<AppSecret>>(Application.StartupPath + @"\AppSecrets.json");
            var appSecret = appSecrets.FirstOrDefault(x => x.AppSecretName.EndsWith(connectionName));



            return new SqlDataConnection(connectionName,
                new MsSqlConnectionParameters(appSecret?.Host, appSecret?.DbName, appSecret?.User, appSecret?.Password, MsSqlAuthorizationType.SqlServer));
        }
    }
}