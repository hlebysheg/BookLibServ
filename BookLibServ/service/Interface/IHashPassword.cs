namespace BookLibServ.service.Interface
{
    public interface IHashPassword
    {
        string HashPass(string password);
        bool Verif(string pass, string hashPass);
    }
}
