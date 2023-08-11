using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppMy.Controllers
{
    public class ArticleController : Controller
    {

        public BlogContext Context { get; set; }

        public ILogger Logger { get; set; }
        public ArticleController(BlogContext context, ILogger<ArticleController> logger)
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
        public async Task<IActionResult> Create(string title, string description, string login)
        {
            Logger.LogInformation("Create method was called");

            try
            {

                User user = await Context.Users.Where(x => x.Login == login).FirstOrDefaultAsync();


                Article article = new Article()
                {
                    Title = title,
                    Description = description,
                    User = user,
                    UserId = user.Id
                };

                await Context.Articles.AddAsync(article);
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
        public async Task<IActionResult> Update(string title, string descriptionNew)
        {
            Logger.LogInformation("Update method was called");

            try
            {

                Article article = new Article();
                article = await Context.Articles.Where(x => x.Title == title).FirstOrDefaultAsync();

                article.Description = descriptionNew;

                Context.Articles.Update(article);
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
                Article article = await Context.Articles.Where(x => x.Title == title).FirstOrDefaultAsync();
                Context.Articles.Remove(article);

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
                Article[] articles = await Context.Articles.ToArrayAsync();

                return View(articles);
            }
            catch(System.Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }


        public IActionResult ReadForTitle()
        {
            return View();
        }

        public async Task<IActionResult> ReadForTitle(string titleArticle) 
        {
            Logger.LogInformation("ReadForTitle method was called");

            try
            {

                Article article = await Context.Articles.Where(x => x.Title == titleArticle).FirstOrDefaultAsync();

                return View("ResultToFind", article);
            }
            catch(System.Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
