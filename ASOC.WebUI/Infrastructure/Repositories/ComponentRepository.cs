using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOC.WebUI.Infrastructure.Repositories
{
    public class ComponentRepository: IRepository<COMPONENT>
    {
        private Entities db = new Entities();

        public void Create(COMPONENT n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Added;
        }
        public void Update(COMPONENT n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Modified;
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            COMPONENT u = db.COMPONENT.Find(id);     
            db.COMPONENT.Remove(u);
        }

        public IEnumerable<COMPONENT> GetAllList()
        {
            return db.COMPONENT.ToList();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}