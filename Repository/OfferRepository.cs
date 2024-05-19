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
    public class OfferRepository : IOfferRepository
    {
        private readonly dbContext _context = null;
        private readonly IConfiguration _configuration;

        public OfferRepository(dbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<OfferModel>> GetAllOffers(string userId)
        {
            return await _context.Offers.Where(x => x.UserId == userId)
                 .Select(offer => new OfferModel()
                 {
                     ProjectId = offer.ProjectId,
                     OfferId = offer.Id,
                     Desciption = offer.Desciption,
                     Amount = offer.Amount,
                     UserId = offer.UserId,
                     CreatedById = offer.CreatedById,
                     CreatedBy = offer.CreatedBy,
                     CreatedAt = offer.CreatedAt,
                     UpdatedById = offer.UpdatedById,
                     UpdatedAt = offer.UpdatedAt,
                 }).ToListAsync();
        }

        public async Task<int> SendOffer(OfferModel model)
        {
            var newOffer = new Offers()
            {
                ProjectId = model.ProjectId,
                Desciption = model.Desciption,
                Amount = model.Amount,
                UserId = model.CreatedById,
                CreatedById = model.CreatedById,
                CreatedBy = model.CreatedBy,
                CreatedAt = DateTime.Now,
            };

            await _context.Offers.AddAsync(newOffer);
            await _context.SaveChangesAsync();

            return newOffer.Id;
        }

        public async Task<List<OfferModel>> GetAllOffersByProjects(List<int> projectsIds)
        {
            if (projectsIds == null || projectsIds.Count == 0)
            {
                return new List<OfferModel>();
            }

            return await _context.Offers
                .Where(offer => projectsIds.Contains(offer.ProjectId))
                .Select(offer => new OfferModel
                {
                    ProjectId = offer.ProjectId,
                    OfferId = offer.Id,
                    Desciption = offer.Desciption,
                    Amount = offer.Amount,
                    UserId = offer.UserId,
                    CreatedById = offer.CreatedById,
                    CreatedBy = offer.CreatedBy,
                    CreatedAt = offer.CreatedAt,
                    UpdatedById = offer.UpdatedById,
                    UpdatedAt = offer.UpdatedAt
                }).ToListAsync();
        }
    }
}
