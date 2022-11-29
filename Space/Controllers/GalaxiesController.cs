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
    public class GalaxiesController : Controller
    {
        private readonly SpaceContext _context;

        public GalaxiesController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["GlxNameSortParm"] = sortOrder == "GlxName" ? "GlxName_desc" : "GlxName";
            ViewData["GlxTypeSortParm"] = sortOrder == "GlxType" ? "GlxType_desc" : "GlxType";
            ViewData["GlxRightAscensionSortParm"] = sortOrder == "GlxRightAscension" ? "GlxRightAscension_desc" : "GlxRightAscension";
            ViewData["GlxDeclinationSortParm"] = sortOrder == "GlxDeclination" ? "GlxDeclination_desc" : "GlxDeclination";
            ViewData["GlxRedshiftSortParm"] = sortOrder == "GlxRedshift" ? "GlxRedshift_desc" : "GlxRedshift";
            ViewData["GlxDistanceSortParm"] = sortOrder == "GlxDistance" ? "GlxDistance_desc" : "GlxDistance";
            ViewData["GlxApparentMagnitudeSortParm"] = sortOrder == "GlxApparentMagnitude" ? "GlxApparentMagnitude_desc" : "GlxApparentMagnitude";
            ViewData["GlxRadialVelocitySortParm"] = sortOrder == "GlxRadialVelocity" ? "GlxRadialVelocity_desc" : "GlxRadialVelocity";
            ViewData["GlxRadiusSortParm"] = sortOrder == "GlxRadius" ? "GlxRadius_desc" : "GlxRadius";
            ViewData["ConsSortParm"] = sortOrder == "Cons" ? "Cons_desc" : "Cons";
            ViewData["GlxclusterSortParm"] = sortOrder == "Glxcluster" ? "Glxcluster_desc" : "Glxcluster";
            ViewData["GlxgroupSortParm"] = sortOrder == "Glxgroup" ? "Glxgroup_desc" : "Glxgroup";

            var galaxies = from g in _context.Galaxies.Include(g => g.Cons).Include(g => g.Glxcluster).Include(g => g.Glxgroup)
                            select g;

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "GlxName";
                sortOrder = "GlxType";
                sortOrder = "GlxRightAscension";
                sortOrder = "GlxDeclination";
                sortOrder = "GlxRedshift";
                sortOrder = "GlxDistance";
                sortOrder = "GlxApparentMagnitude";
                sortOrder = "GlxRadialVelocity";
                sortOrder = "GlxRadius";
                sortOrder = "Cons";
                sortOrder = "Glxcluster";
                sortOrder = "Glxgroup";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                galaxies = galaxies.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                galaxies = galaxies.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            return View(await galaxies.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxies = await _context.Galaxies
                .Include(g => g.Cons)
                .Include(g => g.Glxcluster)
                .Include(g => g.Glxgroup)
                .FirstOrDefaultAsync(m => m.GlxId == id);
            if (galaxies == null)
            {
                return NotFound();
            }

            return View(galaxies);
        }

        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName");
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GlxId,ConsId,GlxclusterId,GlxgroupId,GlxName,GlxType,GlxRightAscension,GlxDeclination,GlxRedshift,GlxDistance,GlxApparentMagnitude,GlxRadialVelocity,GlxRadius")] Galaxies galaxies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galaxies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxies.ConsId);
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName", galaxies.GlxclusterId);
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName", galaxies.GlxgroupId);
            return View(galaxies);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxies = await _context.Galaxies.FindAsync(id);
            if (galaxies == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxies.ConsId);
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName", galaxies.GlxclusterId);
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName", galaxies.GlxgroupId);
            return View(galaxies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GlxId,ConsId,GlxclusterId,GlxgroupId,GlxName,GlxType,GlxRightAscension,GlxDeclination,GlxRedshift,GlxDistance,GlxApparentMagnitude,GlxRadialVelocity,GlxRadius")] Galaxies galaxies)
        {
            if (id != galaxies.GlxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galaxies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalaxiesExists(galaxies.GlxId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxies.ConsId);
            ViewData["GlxclusterId"] = new SelectList(_context.GalaxyClusters, "GlxclusterId", "GlxclusterName", galaxies.GlxclusterId);
            ViewData["GlxgroupId"] = new SelectList(_context.GalaxyGroups, "GlxgroupId", "GlxgroupName", galaxies.GlxgroupId);
            return View(galaxies);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galaxies == null)
            {
                return NotFound();
            }

            var galaxies = await _context.Galaxies
                .Include(g => g.Cons)
                .Include(g => g.Glxcluster)
                .Include(g => g.Glxgroup)
                .FirstOrDefaultAsync(m => m.GlxId == id);
            if (galaxies == null)
            {
                return NotFound();
            }

            return View(galaxies);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galaxies == null)
            {
                return Problem("Entity set 'SpaceContext.Galaxies'  is null.");
            }
            var galaxies = await _context.Galaxies.FindAsync(id);
            if (galaxies != null)
            {
                _context.Galaxies.Remove(galaxies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxiesExists(int id)
        {
          return (_context.Galaxies?.Any(e => e.GlxId == id)).GetValueOrDefault();
        }
    }
}
