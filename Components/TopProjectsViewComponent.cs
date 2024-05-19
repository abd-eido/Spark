using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spark.Repository;

namespace Spark.Components
{
    public class TopProjectsViewComponent : ViewComponent
    {
        private readonly IProjectRepository _projectRepository;

        public TopProjectsViewComponent(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var projects = await _projectRepository.GetTopProjectsAsync(count);
            return View(projects);
        }
    }
}
