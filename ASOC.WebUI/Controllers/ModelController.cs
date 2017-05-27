using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using ASOC.WebUI.ViewModels;
using PagedList;
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
        IGetList getList;
        IRepository<TYPE> typeRepository;

        public ModelController(IRepository<MODEL> modelRepositoryParam, IGetList getListParam,
            IRepository<TYPE> typeRepositoryParam)
        {
            modelRepository = modelRepositoryParam;
            getList = getListParam;
            typeRepository = typeRepositoryParam;
        }

        // GET: Role
        public ActionResult Index(int? page, ModelViewModel modelData)
        {
            if (modelData.searchString != null)
            {
                page = 1;
            }
            else
            {
                modelData.ID_TYPE = modelData.ID_TYPE;               
            }

            modelData.currentFilter = modelData.searchString;

            var models = modelRepository.GetAllList();

            if (!String.IsNullOrEmpty(modelData.searchString))
            {
                models = models.Where(s => s.NAME.Contains(modelData.searchString)).OrderBy(s => s.NAME);
            }

            if(modelData.ID_TYPE!=0)
            {
                var type = typeRepository.GetAllList().First(m => m.ID.Equals(modelData.ID_TYPE)); 
                models = models.Where(s => s.TYPE.NAME.Contains(type.NAME)).OrderBy(s => s.NAME);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ModelViewModel model = new ModelViewModel
            {
                modelList = models.ToPagedList(pageNumber, pageSize),
                typeList = getList.getTypeSelectList(),
                searchString = modelData.searchString,
                currentFilter = modelData.currentFilter,
                ID_TYPE = modelData.ID_TYPE
            };           
            return View(model);
        }    

        // GET: Delete
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            MODEL status = modelRepository.GetAllList().First(x => x.ID.Equals(Convert.ToDecimal(id)));

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
            MODEL model = modelRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(Convert.ToDecimal(id)));
            if (model == null)
            {
                return HttpNotFound();
            }

            ModelViewModel modelData = new ModelViewModel()
            {
                ID = model.ID,
                ID_TYPE = model.ID_TYPE,
                NAME = model.NAME,
                typeList = getList.getTypeSelectList()
            };
            return View(modelData);
        }

        // POST: Edit              
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelViewModel modelData)
        {
            if (ModelState.IsValid)
            {
                var model = new MODEL()
                {
                    ID = modelData.ID,
                    ID_TYPE = modelData.ID_TYPE,
                    NAME = modelData.NAME
                };
                modelRepository.Update(model);
                modelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(modelData);
        }

        // Get: Create
        public ActionResult Create()
        {
            ModelViewModel modelData = new ModelViewModel()
            {               
                typeList = getList.getTypeSelectList()
            };
            return View(modelData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModelViewModel modelData)
        {
            if (ModelState.IsValid)
            {
                MODEL model = new MODEL()
                {
                    ID_TYPE = modelData.ID_TYPE,
                    NAME = modelData.NAME
                };
                modelRepository.Create(model);
                modelRepository.Save();
                return RedirectToAction("Index");
            }
            return View(modelData);
        }
    }
}