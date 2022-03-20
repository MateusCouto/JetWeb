using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JetWeb.API.Helpers
{
    public interface IUtil
    {
        Task<string> SaveImage(IFormFile imageFile, string dst);
        void DeleteImage(string imageName, string dst);
        bool CheckExtImage(IFormFile imageFile);
        bool CheckFileLimitImage(IFormFile imageFile);
    }
}