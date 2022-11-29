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
    public class PlanetarySystemsController : Controller
    {
        private readonly SpaceContext _context;

        public PlanetarySystemsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["PlanetsystemNameSortParm"] = sortOrder == "PlanetsystemName" ? "PlanetsystemName_desc" : "PlanetsystemName";
            ViewData["PlanetsystemConfirmedPlanetsSortParm"] = sortOrder == "PlanetsystemConfirmedPlanets" ? "PlanetsystemConfirmedPlanets_desc" : "PlanetsystemConfirmedPlanets";
            ViewData["ConsSortParm"] = sortOrder == "Cons" ? "Cons_desc" : "Cons";
            ViewData["GlxSortParm"] = sortOrder == "Glx" ? "Glx_desc" : "Glx";

            var planetarySystems = from p in _context.PlanetarySystems.Include(p => p.Cons).Include(p => p.Glx)
                            select p;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "PlanetsystemName";
                sortOrder = "PlanetsystemConfirmedPlanets";
                sortOrder = "Cons";
                sortOrder = "Glx";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                planetarySystems = planetarySystems.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                planetarySystems = planetarySystems.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await planetarySystems.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlanetarySystems == null)
            {
                return NotFound();
            }

            var planetarySystems = await _context.PlanetarySystems
                .Include(p => p.Cons)
                .Include(p => p.Glx)
                .FirstOrDefaultAsync(m => m.PlanetsystemId == id);
            if (planetarySystems == null)
            {
                return NotFound();
            }

            return View(planetarySystems);
        }

        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanetsystemId,ConsId,GlxId,PlanetsystemName,PlanetsystemConfirmedPlanets,PlanetsystemImage")] PlanetarySystems planetarySystems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planetarySystems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlanetarySystems == null)
            {
                return NotFound();
            }

            var planetarySystems = await _context.PlanetarySystems.FindAsync(id);
            if (planetarySystems == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanetsystemId,ConsId,GlxId,PlanetsystemName,PlanetsystemConfirmedPlanets,PlanetsystemImage")] PlanetarySystems planetarySystems)
        {
            if (id != planetarySystems.PlanetsystemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planetarySystems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetarySystemsExists(planetarySystems.PlanetsystemId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlanetarySystems == null)
            {
                return NotFound();
            }

            var planetarySystems = await _context.PlanetarySystems
                .Include(p => p.Cons)
                .Include(p => p.Glx)
                .FirstOrDefaultAsync(m => m.PlanetsystemId == id);
            if (planetarySystems == null)
            {
                return NotFound();
            }

            return View(planetarySystems);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlanetarySystems == null)
            {
                return Problem("Entity set 'SpaceContext.PlanetarySystems'  is null.");
            }
            var planetarySystems = await _context.PlanetarySystems.FindAsync(id);
            if (planetarySystems != null)
            {
                _context.PlanetarySystems.Remove(planetarySystems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetarySystemsExists(int id)
        {
          return (_context.PlanetarySystems?.Any(e => e.PlanetsystemId == id)).GetValueOrDefault();
        }
    }
}
