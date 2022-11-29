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

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["BlackholeNameSortParm"] = sortOrder == "BlackholeName" ? "BlackholeName_desc" : "BlackholeName";
            ViewData["BlackholeTypeSortParm"] = sortOrder == "BlackholeType" ? "BlackholeType_desc" : "BlackholeType";
            ViewData["BlackholeRightAscensionSortParm"] = sortOrder == "BlackholeRightAscension" ? "BlackholeRightAscension_desc" : "BlackholeRightAscension";
            ViewData["BlackholeDeclinationSortParm"] = sortOrder == "BlackholeDeclination" ? "BlackholeDeclination_desc" : "BlackholeDeclination";
            ViewData["BlackholeDistanceSortParm"] = sortOrder == "BlackholeDistance" ? "BlackholeDistance_desc" : "BlackholeDistance";
            ViewData["ConsSortParm"] = sortOrder == "Cons" ? "Cons_desc" : "Cons";
            ViewData["GlxSortParm"] = sortOrder == "Glx" ? "Glx_desc" : "Glx";


            var blackHoles = from b in _context.BlackHoles.Include(b => b.Cons).Include(b => b.Glx)
                            select b;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "BlackholeName";
                sortOrder = "BlackholeType";
                sortOrder = "BlackholeRightAscension";
                sortOrder = "BlackholeDeclination";
                sortOrder = "BlackholeDistance";
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
                blackHoles = blackHoles.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                blackHoles = blackHoles.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await blackHoles.AsNoTracking().ToListAsync());
        }

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

        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

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
