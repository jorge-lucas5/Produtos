using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Estudos.App.Web.Util
{
    public class FileHelper
    {
        public static async Task<string> UploadArquivo(IFormFile arquivo, IConfiguration configuration,  string imgPrefixo = null)
        {
            if (string.IsNullOrEmpty(imgPrefixo))
                imgPrefixo = GerarPrefixo();

            if (arquivo.Length <= 0) return null;

            var uploadFilePath = GetUploadFilePath(configuration);
            imgPrefixo += arquivo.FileName;

            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadFilePath, imgPrefixo);

            if (System.IO.File.Exists(path)) return null;

            using (var stream = new FileStream(path,FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return imgPrefixo;


        }

        public static string GerarPrefixo()
        {
            return Guid.NewGuid() + "_";
        }

        public static string GetUploadFilePath(IConfiguration configuration)
        {
           return configuration.GetValue("UploadFilePath", "wwwroot/uploadImagens");
        }
        public static string GetRelativeFilePath(IConfiguration configuration)
        {
            return configuration.GetValue<string>("RelativeUploadFilePath");
           
        }
    }
}