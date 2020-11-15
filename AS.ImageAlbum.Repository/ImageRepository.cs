﻿using AS.ImageAlbum.DataAccess;
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
            TblImage tblImage = GetDBModelByID(entityToDelete.ImageId);
            dbContext.TblImage.Remove(tblImage);
        }

        public virtual void Delete(object id)
        {
            TblImage entity = dbContext.TblImage.Find(id);
            Delete(entity);
        }

        public virtual Image GetByID(object id)
        {
            return MapToRepoModel(GetDBModelByID(id));
        }

        private TblImage GetDBModelByID(object id)
        {
            return dbContext.TblImage.Find(id);
        }

        public virtual Guid Insert(Image entity)
        {
            TblImage tblImage = new TblImage();
            MapToDBModel(entity, tblImage);
            tblImage.ImageId = Guid.NewGuid();
            dbContext.TblImage.Add(tblImage);
            this.dbContext.SaveChanges();
            return tblImage.ImageId;
        }

        public virtual void Update(Image entityToUpdate)
        {
            TblImage tblImage = GetDBModelByID(entityToUpdate.ImageId);
            MapToDBModel(entityToUpdate, tblImage);
            dbContext.TblImage.Update(tblImage);
        }

        public virtual List<Image> GetAll()
        {
            List<Image> imageList = new List<Image>();
            foreach(TblImage tblImage in dbContext.TblImage)
            {
                imageList.Add(MapToRepoModel(tblImage));
            }
            return imageList;
        }
    }
}
