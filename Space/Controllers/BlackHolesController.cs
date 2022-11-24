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
    public class BlackHolesController : Controller
    {
        private readonly SpaceContext _context;

        public BlackHolesController(SpaceContext context)
        {
            _context = context;
        }

        // GET: BlackHoles
        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.BlackHoles.Include(b => b.Cons).Include(b => b.Glx);
            return View(await spaceContext.ToListAsync());
        }

        // GET: BlackHoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BlackHoles == null)
            {
                return NotFound();
            }

            var blackHoles = await _context.BlackHoles
                .Include(b => b.Cons)
                .Include(b => b.Glx)
                .FirstOrDefaultAsync(m => m.BlackHoleId == id);
            if (blackHoles == null)
            {
                return NotFound();
            }

            return View(blackHoles);
        }

        // GET: BlackHoles/Create
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        // POST: BlackHoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlackHoleId,ConsId,GlxId,BlackholeName,BlackholeType,BlackholeRightAscension,BlackholeDeclination,BlackholeDistance")] BlackHoles blackHoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blackHoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

        // GET: BlackHoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BlackHoles == null)
            {
                return NotFound();
            }

            var blackHoles = await _context.BlackHoles.FindAsync(id);
            if (blackHoles == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

        // POST: BlackHoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlackHoleId,ConsId,GlxId,BlackholeName,BlackholeType,BlackholeRightAscension,BlackholeDeclination,BlackholeDistance")] BlackHoles blackHoles)
        {
            if (id != blackHoles.BlackHoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blackHoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlackHolesExists(blackHoles.BlackHoleId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsAbbreviation", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

        // GET: BlackHoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BlackHoles == null)
            {
                return NotFound();
            }

            var blackHoles = await _context.BlackHoles
                .Include(b => b.Cons)
                .Include(b => b.Glx)
                .FirstOrDefaultAsync(m => m.BlackHoleId == id);
            if (blackHoles == null)
            {
                return NotFound();
            }

            return View(blackHoles);
        }

        // POST: BlackHoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BlackHoles == null)
            {
                return Problem("Entity set 'SpaceContext.BlackHoles'  is null.");
            }
            var blackHoles = await _context.BlackHoles.FindAsync(id);
            if (blackHoles != null)
            {
                _context.BlackHoles.Remove(blackHoles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlackHolesExists(int id)
        {
          return (_context.BlackHoles?.Any(e => e.BlackHoleId == id)).GetValueOrDefault();
        }
    }
}
