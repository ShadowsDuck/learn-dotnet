using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Dtos.Comment;
using learn_dotnet.Models;

namespace learn_dotnet.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment newComment);
        Task<Comment?> UpdateAsync(int id, Comment updateComment);
        Task<Comment?> DeleteAsync(int id);
    }
}