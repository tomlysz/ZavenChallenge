using System;
using ZavenDotNetInterview.App.Models;

namespace ZavenDotNetInterview.App.Services
{
    public interface ILogsService
    {
        Log InsertLog(Guid jobId, string description);
    }
}