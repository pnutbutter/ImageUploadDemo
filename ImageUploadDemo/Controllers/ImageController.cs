using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS.ImageAlbum.BusinessLogic.DTO.Query;
using AS.ImageAlbum.BusinessLogic.Interfaces;
using AS.ImageAlbum.Website.Models.ImageData;
using Microsoft.AspNetCore.Mvc;

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
    }
}
