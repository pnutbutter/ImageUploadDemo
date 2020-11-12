using System;
using System.Collections.Generic;

namespace AS.ImageAlbum.DataAccess
{
    public partial class TblTag
    {
        public TblTag()
        {
            TblImageTag = new HashSet<TblImageTag>();
        }

        public Guid TagId { get; set; }
        public string Tag { get; set; }

        public virtual ICollection<TblImageTag> TblImageTag { get; set; }
    }
}
