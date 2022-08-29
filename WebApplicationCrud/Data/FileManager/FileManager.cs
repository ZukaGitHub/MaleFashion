using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplicationCrud.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private string _imagePath;
        private string _ThumbnailPath;

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
            _ThumbnailPath = config["Path:Thumbnails"];

        }

        public FileStream Imagestream(string Image)
        {
            return new FileStream(Path.Combine(_imagePath, Image), FileMode.Open, FileAccess.Read);
        }
        public bool RemoveImage(string image)
        {
            try
            {
                var file = Path.Combine(_imagePath, image);
                if (File.Exists(file))
                    File.Delete(file);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<string> SaveImageAsync(IFormFile Image)
        {
            try
            {
                var save_path = Path.Combine(_imagePath);
                if (!Directory.Exists(save_path))
                {
                    Directory.CreateDirectory(save_path);
                }
                var mime = Image.FileName.Substring(Image.FileName.LastIndexOf('.'));
                var filename = $"img_{DateTime.Now.ToString("dd-mm-yy-hh-mm-ss")}{mime}";
                using (var filestream = new FileStream(Path.Combine(save_path, filename), FileMode.Create))
                {
                   await Image.CopyToAsync(filestream);
                }
                return filename;

            }
            catch (Exception)
            {
                return "error";
            }
        }

        public FileStream Thumbnailstream(string Thumbnails)
        {
            return new FileStream(Path.Combine(_ThumbnailPath, Thumbnails), FileMode.Open, FileAccess.Read);
        }

        public string SaveThumbnail(IFormFile Thumbnail)
        {
            try
            {
                var save_path = Path.Combine(_ThumbnailPath);
                if (!Directory.Exists(save_path))
                {
                    Directory.CreateDirectory(save_path);
                }
                string Thumbnailname;



                var mime = Thumbnail.FileName.Substring(Thumbnail.FileName.LastIndexOf('.'));
                var filename = $"img_{DateTime.Now.ToString("dd-mm-yy-hh-mm-ss")}{mime}";
                using (var filestream = new FileStream(Path.Combine(save_path, filename), FileMode.Create))
                {
                    Thumbnail.CopyToAsync(filestream);
                }
                Thumbnailname = filename;




                return Thumbnailname;



            }
            catch (Exception e)
            {

                string error;
                error = e.Message;
                return error;
            }
        }
    }

}

