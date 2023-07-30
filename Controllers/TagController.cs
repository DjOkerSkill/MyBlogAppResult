using BlogAppMy.Models;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlogApp.Controllers
{
    public class TagController : Controller
    {
        public BlogContext Context { get; set; }
        public TagController(BlogContext context)
        {
            Context = context;
        }

        public IActionResult Create(Guid id, string name)
        {
            
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
        }

        public string Delete(string name)
        {
            
            {
                Tag tag = new Tag();
                tag = Context.Tags.Where(x => x.Name == name) as Tag;
                Context.Tags.Remove(tag);
                Context.SaveChanges();
                return "Успешно удалено";
            }
        }

        public string Update(string nameOld, string nameNew)
        {
            
            {
                Tag tag = new Tag();
                tag = Context.Tags.Where(x => x.Name == nameOld) as Tag;
                tag.Name = nameNew;
                Context.Tags.Update(tag);
                Context.SaveChanges();
                return "Успешно обновлено";
            }
        }

        public IActionResult ReadAll()
        {
            List<Tag> tags = new List<Tag>();

            
            {
                tags = Context.Tags.ToList();
            }
            return View("ReadAll", tags);
        }

        public IActionResult ReadForId(Guid id)
        {
            
            {
                Tag tag = Context.Tags.Where(x => x.Id == id) as Tag;

                return View("ReadForIdResult", tag);
            }
        }
    }
}
