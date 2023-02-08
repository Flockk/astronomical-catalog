using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Space.Models;
using static Space.Controllers.ExceptionController;

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
         
        public JsonResult IsAstNameExist(string AstName)
        {
            return Json(!_context.Asteroids.Any(a => a.AstName == AstName));
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

        [Authorize]
        public IActionResult Create()
        {
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asteroids asteroids, IFormFile? formFile, string AstName)
        {
            if (ModelState.IsValid)
            {
                if (_context.Asteroids.Any(a => a.AstName == AstName))
                {
                    ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
                    return View(asteroids);
                }

                if (formFile == null)
                {
                    _context.Add(asteroids);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Ast/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/Ast/" + fileName;
                SpaceContext spaceContext = new();

                var data = new Asteroids()
                {
                    AstId = asteroids.AstId,
                    StarId = asteroids.StarId,
                    AstName = asteroids.AstName,
                    AstDiameter = asteroids.AstDiameter,
                    AstOrbitalEccentricity = asteroids.AstOrbitalEccentricity,
                    AstOrbitalInclination = asteroids.AstOrbitalInclination,
                    AstArgumentOfPerihelion = asteroids.AstArgumentOfPerihelion,
                    AstMeanAnomaly = asteroids.AstMeanAnomaly,
                    AstImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id, string? AstName)
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Asteroids asteroids, IFormFile? formFile, string AstName)
        {
            if (id != asteroids.AstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (formFile == null)
                    {
                        _context.Update(asteroids);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Ast/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/Ast/" + fileName;
                    SpaceContext spaceContext = new();

                    var data = new Asteroids()
                    {
                        AstId = asteroids.AstId,
                        StarId = asteroids.StarId,
                        AstName = asteroids.AstName,
                        AstDiameter = asteroids.AstDiameter,
                        AstOrbitalEccentricity = asteroids.AstOrbitalEccentricity,
                        AstOrbitalInclination = asteroids.AstOrbitalInclination,
                        AstArgumentOfPerihelion = asteroids.AstArgumentOfPerihelion,
                        AstMeanAnomaly = asteroids.AstMeanAnomaly,
                        AstImage = uploadedDBpath
                    };

                    _context.Update(data);
                    await _context.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StarId"] = new SelectList(_context.Stars, "StarId", "StarName", asteroids.StarId);
            return View(asteroids);
        }

        public virtual void HandleException(Exception exception)
        {
            if (exception is DbUpdateConcurrencyException concurrencyEx)
            {
                
                throw new ConcurrencyException();
            }
            else if (exception is DbUpdateException dbUpdateEx)
            {
                if (dbUpdateEx.InnerException != null
                        && dbUpdateEx.InnerException.InnerException != null)
                {
                    if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  
                            case 547:   
                            case 2601:  
                                                                  
                                throw new ConcurrencyException();
                            default:      
                                throw new DatabaseAccessException(
                                  dbUpdateEx.Message, dbUpdateEx.InnerException);
                        }
                    }

                    throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                }
            }   
        }

        [Authorize]
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

        [Authorize]
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