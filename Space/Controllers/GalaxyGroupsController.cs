using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class GalaxyGroupsController : Controller
    {
        private readonly SpaceContext _context;

        public GalaxyGroupsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["GlxgroupNameSortParm"] = sortOrder == "GlxgroupName" ? "GlxgroupName_desc" : "GlxgroupName";
            ViewData["GlxgroupTypeSortParm"] = sortOrder == "GlxgroupType" ? "GlxgroupType_desc" : "GlxgroupType";
            ViewData["GlxgroupRightAscensionSortParm"] = sortOrder == "GlxgroupRightAscension" ? "GlxgroupRightAscension_desc" : "GlxgroupRightAscension";
            ViewData["GlxgroupDeclinationSortParm"] = sortOrder == "GlxgroupDeclination" ? "GlxgroupDeclination_desc" : "GlxgroupDeclination";
            ViewData["GlxgroupRedshiftSortParm"] = sortOrder == "GlxgroupRedshift" ? "GlxgroupRedshift_desc" : "GlxgroupRedshift";
            ViewData["ConsSortParm"] = sortOrder == "Cons" ? "Cons_desc" : "Cons";

            var galaxyGroups = from g in _context.GalaxyGroups.Include(g => g.Cons)
                            select g;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "GlxgroupName";
                sortOrder = "GlxgroupType";
                sortOrder = "GlxgroupRightAscension";
                sortOrder = "GlxgroupDeclination";
                sortOrder = "GlxgroupRedshift";
                sortOrder = "Cons";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                galaxyGroups = galaxyGroups.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                galaxyGroups = galaxyGroups.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await galaxyGroups.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GalaxyGroups == null)
            {
                return NotFound();
            }

            var galaxyGroups = await _context.GalaxyGroups
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxgroupId == id);
            if (galaxyGroups == null)
            {
                return NotFound();
            }

            return View(galaxyGroups);
        }

        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GlxgroupId,ConsId,GlxgroupName,GlxgroupType,GlxgroupRightAscension,GlxgroupDeclination,GlxgroupRedshift")] GalaxyGroups galaxyGroups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galaxyGroups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GalaxyGroups == null)
            {
                return NotFound();
            }

            var galaxyGroups = await _context.GalaxyGroups.FindAsync(id);
            if (galaxyGroups == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GlxgroupId,ConsId,GlxgroupName,GlxgroupType,GlxgroupRightAscension,GlxgroupDeclination,GlxgroupRedshift")] GalaxyGroups galaxyGroups)
        {
            if (id != galaxyGroups.GlxgroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galaxyGroups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalaxyGroupsExists(galaxyGroups.GlxgroupId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GalaxyGroups == null)
            {
                return NotFound();
            }

            var galaxyGroups = await _context.GalaxyGroups
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxgroupId == id);
            if (galaxyGroups == null)
            {
                return NotFound();
            }

            return View(galaxyGroups);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GalaxyGroups == null)
            {
                return Problem("Entity set 'SpaceContext.GalaxyGroups'  is null.");
            }
            var galaxyGroups = await _context.GalaxyGroups.FindAsync(id);
            if (galaxyGroups != null)
            {
                _context.GalaxyGroups.Remove(galaxyGroups);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxyGroupsExists(int id)
        {
          return (_context.GalaxyGroups?.Any(e => e.GlxgroupId == id)).GetValueOrDefault();
        }
    }
}
