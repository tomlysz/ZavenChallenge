using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZavenDotNetInterview.App.Models.ViewModels
{
    public class JobDataViewModel
    {
        [Required]
        public string Name { get; set; }
        
        public DateTime? DoAfter { get; set; }
    }
}