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

        public bool Contains(string connectionName)
        {
            return true;
        }

        public IEnumerable<SqlDataConnection> GetConnections()
        {
            var reportConnections = new List<SqlDataConnection>();
            //var file =  @"C:\Dympos\AppSecrets.Json";

            var appSecrets = JsonDeserialize<List<AppSecret>>(Application.StartupPath + @"\AppSecrets.json");


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

                reportConnections.Add(new SqlDataConnection(appSecret.AppSecretName, parameters));
            }


            return reportConnections;
        }

        public static string JsonSerializer<T>(T t, string jsonPanth)
        {
            File.WriteAllText(jsonPanth, JsonConvert.SerializeObject(t));
            return File.ReadAllText(jsonPanth);
        }


        public static T JsonDeserialize<T>(string jsonPanth)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(jsonPanth));
        }

        public void SaveConnection(string connectionName, IDataConnection connection, bool saveCredentials)
        {
        }
    }
}