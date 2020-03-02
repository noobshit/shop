using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public interface IImageManager
    {
        string Upload(IFormFile formFile, string destinationDirectory = "");

        void Delete(string filename, string folder = "");
    }
}