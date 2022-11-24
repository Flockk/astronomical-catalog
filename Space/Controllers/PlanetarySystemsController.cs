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

        // GET: PlanetarySystems
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.PlanetarySystems.Include(p => p.Cons).Include(p => p.Glx);
            return View(await spaceContext.ToListAsync());
        }

        // GET: PlanetarySystems/Details/5
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

        // GET: PlanetarySystems/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        // POST: PlanetarySystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanetsystemId,ConsId,GlxId,PlanetsystemName,PlanetsystemConfirmedPlanets")] PlanetarySystems planetarySystems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planetarySystems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        // GET: PlanetarySystems/Edit/5
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        // POST: PlanetarySystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanetsystemId,ConsId,GlxId,PlanetsystemName,PlanetsystemConfirmedPlanets")] PlanetarySystems planetarySystems)
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        // GET: PlanetarySystems/Delete/5
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

        // POST: PlanetarySystems/Delete/5
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
