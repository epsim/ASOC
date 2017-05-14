using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Controllers
{
    public class ModelController : Controller
    {
        IRepository<MODEL> modelRepository;

        public ModelController(IRepository<MODEL> modelRepositoryParam)
        {
            modelRepository = modelRepositoryParam;
        }

        // GET: Role
        public ActionResult Index()
        {
            var model = modelRepository.GetAllList();
            return View(model);
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            MODEL model = modelRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modelRepository.Delete(id);
            modelRepository.Save();
            return RedirectToAction("Index");
        }

        // Get: Edit
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            MODEL model = modelRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);

        }

        // POST: Edit              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MODEL model)
        {
            if (ModelState.IsValid)
            {
                modelRepository.Update(model);
                modelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Get: Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MODEL model)
        {
            if (ModelState.IsValid)
            {
                modelRepository.Create(model);
                modelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}