using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models.ViewModels
{
    public class JobCreateDataViewModel
    {
        [Required]
        public string Name { get; set; }
        
        public DateTime? DoAfter { get; set; }
        public string DoAfterStr { get => DoAfter.HasValue ? DoAfter.Value.ToString("yyyy-MM-dd") : ""; }
    }
}