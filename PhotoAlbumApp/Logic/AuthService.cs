using Microsoft.AspNetCore.Identity;
using PhotoAlbumApp.Data;
using PhotoAlbumApp.Models;

namespace PhotoAlbumApp.Logic
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepo, IPasswordHasher<User> passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            var existingUser = await _userRepo.GetByUsernameAsync(username);
            if (existingUser != null)
                return false;

            var user = new User { Username = username };
            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            return await _userRepo.CreateAsync(user);
        }

    }


}
