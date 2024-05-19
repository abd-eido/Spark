using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Data
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public decimal Rating { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedById { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CoverImageUrl { get; set; }
        public string ProjectPdfUrl { get; set; }
        public ICollection<ProjectGallery> ProjectGallery { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
