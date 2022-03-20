using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        public DateTime? DoAfter { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        [DefaultValue(0)]
        public int FailedAttemptionCount { get; set; }
        public virtual List<Log> Logs { get; set; }
    }

    public enum JobStatus
    {
        [Description("Failed")]
        Failed = -1,
        [Description("New")]
        New = 0,
        [Description("InProgress")]
        InProgress = 1,
        [Description("Done")]
        Done = 2,
        [Description("Closed")]
        Closed = 3
    }
}