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
    public class StarClustersController : Controller
    {
        private readonly SpaceContext _context;

        public StarClustersController(SpaceContext context)
        {
            _context = context;
        }

        // GET: StarClusters
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.StarClusters.Include(s => s.Cons).Include(s => s.Glx);
            return View(await spaceContext.ToListAsync());
        }

        // GET: StarClusters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .FirstOrDefaultAsync(m => m.StarclusterId == id);
            if (starClusters == null)
            {
                return NotFound();
            }

            return View(starClusters);
        }

        // GET: StarClusters/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        // POST: StarClusters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StarclusterId,ConsId,GlxId,StarclusterName,StarclusterType,StarclusterRightAscension,StarclusterDeclination,StarclusterDistance,StarclusterAge,StarclusterDiameter")] StarClusters starClusters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(starClusters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        // GET: StarClusters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters.FindAsync(id);
            if (starClusters == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        // POST: StarClusters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarclusterId,ConsId,GlxId,StarclusterName,StarclusterType,StarclusterRightAscension,StarclusterDeclination,StarclusterDistance,StarclusterAge,StarclusterDiameter")] StarClusters starClusters)
        {
            if (id != starClusters.StarclusterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(starClusters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarClustersExists(starClusters.StarclusterId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        // GET: StarClusters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .FirstOrDefaultAsync(m => m.StarclusterId == id);
            if (starClusters == null)
            {
                return NotFound();
            }

            return View(starClusters);
        }

        // POST: StarClusters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StarClusters == null)
            {
                return Problem("Entity set 'SpaceContext.StarClusters'  is null.");
            }
            var starClusters = await _context.StarClusters.FindAsync(id);
            if (starClusters != null)
            {
                _context.StarClusters.Remove(starClusters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarClustersExists(int id)
        {
          return (_context.StarClusters?.Any(e => e.StarclusterId == id)).GetValueOrDefault();
        }
    }
}
