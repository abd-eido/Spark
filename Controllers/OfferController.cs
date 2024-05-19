using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spark.Models;
using Spark.Repository;

namespace Spark.Controllers
{
    public class OfferController : Controller
    {
        private readonly IOfferRepository _offerRepository = null;
        private readonly IProjectRepository _projectRepository = null;
        public OfferController(IOfferRepository offerRepository, IProjectRepository projectRepository)
        {
            _offerRepository = offerRepository;
            _projectRepository = projectRepository;
        }

        [Authorize]
        [Route("all-offers")]
        public async Task<ViewResult> GetAllOffers()
        {
            var userId = User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;
            var userType = User.Claims.Where(x => x.Type == "UserType").FirstOrDefault().Value;

            var data = new List<OfferModel>();

            if (userType == "Student")
            {
                var projects = await _projectRepository.GetAllProjectsByUserId(userId);
                data = await _offerRepository.GetAllOffersByProjects(projects.Select(x => x.Id).ToList());
            }
            else
            {
                data = await _offerRepository.GetAllOffers(userId);
            }

            foreach (var offer in data)
            {
                offer.Project = await _projectRepository.GetProjectById(offer.ProjectId);
                if (offer.Project == null)
                    offer.Project = new ProjectModel();
            }

            return View(data);
        }

    }
}
