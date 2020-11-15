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
using AS.ImageAlbum.BusinessLogic.Models;

namespace AS.ImageAlbum.BusinessLogic 
{

    public class ImageService : IImageService
    {
        protected IImageRepository repository;

        public ImageService(IImageRepository repository)
        {
            this.repository = repository;
        }

        private AlbumImage Convert(Image img)
        {
            AlbumImage aImage = new AlbumImage();
            aImage.Image = img.AlbumImage;
            aImage.ImageAlt = img.ImageAlt;
            aImage.ImageId = img.ImageId;
            aImage.ImageName = img.ImageName;
            aImage.ImageUrl = img.ImageUrl;
            return aImage;
        }

        public void Create(CreateImageCommand command)
        {
            throw new NotImplementedException();
        }

        public void FindAll(FindAllServicesQuery query)
        {
            query.AlbumImages = new List<AlbumImage>();
            try
            {
                foreach (Image img in repository.GetAll())
                {
                    query.AlbumImages.Add(Convert(img));
                }
            }
            catch (Exception ex)
            {
                query.Response = String.Format(FindAllServicesQuery.ERROR, ex.Message);
                return;
            }
            query.Response = FindAllServicesQuery.SUCCESS;

        }
    }
}
