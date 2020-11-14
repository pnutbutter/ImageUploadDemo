using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.Models
{
    public class AlbumImageTag
    {
        public Guid ImageTagId { get; set; }
        public Guid ImageId { get; set; }
        public Guid TagId { get; set; }
        public string Name { get; set; }
    }
}
