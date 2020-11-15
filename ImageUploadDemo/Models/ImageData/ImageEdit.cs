using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AS.ImageAlbum.Website.Models.ImageData
{
    public class ImageEdit : ImageCreate
    {
        [Required]
        public Guid ImageId { get; set; }
        
        public string Message { get; set; }
    }
}
