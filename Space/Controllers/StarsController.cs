using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class StarsController : Controller
    {
        private readonly SpaceContext _context;

        public StarsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Stars.Include(s => s.Cons).Include(s => s.Glx).Include(s => s.Planetsystem).Include(s => s.Starcluster);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsStarNameExist(string StarName)
        {
            return Json(!_context.Stars.Any(s => s.StarName == StarName));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stars == null)
            {
                return NotFound();
            }

            var stars = await _context.Stars
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .Include(s => s.Planetsystem)
                .Include(s => s.Starcluster)
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (stars == null)
            {
                return NotFound();
            }

            return View(stars);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName");
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stars stars, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile == null)
                {
                    _context.Add(stars);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Stars/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/Stars/" + fileName;
                SpaceContext spaceContext = new SpaceContext();

                var data = new Stars()
                {
                    StarId = stars.StarId,
                    ConsId = stars.ConsId,
                    GlxId = stars.GlxId,
                    StarclusterId = stars.StarclusterId,
                    PlanetsystemId = stars.PlanetsystemId,
                    StarName = stars.StarName,
                    StarApparentMagnitude = stars.StarApparentMagnitude,
                    StarStellarClass = stars.StarStellarClass,
                    StarDistance = stars.StarDistance,
                    StarDeclination = stars.StarDeclination,
                    StarImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", stars.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", stars.GlxId);
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName", stars.PlanetsystemId);
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName", stars.StarclusterId);
            return View(stars);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stars == null)
            {
                return NotFound();
            }

            var stars = await _context.Stars.FindAsync(id);
            if (stars == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", stars.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", stars.GlxId);
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName", stars.PlanetsystemId);
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName", stars.StarclusterId);
            return View(stars);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Stars stars, IFormFile? formFile)
        {
            if (id != stars.StarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(stars);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Stars/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/Stars/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new Stars()
                    {
                        StarId = stars.StarId,
                        ConsId = stars.ConsId,
                        GlxId = stars.GlxId,
                        StarclusterId = stars.StarclusterId,
                        PlanetsystemId = stars.PlanetsystemId,
                        StarName = stars.StarName,
                        StarApparentMagnitude = stars.StarApparentMagnitude,
                        StarStellarClass = stars.StarStellarClass,
                        StarDistance = stars.StarDistance,
                        StarDeclination = stars.StarDeclination,
                        StarImage = uploadedDBpath
                    };

                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StarsExists(stars.StarId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", stars.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", stars.GlxId);
            ViewData["PlanetsystemId"] = new SelectList(_context.PlanetarySystems, "PlanetsystemId", "PlanetsystemName", stars.PlanetsystemId);
            ViewData["StarclusterId"] = new SelectList(_context.StarClusters, "StarclusterId", "StarclusterName", stars.StarclusterId);
            return View(stars);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stars == null)
            {
                return NotFound();
            }

            var stars = await _context.Stars
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .Include(s => s.Planetsystem)
                .Include(s => s.Starcluster)
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (stars == null)
            {
                return NotFound();
            }

            return View(stars);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stars == null)
            {
                return Problem("Entity set 'SpaceContext.Stars'  is null.");
            }
            var stars = await _context.Stars
                .Include(s => s.Asteroids)
                .Include(s => s.Comets)
                .Include(s => s.Planets)
                .FirstOrDefaultAsync(m => m.StarId == id);
            if (stars != null)
            {
                _context.Stars.Remove(stars);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarsExists(int id)
        {
          return (_context.Stars?.Any(e => e.StarId == id)).GetValueOrDefault();
        }
    }
}
