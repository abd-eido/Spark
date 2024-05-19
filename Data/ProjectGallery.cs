using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Data
{
    public class ProjectGallery
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public Projects Project { get; set; }
    }
}
