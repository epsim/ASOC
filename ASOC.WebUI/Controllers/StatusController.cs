using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Controllers
{
    public class StatusController : Controller
    {
        IRepository<STATUS> statusRepository;

        public StatusController(IRepository<STATUS> statusRepositoryParam)
        {
            statusRepository = statusRepositoryParam;
        }

        // GET: Index
        public ActionResult Index(string filter = null)
        {
            ViewBag.filter = filter;
            return View(GetStatuses(filter));
        }      

        public IEnumerable<STATUS> GetStatuses(string filter)
        {           
            IEnumerable<STATUS> status = statusRepository.GetAllList();           
            return filter == null ? status : status.Where(x => x.NAME.Contains(filter));
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            STATUS status = statusRepository.GetAllList().First(x => x.ID.Equals(Convert.ToDecimal(id)));

            if (status == null)
            {
                return HttpNotFound();
            }

            return View(status);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            statusRepository.Delete(id);
            statusRepository.Save();
            return RedirectToAction("Index");
        }

        // Get: Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            STATUS status = statusRepository.GetAllList().First(x => x.ID.Equals(Convert.ToDecimal(id)));

            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Edit              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(STATUS status)
        {
            if (ModelState.IsValid)
            {
                statusRepository.Update(status);
                statusRepository.Save();
                return RedirectToAction("Index");
            }
            return View(status);
        }

        // Get: Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(STATUS status)
        {
            if (ModelState.IsValid)
            {
                statusRepository.Create(status);
                statusRepository.Save();
                return RedirectToAction("Index");
            }
            return View(status);
        }
    }
}