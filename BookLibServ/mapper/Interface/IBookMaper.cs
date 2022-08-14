using BookLibServ.Models.EntityModel;
using BookLibServ.Models.ViewModel;

namespace BookLibServ.mapper.Interface
{
    public interface IBookMaper
    {
        BookModel GetBookModel(BookView otherModel, TagModel tag);
        BookView GetBookView(BookModel otherModel, TagView tag);
        //tags

        TagModel GetTagModel(TagView otherModel);
        TagView GetTagView(TagModel otherModel);
    }
}
