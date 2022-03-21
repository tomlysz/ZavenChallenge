using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.Domain.Models.ViewModels
{
    public class JobDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        public DateTime? DoAfter { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public List<LogViewModel> Logs { get; set; }

        public string DoAfterStr { get => DoAfter.HasValue ? DoAfter.Value.ToString("yyyy-MM-dd") : ""; }
        public string LastUpdatedAtStr { get => LastUpdatedAt.HasValue ? LastUpdatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; }
    }
}