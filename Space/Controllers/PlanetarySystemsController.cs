using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class PlanetarySystemsController : Controller
    {
        private readonly SpaceContext _context;

        public PlanetarySystemsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.PlanetarySystems.Include(p => p.Cons).Include(p => p.Glx);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsPlanetsystemNameExist(string PlanetsystemName)
        {
            return Json(!_context.PlanetarySystems.Any(p => p.PlanetsystemName == PlanetsystemName));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlanetarySystems == null)
            {
                return NotFound();
            }

            var planetarySystems = await _context.PlanetarySystems
                .Include(p => p.Cons)
                .Include(p => p.Glx)
                .FirstOrDefaultAsync(m => m.PlanetsystemId == id);
            if (planetarySystems == null)
            {
                return NotFound();
            }

            return View(planetarySystems);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanetarySystems planetarySystems, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile == null)
                {
                    _context.Add(planetarySystems);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/PlntSystem/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/PlntSystem/" + fileName;
                SpaceContext spaceContext = new SpaceContext();

                var data = new PlanetarySystems()
                {
                    PlanetsystemId = planetarySystems.PlanetsystemId,
                    ConsId = planetarySystems.ConsId,
                    GlxId = planetarySystems.GlxId,
                    PlanetsystemName = planetarySystems.PlanetsystemName,
                    PlanetsystemConfirmedPlanets = planetarySystems.PlanetsystemConfirmedPlanets,
                    PlanetsystemImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlanetarySystems == null)
            {
                return NotFound();
            }

            var planetarySystems = await _context.PlanetarySystems.FindAsync(id);
            if (planetarySystems == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanetarySystems planetarySystems, IFormFile? formFile)
        {
            if (id != planetarySystems.PlanetsystemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(planetarySystems);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/PlntSystem/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/PlntSystem/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new PlanetarySystems()
                    {
                        PlanetsystemId = planetarySystems.PlanetsystemId,
                        ConsId = planetarySystems.ConsId,
                        GlxId = planetarySystems.GlxId,
                        PlanetsystemName = planetarySystems.PlanetsystemName,
                        PlanetsystemConfirmedPlanets = planetarySystems.PlanetsystemConfirmedPlanets,
                        PlanetsystemImage = uploadedDBpath
                    };

                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetarySystemsExists(planetarySystems.PlanetsystemId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planetarySystems.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", planetarySystems.GlxId);
            return View(planetarySystems);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlanetarySystems == null)
            {
                return NotFound();
            }

            var planetarySystems = await _context.PlanetarySystems
                .Include(p => p.Cons)
                .Include(p => p.Glx)
                .FirstOrDefaultAsync(m => m.PlanetsystemId == id);
            if (planetarySystems == null)
            {
                return NotFound();
            }

            return View(planetarySystems);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlanetarySystems == null)
            {
                return Problem("Entity set 'SpaceContext.PlanetarySystems'  is null.");
            }
            var planetarySystems = await _context.PlanetarySystems
                .Include(p => p.Stars)
                .FirstOrDefaultAsync(m => m.PlanetsystemId == id);
            if (planetarySystems != null)
            {
                _context.PlanetarySystems.Remove(planetarySystems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetarySystemsExists(int id)
        {
          return (_context.PlanetarySystems?.Any(e => e.PlanetsystemId == id)).GetValueOrDefault();
        }
    }
}
