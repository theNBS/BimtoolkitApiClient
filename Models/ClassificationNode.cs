using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIMToolkitAPIClient.Models
{
    public class ClassificationNode
    {
        public string Notation { get; set; }
        public string Title { get; set; }
        public string[] Lods { get; set; }
        public string[] Lois { get; set; }
        public List<ClassificationNode> Children { get; set; }
    }
}