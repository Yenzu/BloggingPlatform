using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingPlatform.DataService.Models;

namespace BloggingPlatform.DataService.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetById(int id);
        Task<int> Add(Post post);

        void AddMultiple(IEnumerable<Post> post);
    }
}