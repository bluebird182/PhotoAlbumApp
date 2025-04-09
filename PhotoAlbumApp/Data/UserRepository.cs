using PhotoAlbumApp.Models;
using System;

namespace PhotoAlbumApp.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly PhotoDbContext _context;

        public UserRepository(PhotoDbContext context)
        {
            _context = context;
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }

}
