using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace JetWeb.API.Helpers
{
    public class Util : IUtil
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly int _uploadsize = 2097152;
        private readonly int _width = 10;
        private readonly int _height = 10;
        public Util(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public async Task<string> SaveImage(IFormFile imageFile, string dst)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @$"Resources/{dst}", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public void DeleteImage(string imageName, string dst)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @$"Resources/{dst}", imageName);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }
        }
        public bool CheckExtImage(IFormFile imageFile)
        {
            bool _ext = (imageFile.ContentType.ToLower() != "image/jpeg" &&
                        imageFile.ContentType.ToLower() != "image/jpg" &&
                        imageFile.ContentType.ToLower() != "image/png") ? true : false;

            return _ext;
        }

        public bool CheckFileLimitImage(IFormFile imageFile)
        {
            bool _status = (imageFile.Length > 0 && imageFile.Length <= _uploadsize) ? true : false;

            return _status;
        }

    }
}