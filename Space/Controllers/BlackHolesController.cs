using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Space.Models;
using static Space.Controllers.ExceptionController;

namespace Space.Controllers
{
    public class BlackHolesController : Controller
    {
        private readonly SpaceContext _context;

        public BlackHolesController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.BlackHoles.Include(b => b.Cons).Include(b => b.Glx);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsBlackHoleNameExist(string BlackholeName)
        {
            return Json(!_context.BlackHoles.Any(b => b.BlackholeName == BlackholeName));
        }
         
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BlackHoles == null)
            {
                return NotFound();
            }

            var blackHoles = await _context.BlackHoles
                .Include(b => b.Cons)
                .Include(b => b.Glx)
                .FirstOrDefaultAsync(m => m.BlackHoleId == id);
            if (blackHoles == null)
            {
                return NotFound();
            }

            return View(blackHoles);
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
        public async Task<IActionResult> Create(BlackHoles blackHoles, IFormFile? formFile, string BlackholeName)
        {
            if (ModelState.IsValid)
            {
                if (_context.BlackHoles.Any(a => a.BlackholeName == BlackholeName))
                {
                    ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
                    ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
                    return View(blackHoles);
                }
                if (formFile == null)
                {
                    _context.Add(blackHoles);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/BlackHoles/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/BlackHoles/" + fileName;

                var data = new BlackHoles()
                {
                    BlackHoleId = blackHoles.BlackHoleId,
                    ConsId = blackHoles.ConsId,
                    GlxId = blackHoles.GlxId,
                    BlackholeName = blackHoles.BlackholeName,
                    BlackholeType = blackHoles.BlackholeType,
                    BlackholeRightAscension = blackHoles.BlackholeRightAscension,
                    BlackholeDeclination = blackHoles.BlackholeDeclination,
                    BlackholeDistance = blackHoles.BlackholeDistance,
                    BlackholeImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BlackHoles == null)
            {
                return NotFound();
            }

            var blackHoles = await _context.BlackHoles.FindAsync(id);
            if (blackHoles == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlackHoles blackHoles, IFormFile? formFile, string BlackholeName)
        {
            if (id != blackHoles.BlackHoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(blackHoles);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/BlackHoles/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/BlackHoles/" + fileName;

                    var data = new BlackHoles()
                    {
                        BlackHoleId = blackHoles.BlackHoleId,
                        ConsId = blackHoles.ConsId,
                        GlxId = blackHoles.GlxId,
                        BlackholeName = blackHoles.BlackholeName,
                        BlackholeType = blackHoles.BlackholeType,
                        BlackholeRightAscension = blackHoles.BlackholeRightAscension,
                        BlackholeDeclination = blackHoles.BlackholeDeclination,
                        BlackholeDistance = blackHoles.BlackholeDistance,
                        BlackholeImage = uploadedDBpath
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
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
            if (id == null || _context.BlackHoles == null)
            {
                return NotFound();
            }

            var blackHoles = await _context.BlackHoles
                .Include(b => b.Cons)
                .Include(b => b.Glx)
                .FirstOrDefaultAsync(m => m.BlackHoleId == id);
            if (blackHoles == null)
            {
                return NotFound();
            }

            return View(blackHoles);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BlackHoles == null)
            {
                return Problem("Entity set 'SpaceContext.BlackHoles'  is null.");
            }
            var blackHoles = await _context.BlackHoles.FindAsync(id);
            if (blackHoles != null)
            {
                _context.BlackHoles.Remove(blackHoles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlackHolesExists(int id)
        {
          return (_context.BlackHoles?.Any(e => e.BlackHoleId == id)).GetValueOrDefault();
        }
    }
}