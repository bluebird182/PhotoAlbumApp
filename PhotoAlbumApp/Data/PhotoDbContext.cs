using Microsoft.EntityFrameworkCore;
using PhotoAlbumApp.Models;

public class PhotoDbContext : DbContext
{
    public PhotoDbContext(DbContextOptions<PhotoDbContext> options)
        : base(options) { }

    public DbSet<Photo> Photos { get; set; }
    public DbSet<User> Users { get; set; }
}
