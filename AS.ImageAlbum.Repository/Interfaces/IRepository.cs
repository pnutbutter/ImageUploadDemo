using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        TEntity GetByID(object id);
        Guid Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        List<TEntity> GetAll();
    }
}
