using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using PagedList;
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
 
        // GET: Index                  
        public ActionResult Index(int? page, string currentFilter, string searchString)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var components = componentRepository.GetAllList();

            if (!String.IsNullOrEmpty(searchString))
            {
                components = components.Where(s => s.NAME.Contains(searchString)).OrderBy(s => s.NAME);
            }
                                 
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(components.ToPagedList(pageNumber, pageSize));
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

        // GET: Details 
        public ActionResult Details(decimal? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var component = componentRepository.GetAllList().Where(x => x.ID.Equals(id));
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