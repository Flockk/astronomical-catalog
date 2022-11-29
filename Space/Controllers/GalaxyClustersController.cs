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

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["GlxclusterNameSortParm"] = sortOrder == "GlxclusterName" ? "GlxclusterName_desc" : "GlxclusterName";
            ViewData["GlxclusterTypeSortParm"] = sortOrder == "GlxclusterType" ? "GlxclusterType_desc" : "GlxclusterType";
            ViewData["GlxclusterRightAscensionSortParm"] = sortOrder == "GlxclusterRightAscension" ? "GlxclusterRightAscension_desc" : "GlxclusterRightAscension";
            ViewData["GlxclusterDeclinationSortParm"] = sortOrder == "GlxclusterDeclination" ? "GlxclusterDeclination_desc" : "GlxclusterDeclination";
            ViewData["GlxclusterRedshiftSortParm"] = sortOrder == "GlxclusterRedshift" ? "GlxclusterRedshift_desc" : "GlxclusterRedshift";
            ViewData["ConsSortParm"] = sortOrder == "Cons" ? "Cons_desc" : "Cons";

            var galaxyClusters = from g in _context.GalaxyClusters.Include(g => g.Cons)
                            select g;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "GlxclusterName";
                sortOrder = "GlxclusterType";
                sortOrder = "GlxclusterRightAscension";
                sortOrder = "GlxclusterDeclination";
                sortOrder = "GlxclusterRedshift";
                sortOrder = "Cons";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                galaxyClusters = galaxyClusters.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                galaxyClusters = galaxyClusters.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await galaxyClusters.AsNoTracking().ToListAsync());
        }

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

        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            return View();
        }

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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

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
