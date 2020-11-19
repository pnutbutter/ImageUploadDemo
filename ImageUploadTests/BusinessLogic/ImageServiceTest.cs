using AS.ImageAlbum.BusinessLogic;
using AS.ImageAlbum.BusinessLogic.DTO;
using AS.ImageAlbum.BusinessLogic.DTO.Command;
using AS.ImageAlbum.BusinessLogic.DTO.Query;
using AS.ImageAlbum.BusinessLogic.Models;
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

namespace AS.ImageAlbum.UniteTests
{
    [TestClass]
    public class ImageServiceTest
    {

        [TestMethod]
        public void FindFromToTagFilter_ReturnsSuccessAndMapsImagesAndMapsTags_WhenPassedAllParameters()
        {
            Mock<ImageRepository> mockImageRepository = new Mock<ImageRepository>();
            FindFromToTagFilterQuery query = new FindFromToTagFilterQuery();
            List<Image> imageList = new List<Image>();
            for(int i = 0; i<5; i++)
            {
                imageList.Add(createMockImage(i));
            }
            
            query.BeginIndex = 0;
            query.EndIndex = 4;
            query.Search = "test";
            query.TagFilters = new List<Guid>();
            Guid tagGuid = Guid.NewGuid();
            query.TagFilters.Add(tagGuid);

            mockImageRepository.Setup(x => x.GetFromTo(query.BeginIndex, query.EndIndex, query.TagFilters, query.Search)).Returns(imageList);
            ImageService imageService = new ImageService(mockImageRepository.Object);
            imageService.FindFromToTagFilter(query);
            Assert.That(query.Response, Is.EqualTo(EventMessage.SUCCESS));
            Assert.That(query.AlbumImages.Count, Is.EqualTo(5));
            Assert.That(query.AlbumImages[0].ImageTags.Count, Is.EqualTo(1));
        }

        [TestMethod]
        public void FindFromToTagFilter_ReturnsSuccess_WhenPassedEmptyOptionalInputs()
        {
            Mock<ImageRepository> mockImageRepository = new Mock<ImageRepository>();
            FindFromToTagFilterQuery query = new FindFromToTagFilterQuery();
            List<Image> imageList = new List<Image>();
            for (int i = 0; i < 5; i++)
            {
                imageList.Add(createMockImage(i));
            }

            query.BeginIndex = 0;
            query.EndIndex = 4;
            query.Search = null;
            query.TagFilters = null;

            mockImageRepository.Setup(x => x.GetFromTo(query.BeginIndex, query.EndIndex, query.TagFilters, query.Search)).Returns(imageList);
            ImageService imageService = new ImageService(mockImageRepository.Object);
            imageService.FindFromToTagFilter(query);
            Assert.That(query.Response, Is.EqualTo(EventMessage.SUCCESS));
        }


        [TestMethod]
        public void Create_ReturnsSuccessAndId_WhenPassedParameters()
        {
            Mock<ImageRepository> mockImageRepository = new Mock<ImageRepository>();
            CreateImageCommand command = new CreateImageCommand();
            command.image = new AlbumImage();
            command.image.ImageAlt = "1";
            command.image.ImageName = "1";
            command.image.ImageUrl = "1";
            Image mockImage = createMockImage(1);
            mockImageRepository.Setup(x => x.Insert(It.IsAny<Image>())).Returns(mockImage.ImageId);

            ImageService imageService = new ImageService(mockImageRepository.Object);
            imageService.Create(command);
            Assert.That(command.Response, Is.EqualTo(EventMessage.SUCCESS));
            Assert.That(command.image.ImageId, Is.EqualTo(mockImage.ImageId));
        }

        private Image createMockImage(int variable)
        {
            string varToSting = variable.ToString();
            Guid guid = new Guid(variable, 0, 0, new byte[8]);
            List<ImageTag> tagList = new List<ImageTag>();
            tagList.Add(new ImageTag { Name = varToSting, TagId=guid});
            return new Image { AlbumImage = new byte[Convert.ToByte(variable)], ImageAlt = varToSting, ImageName = varToSting, ImageUrl = varToSting, ImageId = guid, ImageTags= tagList };
        }
    }
}
