using BloggingPlatform.DataService;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BloggingPlatform.DataService.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BloggingPlatformContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BloggingPlatformContext>>()))
            {
                // Look for any posts.
                if (context.Post.Any())
                {
                    return;   // DB has been seeded
                }
                var postsFaker = new Faker<Post>()
                    .RuleFor(p => p.title, f => f.Lorem.Sentence(8))
                    .RuleFor(p => p.description, f => f.Lorem.Paragraph(5))
                    .RuleFor(p => p.publication_date, f => f.Date.Between(new DateTime(2021, 01, 01), new DateTime(2022, 07, 1)))
                    .RuleFor(p => p.user, f => f.Name.FullName());

                var posts = postsFaker.Generate(50);
                context.Post.AddRange(posts);
                context.SaveChanges();

            }
        }
    }
}