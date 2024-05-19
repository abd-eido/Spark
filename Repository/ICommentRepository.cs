using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Models;

namespace Spark.Repository
{
    public interface ICommentRepository
    {
        Task<int> AddComment(CommentsModel model);

        Task<List<CommentsModel>> GetCommentsByProjectId(int id);
    }
}