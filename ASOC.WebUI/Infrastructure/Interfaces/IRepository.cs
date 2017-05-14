using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOC.WebUI.Infrastructure.Interfaces
{
    public interface IRepository<T>: IDisposable
        where T : class
    {
        void Create(T u); // создание объекта
        void Update(T u); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений      
        IEnumerable<T> GetAllList();
    }
}
