using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private const string imageFolder = "images";
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public Task DeleteProductImage(string imagePath)
        {
            
            throw new NotImplementedException();
        }

        public async Task<string> SaveProductImage(IFormFile file)
        {
            // get path to "wwwroot" for ASP.NET Core
            string root = _environment.WebRootPath;
            string newFileName=Guid.NewGuid().ToString(); // random name of file   picture1
            string extensionFile=Path.GetExtension(file.FileName); //.jpg .png....
            string fullFileName = newFileName + extensionFile;//full name file =>  picture1.png 

            // images/picture1.png    
            string imagePath = Path.Combine(imageFolder, fullFileName);
            //C:\Users\Tetiana\source\repos\ITStepPV114\ShopMVC_part2\ShopMVC\wwwroot\images\picture1.png
            string imageFullPath = Path.Combine(root, imagePath);

            //save file on the folder images

            using (FileStream fileStream = new FileStream(imageFullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return imagePath;
        }
    }
}
