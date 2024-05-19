using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Models;

namespace Spark.Repository
{
    public interface IOfferRepository
    {
        Task<List<OfferModel>> GetAllOffers(string userId);
        Task<int> SendOffer(OfferModel model);
        Task<List<OfferModel>> GetAllOffersByProjects(List<int> projectIds);
    }
}