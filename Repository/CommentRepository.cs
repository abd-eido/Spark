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
    public class CommentRepository : ICommentRepository
    {
        private readonly dbContext _context = null;
        private readonly IConfiguration _configuration;

        public CommentRepository(dbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> AddComment(CommentsModel model)
        {
            var newComment = new Comments()
            {
                ProjectId = model.ProjectId,
                Content = model.Comment,
                CreadtedBy = model.CreadtedBy,
                CreatedById = model.CreatedById,
                CreatedAt = DateTime.Now,
            };

            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return newComment.Id;
        }

        public async Task<List<CommentsModel>> GetCommentsByProjectId(int projectId)
        {
            return await _context.Comments.Where(x => x.ProjectId == projectId)
                 .Select(comments => new CommentsModel()
                 {
                     CommentId = comments.Id,
                     Comment = comments.Content,
                     ProjectId = comments.ProjectId,
                     CreadtedBy = comments.CreadtedBy,
                     CreatedById = comments.CreatedById,
                     CreatedAt = comments.CreatedAt,
                 }).ToListAsync();
        }
    }
}
