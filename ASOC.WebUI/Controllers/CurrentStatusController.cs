using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Controllers
{
    public class CurrentStatusController : Controller
    {
        IRepository<CURRENT_STATUS> currentStatusRepository;

        public CurrentStatusController(IRepository<CURRENT_STATUS> currentStatusRepositoryParam)
        {
            currentStatusRepository = currentStatusRepositoryParam;
        }

        // GET: Role
        public ActionResult Index()
        {
            var model = currentStatusRepository.GetAllList();
            return View(model);
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            CURRENT_STATUS currentStatus = currentStatusRepository.GetAllList().FirstOrDefault(x => x.ID_SERIES.Equals(id));

            if (currentStatus == null)
            {
                return HttpNotFound();
            }
            return View(currentStatus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            currentStatusRepository.Delete(id);
            currentStatusRepository.Save();
            return RedirectToAction("Index");
        }

        // Get: Edit
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }
            CURRENT_STATUS currentStatus = currentStatusRepository.GetAllList().FirstOrDefault(x => x.ID_STATUS.Equals(id));
            if (currentStatus == null)
            {
                return HttpNotFound();
            }
            return View(currentStatus);

        }

        // POST: Edit              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CURRENT_STATUS currentStatus)
        {
            if (ModelState.IsValid)
            {
                currentStatusRepository.Update(currentStatus);
                currentStatusRepository.Save();
                return RedirectToAction("Index");
            }
            return View(currentStatus);
        }

        // Get: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CURRENT_STATUS currentStatus)
        {
            if (ModelState.IsValid)
            {
                currentStatusRepository.Create(currentStatus);
                currentStatusRepository.Save();
                return RedirectToAction("Index");
            }
            return View(currentStatus);
        }
    }
}