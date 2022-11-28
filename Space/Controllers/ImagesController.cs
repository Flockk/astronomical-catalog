using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Space.Models;

namespace Space.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly SpaceContext _context;

        public ImagesController(SpaceContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            webHostEnvironment = webhost;
        }

        [HttpGet]
        public IActionResult Create()
        {
            Planets planets = new Planets();
            return View(planets);
        }

        [HttpPost]
        public ActionResult Create(Planets planets)
        {
            string uniqueFileName = UploadedFile(planets, uniqueFileName);

            planets.PlntImage = uniqueFileName;

            _context.Attach(planets);
            _context.Entry(planets).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private string UploadedFile(Planets planets, string? uniqueFileName)
        {
            string? uniqueFileName = null;

            if (planets.UploadedImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + planets.UploadedImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    planets.UploadedImage.CopyTo(fileStream);
                } 
            }
            return uniqueFileName;
        }

        public IActionResult Index()
        {
            List<Planets> planets;
            planets = _context.Images.ToList();
            return View(planets);
        }
    }
}
