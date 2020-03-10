using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Services
{
    public class ImageManager : IImageManager
    {
        private readonly IWebHostEnvironment _environment;

        public ImageManager(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public void Delete(string filename, string folder)
        {
            string filePath = Path.Combine(_environment.WebRootPath, "images", folder, filename);
            File.Delete(filePath);
        }

        public string Upload(IFormFile formFile, string destinationFolder)
        {
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images", destinationFolder);

            string uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            string serverFilePath = Path.Combine(uploadsFolder, uniqueFilename);
            using( var fs = new FileStream(serverFilePath, FileMode.Create) )
            {
                formFile.CopyTo(fs);
            }

            return uniqueFilename;
        }
    }
}
