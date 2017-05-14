using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Controllers
{
    public class PriceController : Controller
    {
        IRepository<PRICE> priceRepository;

        public PriceController(IRepository<PRICE> priceRepositoryParam)
        {
            priceRepository = priceRepositoryParam;
        }

        // GET: Role
        public ActionResult Index()
        {
            var model = priceRepository.GetAllList();
            return View(model);
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            PRICE price = priceRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));

            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            priceRepository.Delete(id);
            priceRepository.Save();
            return RedirectToAction("Index");
        }

        // Get: Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            PRICE price = priceRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));
            if (price == null)
            {
                return HttpNotFound();
            }
            return View(price);

        }

        // POST: Edit              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PRICE price)
        {
            if (ModelState.IsValid)
            {
                priceRepository.Update(price);
                priceRepository.Save();
                return RedirectToAction("Index");
            }
            return View(price);
        }

        // Get: Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PRICE price)
        {
            if (ModelState.IsValid)
            {
                priceRepository.Create(price);
                priceRepository.Save();
                return RedirectToAction("Index");
            }
            return View(price);
        }
    }
}