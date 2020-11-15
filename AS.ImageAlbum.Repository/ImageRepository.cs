using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository.Models;
using System;
using System.Collections.Generic;

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

        public virtual void Delete(Image entityToDelete)
        {
            try
            {
                TblImage tblImage = GetDBModelByID(entityToDelete.ImageId);
                dbContext.TblImage.Remove(tblImage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(object id)
        {
            try
            {
                TblImage entity = dbContext.TblImage.Find(id);
                Delete(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Image GetByID(object id)
        {
            try
            {
                return MapToRepoModel(GetDBModelByID(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TblImage GetDBModelByID(object id)
        {
            try
            {
                return dbContext.TblImage.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Guid Insert(Image entity)
        {
            try
            {
                TblImage tblImage = new TblImage();
                MapToDBModel(entity, tblImage);
                tblImage.ImageId = Guid.NewGuid();
                dbContext.TblImage.Add(tblImage);
                this.dbContext.SaveChanges();
                return tblImage.ImageId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(Image entityToUpdate)
        {
            try
            {
                TblImage tblImage = GetDBModelByID(entityToUpdate.ImageId);
                if (tblImage.ImageAlt != entityToUpdate.ImageAlt)
                    tblImage.ImageAlt = entityToUpdate.ImageAlt;
                if (tblImage.ImageName != entityToUpdate.ImageName)
                    tblImage.ImageName = entityToUpdate.ImageName;
                if (tblImage.ImageUrl != entityToUpdate.ImageUrl)
                    tblImage.ImageUrl = entityToUpdate.ImageUrl;
                if (entityToUpdate.AlbumImage != null && entityToUpdate.AlbumImage.Length > 0)
                    tblImage.AlbumImage = entityToUpdate.AlbumImage;
                dbContext.TblImage.Update(tblImage);
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual List<Image> GetAll()
        {
            try
            {
                List<Image> imageList = new List<Image>();
                foreach (TblImage tblImage in dbContext.TblImage)
                {
                    imageList.Add(MapToRepoModel(tblImage));
                }
                return imageList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
