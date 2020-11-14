using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.BusinessLogic.Interfaces;
using AS.ImageAlbum.BusinessLogic.DTO.Query;
using AS.ImageAlbum.Repository.Models;
using AS.ImageAlbum.BusinessLogic.DTO.Command;

namespace AS.ImageAlbum.BusinessLogic 
{

    public class ImageService : IImageService
    {
        protected IImageRepository repository;

        public ImageService(IImageRepository repository)
        {
            this.repository = repository;
        }

        public void Create(CreateImageCommand command)
        {
            throw new NotImplementedException();
        }

        public void FindAll(FindAllServicesQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
