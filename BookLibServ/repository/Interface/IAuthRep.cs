

using BookLibServ.Models.EntityModel;

namespace WordBook.reposit.Interface
{
    public interface IAuthRep
    {
        void Create(RefreshToken refTokenToResponse);
        User? Auth(string? name, string? pass);
        bool Reg(string name, string pass, string email);
        User? GetUserByToken(RefreshToken token);
        RefreshToken? TokenFind(string token);
        bool DeleteToken(string token);
    }
}
