using Core.EntityRepositoryBase;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBookDal : IEntityRepository<Book>
    {
    }
}
