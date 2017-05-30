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
    public class PriceController : Controller
    {
        IRepository<PRICE> priceRepository;

        public PriceController(IRepository<PRICE> priceRepositoryParam)
        {
            priceRepository = priceRepositoryParam;
        }

        // GET: Index
        public ActionResult Index(int? page, PriceViewModel modelData)
        {
            if (modelData.searchString != null)
            {
                page = 1;
            }
            else
            {
                modelData.searchString = modelData.currentFilter;
            }

            modelData.currentFilter = modelData.searchString;

            IEnumerable<PRICE> priceLog = priceRepository.GetAllList();           

            if (!String.IsNullOrEmpty(modelData.searchString))
            {
                decimal searchDigit;
                bool isInt = Decimal.TryParse(modelData.searchString, out searchDigit);

                if (isInt)
                {
                    priceLog = priceLog.Where(s => s.COAST.Equals(searchDigit)).
                        OrderBy(s => s.DATE_ADD);
                }
                else                
                {
                    priceLog = priceLog.Where(s => s.MODEL.NAME.Contains(modelData.searchString)).
                        OrderBy(s => s.DATE_ADD);
                }
            }

            if (modelData.firstDate != null)
                priceLog = priceLog.Where(x => x.DATE_ADD >= modelData.firstDate);
            if (modelData.secondDate != null)
                priceLog = priceLog.Where(x => x.DATE_ADD <= modelData.secondDate);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            PriceViewModel model = new PriceViewModel()
            {
                priceList = priceLog.ToPagedList(pageNumber, pageSize),
                currentFilter = modelData.currentFilter, 
                searchString = modelData.searchString,
                firstDate = modelData.firstDate,
                secondDate = modelData.secondDate
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

            PRICE price = priceRepository.GetAllList().FirstOrDefault(x => x.ID.Equals(Convert.ToDecimal(id)));

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
    }
}