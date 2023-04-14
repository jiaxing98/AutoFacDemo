using AutoFacDemo.Dtos;
using AutoFacDemo.Models;

namespace AutoFacDemo.Repositories
{
    public interface IBookRepository
    {
        Task Add(BookDto dto);
        Task DeleteBook(Guid bookId);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBook(Guid bookId);
    }
}