﻿using AS.ImageAlbum.BusinessLogic.DTO.Command;
using AS.ImageAlbum.BusinessLogic.DTO.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.Interfaces
{
    public interface IImageService
    {
        void FindAll(FindAllServicesQuery query);
        void Create(CreateImageCommand command);
    }
}