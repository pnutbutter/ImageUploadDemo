using System;
using System.Collections.Generic;
using System.Text;
using AS.ImageAlbum.BusinessLogic.Models;

namespace AS.ImageAlbum.BusinessLogic.DTO.Query
{
    public class FindAllServicesQuery : EventMessage
    {
        public List<AlbumImage> AlbumImages { get; set; }
    }
}
