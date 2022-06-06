using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using ImageConvert.Helpers;
using ImageConvert.Models;

namespace ImageConvert.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;

        public FileController(ILogger<FileController> logger, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] UploadModel uploadedFile)
        {
            try
            {
                if (uploadedFile.File != null && uploadedFile.File.Length > 0)
                {
                    string categoryDir = uploadedFile.Category;
                    string fullPath = Path.Combine(_appEnvironment.WebRootPath, categoryDir);

                    string uploadedFileName = await FileHelper.UploadFile(uploadedFile.File, fullPath);
                    string savedFilePath = Path.Combine(categoryDir, uploadedFileName);

                    return Ok(savedFilePath.Replace("\\", "/"));
                }

                return BadRequest("Для загрузки файла так то нужен файл:)");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}