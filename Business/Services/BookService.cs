using Application.BusinesWorkers;
using Application.Constans;
using Application.Interfaces;
using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using Domain.Entities;
using Domain.EntitiesDTOs;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService : IBookService
    {
        IBookDal _bookDal;
        private readonly IMapper _mapper;

        public BookService(IBookDal bookDal, IMapper mapper)
        {
            _bookDal = bookDal;
            _mapper = mapper;
        }

        [ClaimOperation("admin")]
        [CacheRemoveAspect("IBookService")]
        public async Task<IResult> AddBookList(List<Book> books)
        {
            _bookDal.AddList(books);
            return new SuccessResult(Messages.BooksAdded);
        }
        [CacheRemoveAspect("IBookService")]
        public async Task<IResult> SetBookImage(UpdateBookImageDTO updateBookImageDTO)
        {
            var updatedImageBook = _bookDal.Get(b => b.Id == updateBookImageDTO.BookId);
            if (updatedImageBook == null)
            {
                return new ErrorResult(Messages.BookUnAvailableForUpdate);
            }
            
            byte[] imageDataByte = Convert.FromBase64String(updateBookImageDTO.Image);
            string resolvedBookImageDataPath = await ConvertBase64ToStringAsync(imageDataByte);
            updatedImageBook.Image = resolvedBookImageDataPath;
            _bookDal.Update(updatedImageBook);
            return new SuccessResult(Messages.BookImageUpdated);
        }


        [ClaimOperation("admin")]
        [CacheRemoveAspect("IBookService")]
        public IResult Update(BookUpdateDTO bookUpdateDTO)
        {
            IResult result = AreThereAnyBook(bookUpdateDTO.Id);
            if (!result.Success)
            {
                return result;
            }

            var existingBook = _bookDal.Get(p => p.Id == bookUpdateDTO.Id);
            _mapper.Map(bookUpdateDTO, existingBook);
            _bookDal.Update(existingBook);
            return new SuccessResult(Messages.BookUpdated);
        }

        [ClaimOperation("admin")]
        [CacheRemoveAspect("IBookService")]
        public IResult Delete(int id)
        {
            IResult result = AreThereAnyBook(id);
            if (!result.Success)
            {
                return result;
            }

            var bookToDelete = _bookDal.Get(b => b.Id == id);
            if (bookToDelete == null)
            {
                return new ErrorResult(Messages.BookUnAvailable);
            }
            _bookDal.Delete(bookToDelete);
            return new SuccessResult(Messages.BookDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Book>> GetAll()
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Book> GetById(int id)
        {
            return new SuccessDataResult<Book>(_bookDal.Get(p => p.Id == id));
        }

        [CacheAspect]
        public async Task<IDataResult<List<Book>>> Search(string term, string[] arguments)
        {
            var books = await _bookDal.Search(term, arguments, 15);
            return new SuccessDataResult<List<Book>>(books);
        }

        private IResult DidThisBookAlreadyExist(int bookId)
        {
            var result = _bookDal.GetAll(p => p.Id == bookId).Any();
            if (result)
            {
                return new ErrorResult(Messages.BookIsAvailable);
            }
            return new SuccessResult();
        }

        private IResult AreThereAnyBook(int bookId)
        {
            var result = _bookDal.GetAll(p => p.Id == bookId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.BookUnAvailable);
            }
            return new SuccessResult();
        }

        private async Task<string> ConvertBase64ToStringAsync(byte[] imageData)
        {
            
            string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
            string filePath = Path.Combine("C:\\Users\\Doğan\\source\\repos\\ErenKitapcilik\\WebApi\\Images", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.WriteAsync(imageData, 0, imageData.Length);
            }
            string uniqueFileNameWithLocalUrl = $"http://localhost:5000/images/{uniqueFileName}";
            return uniqueFileNameWithLocalUrl;
        }

        
    }
}
