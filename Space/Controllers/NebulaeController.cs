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
    public class NebulaeController : Controller
    {
        private readonly SpaceContext _context;

        public NebulaeController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Nebulae.Include(n => n.Cons).Include(n => n.Glx);
            return View(await spaceContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nebulae == null)
            {
                return NotFound();
            }

            var nebulae = await _context.Nebulae
                .Include(n => n.Cons)
                .Include(n => n.Glx)
                .FirstOrDefaultAsync(m => m.NebulaId == id);
            if (nebulae == null)
            {
                return NotFound();
            }

            return View(nebulae);
        }

        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NebulaId,ConsId,GlxId,NebulaName,NebulaType,NebulaRightAscension,NebulaDeclination,NebulaDistance,NebulaImage")] Nebulae nebulae)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nebulae);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", nebulae.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", nebulae.GlxId);
            return View(nebulae);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nebulae == null)
            {
                return NotFound();
            }

            var nebulae = await _context.Nebulae.FindAsync(id);
            if (nebulae == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", nebulae.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", nebulae.GlxId);
            return View(nebulae);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NebulaId,ConsId,GlxId,NebulaName,NebulaType,NebulaRightAscension,NebulaDeclination,NebulaDistance,NebulaImage")] Nebulae nebulae)
        {
            if (id != nebulae.NebulaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nebulae);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NebulaeExists(nebulae.NebulaId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", nebulae.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", nebulae.GlxId);
            return View(nebulae);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nebulae == null)
            {
                return NotFound();
            }

            var nebulae = await _context.Nebulae
                .Include(n => n.Cons)
                .Include(n => n.Glx)
                .FirstOrDefaultAsync(m => m.NebulaId == id);
            if (nebulae == null)
            {
                return NotFound();
            }

            return View(nebulae);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nebulae == null)
            {
                return Problem("Entity set 'SpaceContext.Nebulae'  is null.");
            }
            var nebulae = await _context.Nebulae.FindAsync(id);
            if (nebulae != null)
            {
                _context.Nebulae.Remove(nebulae);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NebulaeExists(int id)
        {
          return (_context.Nebulae?.Any(e => e.NebulaId == id)).GetValueOrDefault();
        }
    }
}
