using BookLibServ.Models.EntityModel;

namespace WordBook.service.Interface
{
    public interface IGenerateJWT
    {
        string Generate(User student);
        string GenerateRandomStr(int len);
    }
}
