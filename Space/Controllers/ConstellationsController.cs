using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Space.Models;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsId,ConsName,ConsAbbreviation,ConsSymbolism,ConsRightAscension,ConsDeclination,ConsSquare,ConsVisibleInLatitudes,ConsImage")] Constellations constellations, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Cons/", fileName);
                var filestream = new FileStream(uploadfilepath, FileMode.Create);
                await formFile.CopyToAsync(filestream);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsId,ConsName,ConsAbbreviation,ConsSymbolism,ConsRightAscension,ConsDeclination,ConsSquare,ConsVisibleInLatitudes,ConsImage")] Constellations constellations)
        {
            if (id != constellations.ConsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(constellations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConstellationsExists(constellations.ConsId))
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
