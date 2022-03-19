using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobLogsService : IJobLogsService
    {
        private readonly ILogsRepository _logsRepo;

        public JobLogsService(ILogsRepository logsRepo)
        {
            _logsRepo = logsRepo;
        }

        public Log InsertLog(Guid jobId, string description)
        {
            var log = new Log
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Description = description,
                JobId = jobId
            };

            return _logsRepo.InsertLog(log);
        }
    }
}