using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Controllers
{
    public class CommentController : Controller
    {
        public BlogContext Context { get; set; }
        public CommentController(BlogContext context)
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
        public async Task<IActionResult> Create(string title, string titleArticle)
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

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string title)
        {

            Comment comment = await Context.Comments.Where(x => x.Title == title).FirstOrDefaultAsync();

            Context.Comments.Remove(comment);

            await Context.SaveChangesAsync();

            return Redirect("/Home/Index");
        }

        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(string titleOld, string titleNew)
        {

            Comment comment = await Context.Comments.Where(x => x.Title == titleOld).FirstOrDefaultAsync();

            comment.Title = titleNew;
            Context.Comments.Update(comment);
            await Context.SaveChangesAsync();

            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> ReadAll()
        {
            Comment[] comments = await Context.Comments.ToArrayAsync();

            return View(comments);
        }
    }
}
