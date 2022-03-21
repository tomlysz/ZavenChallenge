using System;
using ZavenDotNetInterview.Domain.Models;
using ZavenDotNetInterview.Domain.Interfaces;
using System.Configuration;
using ZavenDotNetInterview.Domain.ConstsNamespace;

namespace ZavenDotNetInterview.Domain.Extensions
{
    internal static class JobExtension
    {
        public static void ChangeStatus(this Job job, JobStatus newStatus)
        {
            job.Status = newStatus;
        }

        public static void ChangeStatus(this Job job, ILogsService logsService, JobStatus newStatus)
        {
            ChangeJobStatus(job, newStatus);
            AddLog(logsService, job.Id, newStatus);

            ChangeSatusToClose(job, logsService, newStatus);
        }

        private static void AddLog(ILogsService logsService, Guid jobId, JobStatus status)
        {
            string description = status.GetEnumDescription<JobStatus>();
            logsService.InsertLog(jobId, description);
        }

        private static void ChangeJobStatus(Job job, JobStatus status)
        {
            job.Status = status;
            job.LastUpdatedAt = DateTime.UtcNow;
        }

        private static void ChangeSatusToClose(Job job, ILogsService logsService, JobStatus newStatus)
        {
            if (newStatus == JobStatus.Failed)
            {
                int failedAttempt = job.FailedAttemptionCount;
                job.FailedAttemptionCount = failedAttempt + 1;

                int possibleFailedJobCount = Int32.Parse(ConfigurationManager.AppSettings[Consts.AppSettings.PossibleFailedJobCount]);
                if (job.FailedAttemptionCount == possibleFailedJobCount)
                {
                    ChangeJobStatus(job, JobStatus.Closed);
                    AddLog(logsService, job.Id, JobStatus.Closed);
                }
            }
        }
    }
}