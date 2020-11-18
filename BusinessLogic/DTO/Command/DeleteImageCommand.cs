using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.DTO.Command
{
    public class DeleteImageCommand : EventMessage
    {
        public Guid ImageId { get; set; }
    }
}
