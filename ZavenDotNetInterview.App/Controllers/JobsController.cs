using System;
using System.Web.Mvc;
using ZavenDotNetInterview.App.Models.ViewModels;
using ZavenDotNetInterview.App.Services;

namespace ZavenDotNetInterview.App.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobProcessorService _jobProcessorService;
        private readonly IJobService _jobService;
        public JobsController(IJobProcessorService jobProcessorService, 
            IJobService jobService)
        {
            _jobProcessorService = jobProcessorService;
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
            var jobExistsInDb = _jobService.IfJobExists(data.Name);
            if (jobExistsInDb)
            {
                ModelState.AddModelError("Name", "Name is already taken.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _jobService.CreateJob(data);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("","Something went wrong.");
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
