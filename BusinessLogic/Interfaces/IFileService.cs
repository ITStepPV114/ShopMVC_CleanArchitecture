using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IFileService
    {
        //load and return imagePath file
        Task<string> SaveProductImage(IFormFile file);
        Task DeleteProductImage(string imagePath);
      //  Task<File> UploadFile(string fileName);
        
    }
}
