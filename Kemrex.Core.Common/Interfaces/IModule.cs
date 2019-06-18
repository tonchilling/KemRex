using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Interfaces
{
    public interface IModule<TEntity>
        where TEntity : class
    {
        void Set(TEntity ob);
        void Delete(TEntity ob);
    }

    public interface IModule<TEntity, TPk> : IModule<TEntity>
        where TEntity : class
        where TPk : struct
    {
        TEntity Get(TPk id);
        bool IsExist(TPk id);
    }
}
