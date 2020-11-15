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

        private Image Convert(AlbumImage img)
        {
            Image aImage = new Image();
            aImage.AlbumImage = img.Image;
            aImage.ImageAlt = img.ImageAlt;
            aImage.ImageId = img.ImageId;
            aImage.ImageName = img.ImageName;
            aImage.ImageUrl = img.ImageUrl;
            
            if(img.ImageTags!=null && img.ImageTags.Count>0)
            {
                aImage.ImageTags = new List<ImageTag>();
                foreach(AlbumImageTag tag in img.ImageTags)
                {
                    aImage.ImageTags.Add(new ImageTag { ImageId = img.ImageId, ImageTagId = tag.ImageTagId, TagId = tag.TagId, Name = tag.Name });
                }
            }
            return aImage;
        }

        public void Create(CreateImageCommand command)
        {
            try
            {
                command.image.ImageId=repository.Insert(Convert(command.image));
            }
            catch (Exception ex)
            {
                command.Response = String.Format(FindAllServicesQuery.ERROR, ex.Message);
                return;
            }
            command.Response = FindAllServicesQuery.SUCCESS;
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

        public void Update(EditImageCommand command)
        {
            try
            {
                repository.Update(Convert(command.image));
            }
            catch (Exception ex)
            {
                command.Response = String.Format(EditImageCommand.ERROR, ex.Message);
                return;
            }
            command.Response = EditImageCommand.SUCCESS;
        }
    }
}
