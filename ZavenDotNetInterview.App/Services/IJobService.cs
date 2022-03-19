using System;
using ZavenDotNetInterview.App.Models.ViewModels;

namespace ZavenDotNetInterview.App.Services
{
    public interface IJobService
    {
        JobDetailsViewModel GetJobDetails(Guid guid);
    }
}