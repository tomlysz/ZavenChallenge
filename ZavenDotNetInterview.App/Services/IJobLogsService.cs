using System;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Services
{
    public interface IJobLogsService
    {
        Log InsertLog(Guid jobId, string description);
    }
}