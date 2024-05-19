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
    public class KeywordRepository : IKeywordRepository
    {
        private readonly dbContext _context = null;
        private readonly IConfiguration _configuration;

        public KeywordRepository(dbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> AddNewKeyword(KeywordModel model)
        {
            var newKeyword = new Keywords()
            {
                KeywordContent = model.KeywordContent,
                ProjectId = model.ProjectId,
                CreatedAt = DateTime.Now,
                CreatedById = model.CreatedById,
            };

            await _context.Keywords.AddAsync(newKeyword);
            await _context.SaveChangesAsync();

            return newKeyword.Id;
        }

        public async Task<List<KeywordModel>> GetAllKeywords()
        {
            return await _context.Keywords
                .Select(keyword => new KeywordModel()
                {
                    Id = keyword.Id,
                    KeywordContent = keyword.KeywordContent,
                    CreatedAt = keyword.CreatedAt,
                    CreatedById = keyword.CreatedById,
                    ProjectId = keyword.ProjectId,
                    UpdatedAt = keyword.UpdatedAt,
                    UpdatedById = keyword.UpdatedById
                }).ToListAsync();
        }

        public async Task<List<KeywordModel>> GetProjectKeywords(int projectId)
        {
            return await _context.Keywords.Where(x => x.ProjectId == projectId)
                .Select(keyword => new KeywordModel()
                {
                    Id = keyword.Id,
                    KeywordContent = keyword.KeywordContent,
                    CreatedAt = keyword.CreatedAt,
                    CreatedById = keyword.CreatedById,
                    ProjectId = keyword.ProjectId,
                    UpdatedAt = keyword.UpdatedAt,
                    UpdatedById = keyword.UpdatedById
                }).ToListAsync();
        }

    }
}
