using System;
using System.IO;
using System.Threading.Tasks;
using ImageProcessor;
using Microsoft.AspNetCore.Http;

namespace ImageConvert.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> UploadFile(IFormFile uploadedFile, string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string fileName = $"{DateTime.Now.Ticks:x}{Path.GetExtension(uploadedFile.FileName)}";

            using (var memoryStream = new MemoryStream(FormFileConvert(uploadedFile)))
            {
                using var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create);

                using ImageFactory imageFactory = new ImageFactory(preserveExifData: true);

                var filter = new GrayFilter();

                imageFactory.Load(memoryStream)
                    .Filter(filter)
                    .Resolution(300, 300)
                    .Save(fileStream);
            }

            return fileName;
        }

        public static byte[] FormFileConvert(IFormFile file)
        {
            byte[] imageData = null;

            if (file != null)
            {
                // считываем переданный файл в массив байтов
                using var binaryReader = new BinaryReader(file.OpenReadStream());
                imageData = binaryReader.ReadBytes((int)file.Length);
            }

            return imageData;
        }
    }
}