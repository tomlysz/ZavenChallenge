using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.Domain.Models.ViewModels
{
    public class LogViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid JobId { get; set; }
    }
}