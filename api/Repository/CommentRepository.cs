using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learn_dotnet.Data;
using learn_dotnet.Dtos.Comment;
using learn_dotnet.Interfaces;
using learn_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_dotnet.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment newComment)
        {
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
            return newComment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment updateComment)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = updateComment.Title;
            existingComment.Content = updateComment.Content;
            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}