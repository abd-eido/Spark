using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Spark.Enums;
using Microsoft.AspNetCore.Http;

namespace Spark.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the name of your project")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the desciption")]
        public string Desciption { get; set; }

        public int Rating { get; set; }

        public string UserId { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedById { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UpdatedById { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Choose the cover photo of your project")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your project")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel> Gallery { get; set; }

        [Display(Name = "Upload your project in pdf format")]
        [Required]
        public IFormFile ProjectPdf { get; set; }
        public string ProjectPdfUrl { get; set; }

        public List<KeywordModel> Keywords { get; set; }

        public OfferModel Offer { get; set; }

        public CommentsModel Comment { get; set; }

        public List<CommentsModel> Comments { get; set; }

        public bool DisableRatings { get; set; }

        public ProjectModel()
        {
            Keywords = new List<KeywordModel>();
            Comments = new List<CommentsModel>();
        }
    }
}
