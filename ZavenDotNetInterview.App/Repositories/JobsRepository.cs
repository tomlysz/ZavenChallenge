using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;

namespace ZavenDotNetInterview.App.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly ZavenDotNetInterviewContext _ctx;

        public JobsRepository(ZavenDotNetInterviewContext ctx)
        {
            _ctx = ctx;
        }

        public List<Job> GetAllJobs()
        {
            return _ctx.Jobs.ToList();
        }

        public Job GetJob(Guid guid)
        {
            var result = _ctx.Jobs
                .SingleOrDefault(r => r.Id == guid);

            return result;
        }

        public int CreateJob(Job job)
        {
            _ctx.Jobs.Add(job);
            return _ctx.SaveChanges();
        }
    }
}