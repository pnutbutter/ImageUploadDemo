using AS.ImageAlbum.BusinessLogic.DTO.Command;
using AS.ImageAlbum.BusinessLogic.DTO.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.Interfaces
{
    public interface IImageService
    {
        void Create(CreateImageCommand command);

        void Update(EditImageCommand command);

        void FindByID(FindByIDQuery query);
        void FindFromToTagFilter(FindFromToTagFilterQuery query);

        void FindByImageUrl(FindByImageUrlQuery query);

        void FindActiveTags(FindActiveTagsQuery query);

        void DeleteImage(DeleteImageCommand command);

    }
}
