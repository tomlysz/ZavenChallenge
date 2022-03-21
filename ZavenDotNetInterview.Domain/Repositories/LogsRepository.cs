using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.Domain.ConstsNamespace;
using ZavenDotNetInterview.Domain.Models;
using ZavenDotNetInterview.Domain.Interfaces;

namespace ZavenDotNetInterview.Domain.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        public LogsRepository()
        {
        }

        public List<Log> GetJobsLogs(Guid jobId)
        {
            using (var connection = GetSqlConnection(Consts.ConnStr.ZavenDotNetInterview))
            {
                var logs = connection.Query<Log>($"SELECT * FROM Logs WHERE JobId = {jobId}").ToList();
                return logs;
            }
        }

        public Log InsertLog(Log log)
        {
            using (var connection = GetSqlConnection(Consts.ConnStr.ZavenDotNetInterview))
            {
                string sql = "INSERT INTO Logs (Id, Description, CreatedAt, JobId) Values (@Id, @Description, @CreatedAt, @JobId);";

                log.CreatedAt = DateTime.UtcNow;
                var newLog = connection.Execute(sql, new { Id = log.Id, Description = log.Description, CreatedAt = log.CreatedAt, JobId = log.JobId });

                return log;
            }
        }

        private SqlConnection GetSqlConnection(string connectionStrName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStrName].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}