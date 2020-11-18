using System;
using System.Collections.Generic;
using System.Text;
using AS.ImageAlbum.Repository.Models;

namespace AS.ImageAlbum.Repository.Interfaces
{
    public interface IImageRepository
    {
        List<Image> GetFromTo(int start, int end, List<Guid> tagFilters, string search);
        Image GetByImageURL(string ImageUrl);
        List<Tag> GetActiveTags();
        void DeleteImage(Guid id);
        Image GetByID(Guid id);
        Guid Insert(Image entity);
        void Update(Image entityToUpdate);

    }
}
