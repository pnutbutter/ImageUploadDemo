using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.Repository
{
    public class ImageTagRepository : IRepository<ImageTag>
    {
        WebsiteDBContext dbContext;
        public ImageTagRepository()
        {
            dbContext = new WebsiteDBContext();
        }

        private ImageTag MapToRepoModel(TblImageTag imageTag)
        {
            ImageTag repoImageTag = new ImageTag();
            repoImageTag.ImageId = imageTag.ImageId;
            repoImageTag.ImageTagId = imageTag.ImageTagId;
            repoImageTag.TagId = imageTag.TagId;
            return repoImageTag;
        }

        private void MapToDBModel(ImageTag repoImage, TblImageTag image)
        {
            image.ImageId = repoImage.ImageId;
            image.ImageTagId = repoImage.ImageTagId;
            image.TagId = repoImage.TagId;
        }

        public void Delete(ImageTag entityToDelete)
        {
            TblImageTag tblImage = GetDBModelByID(entityToDelete.ImageTagId);
            dbContext.TblImageTag.Remove(tblImage);
        }

        public void Delete(object id)
        {
            TblImageTag entity = dbContext.TblImageTag.Find(id);
            Delete(entity);
        }

        public ImageTag GetByID(object id)
        {
            return MapToRepoModel(GetDBModelByID(id));
        }

        private TblImageTag GetDBModelByID(object id)
        {
            return dbContext.TblImageTag.Find(id);
        }

        public void Insert(ImageTag entity)
        {
            TblImageTag tblImage = new TblImageTag();
            MapToDBModel(entity, tblImage);
            dbContext.TblImageTag.Add(tblImage);
        }

        public void Update(ImageTag entityToUpdate)
        {
            TblImageTag tblImage = GetDBModelByID(entityToUpdate.ImageTagId);
            MapToDBModel(entityToUpdate, tblImage);
            dbContext.TblImageTag.Update(tblImage);
        }
    }
}
