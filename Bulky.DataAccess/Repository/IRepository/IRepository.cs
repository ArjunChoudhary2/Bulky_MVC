using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        //T  Category   
        IEnumerable<T> GetAll(string? includeProperties = null);

        T GetFirstOrDefault(Expression<Func<T,bool>> filter, string? includeProperties = null);
        void Add(T entity);
        //Going to include the update logic in the specific classes interfacing IRepository as Update logic has higher complexity
        //void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
