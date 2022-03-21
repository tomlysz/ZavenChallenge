using System;
using System.Collections.Generic;
using ZavenDotNetInterview.Domain.Models.ViewModels;

namespace ZavenDotNetInterview.Domain.Interfaces
{
    public interface IJobService
    {
        JobDetailsViewModel GetJobDetails(Guid guid);
        List<JobViewModel> GetJobs();
        void CreateJob(JobCreateDataViewModel data);
        bool IfJobExists(string jobName);
    }
}