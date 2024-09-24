using Core.EntityRepositoryBase.EfRepository;
using Domain.Entities;
using Infrastructure.DBContext;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookDal : EfEntityRepositoryBase<Book, DBConnection>, IBookDal
    {
    }
}
