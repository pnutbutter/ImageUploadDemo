using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.Repository.Interfaces;
using System;

namespace AS.ImageAlbum.Repository
{
    public class Repository : IRepository<TblImage>
    {
        WebsiteDBContext dbContext;
        public Repository()
        {
            dbContext = new WebsiteDBContext();
        }

        public void Delete(TblImage entityToDelete)
        {
            dbContext.TblImage.Remove(entityToDelete);
        }

        public void Delete(object id)
        {
            TblImage entity = dbContext.TblImage.Find(id);
            Delete(entity);
        }

        public TblImage GetByID(object id)
        {
            return dbContext.TblImage.Find(id);
        }

        public void Insert(TblImage entity)
        {
            dbContext.Add(entity);
        }

        public void Update(TblImage entityToUpdate)
        {
            dbContext.Update(entityToUpdate);
        }
    }
}
