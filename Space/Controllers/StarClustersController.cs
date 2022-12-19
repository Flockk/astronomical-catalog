using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Space.Models;
using static Space.Controllers.ExceptionController;

namespace Space.Controllers
{
    public class StarClustersController : Controller
    {
        private readonly SpaceContext _context;

        public StarClustersController(SpaceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var spaceContext = _context.StarClusters.Include(s => s.Cons).Include(s => s.Glx);
            return View(await spaceContext.ToListAsync());
        }

        public JsonResult IsStarclusterNameExist(string StarclusterName)
        {
            return Json(!_context.StarClusters.Any(s => s.StarclusterName == StarclusterName));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .FirstOrDefaultAsync(m => m.StarclusterId == id);
            if (starClusters == null)
            {
                return NotFound();
            }

            return View(starClusters);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName");
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StarClusters starClusters, IFormFile? formFile, string StarclusterName)
        {
            if (ModelState.IsValid)
            {
                if (_context.StarClusters.Any(s => s.StarclusterName == StarclusterName))
                {
                    ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", starClusters.ConsId);
                    ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
                    return View(starClusters);
                }

                if (formFile == null)
                {
                    _context.Add(starClusters);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                string fileName = Path.GetFileName(formFile.FileName);
                string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/StarClusters/", fileName);
                using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

                string uploadedDBpath = "/Img/StarClusters/" + fileName;
                SpaceContext spaceContext = new SpaceContext();

                var data = new StarClusters()
                {
                    StarclusterId = starClusters.StarclusterId,
                    ConsId = starClusters.ConsId,
                    GlxId = starClusters.GlxId,
                    StarclusterName = starClusters.StarclusterName,
                    StarclusterType = starClusters.StarclusterType,
                    StarclusterRightAscension = starClusters.StarclusterRightAscension,
                    StarclusterDeclination = starClusters.StarclusterDeclination,
                    StarclusterDistance = starClusters.StarclusterDistance,
                    StarclusterAge = starClusters.StarclusterAge,
                    StarclusterDiameter = starClusters.StarclusterDiameter,
                    StarclusterImage = uploadedDBpath
                };

                _context.Add(data);
                await _context.SaveChangesAsync();
                ViewBag.Image = uploadedDBpath;
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters.FindAsync(id);
            if (starClusters == null)
            {
                return NotFound();
            }
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StarClusters starClusters, IFormFile? formFile)
        {
            if (id != starClusters.StarclusterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (formFile == null)
                    {
                        _context.Update(starClusters);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadfilepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/StarClusters/", fileName);
                    using (var filestream = new FileStream(uploadfilepath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(filestream);
                    }

                    string uploadedDBpath = "/Img/StarClusters/" + fileName;
                    SpaceContext spaceContext = new SpaceContext();

                    var data = new StarClusters()
                    {
                        StarclusterId = starClusters.StarclusterId,
                        ConsId = starClusters.ConsId,
                        GlxId = starClusters.GlxId,
                        StarclusterName = starClusters.StarclusterName,
                        StarclusterType = starClusters.StarclusterType,
                        StarclusterRightAscension = starClusters.StarclusterRightAscension,
                        StarclusterDeclination = starClusters.StarclusterDeclination,
                        StarclusterDistance = starClusters.StarclusterDistance,
                        StarclusterAge = starClusters.StarclusterAge,
                        StarclusterDiameter = starClusters.StarclusterDiameter,
                        StarclusterImage = uploadedDBpath
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
            ViewData["ConsId"] = new SelectList(_context.Constellations, "ConsId", "ConsName", starClusters.ConsId);
            ViewData["GlxId"] = new SelectList(_context.Galaxies, "GlxId", "GlxName", starClusters.GlxId);
            return View(starClusters);
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
            if (id == null || _context.StarClusters == null)
            {
                return NotFound();
            }

            var starClusters = await _context.StarClusters
                .Include(s => s.Cons)
                .Include(s => s.Glx)
                .FirstOrDefaultAsync(m => m.StarclusterId == id);
            if (starClusters == null)
            {
                return NotFound();
            }

            return View(starClusters);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StarClusters == null)
            {
                return Problem("Entity set 'SpaceContext.StarClusters'  is null.");
            }
            var starClusters = await _context.StarClusters
                .Include(s => s.Stars)
                .FirstOrDefaultAsync(m => m.StarclusterId == id);
            if (starClusters != null)
            {
                _context.StarClusters.Remove(starClusters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StarClustersExists(int id)
        {
          return (_context.StarClusters?.Any(e => e.StarclusterId == id)).GetValueOrDefault();
        }
    }
}
