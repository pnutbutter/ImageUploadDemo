using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AS.ImageAlbum.Website.Models.ImageData
{
    public class ImageCreate
    {
        [Required]
        public byte[] Image { get; set; }
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
