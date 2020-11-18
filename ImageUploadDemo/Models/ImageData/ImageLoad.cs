using AS.ImageAlbum.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.ImageAlbum.Website.Models.ImageData
{
    public class ImageLoad
    {
        public int BeginIndex { get; set; }
        public int EndIndex { get; set; }
        public string[] TagFilters { get; set; }
        public string Search { get; set; }
    }
}
