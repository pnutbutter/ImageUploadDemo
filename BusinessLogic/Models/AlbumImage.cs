using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.Models
{
    public class AlbumImage
    {
        public Guid ImageId { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }

        public virtual List<AlbumImageTag> ImageTags { get; set; }
    }
}
