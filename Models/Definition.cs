using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIMToolkitAPIClient.Models
{
    public class Definition
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Notation { get; set; }
        public object Data { get; set; }
    }

}