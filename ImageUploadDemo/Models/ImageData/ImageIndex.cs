﻿using AS.ImageAlbum.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS.ImageAlbum.Website.Models.ImageData
{
    public class ImageIndex
    {
        public string Message { get; set; }
        public List<AlbumImage> AlbumImages { get; set; }
    }
}
