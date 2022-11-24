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
    public class StarsController : Controller
    {
        private readonly SpaceContext _context;

        public StarsController(SpaceContext context)
        {
            _context = context;
        }

        // GET: Stars
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Stars.Include(s => s.Cons).Include(s => s.Glx).Include(s => s.Planetsystem).Include(s => s.Starcluster);
            return View(await spaceContext.ToListAsync());
        }

        // GET: Stars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stars == null)
            {
                return NotFound();
            }

            var stars = await _context.Stars
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .Include(s => s.Planetsystem)
                .Include(s => s.Starcluster)
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (stars == null)
            {
                return NotFound();
            }

            return View(stars);
        }

        // GET: Stars/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName");
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName");
            return View();
        }

        // POST: Stars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StarId,ConsId,GlxId,StarclusterId,PlanetsystemId,StarName,StarApparentMagnitude,StarStellarClass,StarDistance,StarDeclination")] Stars stars)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stars);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", stars.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", stars.GlxId);
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName", stars.PlanetsystemId);
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName", stars.StarclusterId);
            return View(stars);
        }

        // GET: Stars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stars == null)
            {
                return NotFound();
            }

            var stars = await _context.Stars.FindAsync(id);
            if (stars == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", stars.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", stars.GlxId);
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName", stars.PlanetsystemId);
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName", stars.StarclusterId);
            return View(stars);
        }

        // POST: Stars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarId,ConsId,GlxId,StarclusterId,PlanetsystemId,StarName,StarApparentMagnitude,StarStellarClass,StarDistance,StarDeclination")] Stars stars)
        {
            if (id != stars.StarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stars);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarsExists(stars.StarId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", stars.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", stars.GlxId);
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName", stars.PlanetsystemId);
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName", stars.StarclusterId);
            return View(stars);
        }

        // GET: Stars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stars == null)
            {
                return NotFound();
            }

            var stars = await _context.Stars
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .Include(s => s.Planetsystem)
                .Include(s => s.Starcluster)
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (stars == null)
            {
                return NotFound();
            }

            return View(stars);
        }

        // POST: Stars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stars == null)
            {
                return Problem("Entity set 'SpaceContext.Stars'  is null.");
            }
            var stars = await _context.Stars.FindAsync(id);
            if (stars != null)
            {
                _context.Stars.Remove(stars);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarsExists(int id)
        {
          return (_context.Stars?.Any(e => e.StarId == id)).GetValueOrDefault();
        }
    }
}
