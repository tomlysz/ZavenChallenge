using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.Domain.Models;
using ZavenDotNetInterview.Domain.Interfaces;

namespace ZavenDotNetInterview.Domain.Services
{
    public class LogsService : ILogsService
    {
        private readonly ILogsRepository _logsRepo;

        public LogsService(ILogsRepository logsRepo)
        {
            _logsRepo = logsRepo;
        }

        public Log InsertLog(Guid jobId, string description)
        {
            var log = new Log
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Description = description,
                JobId = jobId
            };

            return _logsRepo.InsertLog(log);
        }
    }
}