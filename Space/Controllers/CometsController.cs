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
    public class CometsController : Controller
    {
        private readonly SpaceContext _context;

        public CometsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["CometNameSortParm"] = sortOrder == "CometName" ? "CometName_desc" : "CometName";
            ViewData["CometOrbitalPeriodSortParm"] = sortOrder == "CometOrbitalPeriod" ? "CometOrbitalPeriod_desc" : "CometOrbitalPeriod";
            ViewData["CometSemiMajorAxisSortParm"] = sortOrder == "CometSemiMajorAxis" ? "CometSemiMajorAxis_desc" : "CometSemiMajorAxis";
            ViewData["CometPerihelionSortParm"] = sortOrder == "CometPerihelion" ? "CometPerihelion_desc" : "CometPerihelion";
            ViewData["CometEccentricitySortParm"] = sortOrder == "CometEccentricity" ? "CometEccentricity_desc" : "CometEccentricity";
            ViewData["CometOrbitalInclinationSortParm"] = sortOrder == "CometOrbitalInclination" ? "CometOrbitalInclination_desc" : "CometOrbitalInclination";
            ViewData["StarSortParmSortParm"] = sortOrder == "Star" ? "Star_desc" : "Star";


            var comets = from c in _context.Comets.Include(c => c.Star)
                            select c;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "CometName";
                sortOrder = "CometOrbitalPeriod";
                sortOrder = "CometSemiMajorAxis";
                sortOrder = "CometPerihelion";
                sortOrder = "CometEccentricity";
                sortOrder = "CometOrbitalInclination";
                sortOrder = "Star";

            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                comets = comets.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                comets = comets.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await comets.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comets == null)
            {
                return NotFound();
            }

            var comets = await _context.Comets
                .Include(c => c.Star)
                .FirstOrDefaultAsync(m => m.CometId == id);
            if (comets == null)
            {
                return NotFound();
            }

            return View(comets);
        }

        public IActionResult Create()
        {
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CometId,StarId,CometName,CometOrbitalPeriod,CometSemiMajorAxis,CometPerihelion,CometEccentricity,CometOrbitalInclination")] Comets comets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", comets.StarId);
            return View(comets);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comets == null)
            {
                return NotFound();
            }

            var comets = await _context.Comets.FindAsync(id);
            if (comets == null)
            {
                return NotFound();
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", comets.StarId);
            return View(comets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CometId,StarId,CometName,CometOrbitalPeriod,CometSemiMajorAxis,CometPerihelion,CometEccentricity,CometOrbitalInclination")] Comets comets)
        {
            if (id != comets.CometId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CometsExists(comets.CometId))
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
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", comets.StarId);
            return View(comets);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comets == null)
            {
                return NotFound();
            }

            var comets = await _context.Comets
                .Include(c => c.Star)
                .FirstOrDefaultAsync(m => m.CometId == id);
            if (comets == null)
            {
                return NotFound();
            }

            return View(comets);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comets == null)
            {
                return Problem("Entity set 'SpaceContext.Comets'  is null.");
            }
            var comets = await _context.Comets.FindAsync(id);
            if (comets != null)
            {
                _context.Comets.Remove(comets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CometsExists(int id)
        {
          return (_context.Comets?.Any(e => e.CometId == id)).GetValueOrDefault();
        }
    }
}
