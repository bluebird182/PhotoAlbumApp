namespace PhotoAlbumApp.Logic
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user != null && user.Password == password) // Use hashing in real apps!
                return user;

            return null;
        }
    }

}
