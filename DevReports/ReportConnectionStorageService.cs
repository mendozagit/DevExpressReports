using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Wizard.Model;
using DevExpress.DataAccess.Wizard.Services;
using Newtonsoft.Json;

namespace DevReports
{
    public class ReportConnectionStorageService : IConnectionStorageService
    {
        public bool CanSaveConnection => false;
        public string FileName { get; set; } = "connections.xml";
        public bool Contains(string connectionName)
        {
            return true;
        }

        public IEnumerable<SqlDataConnection> GetConnections()
        {
            var reportConnections = new List<SqlDataConnection>();
            //var file =  @"C:\Dympos\AppSecrets.Json";

            var appSecrets = Program.JsonDeserialize<List<AppSecret>>(Application.StartupPath + @"\AppSecrets.json");


            foreach (var appSecret in appSecrets)
            {
                var parameters = new MsSqlConnectionParameters()
                {
                    ServerName = appSecret.Host,
                    DatabaseName = appSecret.DbName,
                    UserName = appSecret.User,
                    Password = appSecret.Password,
                    AuthorizationType = MsSqlAuthorizationType.SqlServer
                };
               
                var sqlDataConnection = new SqlDataConnection(appSecret.AppSecretName, parameters);
                SaveConnection(appSecret.AppSecretName, sqlDataConnection, true);
                reportConnections.Add(sqlDataConnection);
            }


            return reportConnections;
        }

        

        public void SaveConnection(string connectionName, IDataConnection connection, bool saveCredentials)
        {
            connection.StoreConnectionNameOnly = true;
        }
    }
}