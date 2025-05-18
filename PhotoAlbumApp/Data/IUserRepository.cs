using PhotoAlbumApp.Models;

namespace PhotoAlbumApp.Data
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<bool> CreateAsync(User user);
    }

}
