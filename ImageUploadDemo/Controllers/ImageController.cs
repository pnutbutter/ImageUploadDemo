using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS.ImageAlbum.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AS.ImageAlbum.Website.Controllers
{
    public class ImageController : Controller
    {
        private IImageService _service;

        public ImageController(IImageService service)
        {
            this._service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
