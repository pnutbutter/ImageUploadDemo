using AS.ImageAlbum.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.DTO.Query
{
    public class FindFromToTagFilterQuery : EventMessage
    {
        public int BeginIndex { get; set; }
        public int EndIndex { get; set; }
        public List<Guid> TagFilters { get; set; }
        public List<AlbumImage> AlbumImages { get; set; }
    }
}
