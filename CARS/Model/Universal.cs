using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS.Model
{ 
    public abstract class Universal<T>
    {
        public const string Name01 = "Mr.Nobody";
        public abstract string Create(T entity);
        public abstract void Read(T entity);
        public abstract string Update(T entity);
        public abstract void Delete(T entity);
        public abstract DataTable dt (T entity);
        //public async Task<IAsyncResult> Asyncher() { }
    }
}
