using AS.ImageAlbum.BusinessLogic.DTO.Command;
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
            FindAllServicesQuery query = new FindAllServicesQuery();
            List<Image> imageList = new List<Image>();
            for(int i = 0; i<5; i++)
            {
                imageList.Add(createMockImage(i));
            }
            mockImageRepository.Setup(x => x.GetAll()).Returns(imageList);
            ImageService imageService = new ImageService(mockImageRepository.Object);
            imageService.FindAll(query);
            Assert.That(query.Response, Is.EqualTo(FindAllServicesQuery.SUCCESS));
            Assert.That(query.AlbumImages.Count, Is.EqualTo(5));
        }

        [TestMethod]
        public void Create_ReturnsSuccessAndId()
        {
            Mock<ImageRepository> mockImageRepository = new Mock<ImageRepository>();
            CreateImageCommand command = new CreateImageCommand();
            command.image = new Models.AlbumImage();
            command.image.Image = new byte[Convert.ToByte(1)];
            command.image.ImageAlt = "1";
            command.image.ImageName = "1";
            command.image.ImageUrl = "1";
            Image mockImage = createMockImage(1);
            mockImageRepository.Setup(x => x.Insert(mockImage)).Returns(mockImage.ImageId);

            ImageService imageService = new ImageService(mockImageRepository.Object);
            imageService.Create(command);
            Assert.That(command.Response, Is.EqualTo(CreateImageCommand.SUCCESS));
            Assert.That(command.image.ImageId, Is.EqualTo(mockImage.ImageId));
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
