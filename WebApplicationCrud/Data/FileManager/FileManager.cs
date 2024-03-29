﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebApplicationCrud.Data.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _imagePath;
        
       

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
          
          
            
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
        public bool RemoveImages(List<string> images)
        {
            
            try
            {
                foreach (var image in images)
                {


                    var file = Path.Combine(_imagePath, image);
                    if (File.Exists(file))
                        File.Delete(file);
                   
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<string> SaveImageAsync(IFormFile Image,string path)
        {
            path = _imagePath;
            try
            {
                var save_path = Path.Combine(path);
                if (!Directory.Exists(save_path))
                {
                    Directory.CreateDirectory(save_path);
                }
                var mime = Image.FileName.Substring(Image.FileName.LastIndexOf('.'));
                var filename = $"img_{DateTime.Now:dd-mm-yy-hh-mm-ss}{mime}";
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
        public async Task<List<string>> SaveImagesAsync(List<IFormFile> Images, string path)
        {
            path = _imagePath;
            var filenames = new List<string>();
            try
            {
                for (int i = 0; i < Images.Count; i++)
                {


                    var save_path = Path.Combine(path);
                    if (!Directory.Exists(save_path))
                    {
                        Directory.CreateDirectory(save_path);
                    }
                
                    var filename = $"img_{DateTime.Now:dd-mm-yy-hh-mm-ss}{"_" + i}{"_"+Images[i].FileName}";
                    using (var filestream = new FileStream(Path.Combine(save_path, filename), FileMode.Create))
                    {
                        await Images[i].CopyToAsync(filestream);
                    }
                    filenames.Add(filename);
                   
                }
                return filenames;
            }
            catch (Exception e)
            {
                var error = new List<string>
                {
                    e.Message
                };
                return error;
            }
        }
        public string DeleteImages(List<string> Imagenames)
        {
            try
            {
                foreach (var Image in Imagenames)
                {
                    var Image_path = Path.Combine(_imagePath);
                    var ImageToBeDeleted = Path.Combine(Image_path, Image);
                    if ((System.IO.File.Exists(ImageToBeDeleted)))
                    {
                        System.IO.File.Delete(ImageToBeDeleted);

                    }


                }
                return "success";
            }
            catch (Exception)
            {
                return "error";
            }

        }

        public string SaveThumbnail(IFormFile Thumbnail, string path)
        {
            throw new NotImplementedException();
        }

        public FileStream Thumbnailstream(string Image)
        {
            throw new NotImplementedException();
        }

        //public FileStream Thumbnailstream(string Thumbnails)
        //{
        //    return new FileStream(Path.Combine(_ThumbnailPath, Thumbnails), FileMode.Open, FileAccess.Read);
        //}

        //public string SaveThumbnail(IFormFile Thumbnail,string path)
        //{
        //    try
        //    {
        //        var save_path = Path.Combine(_ThumbnailPath);
        //        if (!Directory.Exists(save_path))
        //        {
        //            Directory.CreateDirectory(save_path);
        //        }
        //        string Thumbnailname;



        //        var mime = Thumbnail.FileName.Substring(Thumbnail.FileName.LastIndexOf('.'));
        //        var filename = $"img_{DateTime.Now.ToString("dd-mm-yy-hh-mm-ss")}{mime}";
        //        using (var filestream = new FileStream(Path.Combine(save_path, filename), FileMode.Create))
        //        {
        //            Thumbnail.CopyToAsync(filestream);
        //        }
        //        Thumbnailname = filename;




        //        return Thumbnailname;



        //    }
        //    catch (Exception e)
        //    {

        //        string error;
        //        error = e.Message;
        //        return error;
        //    }
        //}
    }

}

