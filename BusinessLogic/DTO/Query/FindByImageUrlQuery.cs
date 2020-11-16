using AS.ImageAlbum.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.DTO.Query
{
    public class FindByImageUrlQuery : EventMessage
    {
        public string ImageUrl { get; set; }
        public AlbumImage AlbumImage { get; set; }
    }
}
