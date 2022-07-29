using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloggingPlatform.DataService.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BloggingPlatform.DataService.Interfaces;

namespace BloggingPlatform.BusinessService.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _service;


        public PostController(IPostService service)
        {
            _service = service;
        }
        static HttpClient client = new HttpClient();
        string baseURL = "https://sq1-api-test.herokuapp.com";
        // Search
        // GET: Post       

        // Search
        // GET: Post        
        //[ResponseCache(Duration = 60)]
        public async Task<IActionResult> Index(string searchDate = "", string searchString = "")
        {
            var data = await _service.GetAll();
            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(searchDate))
            {
                return View(data);
            }

            var posts = data.Where(p =>
                                    (p.title!.Contains(searchString) || p.description!.Contains(searchString))
                                    );

            DateTime val;
            if (!string.IsNullOrEmpty(searchDate) && DateTime.TryParse(searchDate, out val) == true)
            {
                posts = data.Where(p => p.publication_date == val);
            }

            return View(posts.ToList());
        }
        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View(new Post());
            }
            var post = await _service.GetById(id.Value);

            if (post == null)
            {
                return View(new Post());
            }

            return View(post);
        }

        // GET: Post/Create
        // [Authorize]
        public IActionResult Create()
        {
            return View(new Post());
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Create([Bind("postId,title,description,publication_date,user")] Post post)
        {
            post.user = User != null ? (string.IsNullOrEmpty(User.Identity.Name) ? "" : User.Identity.Name) : "";
            post.publication_date = DateTime.Today;
            post.postId = await _service.Add(post);
            RedirectToAction(nameof(Index));
            return View(post);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Import()
        {
            Console.WriteLine("import");
            // import data from other blogging platform
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("posts");

                if (getData.IsSuccessStatusCode)
                {
                    var results = getData.Content.ReadAsStringAsync().Result;

                    JObject jObject = JObject.Parse(results);
                    JArray jArray = (JArray)jObject["data"];
                    jArray.ToList().ForEach(item => item["user"] = User.Identity.Name);
                    Post[] posts = jArray.ToObject<Post[]>();

                    if (posts != null)
                    {
                        _service.AddMultiple(posts);
                    }
                    // RedirectToAction(nameof(Index));
                    return View(posts);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        private async Task<bool> PostExists(int? id)
        {
            var data = await _service.GetById(id.Value);
            return data is not null;
        }
    }
}
