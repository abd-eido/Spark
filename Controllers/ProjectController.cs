using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spark.Models;
using Spark.Repository;

namespace Spark.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository = null;
        private readonly IOfferRepository _offerRepository = null;
        private readonly IRatingRepository _ratingRepository = null;
        private readonly ICommentRepository _commentRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IKeywordRepository _keywordRepository = null;

        public ProjectController(IProjectRepository projectRepository, IOfferRepository offerRepository, IRatingRepository ratingRepository, ICommentRepository commentRepository, IWebHostEnvironment webHostEnvironment, IKeywordRepository keywordRepository)
        {
            _projectRepository = projectRepository;
            _offerRepository = offerRepository;
            _ratingRepository = ratingRepository;
            _commentRepository = commentRepository;
            _webHostEnvironment = webHostEnvironment;
            _keywordRepository = keywordRepository;
        }

        [Route("all-projects")]
        public async Task<ViewResult> GetAllProjects()
        {
            var data = await _projectRepository.GetAllProjects();
            foreach (var project in data)
            {
                var keywords = await _keywordRepository.GetProjectKeywords(project.Id);
                project.Keywords = keywords;
            }

            return View(data);
        }

        [Authorize]
        [Route("project-details/{id:int:min(1)}", Name = "projectDetailsRoute")]
        public async Task<ViewResult> GetProject(int id, bool isSuccess = false, string message = "")
        {
            var data = await _projectRepository.GetProjectById(id);

            var keywords = await _keywordRepository.GetProjectKeywords(data.Id);
            data.Keywords = keywords;

            var ratings = await _ratingRepository.GetRatingsByProjectId(data.Id);
            double averageRating = 0;
            if (ratings.Count() != 0)
            {
                averageRating = ratings.Where(r => r.RatingRate != null).Average(rating => (double)rating.RatingRate / 5); // Cast to double for accurate average
                data.Rating = (int)Math.Ceiling(averageRating);
            }
            else
                data.Rating = (int)averageRating;

            var comments = await _commentRepository.GetCommentsByProjectId(data.Id);
            data.Comments = comments;

            var userType = User.Claims.Where(x => x.Type == "UserType").FirstOrDefault().Value;
            data.DisableRatings = userType == "Student" ? true : false;

            ViewBag.IsSuccess = isSuccess;
            ViewBag.Id = id;
            ViewBag.Message = message;
            return View(data);
        }

        [Authorize]
        public async Task<ViewResult> AddNewProject(bool isSuccess = false, int projectId = 0)
        {
            var model = new ProjectModel();

            ViewBag.IsSuccess = isSuccess;
            ViewBag.Id = projectId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProject(ProjectModel projectModel)
        {
            if (ModelState.IsValid)
            {
                var userFirstName = User.Claims.Where(x => x.Type == "UserFirstName").FirstOrDefault().Value;
                var userLastName = User.Claims.Where(x => x.Type == "UserLastName").FirstOrDefault().Value;
                var userId = User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;

                projectModel.CreatedBy = userFirstName + " " + userLastName;
                projectModel.CreatedById = userId;


                if (projectModel.CoverPhoto != null)
                {
                    string folder = "projects/cover/";
                    projectModel.CoverImageUrl = await UploadImage(folder, projectModel.CoverPhoto);
                }

                if (projectModel.GalleryFiles != null)
                {
                    string folder = "projects/gallery/";

                    projectModel.Gallery = new List<GalleryModel>();

                    foreach (var file in projectModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        projectModel.Gallery.Add(gallery);
                    }
                }

                if (projectModel.ProjectPdf != null)
                {
                    string folder = "projects/pdf/";
                    projectModel.ProjectPdfUrl = await UploadImage(folder, projectModel.ProjectPdf);
                }

                int id = await _projectRepository.CreateProject(projectModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewProject), new { isSuccess = true, projectId = id });
                }
            }

            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendOffer(ProjectModel projectModel)
        {
            var userFirstName = User.Claims.Where(x => x.Type == "UserFirstName").FirstOrDefault().Value;
            var userLastName = User.Claims.Where(x => x.Type == "UserLastName").FirstOrDefault().Value;
            var userId = User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;

            projectModel.Offer.CreatedBy = userFirstName + " " + userLastName;
            projectModel.Offer.CreatedById = userId;
            projectModel.Offer.ProjectId = projectModel.Id;
            int id = await _offerRepository.SendOffer(projectModel.Offer);

            return RedirectToAction(actionName: nameof(GetProject), routeValues: new { id = projectModel.Id, isSuccess = true, message = "You have send offer successfully." });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RateProject(int ratingValue, int projectId)
        {
            var userFirstName = User.Claims.Where(x => x.Type == "UserFirstName").FirstOrDefault().Value;
            var userLastName = User.Claims.Where(x => x.Type == "UserLastName").FirstOrDefault().Value;
            var userId = User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;

            var rateModel = new RatingModel();
            rateModel.RatingRate = ratingValue;
            rateModel.ProjectId = projectId;
            rateModel.CreatedById = userId;
            rateModel.CreadtedBy = userFirstName + " " + userLastName;

            int id = await _ratingRepository.RateProject(rateModel);
            return RedirectToAction(actionName: nameof(GetProject), routeValues: new { id = projectId, isSuccess = true, message = "You have rate the project successfully." });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(ProjectModel model)
        {
            var userFirstName = User.Claims.Where(x => x.Type == "UserFirstName").FirstOrDefault().Value;
            var userLastName = User.Claims.Where(x => x.Type == "UserLastName").FirstOrDefault().Value;
            var userId = User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;

            var commentModel = new CommentsModel();
            commentModel.Comment = model.Comment.Comment;
            commentModel.ProjectId = model.Id;
            commentModel.CreatedById = userId;
            commentModel.CreadtedBy = userFirstName + " " + userLastName;

            int id = await _commentRepository.AddComment(commentModel);
            return RedirectToAction(actionName: nameof(GetProject), routeValues: new { id = model.Id, isSuccess = true, message = "You have added new comment successfully." });
        }
    }
}
