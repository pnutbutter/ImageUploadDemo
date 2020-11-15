using AS.ImageAlbum.DataAccess;
using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public virtual Image GetByID(Guid id)
        {
            try
            {
                //populate image fields
                Image img = (from it in dbContext.TblImage
                                                       where id == it.ImageId
                                                       select new Image()
                                                       {
                                                           ImageId = it.ImageId,
                                                           AlbumImage = it.AlbumImage,
                                                           ImageAlt = it.ImageAlt,
                                                           ImageName = it.ImageName,
                                                           ImageUrl = it.ImageUrl
                                                       }).SingleOrDefault();
                if (img == null || img.ImageId != id)
                    throw new ArgumentException("Image not found");

                //Populate image tags
                img.ImageTags = (from it in dbContext.TblImageTag
                                                   join t in dbContext.TblTag on it.TagId equals t.TagId
                                                   where id == it.ImageId
                                                   select new ImageTag()
                                                   {
                                                       ImageId = it.ImageId,
                                                       ImageTagId = it.ImageTagId,
                                                       TagId = it.TagId,
                                                       Name = t.Tag
                                                   }).ToList();

                return img;
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

                AddRemoveTags(tblImage.ImageId, entity.ImageTags);

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

                //only update fields touched on the Image Table
                if (tblImage.ImageAlt != entityToUpdate.ImageAlt)
                    tblImage.ImageAlt = entityToUpdate.ImageAlt;
                if (tblImage.ImageName != entityToUpdate.ImageName)
                    tblImage.ImageName = entityToUpdate.ImageName;
                if (tblImage.ImageUrl != entityToUpdate.ImageUrl)
                    tblImage.ImageUrl = entityToUpdate.ImageUrl;
                if (entityToUpdate.AlbumImage != null && entityToUpdate.AlbumImage.Length > 0)
                    tblImage.AlbumImage = entityToUpdate.AlbumImage;
                dbContext.TblImage.Update(tblImage);

                AddRemoveTags(entityToUpdate.ImageId, entityToUpdate.ImageTags);

                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddRemoveTags(Guid imageID, List<ImageTag> imageTags)
        {
            //only find if tags exist for the current list being sent back
            string[] tagNames = imageTags.Select(t => t.Name).ToArray();

            List<TblTag> existingTagsFromList = (from t in dbContext.TblTag
                                                where tagNames.Contains(t.Tag)
                                                select t).ToList();

            //get all the tags currently tied to the image to see what has been added or removed
            List<ImageTag> currentImageTags = (from it in dbContext.TblImageTag
                                               join t in dbContext.TblTag on it.TagId equals t.TagId
                                               where imageID == it.ImageId
                                               select new ImageTag()
                                               {
                                                   ImageId = it.ImageId,
                                                   ImageTagId = it.ImageTagId,
                                                   TagId = it.TagId,
                                                   Name = t.Tag
                                               }).ToList();

            //remove tags no longer there
            List<ImageTag> deletedTags = currentImageTags.FindAll(ct => !existingTagsFromList.Select(et => et.Tag).ToArray().Contains(ct.Name));
            foreach(ImageTag tag in deletedTags)
            {
                //remove from list of tags to insert in next loop
                imageTags.Remove(tag);
                //remove ImagetTag record connecting image to tag
                TblImageTag imgTag = new TblImageTag() { ImageTagId=tag.ImageTagId };
                dbContext.TblImageTag.Attach(imgTag);
                dbContext.TblImageTag.Remove(imgTag);
            }

            foreach (ImageTag tag in imageTags)
            {
                //if tag already is related to image skip
                if (currentImageTags.Find(ct => ct.Name == tag.Name) != null && !string.IsNullOrWhiteSpace(currentImageTags.Find(ct => ct.Name == tag.Name).Name))
                    continue;

                //Tag exists but imagetag does not exist - just create image tag record
                if (existingTagsFromList.Find(et => et.Tag==tag.Name)!=null && !string.IsNullOrWhiteSpace(existingTagsFromList.Find(et => et.Tag == tag.Name).Tag))
                {
                    //create image tag record
                    dbContext.TblImageTag.Add(new TblImageTag { ImageId = imageID, TagId = existingTagsFromList.Find(et => et.Tag == tag.Name).TagId, ImageTagId = Guid.NewGuid() });
                }
                else
                {
                    //if tag doesn't exist create tag and image tag record
                    //create tag
                    Guid newTagGuid = Guid.NewGuid();
                    dbContext.TblTag.Add(new TblTag { Tag = tag.Name, TagId = newTagGuid });

                    //create image tag
                    dbContext.TblImageTag.Add(new TblImageTag { ImageId = imageID, TagId = newTagGuid, ImageTagId = Guid.NewGuid() });
                }
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
