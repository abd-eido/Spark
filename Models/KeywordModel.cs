using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Models
{
    public class KeywordModel
    {
        public int Id { get; set; }

        public string KeywordContent { get; set; }

        public int ProjectId { get; set; }

        public string CreatedById { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UpdatedById { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
