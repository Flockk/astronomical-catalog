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

        // GET: GalaxyGroups
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.GalaxyGroups.Include(g => g.Cons);
            return View(await spaceContext.ToListAsync());
        }

        // GET: GalaxyGroups/Details/5
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

        // GET: GalaxyGroups/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            return View();
        }

        // POST: GalaxyGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        // GET: GalaxyGroups/Edit/5
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        // POST: GalaxyGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        // GET: GalaxyGroups/Delete/5
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

        // POST: GalaxyGroups/Delete/5
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
