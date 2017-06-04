using ASOC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.ViewModels
{
    public class CurrentStatusViewModel: CURRENT_STATUS
    {
        public SelectList statusList { get; set; }        
    }
}