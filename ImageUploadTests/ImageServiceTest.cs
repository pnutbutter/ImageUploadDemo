using AS.ImageAlbum.BusinessLogic.DTO.Query;
using AS.ImageAlbum.Repository;
using AS.ImageAlbum.Repository.Interfaces;
using AS.ImageAlbum.Repository.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using Assert = NUnit.Framework.Assert;

namespace AS.ImageAlbum.BusinessLogic.Tests
{
    [TestClass]
    public class ImageServiceTest
    {
        [TestMethod]
        public void FindAllImages_ReturnsSuccessAndItems()
        {
            Mock<ImageRepository> mockImageRepository = new Mock<ImageRepository>();
            List<Image> imageList = new List<Image>();
            for(int i = 0; i<5; i++)
            {
                imageList.Add(createMockImage(i));
            }
            mockImageRepository.Setup(x => x.GetAll()).Returns(imageList);
            FindAllServicesQuery query = new FindAllServicesQuery();
            ImageService imageService = new ImageService(mockImageRepository.Object);
            imageService.FindAll(query);
            Assert.That(query.Response, Is.EqualTo(FindAllServicesQuery.SUCCESS));
            Assert.That(query.AlbumImages.Count, Is.EqualTo(5));
        }

        private Image createMockImage(int variable)
        {
            string varToSting = variable.ToString();
            Guid guid = new Guid(variable, 0, 0, new byte[8]);
            List<Tag> tagList = new List<Tag>();
            tagList.Add(new Tag { Name = varToSting, TagId = guid });
            return new Image { AlbumImage = new byte[Convert.ToByte(variable)], ImageAlt = varToSting, ImageName = varToSting, ImageUrl = varToSting, ImageId = guid, };
        }
    }
}
