using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.Repository.Interfaces
{
    public interface IBaseRepository<T> 
    {
        IEnumerable<T> SelectCommond(string sql, object parameters = null);
        IEnumerable<T> GetAll(string tableName);
        T GetById(int? id);
    }
}
