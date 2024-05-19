using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Models;

namespace Spark.Repository
{
    public interface IKeywordRepository
    {
        Task<int> AddNewKeyword(KeywordModel model);

        Task<List<KeywordModel>> GetAllKeywords();

        Task<List<KeywordModel>> GetProjectKeywords(int projectId);
    }
}
