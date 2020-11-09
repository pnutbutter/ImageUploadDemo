using System;
using System.Collections.Generic;

namespace ImageUploadDemo
{
    public partial class TblImage
    {
        public TblImage()
        {
            TblImageTag = new HashSet<TblImageTag>();
        }

        public Guid ImageId { get; set; }
        public byte[] AlbumImage { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }

        public virtual ICollection<TblImageTag> TblImageTag { get; set; }
    }
}
