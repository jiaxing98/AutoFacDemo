using AutoFacDemo.Data;
using AutoFacDemo.Dtos;
using AutoFacDemo.Models;
using AutoMapper;
using MongoDB.Driver;

namespace AutoFacDemo.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoDBContext _context;
        private readonly IMongoCollection<Book> _collection;
        private readonly IMapper _mapper;

        private readonly FilterDefinitionBuilder<Book> _builder = Builders<Book>.Filter;

        public BookRepository(IMongoDBContext context, IMapper mapper)
        {
            _context = context;
            _collection = _context.GetCollection<Book>("Books");
            _mapper = mapper;
        }

        public async Task Add(BookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _collection.InsertOneAsync(book);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _collection.Find(_ => true).ToListAsync();
            return books;
        }

        public async Task<Book> GetBook(Guid bookId)
        {
            var filter = _builder.Eq(x => x.Id, bookId);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task DeleteBook(Guid bookId)
        {
            var filter = _builder.Eq(x => x.Id, bookId);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
