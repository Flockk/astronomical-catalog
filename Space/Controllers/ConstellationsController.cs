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

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["ConsNameSortParm"] = sortOrder == "ConsName" ? "ConsName_desc" : "ConsName";
            ViewData["ConsAbbreviationSortParm"] = sortOrder == "ConsAbbreviation" ? "ConsAbbreviation_desc" : "ConsAbbreviation";
            ViewData["ConsSymbolismSortParm"] = sortOrder == "ConsSymbolism" ? "ConsSymbolism_desc" : "ConsSymbolism";
            ViewData["ConsRightAscensionSortParm"] = sortOrder == "ConsRightAscension" ? "ConsRightAscension_desc" : "ConsRightAscension";
            ViewData["ConsDeclinationSortParm"] = sortOrder == "ConsDeclination" ? "ConsDeclination_desc" : "ConsDeclination";
            ViewData["ConsSquareSortParm"] = sortOrder == "ConsSquare" ? "ConsSquare_desc" : "ConsSquare";
            ViewData["ConsVisibleInLatitudesSortParm"] = sortOrder == "ConsVisibleInLatitudes" ? "ConsVisibleInLatitudes_desc" : "ConsVisibleInLatitudes";

            var constellations = from c in _context.Constellations
                            select c;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "ConsName";
                sortOrder = "ConsAbbreviation";
                sortOrder = "ConsSymbolism";
                sortOrder = "ConsRightAscension";
                sortOrder = "ConsDeclination";
                sortOrder = "ConsSquare";
                sortOrder = "ConsVisibleInLatitudes";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                constellations = constellations.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                constellations = constellations.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await constellations.AsNoTracking().ToListAsync());
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
        public async Task<IActionResult> Create([Bind("ConsId,ConsName,ConsAbbreviation,ConsSymbolism,ConsRightAscension,ConsDeclination,ConsSquare,ConsVisibleInLatitudes,ConsImage")] Constellations constellations)
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
