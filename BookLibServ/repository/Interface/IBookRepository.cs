using BookLibServ.Models.ViewModel;

namespace WordBook.reposit.Interface
{
    public interface IBookRepository
    {
        Task<List<BookView>> GetAllAsync();
        List<BookView> GetByPaggination(int pageNumber, int pageSize);
        Task<BookView> UpdateAsync(BookView bookToUpdate);
        Task<BookView?> CreateAsync(BookView book);
        Task<bool> DeleteAsync(int id);
        Task<List<TagView>> GetTagsAsync();
        Task<TagView?> CreateTagAsync(TagView tag);
        void Save();
    }
}
