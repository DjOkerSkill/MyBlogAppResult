using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppMy.Controllers
{
    public class ArticleController : Controller
    {

        public BlogContext Context { get; set; }
        public ArticleController(BlogContext context)
        {
            Context = context;
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

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(string title, string descriptionNew)
        {
            Article article = new Article();
            article = await Context.Articles.Where(x => x.Title == title).FirstOrDefaultAsync();

            article.Description = descriptionNew;

            Context.Articles.Update(article);
            await Context.SaveChangesAsync();

            return Redirect("/Home/Index");
        }

        public IActionResult Delete()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string title)
        {
            Article article = await Context.Articles.Where(x => x.Title == title).FirstOrDefaultAsync();
            Context.Articles.Remove(article);

            await Context.SaveChangesAsync();

            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> ReadAll()
        {
            Article[] articles = await Context.Articles.ToArrayAsync();

            return View(articles);
        }


        public IActionResult ReadForTitle()
        {
            return View();
        }

        public async Task<IActionResult> ReadForTitle(string titleArticle) 
        {
            Article article = await Context.Articles.Where(x => x.Title == titleArticle).FirstOrDefaultAsync();

            return View("ResultToFind", article);
        }
    }
}
