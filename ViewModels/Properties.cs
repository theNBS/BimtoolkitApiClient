using BIMToolkitAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIMToolkitAPIClient.ViewModels
{
    public class Properties
    {
        public List<Property> PropertiesList = new List<Property>();

        public string Json { get; set; }
    }
}