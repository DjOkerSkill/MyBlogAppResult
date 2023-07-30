using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppMy.Controllers
{
    public class UserController : Controller
    {
        public BlogContext Context { get; set; }
        public UserController(BlogContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string name, string lastName, int age, string login, string password)
        {
            User user = new User()
            {
                Name = name,
                LastName = lastName,
                Age = age,
                Login = login,
                Password = password
            };

            var entry = Context.Entry(user);
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                await Context.AddAsync(user);
            }
            await Context.SaveChangesAsync();

            return Redirect("/Home/Index");
        }

        public IActionResult ReadAll()
        {
            List<User> users = new List<User>();

            users = Context.Users.ToList();

            return View(users);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string login)
        {
            User user = new User();
            user = Context.Users.Where(x => x.Login == login).FirstOrDefault();
            Context.Users.Remove(user);
            Context.SaveChanges();
            return Redirect("/Home/Index");
        }
        
        
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(string login, string name, int age)
        {
            User user = new User();
            user = Context.Users.Where(x => x.Login == login).FirstOrDefault();
            user.Name = name;
            user.Age = age;

            Context.Users.Update(user);
            Context.SaveChanges();

            return Redirect("/Home/Index");
        }

        public IActionResult ReadForLogin() 
        { 
            return View();
        }

        public async Task<IActionResult> ReadForLogin(string login)
        {
            User user = await Context.Users.Where(x=>x.Login== login).FirstOrDefaultAsync();
            
            return View("ResultToFind",user);
        }


    }
}
