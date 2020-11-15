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
using System.IO;

namespace AS.ImageAlbum.Website.Controllers
{
    public class ImageController : Controller
    {
        private IImageService service;

        public ImageController(IImageService service)
        {
            this.service = service;
        }

        public IActionResult Index(string message)
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
            catch (Exception ex)
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

            if (!ModelState.IsValid)
                return View(model);
            try
            {
                if (model.ImageFile == null || model.ImageFile.Length == 0)
                    throw new ArgumentException("Image not found");
                if (model.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.ImageFile.CopyTo(ms);
                        command.image = new AlbumImage { Image = ms.ToArray(), ImageAlt = model.ImageAlt, ImageName = model.ImageName, ImageUrl = model.ImageUrl };
                        command.image.ImageTags = new List<AlbumImageTag>();
                    }
                }


                this.service.Create(command);
                if (command.Response != CreateImageCommand.SUCCESS)
                {

                    throw new ArgumentException(CreateImageCommand.ERROR, "Business Logic Error");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Edit", new { id = command.image.ImageId, message = "Image Saved!" });
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

                    model.ImageDisplay = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(image.Image));
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

        [HttpPost]
        public IActionResult Edit(ImageEdit model)
        {
            EditImageCommand command = new EditImageCommand();
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                command.image = new AlbumImage { ImageAlt = model.ImageAlt, ImageName = model.ImageName, ImageUrl = model.ImageUrl, ImageId = model.ImageId };
                command.image.ImageTags = new List<AlbumImageTag>();

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.ImageFile.CopyTo(ms);
                        command.image.Image = ms.ToArray();
                    }
                }
                this.service.Update(command);

                if (command.Response != EditImageCommand.SUCCESS)
                {

                    throw new ArgumentException(EditImageCommand.ERROR, "Business Logic Error");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Edit", new { id = model.ImageId, message = "Image Saved!" });
        }
    }
}
