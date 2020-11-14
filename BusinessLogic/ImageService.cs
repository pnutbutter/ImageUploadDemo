using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.BusinessLogic.Interfaces;
using AS.ImageAlbum.BusinessLogic.DTO.Query;

namespace AS.ImageAlbum.BusinessLogic 
{

    public class ImageService : IImageService
    {
        protected IRepository<TblImage> repository = new ImageRepository();

        public ImageService(IRepository<TblImage> repository)
        {

        }

        public void FindAll(FindAllServicesQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
