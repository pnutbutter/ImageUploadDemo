using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AS.ImageAlbum.Website.Models.ImageData
{
    public class ImageEdit
    {
        [Required]
        public Guid ImageId { get; set; }

        public string ImageDisplay { get; set; }
        
        public string Message { get; set; }

        public IFormFile ImageFile { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string ImageName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Maximum 500 characters")]
        public string ImageAlt { get; set; }
    }
}
