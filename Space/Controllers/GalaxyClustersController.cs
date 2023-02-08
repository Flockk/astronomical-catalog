using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Space.Models;
using static Space.Controllers.ExceptionController;

namespace Space.Controllers
{
    public class GalaxyClustersController : Controller
    {
        private readonly SpaceContext _context;

        public GalaxyClustersController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.GalaxyClusters.Include(g => g.Cons);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsGlxclusterNameExist(string GlxclusterName)
        {
            return Json(!_context.GalaxyClusters.Any(g => g.GlxclusterName == GlxclusterName));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GalaxyClusters == null)
            {
                return NotFound();
            }

            var galaxyClusters = await _context.GalaxyClusters
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxclusterId == id);
            if (galaxyClusters == null)
            {
                return NotFound();
            }

            return View(galaxyClusters);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GalaxyClusters galaxyClusters, IFormFile? formFile, string GlxclusterName)
        {
            if (ModelState.IsValid)
            {
                if (_context.GalaxyClusters.Any(g => g.GlxclusterName == GlxclusterName))
                {
                    ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyClusters.ConsId);
                    return View(galaxyClusters);
                }

                if (formFile == null)
                {
                    _context.Add(galaxyClusters);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/GlxClusters/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/GlxClusters/" + fileName;
                SpaceContext spaceContext = new();

                var data = new GalaxyClusters()
                {
                    GlxclusterId = galaxyClusters.GlxclusterId,
                    ConsId = galaxyClusters.ConsId,
                    GlxclusterName = galaxyClusters.GlxclusterName,
                    GlxclusterType = galaxyClusters.GlxclusterType,
                    GlxclusterRightAscension = galaxyClusters.GlxclusterRightAscension,
                    GlxclusterDeclination = galaxyClusters.GlxclusterDeclination,
                    GlxclusterRedshift = galaxyClusters.GlxclusterRedshift,
                    GlxclusterImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GalaxyClusters == null)
            {
                return NotFound();
            }

            var galaxyClusters = await _context.GalaxyClusters.FindAsync(id);
            if (galaxyClusters == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyClusters.ConsId);
            return View(galaxyClusters);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GalaxyClusters galaxyClusters, IFormFile? formFile)
        {
            if (id != galaxyClusters.GlxclusterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(galaxyClusters);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/GlxClusters/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/GlxClusters/" + fileName;
                    SpaceContext spaceContext = new();

                    var data = new GalaxyClusters()
                    {
                        GlxclusterId = galaxyClusters.GlxclusterId,
                        ConsId = galaxyClusters.ConsId,
                        GlxclusterName = galaxyClusters.GlxclusterName,
                        GlxclusterType = galaxyClusters.GlxclusterType,
                        GlxclusterRightAscension = galaxyClusters.GlxclusterRightAscension,
                        GlxclusterDeclination = galaxyClusters.GlxclusterDeclination,
                        GlxclusterRedshift = galaxyClusters.GlxclusterRedshift,
                        GlxclusterImage = uploadedDBpath
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", galaxyClusters.ConsId);
            return View(galaxyClusters);
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
            if (id == null || _context.GalaxyClusters == null)
            {
                return NotFound();
            }

            var galaxyClusters = await _context.GalaxyClusters
                .Include(g => g.Cons)
                .FirstOrDefaultAsync(m => m.GlxclusterId == id);
            if (galaxyClusters == null)
            {
                return NotFound();
            }

            return View(galaxyClusters);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GalaxyClusters == null)
            {
                return Problem("Entity set 'SpaceContext.GalaxyClusters'  is null.");
            }
            var galaxyClusters = await _context.GalaxyClusters
                .Include(g => g.Galaxies)
                .FirstOrDefaultAsync(m => m.GlxclusterId == id);
            if (galaxyClusters != null)
            {
                _context.GalaxyClusters.Remove(galaxyClusters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalaxyClustersExists(int id)
        {
          return (_context.GalaxyClusters?.Any(e => e.GlxclusterId == id)).GetValueOrDefault();
        }
    }
}
