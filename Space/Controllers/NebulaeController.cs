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

        // GET: Nebulae
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Nebulae.Include(n => n.Cons).Include(n => n.Glx);
            return View(await spaceContext.ToListAsync());
        }

        // GET: Nebulae/Details/5
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

        // GET: Nebulae/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        // POST: Nebulae/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NebulaId,ConsId,GlxId,NebulaName,NebulaType,NebulaRightAscension,NebulaDeclination,NebulaDistance")] Nebulae nebulae)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nebulae);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", nebulae.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", nebulae.GlxId);
            return View(nebulae);
        }

        // GET: Nebulae/Edit/5
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", nebulae.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", nebulae.GlxId);
            return View(nebulae);
        }

        // POST: Nebulae/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NebulaId,ConsId,GlxId,NebulaName,NebulaType,NebulaRightAscension,NebulaDeclination,NebulaDistance")] Nebulae nebulae)
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", nebulae.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", nebulae.GlxId);
            return View(nebulae);
        }

        // GET: Nebulae/Delete/5
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

        // POST: Nebulae/Delete/5
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
