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
    public class GalaxiesController : Controller
    {
        private readonly SpaceContext _context;

        public GalaxiesController(SpaceContext context)
        {
            _context = context;
        }

        // GET: Galaxies
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Galaxies.Include(g => g.Cons).Include(g => g.Glxcluster).Include(g => g.Glxgroup);
            return View(await spaceContext.ToListAsync());
        }

        // GET: Galaxies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxies = await _context.Galaxies
                .Include(g => g.Cons)
                .Include(g => g.Glxcluster)
                .Include(g => g.Glxgroup)
                .FirstOrDefaultAsync(m => m.GlxId == id);
            if (galaxies == null)
            {
                return NotFound();
            }

            return View(galaxies);
        }

        // GET: Galaxies/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName");
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName");
            return View();
        }

        // POST: Galaxies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GlxId,ConsId,GlxclusterId,GlxgroupId,GlxName,GlxType,GlxRightAscension,GlxDeclination,GlxRedshift,GlxDistance,GlxApparentMagnitude,GlxRadialVelocity,GlxRadius")] Galaxies galaxies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galaxies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxies.ConsId);
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName", galaxies.GlxclusterId);
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName", galaxies.GlxgroupId);
            return View(galaxies);
        }

        // GET: Galaxies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxies = await _context.Galaxies.FindAsync(id);
            if (galaxies == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxies.ConsId);
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName", galaxies.GlxclusterId);
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName", galaxies.GlxgroupId);
            return View(galaxies);
        }

        // POST: Galaxies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GlxId,ConsId,GlxclusterId,GlxgroupId,GlxName,GlxType,GlxRightAscension,GlxDeclination,GlxRedshift,GlxDistance,GlxApparentMagnitude,GlxRadialVelocity,GlxRadius")] Galaxies galaxies)
        {
            if (id != galaxies.GlxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galaxies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalaxiesExists(galaxies.GlxId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxies.ConsId);
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName", galaxies.GlxclusterId);
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName", galaxies.GlxgroupId);
            return View(galaxies);
        }

        // GET: Galaxies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxies = await _context.Galaxies
                .Include(g => g.Cons)
                .Include(g => g.Glxcluster)
                .Include(g => g.Glxgroup)
                .FirstOrDefaultAsync(m => m.GlxId == id);
            if (galaxies == null)
            {
                return NotFound();
            }

            return View(galaxies);
        }

        // POST: Galaxies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galaxies == null)
            {
                return Problem("Entity set 'SpaceContext.Galaxies'  is null.");
            }
            var galaxies = await _context.Galaxies.FindAsync(id);
            if (galaxies != null)
            {
                _context.Galaxies.Remove(galaxies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxiesExists(int id)
        {
          return (_context.Galaxies?.Any(e => e.GlxId == id)).GetValueOrDefault();
        }
    }
}
