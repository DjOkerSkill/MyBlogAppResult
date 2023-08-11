using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlogApp.Controllers
{
    public class TagController : Controller
    {
        public BlogContext Context { get; set; }
        public ILogger Logger { get; set; }
        public TagController(BlogContext context, ILogger<TagController> logger)
        {
            Context = context;
            Logger = logger;
        }

        public IActionResult Create(Guid id, string name)
        {
            Logger.LogInformation("Create method was called");
            try
            {
                if (Context.Tags.Where(x => x.Id == id) != null)
                {
                    Tag tag = new Tag()
                    {
                        Id = id,
                        Name = name
                    };
                    Context.Tags.Add(tag);
                    Context.SaveChanges();
                    ViewBag.Name = "Тэг создан";
                    return View("Create_tag");
                }
                else
                    return NotFound();
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
            
        }

        public string Delete(string name)
        {
            Logger.LogInformation("Delete method was called");
            try
            {
                Tag tag = new Tag();
                tag = Context.Tags.Where(x => x.Name == name) as Tag;
                Context.Tags.Remove(tag);
                Context.SaveChanges();
                return "Успешно удалено";
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return "Error on server";
            }
            

        }

        public string Update(string nameOld, string nameNew)
        {
            Logger.LogInformation("Update method was called");
            try
            {
                Tag tag = new Tag();
                tag = Context.Tags.Where(x => x.Name == nameOld) as Tag;
                tag.Name = nameNew;
                Context.Tags.Update(tag);
                Context.SaveChanges();
                return "Успешно обновлено";
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return "Error on server";
            }
            
        }

        public IActionResult ReadAll()
        {
            Logger.LogInformation("ReadAll method was called");

            try
            {
                List<Tag> tags = new List<Tag>();
                {
                    tags = Context.Tags.ToList();
                }
                return View("ReadAll", tags);
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
        }

        public IActionResult ReadForId(Guid id)
        {
            Logger.LogInformation("ReadForId method was called");
            try
            {
                Tag tag = Context.Tags.Where(x => x.Id == id) as Tag;

                return View("ReadForIdResult", tag);
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex, "Exception {ErrorMessege}", ex.Message);
                return StatusCode(500);
            }
            
        }
    }
}
