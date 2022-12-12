using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;


namespace Space.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly SpaceContext _context;


        public PlanetsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Planets.Include(p => p.Cons).Include(p => p.Star);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsPlntNameExist(string PlntName)
        {
            return Json(!_context.Planets.Any(p => p.PlntName == PlntName));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Planets == null)
            {
                return NotFound();
            }

            var planets = await _context.Planets
                .Include(p => p.Cons)
                .Include(p => p.Star)
                .FirstOrDefaultAsync(m => m.PlntId == id);
            if (planets == null)
            {
                return NotFound();
            }

            return View(planets);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Planets planets, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                if (formFile == null)
                {
                    _context.Add(planets);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Plnt/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/Plnt/" + fileName;
                SpaceContext spaceContext = new SpaceContext();

                var data = new Planets()
                {
                    PlntId = planets.PlntId,
                    ConsId = planets.ConsId,
                    StarId = planets.StarId,
                    PlntName = planets.PlntName,
                    PlntEccentricity = planets.PlntEccentricity,
                    PlntSemiMajorAxis = planets.PlntSemiMajorAxis,
                    PlntArgumentOfPerihelion = planets.PlntArgumentOfPerihelion,
                    PlntMass = planets.PlntMass,
                    PlntImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planets.ConsId);
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", planets.StarId);
            return View(planets);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Planets == null)
            {
                return NotFound();
            }

            var planets = await _context.Planets.FindAsync(id);
            if (planets == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planets.ConsId);
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", planets.StarId);
            return View(planets);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Planets planets, IFormFile? formFile)
        {
            if (id != planets.PlntId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(planets);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Plnt/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/Plnt/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new Planets()
                    {
                        PlntId = planets.PlntId,
                        ConsId = planets.ConsId,
                        StarId = planets.StarId,
                        PlntName = planets.PlntName,
                        PlntEccentricity = planets.PlntEccentricity,
                        PlntSemiMajorAxis = planets.PlntSemiMajorAxis,
                        PlntArgumentOfPerihelion = planets.PlntArgumentOfPerihelion,
                        PlntMass = planets.PlntMass,
                        PlntImage = uploadedDBpath
                    };

                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetsExists(planets.PlntId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", planets.ConsId);
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", planets.StarId);
            return View(planets);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Planets == null)
            {
                return NotFound();
            }

            var planets = await _context.Planets
                .Include(p => p.Cons)
                .Include(p => p.Star)
                .FirstOrDefaultAsync(m => m.PlntId == id);
            if (planets == null)
            {
                return NotFound();
            }

            return View(planets);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Planets == null)
            {
                return Problem("Entity set 'SpaceContext.Planets'  is null.");
            }
            var planets = await _context.Planets.FindAsync(id);
            if (planets != null)
            {
                _context.Planets.Remove(planets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetsExists(int id)
        {
          return (_context.Planets?.Any(e => e.PlntId == id)).GetValueOrDefault();
        }
    }
}
