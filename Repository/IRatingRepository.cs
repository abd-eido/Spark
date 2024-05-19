using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Models;

namespace Spark.Repository
{
    public interface IRatingRepository
    {
        Task<int> RateProject(RatingModel model);

        Task<List<RatingModel>> GetRatingsByProjectId(int id);
    }
}