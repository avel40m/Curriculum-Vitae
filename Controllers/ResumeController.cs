using System.Security.Cryptography.X509Certificates;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using resumemanager.Data;
using resumemanager.Models;
using resumemanager.obj;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace resumemanager.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumenDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public ResumeController(ResumenDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        
        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();
            return View(applicants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Applicant applicant = new Applicant();
            applicant.Experiencies.Add(new Models.Experience() { ExperienceId = 1 });
            // applicant.Experiencies.Add(new Models.Experience() { ExperienceId = 2 });
            // applicant.Experiencies.Add(new Models.Experience() { ExperienceId = 3 });
            return View(applicant);
        }

        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {
            applicant.Experiencies.RemoveAll(x => x.YearsWorked == 0);

            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;

            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;

            if(applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder,uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

      
        public IActionResult Details(int id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.Experiencies).Where(a => a.Id == id).FirstOrDefault();
            return View(applicant);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Applicant applicant = _context.Applicants.Include(e => e.Experiencies).Where(x => x.Id == id).FirstOrDefault();
            return View(applicant);
        }

        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");       
        }
    }
}