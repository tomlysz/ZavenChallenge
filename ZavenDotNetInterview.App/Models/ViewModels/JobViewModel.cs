using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models.ViewModels
{
    public class JobViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public JobStatus Status { get; set; }
    }
}