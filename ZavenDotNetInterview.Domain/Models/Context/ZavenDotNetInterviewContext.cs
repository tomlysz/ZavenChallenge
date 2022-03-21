using System.Data.Entity;

namespace ZavenDotNetInterview.Domain.Models.Context
{
    public class ZavenDotNetInterviewContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ZavenDotNetInterviewContext() : base("name=ZavenDotNetInterview")
        {
        }
    }
}