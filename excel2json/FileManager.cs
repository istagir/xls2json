﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace excel2json
{
    public class FileManager
    {
        private IWebHostEnvironment _hostingEnvironment;
        private string filePath;

        public FileManager(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
            filePath = "";
        }

        public string Upload(IFormFile file)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            var filePath = Path.Combine(uploads, file.FileName);

            this.filePath = filePath;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                 file.CopyTo(fileStream);
            }
            
            return filePath;
        }

        public void Delete()
        {
            if (filePath != "")
            {
                File.Delete(filePath);
            }
        }

        public string GetFilePath()
        {
            return filePath;
        }

    }
}
