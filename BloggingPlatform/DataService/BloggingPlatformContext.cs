
using Microsoft.EntityFrameworkCore;
using BloggingPlatform.DataService.Models;
namespace BloggingPlatform.DataService;

public class BloggingPlatformContext : DbContext
{
    public BloggingPlatformContext(DbContextOptions<BloggingPlatformContext> options)
        : base(options)
    {
    }

    public DbSet<BloggingPlatform.DataService.Models.Post> Post { get; set; } = default!;
}
