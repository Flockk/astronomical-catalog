using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class StarClustersController : Controller
    {
        private readonly SpaceContext _context;

        public StarClustersController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.StarClusters.Include(s => s.Cons).Include(s => s.Glx);
            return View(await spaceContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .FirstOrDefaultAsync(m => m.StarclusterId == id);
            if (starClusters == null)
            {
                return NotFound();
            }

            return View(starClusters);
        }

        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StarclusterId,ConsId,GlxId,StarclusterName,StarclusterType,StarclusterRightAscension,StarclusterDeclination,StarclusterDistance,StarclusterAge,StarclusterDiameter,StarclusterImage")] StarClusters starClusters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(starClusters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters.FindAsync(id);
            if (starClusters == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StarclusterId,ConsId,GlxId,StarclusterName,StarclusterType,StarclusterRightAscension,StarclusterDeclination,StarclusterDistance,StarclusterAge,StarclusterDiameter,StarclusterImage")] StarClusters starClusters)
        {
            if (id != starClusters.StarclusterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(starClusters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarClustersExists(starClusters.StarclusterId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .FirstOrDefaultAsync(m => m.StarclusterId == id);
            if (starClusters == null)
            {
                return NotFound();
            }

            return View(starClusters);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StarClusters == null)
            {
                return Problem("Entity set 'SpaceContext.StarClusters'  is null.");
            }
            var starClusters = await _context.StarClusters.FindAsync(id);
            if (starClusters != null)
            {
                _context.StarClusters.Remove(starClusters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarClustersExists(int id)
        {
          return (_context.StarClusters?.Any(e => e.StarclusterId == id)).GetValueOrDefault();
        }
    }
}
