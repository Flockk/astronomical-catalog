using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class GalaxyGroupsController : Controller
    {
        private readonly SpaceContext _context;

        public GalaxyGroupsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.GalaxyGroups.Include(g => g.Cons);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsGlxgroupNameExist(string GlxgroupName)
        {
            return Json(!_context.GalaxyGroups.Any(g => g.GlxgroupName == GlxgroupName));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GalaxyGroups == null)
            {
                return NotFound();
            }

            var galaxyGroups = await _context.GalaxyGroups
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxgroupId == id);
            if (galaxyGroups == null)
            {
                return NotFound();
            }

            return View(galaxyGroups);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GalaxyGroups galaxyGroups, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile == null)
                {
                    _context.Add(galaxyGroups);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/GlxGroups/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/GlxGroups/" + fileName;
                SpaceContext spaceContext= new SpaceContext();

                var data = new GalaxyGroups()
                {
                    GlxgroupId= galaxyGroups.GlxgroupId,
                    ConsId = galaxyGroups.ConsId,
                    GlxgroupName = galaxyGroups.GlxgroupName,
                    GlxgroupType = galaxyGroups.GlxgroupType,
                    GlxgroupRightAscension = galaxyGroups.GlxgroupRightAscension,
                    GlxgroupDeclination = galaxyGroups.GlxgroupDeclination,
                    GlxgroupRedshift = galaxyGroups.GlxgroupRedshift,
                    GlxgroupImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GalaxyGroups == null)
            {
                return NotFound();
            }

            var galaxyGroups = await _context.GalaxyGroups.FindAsync(id);
            if (galaxyGroups == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GalaxyGroups galaxyGroups, IFormFile? formFile)
        {
            if (id != galaxyGroups.GlxgroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(galaxyGroups);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/GlxGroups/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/GlxGroups/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new GalaxyGroups()
                    {
                        GlxgroupId = galaxyGroups.GlxgroupId,
                        ConsId = galaxyGroups.ConsId,
                        GlxgroupName = galaxyGroups.GlxgroupName,
                        GlxgroupType = galaxyGroups.GlxgroupType,
                        GlxgroupRightAscension = galaxyGroups.GlxgroupRightAscension,
                        GlxgroupDeclination = galaxyGroups.GlxgroupDeclination,
                        GlxgroupRedshift = galaxyGroups.GlxgroupRedshift,
                        GlxgroupImage = uploadedDBpath
                    };
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalaxyGroupsExists(galaxyGroups.GlxgroupId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyGroups.ConsId);
            return View(galaxyGroups);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GalaxyGroups == null)
            {
                return NotFound();
            }

            var galaxyGroups = await _context.GalaxyGroups
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxgroupId == id);
            if (galaxyGroups == null)
            {
                return NotFound();
            }

            return View(galaxyGroups);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GalaxyGroups == null)
            {
                return Problem("Entity set 'SpaceContext.GalaxyGroups'  is null.");
            }
            var galaxyGroups = await _context.GalaxyGroups
                .Include(g => g.Galaxies)
                .FirstOrDefaultAsync(m => m.GlxgroupId == id);
            if (galaxyGroups != null)
            {
                _context.GalaxyGroups.Remove(galaxyGroups);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxyGroupsExists(int id)
        {
          return (_context.GalaxyGroups?.Any(e => e.GlxgroupId == id)).GetValueOrDefault();
        }
    }
}
