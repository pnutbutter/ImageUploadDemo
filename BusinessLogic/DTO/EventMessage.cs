using System;
using System.Collections.Generic;
using System.Text;

namespace AS.ImageAlbum.BusinessLogic.DTO
{
    public class EventMessage
    {
        public static string SUCCESS = "success";
        public static string ERROR = "Error: {0}";
        public string Response { get; set; }
    }
}
