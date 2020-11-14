using AS.ImageAlbum.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.DTO.Command
{
    public class CreateImageCommand : EventMessage
    {
        public AlbumImage image { get; set; }  
    }
}
