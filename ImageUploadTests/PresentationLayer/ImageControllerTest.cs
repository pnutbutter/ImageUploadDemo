using AS.ImageAlbum.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using AS.ImageAlbum.Website.Controllers;
using AS.ImageAlbum.BusinessLogic.Interfaces;
using AS.ImageAlbum.BusinessLogic.DTO.Query;
using AS.ImageAlbum.Website.Models.ImageData;
using Microsoft.AspNetCore.Mvc;
using AS.ImageAlbum.BusinessLogic.DTO;
using AS.ImageAlbum.BusinessLogic.Models;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using System.Reflection;
using AS.ImageAlbum.Repository;
using Newtonsoft.Json;

namespace AS.ImageAlbum.UnitTests.PresentationLayer
{
    [TestClass]
    public class ImageControllerTest
    {
        delegate void MockFindFromToTagFilterCallback(FindFromToTagFilterQuery query);

        [TestMethod]
        public void ImageLoad_ReturnsCorrectJSONdata_WhenPassedAllParameters()
        {
            //Mock<ImageRepository> mockImageRepository = new Mock<ImageRepository>();
            Mock<IImageService> mockService = new Mock<IImageService>();
            Guid imageGuid = Guid.NewGuid();
            Guid tagGuid = Guid.NewGuid();

            //setup model for passing into the controller
            ImageLoad model = new ImageLoad();
            model.BeginIndex = 0;
            model.EndIndex = 4;
            model.Search = "test";
            model.TagFilters = new string[] { imageGuid.ToString(), tagGuid.ToString() };

            //setup reference object to be returned when calling the image service
            List<AlbumImage> images = new List<AlbumImage>();
            List<AlbumImageTag> tags = new List<AlbumImageTag>();
            tags.Add(new AlbumImageTag { ImageId = imageGuid, ImageTagId = tagGuid, Name = "tag", TagId = tagGuid });
            images.Add(new AlbumImage { Image = new byte[0], ImageId = imageGuid, ImageAlt = "testalt", ImageName = "testname", ImageUrl = "testurl", ImageTags = tags });
            FindFromToTagFilterQuery query = new FindFromToTagFilterQuery();
            query.BeginIndex = model.BeginIndex;
            query.EndIndex = model.EndIndex;
            query.AlbumImages = images;
            query.Response = EventMessage.SUCCESS;
            query.Search = model.Search;
            query.TagFilters = new List<Guid>();
            query.TagFilters.Add(imageGuid);
            query.TagFilters.Add(tagGuid);

            //because the method FindFromToTagFirlter returns void and insteads gets data by passing 
            //FindFromToTagFilterQuery object by reference...
            //set up the MOQ to return the reference object inside the controller
            mockService.Setup(x => x.FindFromToTagFilter(It.IsAny<FindFromToTagFilterQuery>()))
            .Callback((FindFromToTagFilterQuery c) =>
            {
                c.AlbumImages = query.AlbumImages;
                c.BeginIndex = query.BeginIndex;
                c.EndIndex = query.EndIndex;
                c.Response = query.Response;
                c.Search = query.Search;
                c.TagFilters = query.TagFilters;
            });


            ImageController controller = new ImageController(mockService.Object);

            JsonResult result = controller.ImageLoad(model) as JsonResult;
            Assert.That(result.Value, Is.Not.Null);
            List<ImageJson> imageList = JsonConvert.DeserializeObject<List<ImageJson>>(result.Value.ToString());
            Assert.That(imageList.Count, Is.EqualTo(1));
            Assert.That(imageList[0].ImageAlt, Is.EqualTo(query.AlbumImages[0].ImageAlt));
            Assert.That(imageList[0].ImageId, Is.EqualTo(query.AlbumImages[0].ImageId));
            Assert.That(imageList[0].ImageName, Is.EqualTo(query.AlbumImages[0].ImageName));
            Assert.That(imageList[0].ImageUrl, Is.EqualTo(query.AlbumImages[0].ImageUrl));
        }


    }
}
