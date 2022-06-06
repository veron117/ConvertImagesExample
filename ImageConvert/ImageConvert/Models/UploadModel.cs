using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ImageConvert.Models
{
    public class UploadModel
    {
        [Required]
        public string Category { get; set; }

        public IFormFile File { get; set; }
    }
}