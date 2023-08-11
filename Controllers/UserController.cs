using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAppMy.Controllers
{
    public class UserController : Controller
    {
        public BlogContext Context { get; set; }
        public ILogger Logger { get; set; }
        public UserController(BlogContext context, ILogger<UserController> logger)
        {
            Context = context;
            Logger = logger;
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
            Logger.LogInformation("Register method was called");

            try
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
            catch(Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }

        public IActionResult ReadAll()
        {
            Logger.LogInformation("ReadAll method was called");

            try
            {
                List<User> users = new List<User>();

                users = Context.Users.ToList();

                return View(users);
            }
            catch(Exception ex) 
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
        public IActionResult Delete(string login)
        {
            Logger.LogInformation("Delete method was called");

            try
            {
                User user = new User();
                user = Context.Users.Where(x => x.Login == login).FirstOrDefault();
                Context.Users.Remove(user);
                Context.SaveChanges();
                return Redirect("/Home/Index");
            }
            catch(Exception ex) 
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
        public IActionResult Update(string login, string name, int age)
        {
            Logger.LogInformation("Update method was called");

            try
            {
                User user = new User();
                user = Context.Users.Where(x => x.Login == login).FirstOrDefault();
                user.Name = name;
                user.Age = age;

                Context.Users.Update(user);
                Context.SaveChanges();

                return Redirect("/Home/Index");
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }

        public IActionResult ReadForLogin() 
        { 
            return View();
        }

        public async Task<IActionResult> ReadForLogin(string login)
        {
            Logger.LogInformation("ReadForLogin method was called");

            try
            {

                User user = await Context.Users.Where(x => x.Login == login).FirstOrDefaultAsync();

                return View("ResultToFind", user);
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }


    }
}
