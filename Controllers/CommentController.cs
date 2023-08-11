using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Controllers
{
    public class CommentController : Controller
    {
        public BlogContext Context { get; set; }

        public ILogger Logger { get; set; }
        public CommentController(BlogContext context, ILogger<CommentController> logger)
        {
            Context = context;
            Logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string titleArticle)
        {
            Logger.LogInformation("Create method was called");

            try
            {
                Article article = await Context.Articles.Where(x => x.Title == titleArticle).FirstOrDefaultAsync();

                Comment comment = new Comment()
                {
                    Title = title,
                    Article = article,
                    ArticleId = article.Id
                };

                await Context.Comments.AddAsync(comment);
                await Context.SaveChangesAsync();

                return Redirect("/Home/Index");
            }
            catch(System.Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string title)
        {
            Logger.LogInformation("Delete method was called");

            try
            {
                Comment comment = await Context.Comments.Where(x => x.Title == title).FirstOrDefaultAsync();

                Context.Comments.Remove(comment);

                await Context.SaveChangesAsync();

                return Redirect("/Home/Index");
            }
            catch(System.Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }

        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(string titleOld, string titleNew)
        {

            Logger.LogInformation("Update method was called");

            try
            {

                Comment comment = await Context.Comments.Where(x => x.Title == titleOld).FirstOrDefaultAsync();

                comment.Title = titleNew;
                Context.Comments.Update(comment);
                await Context.SaveChangesAsync();

                return Redirect("/Home/Index");
            }
            catch(System.Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> ReadAll()
        {
            Logger.LogInformation("ReadAll method was called");

            try
            {

                Comment[] comments = await Context.Comments.ToArrayAsync();

                return View(comments);
            }
            catch(System.Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
