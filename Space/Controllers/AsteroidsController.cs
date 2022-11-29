using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;
namespace Space.Controllers
{
    public class AsteroidsController : Controller
    {
        private readonly SpaceContext _context;

        public AsteroidsController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.Asteroids.Include(c => c.Star);
            return View(await spaceContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asteroids == null)
            {
                return NotFound();
            }

            var asteroids = await _context.Asteroids
                .Include(a => a.Star)
                .FirstOrDefaultAsync(m => m.AstId == id);
            if (asteroids == null)
            {
                return NotFound();
            }

            return View(asteroids);
        }

        public IActionResult Create()
        {
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AstId,StarId,AstName,AstDiameter,AstOrbitalEccentricity,AstOrbitalInclination,AstArgumentOfPerihelion,AstMeanAnomaly")] Asteroids asteroids)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asteroids);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asteroids == null)
            {
                return NotFound();
            }

            var asteroids = await _context.Asteroids.FindAsync(id);
            if (asteroids == null)
            {
                return NotFound();
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AstId,StarId,AstName,AstDiameter,AstOrbitalEccentricity,AstOrbitalInclination,AstArgumentOfPerihelion,AstMeanAnomaly")] Asteroids asteroids)
        {
            if (id != asteroids.AstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asteroids);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsteroidsExists(asteroids.AstId))
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
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asteroids == null)
            {
                return NotFound();
            }

            var asteroids = await _context.Asteroids
                .Include(a => a.Star)
                .FirstOrDefaultAsync(m => m.AstId == id);
            if (asteroids == null)
            {
                return NotFound();
            }

            return View(asteroids);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asteroids == null)
            {
                return Problem("Entity set 'SpaceContext.Asteroids'  is null.");
            }
            var asteroids = await _context.Asteroids.FindAsync(id);
            if (asteroids != null)
            {
                _context.Asteroids.Remove(asteroids);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsteroidsExists(int id)
        {
          return (_context.Asteroids?.Any(e => e.AstId == id)).GetValueOrDefault();
        }
    }
}
