using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOC.WebUI.Infrastructure.Repositories
{
    public class CurrentStatusRepository: IRepository<CURRENT_STATUS>
    {
        private Entities db = new Entities();

        public void Create(CURRENT_STATUS n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Added;
        }
        public void Update(CURRENT_STATUS n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Modified;
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            CURRENT_STATUS u = db.CURRENT_STATUS.Find(id);
            db.CURRENT_STATUS.Remove(u);
        }

        public IEnumerable<CURRENT_STATUS> GetAllList()
        {
            return db.CURRENT_STATUS.ToList();
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