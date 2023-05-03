using P013EStore.Core.Entities;
using P013EStore.Data;
using P013EStore.Data.Concrete;
using P013EStore.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013EStore.Service.Concrete
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(DatabaseContext context) : base(context)
        {

        }
    }
}
