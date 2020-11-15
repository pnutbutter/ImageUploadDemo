using AS.ImageAlbum.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.DTO.Query
{
    public class FindByIDQuery : EventMessage
    {
        public Guid ImageId { get; set; }
        public AlbumImage Record { get; set; }
    }
}
