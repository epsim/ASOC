using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASOC.WebUI.Infrastructure.Concrete
{
    public class GetList : IGetList
    {
        private Entities db = new Entities();
        public SelectList getTypeSelectList()
        {
            return new SelectList(db.TYPE, "id","name");
        }
    }
}