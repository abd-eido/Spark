using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Data
{
    public class Comments
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int ProjectId { get; set; }

        public string CreatedById { get; set; }

        public string CreadtedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Rating { get; set; }
    }
}
