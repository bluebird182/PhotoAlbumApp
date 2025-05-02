using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoAlbumApp.Models;

[Authorize]
public class PhotoController : Controller
{
    private readonly PhotoDbContext _context;
    private readonly IWebHostEnvironment _env;

    public PhotoController(PhotoDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [Authorize]
    public async Task<IActionResult> Index(string sort = "name")
    {
        var photos = sort == "date"
            ? await _context.Photos.OrderBy(p => p.UploadDate).ToListAsync()
            : await _context.Photos.OrderBy(p => p.Name).ToListAsync();

        return View(photos);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var photo = await _context.Photos.FindAsync(id);
        if (photo == null) return NotFound();
        return View(photo);
    }

    public IActionResult Upload() => View();

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, string name)
    {
        if (file == null || string.IsNullOrEmpty(name)) return View();

        var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploadsPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var photo = new Photo
        {
            Name = name,
            FilePath = "/uploads/" + fileName,
            UserId = "valaki"//User.Identity.Name
        };

        _context.Add(photo);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var photo = await _context.Photos.FindAsync(id);
        if (photo == null) return NotFound();

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
