using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Contexts;
using ErrorSystem.Models.Entities;
using ErrorSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ErrorSystem.Services
{
    internal class CommentService
    {
        private static readonly DataContext _context = new();



        // CREATE
        public static async Task CreateAsync(Comment comment)
        {

            var _commentEntity = new CommentEntity
            {
                CommentText = comment.CommentText,
                CommentCreated = DateTime.Now
            };

            _context.Add(_commentEntity);
            await _context.SaveChangesAsync();
        }
         
        // GET ALL
        public static async Task<IEnumerable<Comment>> GetAllAsync()
        {
            var _comments = new List<Comment>();

            foreach (var _comment in await _context.Comments
                .Include(x => x.Customer)
                .ToListAsync())
                _comments.Add(new Comment
                {
                    CommentText = _comment.CommentText,
                    CommentCreated = DateTime.Now,

                    CustomId = _comment.CustomId,
                    FirstName = _comment.Customer.FirstName,
                    LastName = _comment.Customer.LastName,


                });

            return _comments;
        }

        // GET ONE
        public static async Task<Comment> GetAsync(int CommentId)
        {
            var _comment = await _context.Comments
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.CommentId == CommentId);
            if (_comment != null)
                return new Comment
                {
                  CommentId = _comment.CommentId,
                  CommentCreated = _comment.CommentCreated,
                  CommentText = _comment.CommentText,

                  CustomId= _comment.CustomId,
                  FirstName = _comment.Customer.FirstName,
                  LastName = _comment.Customer.LastName,
                };
            else
                return null!;
        }

    }
}
