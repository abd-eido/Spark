using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Models;

namespace Spark.Repository
{
    public interface IProjectRepository
    {
        Task<int> CreateProject(ProjectModel model);
        Task UpdateProject(ProjectModel model);
        Task DeleteProject(int projectId);
        Task<ProjectModel> GetProjectById(int projectId);
        Task<List<ProjectModel>> GetAllProjects();
        Task<List<ProjectModel>> GetAllProjectsByUserId(string userId);
        Task<List<ProjectModel>> GetTopProjectsAsync(int count);

    }
}
