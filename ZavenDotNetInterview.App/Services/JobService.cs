using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZavenDotNetInterview.App.Extensions;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.ViewModels;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobService : IJobService
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly ILogsRepository _logsRepository;

        public JobService(IJobsRepository jobsRepository,
            ILogsRepository logsRepository)
        {
            _jobsRepository = jobsRepository;
            _logsRepository = logsRepository;
        }

        public List<JobViewModel> GetJobs()
        {
            var jobs = _jobsRepository.GetAllJobs();

            var result = jobs
                .OrderBy(j => j.Logs.First(l => l.Description == JobStatus.New.GetEnumDescription<JobStatus>()).CreatedAt)
                .Select(j =>
                new JobViewModel
                {
                    Id = j.Id,
                    Name = j.Name,
                    Status = j.Status
                }).ToList();

            return result;
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

        public void CreateJob(JobCreateDataViewModel data)
        {
            Job newJob = new Job()
            {
                Id = Guid.NewGuid(),
                DoAfter = data.DoAfter,
                Name = data.Name,
                Status = JobStatus.New,
                LastUpdatedAt = DateTime.UtcNow
            };

            int result = _jobsRepository.CreateJob(newJob);

            if (result > 0)
            {
                _logsRepository.InsertLog(
                    new Log
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        Description = JobStatus.New.GetEnumDescription<JobStatus>(),
                        Job = newJob,
                        JobId = newJob.Id
                    });
            }
        }

        public bool IfJobExists(string jobName)
        {
            var jobs = _jobsRepository.GetAllJobs();

            return jobs.FirstOrDefault(j => j.Name == jobName) != null ? true : false;
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