using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Decision.Framework.Domain.Entities;

namespace Decision.Framework.Domain.Services
{
    public interface IDeleteService
    {
        void Delete(Entity model);
        Task DeleteAsync(Entity model);
    }
}
