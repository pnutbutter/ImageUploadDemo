using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository.Models;
using System;

namespace AS.ImageAlbum.Repository
{
    public class ImageRepository : IImageRepository
    { 
        WebsiteDBContext dbContext;
        public ImageRepository()
        {
            dbContext = new WebsiteDBContext();
        }

        private Image MapToRepoModel(TblImage image)
        {
            Image repoImage = new Image();
            repoImage.AlbumImage = image.AlbumImage;
            repoImage.ImageAlt = image.ImageAlt;
            repoImage.ImageId = image.ImageId;
            repoImage.ImageName = image.ImageName;
            repoImage.ImageUrl = image.ImageUrl;

            if (image.TblImageTag != null && image.TblImageTag.Count > 0)
            {
                foreach (TblImageTag tag in image.TblImageTag)
                {
                    repoImage.ImageTags.Add(new ImageTag { ImageId = tag.ImageId, ImageTagId = tag.ImageTagId });
                }
            }
            return repoImage;
        }

        private void MapToDBModel(Image repoImage, TblImage image)
        {
            image.AlbumImage = repoImage.AlbumImage;
            image.ImageAlt = repoImage.ImageAlt;
            image.ImageId = repoImage.ImageId;
            image.ImageName = repoImage.ImageName;
            image.ImageUrl = repoImage.ImageUrl;
        }

        public void Delete(Image entityToDelete)
        {
            TblImage tblImage = GetDBModelByID(entityToDelete.ImageId);
            dbContext.TblImage.Remove(tblImage);
        }

        public void Delete(object id)
        {
            TblImage entity = dbContext.TblImage.Find(id);
            Delete(entity);
        }

        public Image GetByID(object id)
        {
            return MapToRepoModel(GetDBModelByID(id));
        }

        private TblImage GetDBModelByID(object id)
        {
            return dbContext.TblImage.Find(id);
        }

        public void Insert(Image entity)
        {
            TblImage tblImage = new TblImage();
            MapToDBModel(entity, tblImage);
            dbContext.TblImage.Add(tblImage);
        }

        public void Update(Image entityToUpdate)
        {
            TblImage tblImage = GetDBModelByID(entityToUpdate.ImageId);
            MapToDBModel(entityToUpdate, tblImage);
            dbContext.TblImage.Update(tblImage);
        }
    }
}
