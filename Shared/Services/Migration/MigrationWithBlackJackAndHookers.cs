using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Migration
{
    public class MigrationWithBlackJackAndHookers
    {
        private readonly string connectionString;

        public MigrationWithBlackJackAndHookers(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public void Migrate(string folderPath)
        {
            if (IsExist(typeof(Tank).Name) ||
                  IsExist(typeof(Factory).Name) ||
                  IsExist(typeof(Unit).Name))
            {
                return;
            }
            ExecuteQueriesFromFolder(folderPath);
        }
        public bool IsExist(string tableName)
        {
            string sqlQuery = $"SELECT COUNT(*) FROM information_schema.tables WHERE TABLE_NAME = N'{tableName}'";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<int>(sqlQuery).FirstOrDefault() != 0;
            }
        }

        public void ExecuteQueryFromFile(string filename)
        {
            string sqlQuery = File.ReadAllText(filename, Encoding.GetEncoding(1251));
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlQuery);
            }
        }

        public void ExecuteQueriesFromFolder(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*.sql");
            foreach (string filename in files)
            {
                ExecuteQueryFromFile(filename);
            }
        }
    }
}
