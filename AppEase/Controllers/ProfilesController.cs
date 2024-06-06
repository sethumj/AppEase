using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppEase.Models;
using AppEase.Service;

namespace AppEase.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly AppEaseDbContext _context;

        public ProfilesController(AppEaseDbContext context)
        {
            _context = context;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profiles.ToListAsync());
        }

        [HttpGet("cover-letter")]
        public IActionResult GetCoverLetterPdf()
        {
            var pdfBytes = GeneratePdfService.getCoverLetterPdf();
            Response.Headers.Add("Content-Disposition", "inline; filename=cover-letter.pdf");
            return File(pdfBytes, "application/pdf");
        }


        [HttpGet("cover-letter-download")]
        public IActionResult GetCoverLetterDownloadPdf()
        {
            var pdfBytes = GeneratePdfService.getCoverLetterPdf();
            return File(pdfBytes, "application/pdf", "cover-letter.pdf");
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.Include(p => p.Jobs).FirstOrDefaultAsync(p => p.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);

        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,LinkedIn,Github,Resume,SkillSet")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> AddJob(int id, [Bind("Title", "Description", "ProfileId")] Job newJob)
        {
            if (ModelState.IsValid)
            {
                var profile = await _context.Profiles.Include(p => p.Jobs).FirstOrDefaultAsync(p => p.Id == id);
                if (profile != null)
                {
                    profile.Jobs ??= new List<Job>();
                    newJob.ProfileId = profile.Id;
                    newJob.Profile = profile;
                    profile.Jobs.Add(newJob);
                    _context.Add(newJob);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Details", new { id });
            }
            return View("Details", id);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,LinkedIn,Github,Resume,SkillSet")] Profile profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
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
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
