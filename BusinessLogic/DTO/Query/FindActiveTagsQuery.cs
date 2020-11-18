using AS.ImageAlbum.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.DTO.Query
{
    public class FindActiveTagsQuery : EventMessage
    {
        public List<AlbumTag> Tags { get; set; }
    }
}
