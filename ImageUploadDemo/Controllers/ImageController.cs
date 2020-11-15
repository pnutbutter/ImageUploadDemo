using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS.ImageAlbum.BusinessLogic.DTO.Command;
using AS.ImageAlbum.BusinessLogic.DTO.Query;
using AS.ImageAlbum.BusinessLogic.Interfaces;
using AS.ImageAlbum.Website.Models.ImageData;
using Microsoft.AspNetCore.Mvc;
using AS.ImageAlbum.BusinessLogic.Models;

namespace AS.ImageAlbum.Website.Controllers
{
    public class ImageController : Controller
    {
        private IImageService service;

        public ImageController(IImageService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            ImageIndex viewModel = new ImageIndex();
            try
            {
                FindAllServicesQuery query = new FindAllServicesQuery();
                this.service.FindAll(query);
                if (query.Response == FindAllServicesQuery.SUCCESS)
                {
                    
                    viewModel.AlbumImages = query.AlbumImages;
                }
                else
                {
                    throw new ArgumentException(FindAllServicesQuery.ERROR, "Business Logic Error");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ImageCreate model = new ImageCreate();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ImageCreate model)
        {
            CreateImageCommand command = new CreateImageCommand();
            if (ModelState.IsValid)
            {
                try
                {
                    
                    command.image = new AlbumImage{Image=model.Image, ImageAlt=model.ImageAlt, ImageName=model.ImageName, ImageUrl=model.ImageUrl };
                    this.service.Create(command);
                    if (command.Response != FindAllServicesQuery.SUCCESS)
                    {

                        throw new ArgumentException(FindAllServicesQuery.ERROR, "Business Logic Error");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return RedirectToAction("Edit", new { id=command.image.ImageId, message="Image Saved!" });
        }

        [HttpGet]
        public IActionResult Edit(Guid id, string message)
        {
            ImageEdit model = new ImageEdit();
            model.ImageId = id;
            model.Message = message;
            try
            {
                FindAllServicesQuery query = new FindAllServicesQuery();
                this.service.FindAll(query);
                if (query.Response == FindAllServicesQuery.SUCCESS)
                {

                    AlbumImage image = query.AlbumImages.Find(x => x.ImageId == id);

                    model.Image = image.Image;
                    model.ImageAlt = image.ImageAlt;
                    model.ImageName = image.ImageName;
                    model.ImageUrl = image.ImageUrl;
                }
                else
                {
                    throw new ArgumentException(FindAllServicesQuery.ERROR, "Business Logic Error");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }
    }
}
