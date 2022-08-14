
using BookLibServ.mapper.Interface;
using BookLibServ.Models.EntityModel;
using BookLibServ.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using WordBook.reposit.Interface;

namespace BookLibServ.repository.realisation
{
    public class _bookRep : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IBookMaper _mapper;

        public _bookRep(ApplicationDbContext context, IBookMaper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<BookView?> CreateAsync(BookView book)
        {
            TagModel? tag = await _db.Tag.FindAsync(book.Tag.TagId);

            if (tag == null)
                return null;

            BookModel bookModel = _mapper.GetBookModel(book, tag);
            _db.Add(bookModel);
            Save();

            return _mapper.GetBookView(bookModel, _mapper.GetTagView(tag));
        }

        public async Task<TagView?> CreateTagAsync(TagView tag)
        {
            TagModel tagToCreate = _mapper.GetTagModel(tag);

            if (tagToCreate == null)
                return null;

            await _db.Tag.AddAsync(tagToCreate);
            Save();

            return _mapper.GetTagView(tagToCreate);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            BookModel? book = await _db.Books.FindAsync(id);

            if (book == null)
                return false;

            _db.Books.Remove(book);
            Save();

            return true;
        }

        public async Task<List<BookView>> GetAllAsync()
        {
            List<BookModel> books = await _db.Books.ToListAsync();
            List<BookView> result = new List<BookView>();

            foreach (BookModel book in books)
            {
                TagModel? tag =await  _db.Tag.FindAsync(book.TagId);
                result.Add(_mapper.GetBookView(book, _mapper.GetTagView(tag)));
            }
                

            return result;
        }

        public List<BookView> GetByPaggination(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagView>> GetTagsAsync()
        {
            List<TagView> result = new List<TagView>();

            List<TagModel> tags = await _db.Tag.ToListAsync();

            foreach (TagModel tag in tags) result.Add(_mapper.GetTagView(tag));

            return result;
        }

        public async Task<BookView?> UpdateAsync(BookView bookToUpdate)
        {
            BookModel? book = await _db.Books.FindAsync(bookToUpdate.Id);
            TagModel? tag = await _db.Tag.FindAsync(bookToUpdate.Tag.TagId);

            if (book == null || tag == null)
                return null;

            book.Release = bookToUpdate.Release;
            book.Name = bookToUpdate.Name;
            book.Author = bookToUpdate.Author;
            book.Tag = tag;

            _db.Books.Update(book);
            Save();

            return _mapper.GetBookView(book, _mapper.GetTagView(tag)); ;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
