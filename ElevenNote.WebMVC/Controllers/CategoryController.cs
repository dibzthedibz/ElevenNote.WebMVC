using ElevenNote.Models.CatFolder;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var service = CreateCatService();
            var model = service.GetCats();

            return View(model);
        }
        //Get: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post: Category/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatCreate cat)
        {
            if (!ModelState.IsValid)
            {
                return View(cat);
            }
            var service = CreateCatService();
            if (service.CreateCat(cat))
            {
                TempData["SaveResult"] = "Category was Successfully Created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Category Could Not Be Created. Try Again.");
            return View(cat);
        }
        private CategoryService CreateCatService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
    }
}