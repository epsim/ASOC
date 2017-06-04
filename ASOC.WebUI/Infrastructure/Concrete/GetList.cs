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
        public SelectList getModelSelectList()
        {
            return new SelectList(db.MODEL, "id","name");
        }
        public SelectList getModelSelectList(int id)
        {
            return new SelectList(db.MODEL.Where(c => c.ID_TYPE == id).ToList());
        }
        public SelectList getStatusSelectList()
        {
            return new SelectList(db.STATUS, "id", "name");
        }       
    }
}