using Spark.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Data
{
    public class Offers
    {
        public int Id { get; set; }

        public string Desciption { get; set; }

        public decimal? Amount { get; set; }

        public string UserId { get; set; }

        public int ProjectId { get; set; }

        public string CreatedById { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UpdatedById { get; set; }

        public DateTime UpdatedAt { get; set; }

        public OfferStatusEnum OfferStatus { get; set; }

        public string ReplayMessage { get; set; }
    }
}
