using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Controllers
{
    public class TypeController : Controller
    {
        IRepository<TYPE> typeRepository;

        public TypeController(IRepository<TYPE> typeRepositoryParam)
        {
            typeRepository = typeRepositoryParam;
        }

        // GET: Role
        public ActionResult Index()
        {            
                var model = typeRepository.GetAllList();
                return View(model);                        
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {
           
            if (id == null)
            {
                return HttpNotFound();
            }

            TYPE type = typeRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));

            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);   
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {            
            typeRepository.Delete(id);
            typeRepository.Save();
            return RedirectToAction("Index");         
        }

        // Get: Edit
        public ActionResult Edit(int? id)
        {
          
            if (id == null)
            {
                return HttpNotFound();
            }
            TYPE type = typeRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(id));
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
            
        }

        // POST: Edit              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TYPE type)
        {           
            if (ModelState.IsValid)
            {
                typeRepository.Update(type);
                typeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(type);           
        }

        // Get: Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TYPE type)
        {            
            if (ModelState.IsValid)
            {
                typeRepository.Create(type);
                typeRepository.Save();
                return RedirectToAction("Index");
            }
            return View(type);         
        }
    }
}