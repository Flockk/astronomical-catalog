using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class AsteroidsController : Controller
    {
        private readonly SpaceContext _context;

        public AsteroidsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["AstNameSortParm"] = sortOrder == "AstName" ? "AstName_desc" : "AstName";
            ViewData["AstDiameterSortParm"] = sortOrder == "AstDiameter" ? "AstDiameter_desc" : "AstDiameter";
            ViewData["AstOrbitalEccentricitySortParm"] = sortOrder == "AstOrbitalEccentricity" ? "AstOrbitalEccentricity_desc" : "AstOrbitalEccentricity";
            ViewData["AstOrbitalInclinationSortParm"] = sortOrder == "AstOrbitalInclination" ? "AstOrbitalInclination_desc" : "AstOrbitalInclination";
            ViewData["AstArgumentOfPerihelionSortParm"] = sortOrder == "AstArgumentOfPerihelion" ? "AstArgumentOfPerihelion_desc" : "AstArgumentOfPerihelion";
            ViewData["AstMeanAnomalySortParm"] = sortOrder == "AstMeanAnomaly" ? "AstMeanAnomaly_desc" : "AstMeanAnomaly";
            ViewData["StarSortParm"] = sortOrder == "Star" ? "Star_desc" : "Star";

            var asteroids = from a in _context.Asteroids.Include(a => a.Star)
                            select a;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "AstName";
                sortOrder = "AstDiameter";
                sortOrder = "AstOrbitalEccentricity";
                sortOrder = "AstOrbitalInclination";
                sortOrder = "AstArgumentOfPerihelion";
                sortOrder = "AstMeanAnomaly";
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
                asteroids = asteroids.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                asteroids = asteroids.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await asteroids.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asteroids == null)
            {
                return NotFound();
            }

            var asteroids = await _context.Asteroids
                .Include(a => a.Star)
                .FirstOrDefaultAsync(m => m.AstId == id);
            if (asteroids == null)
            {
                return NotFound();
            }

            return View(asteroids);
        }

        public IActionResult Create()
        {
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AstId,StarId,AstName,AstDiameter,AstOrbitalEccentricity,AstOrbitalInclination,AstArgumentOfPerihelion,AstMeanAnomaly")] Asteroids asteroids)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asteroids);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asteroids == null)
            {
                return NotFound();
            }

            var asteroids = await _context.Asteroids.FindAsync(id);
            if (asteroids == null)
            {
                return NotFound();
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AstId,StarId,AstName,AstDiameter,AstOrbitalEccentricity,AstOrbitalInclination,AstArgumentOfPerihelion,AstMeanAnomaly")] Asteroids asteroids)
        {
            if (id != asteroids.AstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asteroids);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsteroidsExists(asteroids.AstId))
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
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asteroids == null)
            {
                return NotFound();
            }

            var asteroids = await _context.Asteroids
                .Include(a => a.Star)
                .FirstOrDefaultAsync(m => m.AstId == id);
            if (asteroids == null)
            {
                return NotFound();
            }

            return View(asteroids);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asteroids == null)
            {
                return Problem("Entity set 'SpaceContext.Asteroids'  is null.");
            }
            var asteroids = await _context.Asteroids.FindAsync(id);
            if (asteroids != null)
            {
                _context.Asteroids.Remove(asteroids);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsteroidsExists(int id)
        {
          return (_context.Asteroids?.Any(e => e.AstId == id)).GetValueOrDefault();
        }
    }
}
