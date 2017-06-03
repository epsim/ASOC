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
    public class ComponentController : Controller
    {
        IRepository<COMPONENT> componentRepository;
        IGetList getList;

        public ComponentController(IRepository<COMPONENT> componentRepositoryParam, IGetList getListParam)
        {
            componentRepository = componentRepositoryParam;
            getList = getListParam;
        }

        // GET: Index                  
        public ActionResult Index(int? page, ComponentViewModel modelData)
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

            var components = componentRepository.GetAllList();

            if (modelData.ID_TYPE != null)
            {
                components = components.Where(s => s.ID_TYPE.Equals(modelData.ID_TYPE));
            }           

            decimal searchDigit;
            bool isInt = Decimal.TryParse(modelData.searchString, out searchDigit);

            if (!String.IsNullOrEmpty(modelData.searchString))
            {
                if (!isInt)
                {                    
                    if (components.Where(m => m.TYPE.NAME.Contains(modelData.searchString)).Count() != 0)
                    {
                        components = components.Where(m => m.TYPE.NAME.Contains(modelData.searchString));
                    }
                    if(components.Where(m => m.MODEL.NAME.Contains(modelData.searchString)).Count() != 0)
                    {
                        components = components.Where(s => s.MODEL.NAME.Contains(modelData.searchString));
                    }                   
                }
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<ComponentViewModel> componentList = new List<ComponentViewModel>();

            foreach (COMPONENT item in components)
            {
                componentList.Add(new ComponentViewModel()
                {
                    ID = item.ID,
                    AMOUNT = item.AMOUNT,
                    DATE_ADD = item.DATE_ADD,
                    ID_MODEL = item.ID_MODEL,
                    ID_SERIES = item.ID_SERIES,
                    ID_TYPE = item.ID_TYPE,
                    NAME = item.NAME,
                    PARTNUMBER = item.PARTNUMBER,
                    CURRENT_STATUS = item.CURRENT_STATUS,
                    MODEL = item.MODEL,
                    TYPE = item.TYPE,
                    currentCoast = item.MODEL.PRICE.Where(x => x.ID_MODEL.Equals(item.ID))
                        .OrderByDescending(x => x.DATE_ADD).FirstOrDefault().COAST
                });
            }

            if (!String.IsNullOrEmpty(modelData.searchString))
            {
                if (isInt)
                {
                    componentList = componentList.FindAll(m => m.currentCoast.Equals(searchDigit));
                }
            }

            ComponentViewModel model = new ComponentViewModel
            {
                componentList = componentList.ToPagedList(pageNumber, pageSize),
                typeList = getList.getTypeSelectList(),
                searchString = modelData.searchString,
                currentFilter = modelData.currentFilter,
                statusList = getList.getStatusSelectList()
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