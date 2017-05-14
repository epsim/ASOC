using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Controllers
{
    public class ComponentController : Controller
    {
        IRepository<COMPONENT> componentRepository;

        public ComponentController(IRepository<COMPONENT> componentRepositoryParam)
        {
            componentRepository = componentRepositoryParam;
        }

        // GET: Role
        public ActionResult Index()
        {
            var model = componentRepository.GetAllList();
            return View(model);
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            COMPONENT component = componentRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));

            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            componentRepository.Delete(id);
            componentRepository.Save();
            return RedirectToAction("Index");
        }

        // Get: Edit
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            COMPONENT component = componentRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);

        }

        // POST: Edit              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(COMPONENT component)
        {
            if (ModelState.IsValid)
            {
                componentRepository.Update(component);
                componentRepository.Save();
                return RedirectToAction("Index");
            }
            return View(component);
        }

        // Get: Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(COMPONENT component)
        {
            if (ModelState.IsValid)
            {
                componentRepository.Create(component);
                componentRepository.Save();
                return RedirectToAction("Index");
            }
            return View(component);
        }
    }
}