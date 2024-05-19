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
    public class RatingRepository : IRatingRepository
    {
        private readonly dbContext _context = null;
        private readonly IConfiguration _configuration;

        public RatingRepository(dbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> RateProject(RatingModel model)
        {
            var newRate = new Ratings()
            {
                RatingRate = model.RatingRate,
                Notes = model.Notes,
                ProjectId = model.ProjectId,
                UserId = model.CreatedById,
                CreadtedBy = model.CreadtedBy,
                CreatedById = model.CreatedById,
                CreatedAt = DateTime.Now,
            };

            await _context.Ratings.AddAsync(newRate);
            await _context.SaveChangesAsync();

            return newRate.Id;
        }

        public async Task<List<RatingModel>> GetRatingsByProjectId(int projectId)
        {
            return await _context.Ratings.Where(x => x.ProjectId == projectId)
                 .Select(rates => new RatingModel()
                 {
                     Id = rates.Id,
                     RatingRate = rates.RatingRate,
                     Notes = rates.Notes,
                     ProjectId = rates.ProjectId,
                     UserId = rates.UserId,
                     CreatedById = rates.UserId,
                     CreatedAt = rates.CreatedAt,
                     UpdatedAt = rates.UpdatedAt,
                     UpdatedById = rates.UpdatedById,
                 }).ToListAsync();
        }
    }
}
