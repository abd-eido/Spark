using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Data
{
    public class Ratings
    {
        public int Id { get; set; }

        public string Notes { get; set; }

        public decimal RatingRate { get; set; }

        public int ProjectId { get; set; }

        public string UserId { get; set; }

        public string CreadtedBy { get; set; }

        public string CreatedById { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UpdatedById { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
