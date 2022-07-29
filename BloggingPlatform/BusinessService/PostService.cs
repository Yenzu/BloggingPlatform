using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingPlatform.DataService;
using BloggingPlatform.DataService.Interfaces;
using BloggingPlatform.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.BusinessService
{
    public class PostService : IPostService
    {
        private readonly BloggingPlatformContext _context;
        public PostService(BloggingPlatformContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Post post)
        {
            _context.Add(post);
            return await _context.SaveChangesAsync();
        }

        public async void AddMultiple(IEnumerable<Post> posts)
        {
            _context.Post.AddRange(posts);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var result = await _context.Post.ToListAsync();
            return result;
        }

        public async Task<Post> GetById(int id)
        {
            var post = await _context.Post
                 .FirstOrDefaultAsync(m => m.postId == id);
            return post;
        }
    }
}