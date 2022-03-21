using System;
using System.Collections.Generic;
using ZavenDotNetInterview.Domain.Models;

namespace ZavenDotNetInterview.Domain.Interfaces
{
    public interface ILogsRepository
    {
        List<Log> GetJobsLogs(Guid jobId);
        Log InsertLog(Log log);
    }
}