using BookLibServ.mapper.Interface;
using BookLibServ.Models.EntityModel;
using BookLibServ.Models.ViewModel;

namespace BookLibServ.mapper.realisation
{
    public class BookMapper : IBookMaper
    {

        public BookModel GetBookModel(BookView otherModel, TagModel tag)
        {
            return new BookModel
            {
                Author = otherModel.Author,
                Tag = tag,
                Name = otherModel.Name,
                Release = otherModel.Release,
            };
        }

        public BookView GetBookView(BookModel otherModel, TagView tag)
        {
            return new BookView
            {
                Author = otherModel.Author,
                Tag = tag,
                Name = otherModel.Name,
                Release = otherModel.Release,
                Id = otherModel.Id,
            };
        }

        public TagModel GetTagModel(TagView otherModel)
        {
            return new TagModel
            {
                TagId = otherModel.TagId,
                TagName = otherModel.TagName,
            };
        }

        public TagView GetTagView(TagModel otherModel)
        {
            return new TagView
            {
                TagId = otherModel.TagId,
                TagName = otherModel.TagName,
            };
        }
    }
}
