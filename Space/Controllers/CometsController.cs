using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class CometsController : Controller
    {
        private readonly SpaceContext _context;

        public CometsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Comets.Include(c => c.Star);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsCometNameExist(string CometName)
        {
            return Json(!_context.Comets.Any(c => c.CometName == CometName));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comets == null)
            {
                return NotFound();
            }

            var comets = await _context.Comets
                .Include(c => c.Star)
                .FirstOrDefaultAsync(m => m.CometId == id);
            if (comets == null)
            {
                return NotFound();
            }

            return View(comets);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comets comets, IFormFile? formFile, string CometName)
        {
            if (ModelState.IsValid)
            {
                if (_context.Comets.Any(c => c.CometName == CometName))
                {
                    return View(comets);
                }
                else
                {
                    if (formFile == null)
                    {
                        _context.Add(comets);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Comets/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/Comets/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new Comets()
                    {
                        CometId = comets.CometId,
                        StarId = comets.StarId,
                        CometName = comets.CometName,
                        CometOrbitalPeriod = comets.CometOrbitalPeriod,
                        CometSemiMajorAxis = comets.CometSemiMajorAxis,
                        CometPerihelion = comets.CometPerihelion,
                        CometEccentricity = comets.CometEccentricity,
                        CometOrbitalInclination = comets.CometOrbitalInclination,
                        CometImage = uploadedDBpath
                    };

                    _context.Add(data);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", comets.StarId);
            return View(comets);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comets == null)
            {
                return NotFound();
            }

            var comets = await _context.Comets.FindAsync(id);
            if (comets == null)
            {
                return NotFound();
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", comets.StarId);
            return View(comets);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Comets comets, IFormFile? formFile)
        {
            if (id != comets.CometId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(comets);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Comets/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/Comets/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new Comets()
                    {
                        CometId = comets.CometId,
                        StarId = comets.StarId,
                        CometName = comets.CometName,
                        CometOrbitalPeriod = comets.CometOrbitalPeriod,
                        CometSemiMajorAxis = comets.CometSemiMajorAxis,
                        CometPerihelion = comets.CometPerihelion,
                        CometEccentricity = comets.CometEccentricity,
                        CometOrbitalInclination = comets.CometOrbitalInclination,
                        CometImage = uploadedDBpath
                    };

                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CometsExists(comets.CometId))
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
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", comets.StarId);
            return View(comets);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comets == null)
            {
                return NotFound();
            }

            var comets = await _context.Comets
                .Include(c => c.Star)
                .FirstOrDefaultAsync(m => m.CometId == id);
            if (comets == null)
            {
                return NotFound();
            }

            return View(comets);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comets == null)
            {
                return Problem("Entity set 'SpaceContext.Comets'  is null.");
            }
            var comets = await _context.Comets.FindAsync(id);
            if (comets != null)
            {
                _context.Comets.Remove(comets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CometsExists(int id)
        {
          return (_context.Comets?.Any(e => e.CometId == id)).GetValueOrDefault();
        }
    }
}
