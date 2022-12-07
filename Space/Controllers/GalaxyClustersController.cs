using Microsoft.AspNetCore.Http;
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

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.GalaxyClusters.Include(g => g.Cons);
            return View(await spaceContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("GlxclusterId,ConsId,GlxclusterName,GlxclusterType,GlxclusterRightAscension,GlxclusterDeclination,GlxclusterRedshift")] GalaxyClusters galaxyClusters, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/GlxClusters/", fileName);
                var filestream = new FileStream(uploadfilepath, FileMode.Create);
                await formFile.CopyToAsync(filestream);

                string uploadedDBpath = "/Img/GlxClusters/" + fileName;
                SpaceContext spaceContext = new SpaceContext();

                var data = new GalaxyClusters()
                {
                    GlxclusterId = galaxyClusters.GlxclusterId,
                    ConsId = galaxyClusters.ConsId,
                    GlxclusterName = galaxyClusters.GlxclusterName,
                    GlxclusterType = galaxyClusters.GlxclusterType,
                    GlxclusterRightAscension = galaxyClusters.GlxclusterRightAscension,
                    GlxclusterDeclination = galaxyClusters.GlxclusterDeclination,
                    GlxclusterRedshift = galaxyClusters.GlxclusterRedshift,
                    GlxclusterImage = uploadedDBpath
                };

                _context.Add(data);
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
            var galaxyClusters = await _context.GalaxyClusters
                .Include(g => g.Galaxies)
                .FirstOrDefaultAsync(m => m.GlxclusterId == id);
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
