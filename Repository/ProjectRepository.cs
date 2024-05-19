using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spark.Data;
using Spark.Models;

namespace Spark.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly dbContext _context = null;
        private readonly IConfiguration _configuration;

        public ProjectRepository(dbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> CreateProject(ProjectModel model)
        {
            var newProject = new Projects()
            {
                Name = model.Name,
                Desciption = model.Desciption,
                UserId = model.CreatedById,
                CreatedById = model.CreatedById,
                CreatedBy = model.CreatedBy,
                CreatedAt = DateTime.Now,
                CoverImageUrl = model.CoverImageUrl,
                ProjectPdfUrl = model.ProjectPdfUrl
            };

            newProject.ProjectGallery = new List<ProjectGallery>();

            foreach (var file in model.Gallery)
            {
                newProject.ProjectGallery.Add(new ProjectGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();

            return newProject.Id;
        }


        public async Task UpdateProject(ProjectModel model)
        {
            var dbProject = await GetProjectById(model.Id);

            dbProject.Name = dbProject.Name;
            dbProject.Desciption = dbProject.Desciption;
            dbProject.UserId = dbProject.UserId;
            dbProject.UpdatedById = dbProject.UserId;
            dbProject.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(int projectId)
        {
            var dbProject = _context.Projects.FirstOrDefault(x => x.Id == projectId);
            _context.Remove(dbProject);
        }

        public async Task<ProjectModel> GetProjectById(int id)
        {
            return await _context.Projects.Where(x => x.Id == id)
                 .Select(project => new ProjectModel()
                 {
                     Id = project.Id,
                     Name = project.Name,
                     Desciption = project.Desciption,
                     UserId = project.UserId,
                     CreatedById = project.CreatedById,
                     CreatedBy = project.CreatedBy,
                     CreatedAt = project.CreatedAt,
                     UpdatedById = project.UpdatedById,
                     UpdatedAt = project.UpdatedAt,
                     Rating = (int)project.Rating,
                     Gallery = project.ProjectGallery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList(),
                     ProjectPdfUrl = project.ProjectPdfUrl
                 }).FirstOrDefaultAsync();
        }

        public async Task<List<ProjectModel>> GetAllProjects()
        {
            return await _context.Projects
                  .Select(project => new ProjectModel()
                  {
                      Id = project.Id,
                      Name = project.Name,
                      Desciption = project.Desciption,
                      UserId = project.UserId,
                      CreatedById = project.UserId,
                      CreatedAt = project.CreatedAt,
                      UpdatedAt = project.UpdatedAt,
                      UpdatedById = project.UpdatedById,
                      CoverImageUrl = project.CoverImageUrl,
                  }).ToListAsync();
        }

        public async Task<List<ProjectModel>> GetAllProjectsByUserId(string userId)
        {
            return await _context.Projects.Where(x => x.UserId == userId)
                 .Select(project => new ProjectModel()
                 {
                     Id = project.Id,
                     Name = project.Name,
                     Desciption = project.Desciption,
                     UserId = project.UserId,
                     CreatedById = project.UserId,
                     CreatedAt = project.CreatedAt,
                     UpdatedAt = project.UpdatedAt,
                     UpdatedById = project.UpdatedById,
                     CoverImageUrl = project.CoverImageUrl,
                 }).ToListAsync();
        }

        public async Task<List<ProjectModel>> GetTopProjectsAsync(int count)
        {
            return await _context.Projects
                  .Select(project => new ProjectModel()
                  {
                      Id = project.Id,
                      Name = project.Name,
                      Desciption = project.Desciption,
                      UserId = project.UserId,
                      CreatedById = project.UserId,
                      CreatedAt = project.CreatedAt,
                      UpdatedAt = project.UpdatedAt,
                      UpdatedById = project.UpdatedById,
                      CoverImageUrl = project.CoverImageUrl,
                  }).Take(count).ToListAsync();
        }
    }
}
