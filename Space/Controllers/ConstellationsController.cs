using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Space.Models;
using System.Diagnostics;

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

        public JsonResult IsConsNameExist(string ConsName)
        {
            return Json(!_context.Constellations.Any(c => c.ConsName == ConsName));
        }

        public JsonResult IsConsAbbreviationExist(string ConsAbbreviation)
        {
            return Json(!_context.Constellations.Any(c => c.ConsAbbreviation == ConsAbbreviation));
        }

        public JsonResult IsConsSymbolismExist(string ConsSymbolism)
        {
            return Json(!_context.Constellations.Any(c => c.ConsSymbolism == ConsSymbolism));
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Constellations constellations, IFormFile? formFile, string ConsName, string ConsAbbreviation, string ConsSymbolism)
        {
            if (ModelState.IsValid)
            {
                if (_context.Constellations.Any(c => c.ConsName == ConsName) || _context.Constellations.Any(c => c.ConsAbbreviation == ConsAbbreviation) || _context.Constellations.Any(c => c.ConsSymbolism == ConsSymbolism))
                {
                    return View(constellations);
                }

                if (formFile == null)
                {
                    _context.Add(constellations);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Cons/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/Cons/" + fileName;
                SpaceContext spaceContext = new SpaceContext();

                var data = new Constellations()
                {
                    ConsId = constellations.ConsId,
                    ConsName = constellations.ConsName,
                    ConsAbbreviation = constellations.ConsAbbreviation,
                    ConsSymbolism = constellations.ConsSymbolism,
                    ConsRightAscension = constellations.ConsRightAscension,
                    ConsDeclination = constellations.ConsDeclination,
                    ConsSquare = constellations.ConsSquare,
                    ConsVisibleInLatitudes = constellations.ConsVisibleInLatitudes,
                    ConsImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(constellations);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Constellations constellations, IFormFile? formFile)
        {
            if (id != constellations.ConsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(constellations);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Cons/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/Cons/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new Constellations()
                    {
                        ConsId = constellations.ConsId,
                        ConsName = constellations.ConsName,
                        ConsAbbreviation = constellations.ConsAbbreviation,
                        ConsSymbolism = constellations.ConsSymbolism,
                        ConsRightAscension = constellations.ConsRightAscension,
                        ConsDeclination = constellations.ConsDeclination,
                        ConsSquare = constellations.ConsSquare,
                        ConsVisibleInLatitudes = constellations.ConsVisibleInLatitudes,
                        ConsImage = uploadedDBpath
                    };

                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConstellationsExists(constellations.ConsId) || constellations.ConsName == null)
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

        [Authorize]
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

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Constellations == null)
            {
                return Problem("Entity set 'SpaceContext.Constellations'  is null.");
            }
            var constellations = await _context.Constellations
                .Include(c => c.BlackHoles)
                .Include(c => c.Galaxies)
                .Include(c => c.GalaxyClusters)
                .Include(c => c.GalaxyGroups)
                .Include(c => c.Nebulae)
                .Include(c => c.PlanetarySystems)
                .Include(c => c.Planets)
                .Include(c => c.StarClusters)
                .Include(c => c.Stars)
                .FirstOrDefaultAsync(e => e.ConsId == id);
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
