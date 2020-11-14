using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.Repository.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public byte[] AlbumImage { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }

        public virtual List<ImageTag> ImageTags { get; set; }
    }
}
