using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Interfaces
{
    public interface ICRUDRepository<T>
    {
        public Task<bool> Add(T model);
        public Task<bool> Update(T model);
        public Task<bool> Delete(T model);
    }
}
