using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.Repository
{
    public class TagRepository : IRepository<Tag>
    {
        WebsiteDBContext dbContext;
        public TagRepository()
        {
            dbContext = new WebsiteDBContext();
        }

        private Tag MapToRepoModel(TblTag tag)
        {
            Tag repoTag = new Tag();
            repoTag.Name = tag.Tag;
            repoTag.TagId = tag.TagId;
            return repoTag;
        }

        private void MapToDBModel(Tag repoTag, TblTag tag)
        {
            tag.Tag = repoTag.Name;
            tag.TagId = repoTag.TagId;
        }

        public void Delete(Tag entityToDelete)
        {
            TblTag tblImage = GetDBModelByID(entityToDelete.TagId);
            dbContext.TblTag.Remove(tblImage);
        }

        public void Delete(object id)
        {
            TblTag entity = dbContext.TblTag.Find(id);
            Delete(entity);
        }

        public Tag GetByID(object id)
        {
            return MapToRepoModel(GetDBModelByID(id));
        }

        private TblTag GetDBModelByID(object id)
        {
            return dbContext.TblTag.Find(id);
        }

        public void Insert(Tag entity)
        {
            TblTag tblImage = new TblTag();
            MapToDBModel(entity, tblImage);
            dbContext.TblTag.Add(tblImage);
        }

        public void Update(Tag entityToUpdate)
        {
            TblTag tblImage = GetDBModelByID(entityToUpdate.TagId);
            MapToDBModel(entityToUpdate, tblImage);
            dbContext.TblTag.Update(tblImage);
        }
        public List<Tag> GetAll()
        {
            List<Tag> imageList = new List<Tag>();
            foreach (TblTag tblImage in dbContext.TblTag)
            {
                imageList.Add(MapToRepoModel(tblImage));
            }
            return imageList;
        }
    }
}
