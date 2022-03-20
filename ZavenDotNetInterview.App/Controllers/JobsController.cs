﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavenDotNetInterview.App.Extensions;
using ZavenDotNetInterview.App.Models;
using ZavenDotNetInterview.App.Models.Context;
using ZavenDotNetInterview.App.Models.ViewModels;
using ZavenDotNetInterview.App.Repositories;
using ZavenDotNetInterview.App.Services;

namespace ZavenDotNetInterview.App.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobProcessorService _jobProcessorService;
        private readonly IJobService _jobService;
        private readonly ILogsRepository _logsRepository;
        public JobsController(IJobProcessorService jobProcessorService,
            ILogsRepository logsRepository, IJobService jobService)
        {
            _jobProcessorService = jobProcessorService;
            _logsRepository = logsRepository;
            _jobService = jobService;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            var result = _jobService.GetJobs();
            return View(result);

        }

        // POST: Tasks/Process
        [HttpGet]
        public ActionResult Process()
        {
            _jobProcessorService.ProcessJobs();

            return RedirectToAction("Index");
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View(new JobCreateDataViewModel());
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(JobCreateDataViewModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (ZavenDotNetInterviewContext _ctx = new ZavenDotNetInterviewContext())
                    {
                        var jobFromDb = _ctx.Jobs.FirstOrDefault(j => j.Name == data.Name);
                        if (jobFromDb != null)
                        {
                            ModelState.AddModelError("Name", "Name is already taken.");
                            return View(data);
                        }

                        Job newJob = new Job() { Id = Guid.NewGuid(), DoAfter = data.DoAfter, Name = data.Name, Status = JobStatus.New, LastUpdatedAt = DateTime.UtcNow };
                        newJob = _ctx.Jobs.Add(newJob);

                        if (_ctx.SaveChanges() > 0)
                        {
                            _logsRepository.InsertLog(new Log { Id = Guid.NewGuid(), CreatedAt = DateTime.Now, Description = JobStatus.New.GetEnumDescription<JobStatus>(), Job = newJob, JobId = newJob.Id });
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(data);
                }
            }
            return View(data);
        }

        public ActionResult Details(Guid jobId)
        {
            var result = _jobService.GetJobDetails(jobId);
            return View(result);
        }
    }
}
