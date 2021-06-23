using ElevenNote.Data;
using ElevenNote.Models;
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
    public class NoteController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);
        }

        //Get /Note
        public ActionResult Create()
        {
            return View();
        }
        //Post: /Note
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate note)
        {
            if (ModelState.IsValid)
            {
                return View(note);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            service.CreateNote(note);

            return RedirectToAction("Index");
        }
    }
}