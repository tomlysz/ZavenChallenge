using System;
using System.Collections.Generic;
using ZavenDotNetInterview.App.Models.ViewModels;

namespace ZavenDotNetInterview.App.Services
{
    public interface IJobService
    {
        JobDetailsViewModel GetJobDetails(Guid guid);
        List<JobViewModel> GetJobs();
        void CreateJob(JobCreateDataViewModel data);
        bool IfJobExists(string jobName);
    }
}