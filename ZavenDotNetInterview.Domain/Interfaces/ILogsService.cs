using System;
using ZavenDotNetInterview.Domain.Models;

namespace ZavenDotNetInterview.Domain.Interfaces
{
    public interface ILogsService
    {
        Log InsertLog(Guid jobId, string description);
    }
}