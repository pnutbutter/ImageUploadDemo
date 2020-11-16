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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

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

                if (model.TagIDs != null && model.TagIDs.Length > 0 && model.TagNames != null && model.TagNames.Length > 0)
                {
                    if (model.TagNames.Length == model.TagIDs.Length)
                    {
                        command.image.ImageTags = new List<AlbumImageTag>();
                        for (int i = 0; i < model.TagIDs.Length; i++)
                        {
                            command.image.ImageTags.Add(new AlbumImageTag { ImageTagId = model.TagIDs[i], Name = model.TagNames[i] });
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Tag Name and Tag Ids don't have the same amount");
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
                FindByIDQuery query = new FindByIDQuery();
                query.ImageId = id;
                this.service.FindByID(query);
                if (query.Response == FindByIDQuery.SUCCESS)
                {
                    model.ImageDisplay = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(query.Record.Image));
                    model.ImageAlt = query.Record.ImageAlt;
                    model.ImageName = query.Record.ImageName;
                    model.ImageUrl = query.Record.ImageUrl;

                    query.Record.ImageTags.Sort((t1, t2) => string.Compare(t1.Name, t2.Name));

                    model.TagIDs = query.Record.ImageTags.Select(t => t.ImageTagId).ToArray();
                    model.TagNames = query.Record.ImageTags.Select(t => t.Name).ToArray();

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

                //each tag should have a corrisponding tag name but just incase lets check
                if (model.TagIDs != null && model.TagIDs.Length > 0 && model.TagNames != null && model.TagNames.Length > 0)
                {
                    if (model.TagNames.Length == model.TagIDs.Length)
                    {
                        command.image.ImageTags = new List<AlbumImageTag>();
                        for (int i = 0; i < model.TagIDs.Length; i++)
                        {
                            command.image.ImageTags.Add(new AlbumImageTag { ImageId = model.ImageId, ImageTagId = model.TagIDs[i], Name = model.TagNames[i] });
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Tag Name and Tag Ids don't have the same amount");
                    }
                }

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

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        //[EnableCors("CorsPolicy")]
        public IActionResult ImageLoad([FromForm] ImageLoad model)
        {
            FindFromToTagFilterQuery query = new FindFromToTagFilterQuery();
            List<ImageJson> imageList = new List<ImageJson>();
            try
            {
                query.BeginIndex = model.BeginIndex;
                query.EndIndex = model.EndIndex;
                query.TagFilters = new List<Guid>();

                if (model.TagFilters != null)
                {
                    foreach (string str in model.TagFilters)
                    {
                        query.TagFilters.Add(Guid.Parse(str));
                    }
                }
                this.service.FindFromToTagFilter(query);

                foreach (AlbumImage image in query.AlbumImages)
                {
                    imageList.Add(new ImageJson { ImageAlt = image.ImageAlt.Trim(), ImageName = image.ImageName, ImageUrl = image.ImageUrl });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(JsonConvert.SerializeObject(imageList));
        }

        [HttpGet]
        public ActionResult Images(string id)
        {
            FindByImageUrlQuery query = new FindByImageUrlQuery();
            try
            {
                query.ImageUrl = id;

                this.service.FindByImageUrl(query);

                MemoryStream imageStream = new MemoryStream();
                imageStream.Write(query.AlbumImage.Image, 0, query.AlbumImage.Image.Length);
                imageStream.Position = 0;
                return new FileStreamResult(imageStream, "image/jpeg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
