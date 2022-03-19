using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Services;

namespace ZavenDotNetInterview.App.Extensions
{
    internal static class JobExtension
    {
        public static void ChangeStatus(this Job job, JobStatus newStatus)
        {
            job.Status = newStatus;
        }

        public static void ChangeStatus(this Job job, IJobLogsService logsService, JobStatus newStatus)
        {
            job.Status = newStatus;
            job.LastUpdatedAt = DateTime.UtcNow;

            string description = newStatus.GetEnumDescription<JobStatus>();
            logsService.InsertLog(job.Id, description);
        }
    }
}