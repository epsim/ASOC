using ASOC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOC.WebUI.ViewModels
{
    public class ComponentViewModel: COMPONENT
    {
        public decimal COAST { get; set; }
        public int STATUS { get; set; }
    }
}