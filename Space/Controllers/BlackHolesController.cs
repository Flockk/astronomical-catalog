using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space.Models;

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
                    return View(blackHoles);
                }
                else
                {
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
        public async Task<IActionResult> Edit(int id, BlackHoles blackHoles, IFormFile? formFile)
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
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlackHolesExists(blackHoles.BlackHoleId))
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", blackHoles.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", blackHoles.GlxId);
            return View(blackHoles);
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