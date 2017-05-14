using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOC.WebUI.Infrastructure.Repositories
{
    public class ModelRepository: IRepository<MODEL>
    {
        private Entities db = new Entities();

        public void Create(MODEL n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Added;
        }
        public void Update(MODEL n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Modified;
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            MODEL u = db.MODEL.Find(id);
            db.MODEL.Remove(u);
        }

        public IEnumerable<MODEL> GetAllList()
        {
            return db.MODEL.ToList();
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