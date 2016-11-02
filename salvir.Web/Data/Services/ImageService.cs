﻿using deprosa.Common;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Service
{
    public class ImageService
    {

        private const string Imagefolder = "/Images/";


        public bool ValidateExtension(string extension)
        {
            var extensions = extension.Split('/');
            extension = extensions[extensions.Length-1];

            switch (extension)
            {
                case ".jpg":
                case ".png":
                case ".jpeg":
                case "jpg":
                case "png":
                case "jpeg":
                    return true;
                default:
                    return false;
            }
        }

        public string GenerateImageName()
        {
            var guidpart = Guid.NewGuid();
           var newimagename = guidpart;
            return newimagename.ToString();
        }
    }
}
