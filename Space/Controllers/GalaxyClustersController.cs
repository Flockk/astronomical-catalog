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
    public class GalaxyClustersController : Controller
    {
        private readonly SpaceContext _context;

        public GalaxyClustersController(SpaceContext context)
        {
            _context = context;
        }

        // GET: GalaxyClusters
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.GalaxyClusters.Include(g => g.Cons);
            return View(await spaceContext.ToListAsync());
        }

        // GET: GalaxyClusters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GalaxyClusters == null)
            {
                return NotFound();
            }

            var galaxyClusters = await _context.GalaxyClusters
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxclusterId == id);
            if (galaxyClusters == null)
            {
                return NotFound();
            }

            return View(galaxyClusters);
        }

        // GET: GalaxyClusters/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            return View();
        }

        // POST: GalaxyClusters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GlxclusterId,ConsId,GlxclusterName,GlxclusterType,GlxclusterRightAscension,GlxclusterDeclination,GlxclusterRedshift")] GalaxyClusters galaxyClusters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galaxyClusters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

        // GET: GalaxyClusters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GalaxyClusters == null)
            {
                return NotFound();
            }

            var galaxyClusters = await _context.GalaxyClusters.FindAsync(id);
            if (galaxyClusters == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

        // POST: GalaxyClusters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GlxclusterId,ConsId,GlxclusterName,GlxclusterType,GlxclusterRightAscension,GlxclusterDeclination,GlxclusterRedshift")] GalaxyClusters galaxyClusters)
        {
            if (id != galaxyClusters.GlxclusterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galaxyClusters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalaxyClustersExists(galaxyClusters.GlxclusterId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

        // GET: GalaxyClusters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GalaxyClusters == null)
            {
                return NotFound();
            }

            var galaxyClusters = await _context.GalaxyClusters
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxclusterId == id);
            if (galaxyClusters == null)
            {
                return NotFound();
            }

            return View(galaxyClusters);
        }

        // POST: GalaxyClusters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GalaxyClusters == null)
            {
                return Problem("Entity set 'SpaceContext.GalaxyClusters'  is null.");
            }
            var galaxyClusters = await _context.GalaxyClusters.FindAsync(id);
            if (galaxyClusters != null)
            {
                _context.GalaxyClusters.Remove(galaxyClusters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxyClustersExists(int id)
        {
          return (_context.GalaxyClusters?.Any(e => e.GlxclusterId == id)).GetValueOrDefault();
        }
    }
}
