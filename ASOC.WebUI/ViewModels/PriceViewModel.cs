using ASOC.Domain;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOC.WebUI.ViewModels
{
    public class PriceViewModel: PRICE
    {
        public string searchString { get; set; }
        public string currentFilter { get; set; }
        public Nullable<DateTime> firstDate { get; set; }
        public Nullable<DateTime> secondDate { get; set; }
        public IPagedList<PRICE> priceList { get; set; }
    }
}