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
            job.CheckFailedStatus(ref newStatus);
            job.Status = newStatus;
            job.LastUpdatedAt = DateTime.UtcNow;            

            string description = newStatus.GetEnumDescription<JobStatus>();
            logsService.InsertLog(job.Id, description);
        }

        private static JobStatus CheckFailedStatus(this Job job, ref JobStatus newStatus)
        {
            if (newStatus == JobStatus.Failed)
            {
                int failedAttempt = job.FailedAttemptionCount;
                job.FailedAttemptionCount = failedAttempt + 1;

                if (job.FailedAttemptionCount == 5)
                    return JobStatus.Closed;
            }
            return newStatus;

        }
    }
}