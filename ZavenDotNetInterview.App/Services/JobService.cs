using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.ViewModels;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobService : IJobService
    {
        private readonly IJobsRepository _jobsRepository;

        public JobService(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        public JobDetailsViewModel GetJobDetails(Guid guid)
        {
            var job = _jobsRepository.GetJob(guid);

            if (job == null)
                return new JobDetailsViewModel();

            var logList = MapLogs(job.Logs);
            var result = MapJob(job, logList.OrderBy(l => l.CreatedAt).ToList());           

            return result;
        }

        private List<LogViewModel> MapLogs(List<Log> logs)
        {
            return logs.Select(l => new LogViewModel
            {
                Id = l.Id,
                CreatedAt = l.CreatedAt,
                Description = l.Description,
                JobId = l.JobId
            }).ToList();
        }

        private JobDetailsViewModel MapJob(Job job, List<LogViewModel> logList)
        {
            return new JobDetailsViewModel
            {
                Id = job.Id,
                DoAfter = job.DoAfter,
                LastUpdatedAt = job.LastUpdatedAt,
                Name = job.Name,
                Status = job.Status,
                Logs = logList
            };
        }
    }
}