using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoAlbumApp.Models;
public class ProfileController : Controller
{
    private readonly ProfileDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProfileController(ProfileDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }
}
