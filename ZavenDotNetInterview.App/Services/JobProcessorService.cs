using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZavenDotNetInterview.App.Extensions;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Repositories;

namespace ZavenDotNetInterview.App.Services
{
    public class JobProcessorService : IJobProcessorService
    {
        private ZavenDotNetInterviewContext _ctx;
        private readonly ILogsService _logsService;

        public JobProcessorService(ZavenDotNetInterviewContext ctx,
            ILogsService logsService)
        {
            _ctx = ctx;
            _logsService = logsService;
        }

        public void ProcessJobs()
        {
            IJobsRepository jobsRepository = new JobsRepository(_ctx);
            var allJobs = jobsRepository.GetAllJobs();
            var jobsToProcess =
                allJobs
                .Where(x => (DateTime.UtcNow > x.DoAfter || !x.DoAfter.HasValue)
                && (x.Status == JobStatus.New || x.Status == JobStatus.Failed))
                .ToList();

            jobsToProcess.ForEach(job => job.ChangeStatus(_logsService, JobStatus.InProgress));

            _ctx.SaveChanges();

            Parallel.ForEach(jobsToProcess, (currentjob) =>
            {
                bool result = this.ProcessJob();
                if (result)
                {
                    currentjob.ChangeStatus(_logsService, JobStatus.Done);
                }
                else
                {
                    currentjob.ChangeStatus(_logsService, JobStatus.Failed);
                }
            });

            _ctx.SaveChanges();
        }

        private bool ProcessJob()
        {
            Random rand = new Random();
            if (rand.Next(10) < 5)
            {
                Thread.Sleep(2000);
                return false;
            }
            else
            {
                Thread.Sleep(1000);
                return true;
            }
        }
    }
}