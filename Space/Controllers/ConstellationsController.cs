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
    public class ConstellationsController : Controller
    {
        private readonly SpaceContext _context;

        public ConstellationsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Constellations != null ? 
                          View(await _context.Constellations.ToListAsync()) :
                          Problem("Entity set 'SpaceContext.Constellations'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Constellations == null)
            {
                return NotFound();
            }

            var constellations = await _context.Constellations
                .FirstOrDefaultAsync(m => m.ConsId == id);
            if (constellations == null)
            {
                return NotFound();
            }

            return View(constellations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsImage,ConsId,ConsName,ConsAbbreviation,ConsSymbolism,ConsRightAscension,ConsDeclination,ConsSquare,ConsVisibleInLatitudes")] Constellations constellations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(constellations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(constellations);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Constellations == null)
            {
                return NotFound();
            }

            var constellations = await _context.Constellations.FindAsync(id);
            if (constellations == null)
            {
                return NotFound();
            }
            return View(constellations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsId,ConsName,ConsAbbreviation,ConsSymbolism,ConsRightAscension,ConsDeclination,ConsSquare,ConsVisibleInLatitudes,ConsImage")] Constellations constellations)
        {
            if (id != constellations.ConsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(constellations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConstellationsExists(constellations.ConsId))
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
            return View(constellations);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Constellations == null)
            {
                return NotFound();
            }

            var constellations = await _context.Constellations
                .FirstOrDefaultAsync(m => m.ConsId == id);
            if (constellations == null)
            {
                return NotFound();
            }

            return View(constellations);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Constellations == null)
            {
                return Problem("Entity set 'SpaceContext.Constellations'  is null.");
            }
            var constellations = await _context.Constellations.FindAsync(id);
            if (constellations != null)
            {
                _context.Constellations.Remove(constellations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConstellationsExists(int id)
        {
          return (_context.Constellations?.Any(e => e.ConsId == id)).GetValueOrDefault();
        }
    }
}
