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
        IRepository<CURRENT_STATUS> statusRepository;

        public ComponentController(IRepository<COMPONENT> componentRepositoryParam, IGetList getListParam,
            IRepository<CURRENT_STATUS> statusRepositoryParam)
        {
            componentRepository = componentRepositoryParam;
            getList = getListParam;
            statusRepository = statusRepositoryParam;
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
                    DATE_ADD = item.DATE_ADD,
                    ID_MODEL = item.ID_MODEL,
                    ID_SERIES = item.ID_SERIES,
                    ID_TYPE = item.ID_TYPE,                   
                    PARTNUMBER = item.PARTNUMBER,
                    currentStatus = item.CURRENT_STATUS.Where(x => x.ID_COMPLECT.Equals(item.ID))
                        .OrderByDescending(x => x.DATE_STATUS).FirstOrDefault().STATUS.NAME,
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

        [HttpGet]
        public ActionResult StatusChange(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            COMPONENT component = componentRepository.GetAllList().
                FirstOrDefault(x => x.ID.Equals(Convert.ToDecimal(id)));

            if (component == null)
            {
                return HttpNotFound();
            }

            decimal status = component.CURRENT_STATUS.Where(x => x.ID_COMPLECT.Equals(component.ID))
                .OrderByDescending(x => x.DATE_STATUS).FirstOrDefault().ID_STATUS;
            string statusName = component.CURRENT_STATUS.Where(x => x.ID_COMPLECT.Equals(component.ID))
                .OrderByDescending(x => x.DATE_STATUS).FirstOrDefault().STATUS.NAME;

            CurrentStatusViewModel modelData = new CurrentStatusViewModel()
            {
                ID = component.ID,
                ID_COMPLECT = component.ID,
                ID_STATUS = status,
                statusList = getList.getStatusSelectList()                                       
            };

            return View(modelData);
        }

        [HttpPost]
        public ActionResult StatusChange(CurrentStatusViewModel modelData)
        {
            if (ModelState.IsValid)
            {
                CURRENT_STATUS status = new CURRENT_STATUS()
                {                  
                    ID_COMPLECT = modelData.ID_COMPLECT,
                    ID_STATUS = modelData.ID_STATUS,
                    DATE_STATUS = DateTime.Now
                };

                statusRepository.Create(status);
                statusRepository.Save();
                return RedirectToAction("Index");
            }
            else
                return HttpNotFound();
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