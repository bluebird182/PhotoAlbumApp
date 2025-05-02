using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }

}
