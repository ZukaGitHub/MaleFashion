using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WebApplicationCrud.Data.FileManager
{
    public interface IFileManager
    {
        FileStream Imagestream(string Image);
        Task<string> SaveImageAsync(IFormFile Image,string path);
        string SaveThumbnail(IFormFile Thumbnail,string path);

        FileStream Thumbnailstream(string Image);
        bool RemoveImage(string image);
    }
}
