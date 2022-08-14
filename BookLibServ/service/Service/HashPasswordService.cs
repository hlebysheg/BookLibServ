using BookLibServ.service.Interface;

namespace BookLibServ.service.Service
{
    public class HashPasswordService: IHashPassword
    {
        public string HashPass(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password + "adasd");
        }
        public bool Verif(string pass, string hashPass)
        {
            return BCrypt.Net.BCrypt.Verify(pass + "adasd", hashPass);
        }
    }
}
