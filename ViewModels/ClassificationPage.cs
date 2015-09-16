using BIMToolkitAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIMToolkitAPIClient.ViewModels
{
    public class ClassificationPage
    {
        public ClassificationNode Classification { get; set; }
        public string Lod { get; set; }
        public string Loi { get; set; }
    }
}