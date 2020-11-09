using System;
using System.Collections.Generic;

namespace ImageUploadDemo
{
    public partial class TblImageTag
    {
        public Guid ImageTagId { get; set; }
        public Guid ImageId { get; set; }
        public Guid TagId { get; set; }

        public virtual TblImage Image { get; set; }
        public virtual TblTag Tag { get; set; }
    }
}
