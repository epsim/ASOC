using ASOC.Domain;
using ASOC.WebUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASOC.WebUI.Infrastructure.Repositories
{
    public class TypeRepository: IRepository<TYPE>
    {
        private Entities db = new Entities();

        public void Create(TYPE n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Added;
        }
        public void Update(TYPE n)
        {
            db.Entry(n).State = System.Data.Entity.EntityState.Modified;
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            TYPE u = db.TYPE.Find(id);
            db.TYPE.Remove(u);
        }

        public IEnumerable<TYPE> GetAllList()
        {
            return db.TYPE.ToList();
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