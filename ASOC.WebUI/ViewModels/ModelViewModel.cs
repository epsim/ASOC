using ASOC.Domain;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.ViewModels
{
    public class ModelViewModel : MODEL
    {
        public SelectList typeList { get; set; }
        public IPagedList<MODEL> modelList { get; set; }
        public string searchString { get; set; }
        public string currentFilter { get; set; }
        public int currentType { get; set; }
    }
}