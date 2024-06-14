using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppEase.Models;
using AppEase.Service;
using Microsoft.AspNetCore.Authorization;

namespace AppEase.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly AppEaseDbContext _context;

        public JobsController(AppEaseDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var appEaseDbContext = _context.Jobs.Include(j => j.Profile);
            return View(await appEaseDbContext.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Email");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,Title,Description,ProfileId")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Email", job.ProfileId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Email", job.ProfileId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,Title,Description,ProfileId")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Email", job.ProfileId);
            return View(job);
        }
        // POST: Jobs/AddJob
        


        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Profile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
        private async Task<Profile> getProfile(int id)
        {
            var profile = await _context.Profiles.Include(p => p.Jobs).Include(p => p.Address).FirstOrDefaultAsync(p => p.Id == id);
            return profile;
        }
        private async Task<Job> getJob(int id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(p => p.Id == id);
            return job;
        }

        [HttpPost("cover-letter")]
        public IActionResult GetCoverLetterPdf([FromForm] Job job)
        {
            Profile profile = getProfile(job.ProfileId).Result;
            Job job1 = getJob(job.Id).Result;
            var pdfBytes = GeneratePdfService.getCoverLetterPdf(job1, profile);
            Response.Headers.Add("Content-Disposition", "inline; filename=cover-letter.pdf");
            return File(pdfBytes, "application/pdf");
        }


        [HttpPost("cover-letter-download")]
        public  IActionResult GetCoverLetterDownloadPdf([FromForm] Job job)
        {

            Profile profile = getProfile(job.ProfileId).Result;
            Job job1 = getJob(job.Id).Result;
            var pdfBytes = GeneratePdfService.getCoverLetterPdf(job1,profile);
            return File(pdfBytes, "application/pdf", "cover-letter.pdf");
        }
    }
}
