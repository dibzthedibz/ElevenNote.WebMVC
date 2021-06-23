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
            var service = CreateNoteService();
            var model = service.GetNotes();

            return View(model);
        }

        //Get /Note/Create
        public ActionResult Create()
        {
            var catList = _db.Categories.ToList();
            ViewBag.CategoryId = new SelectList(catList, "CategoryId", "Title");
            return View();
        }
        //Post: /Note/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate note)
        {
            if (!ModelState.IsValid)
            {
                return View(note);
            }
            var service = CreateNoteService();
            if (service.CreateNote(note))
            {
                TempData["SaveResult"] = "Note was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");
            return View(note);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var catList = _db.Categories.ToList();
            ViewBag.CategoryId = new SelectList(catList, "CategoryId", "Title");
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model = new NoteEdit
            {
                NoteId = detail.NoteId,
                Title = detail.Title,
                Content = detail.Content,
                CategoryId = detail.CategoryId
            };
            return View(model);
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit note)
        {
            if (!ModelState.IsValid) return View(note);

            if (note.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(note);
            }

            var service = CreateNoteService();
            if (service.UpdateNote(note))
            {
                TempData["SaveResult"] = "Note Successfully Updated.";
                return RedirectToAction("Index");
            }

            return View(note);
        }
        //Get: Product/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }
        //Post: Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Note Successfully Deleted";

            return RedirectToAction("Index");
        }
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}