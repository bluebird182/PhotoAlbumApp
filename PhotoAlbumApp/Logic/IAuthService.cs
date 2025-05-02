using PhotoAlbumApp.Models;

namespace PhotoAlbumApp.Logic
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(string username, string password);
    }

}
