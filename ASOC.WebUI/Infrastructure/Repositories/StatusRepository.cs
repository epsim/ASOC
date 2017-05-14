using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOC.WebUI.Infrastructure.Repositories
{
    public class StatusRepository : IRepository<STATUS>
    {
        private Entities db = new Entities();

        public void Create(STATUS n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Added;
        }
        public void Update(STATUS n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Modified;
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            STATUS u = db.STATUS.Find(id);
            db.STATUS.Remove(u);
        }

        public IEnumerable<STATUS> GetAllList()
        {
            return db.STATUS.ToList();
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