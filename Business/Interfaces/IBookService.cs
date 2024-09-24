using Core.Utilities.Results;
using Domain.Entities;
using Domain.EntitiesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<IResult> AddBookList(List<Book> books);
        Task<IResult> SetBookImage(UpdateBookImageDTO updateBookImageDTO);
        IResult Update(BookUpdateDTO bookUpdateDTO);
        IResult Delete(int id);
        IDataResult<Book> GetById(int id);
        IDataResult<List<Book>> GetAll();
        Task<IDataResult<List<Book>>> Search(string term, string[] arguments);
    }
}
