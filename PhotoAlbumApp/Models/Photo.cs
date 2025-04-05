namespace PhotoAlbumApp.Models;
using System.ComponentModel.DataAnnotations;

public class Photo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A n�v megad�sa k�telez�")]
    [MaxLength(40, ErrorMessage = "Legfeljebb 40 karakter lehet")]
    public string Name { get; set; }

    public DateTime UploadDate { get; set; } = DateTime.Now;

    public string FilePath { get; set; }

    public string UserId { get; set; }
}
