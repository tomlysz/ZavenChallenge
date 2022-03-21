using System;
using System.Collections.Generic;
using ZavenDotNetInterview.Domain.Models;

namespace ZavenDotNetInterview.Domain.Interfaces
{
    public interface IJobsRepository
    {
        List<Job> GetAllJobs();
        Job GetJob(Guid guid);
        int CreateJob(Job job);
    }
}