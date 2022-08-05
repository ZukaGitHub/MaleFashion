using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebApplicationCrud.Data.FileManager
{
    public interface IFileManager
    {
        FileStream Imagestream(string Image);
        string SaveImage(IFormFile Image);
        string SaveThumbnail(IFormFile Thumbnail);

        FileStream Thumbnailstream(string Image);

    }
}
