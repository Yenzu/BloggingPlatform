using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingPlatform.BusinessService.Controllers;
using BloggingPlatform.DataService.Interfaces;
using BloggingPlatform.DataService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BloggingPlatform.Tests
{
    public class PostControllerTests
    {
        private readonly Mock<IPostService> _mockRepo;
        private readonly PostController _controller;
        public PostControllerTests()
        {
            _mockRepo = new Mock<IPostService>();
            _controller = new PostController(_mockRepo.Object);
        }
        /*
        Index
        */

        [Fact]
        public async void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnsExactNumberOfPost()
        {
            _mockRepo.Setup(repo => repo.GetAll())
                .ReturnsAsync(new List<Post>() { new Post(), new Post() });
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var posts = Assert.IsType<List<Post>>(viewResult.Model);
            Assert.Equal(2, posts.Count);
        }

        /*
        Details
        */
        [Fact]
        public async void Details_ActionExecutes_ReturnsViewForIndex()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Details_ActionExecutes_ReturnsViewForPost()
        {
            _mockRepo.Setup(repo => repo.GetById(1))
               .ReturnsAsync(new Post() { postId = 1 });
            var result = await _controller.Details(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var post = Assert.IsType<Post>(viewResult.Model);
            Assert.Equal(1, post.postId);
        }


        /*
        Creates
        */
        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_ModelStateValid_CreateEmployeeCalledOnce()
        {
            Post? initPost = null;
            _mockRepo.Setup(r => r.Add(It.IsAny<Post>()))
                .Callback<Post>(x => initPost = x);
            var post = new Post
            {
                postId = -12,
                title = "title test",
                description = "description test ",
                user = "user test ",
                publication_date = DateTime.Today,
            };
            await _controller.Create(post);
            _mockRepo.Verify(x => x.Add(It.IsAny<Post>()), Times.Once);
            Assert.Equal(initPost.title, post.title);
            Assert.Equal(initPost.description, post.description);
            Assert.Equal(initPost.user, post.user);
        }
    }
}