using BookLibServ.Models.EntityModel;
using BookLibServ.service.Interface;
using WordBook.reposit.Interface;

namespace WordBook.reposit
{
    public class _userRep: IAuthRep
    {
        private readonly ApplicationDbContext db;
        private readonly IHashPassword _hashService;

        public _userRep(ApplicationDbContext context, IHashPassword hashService)
        {
            db = context;
            _hashService = hashService;
        }

        private void SetUsed(RefreshToken token)
        {
            token.Used = true;
            db.RefreshToken.Update(token);
            db.SaveChanges();
        }

        public void Create(RefreshToken refTokenToResponse)
        {
            db.RefreshToken.Add(refTokenToResponse);
            db.SaveChanges();
        }

        public User? Auth(string? name, string? pass)
        {
            User? user = db.User.FirstOrDefault(p => p.Name == name);
            if (user != null)
            {
                if(_hashService.Verif(pass, user.Password))
                {
                    return user;
                }
            }
            return null;
        }

        public bool Reg(string name, string pass, string email)
        {
            User? IsNameReserv = db.User.FirstOrDefault(p => p.Name == name);
            User? IsEmailReserve = db.User.FirstOrDefault(p => p.Email == email);

            if (IsNameReserv == null && IsEmailReserve == null)
            {
                User user = new User
                {
                    Name = name,
                    Email = email,
                    Password = _hashService.HashPass(pass)
                };
                db.User.Add(user);
                db.SaveChanges();

                return true ;
            }

            return false;
        }

        public User? GetUserByToken(RefreshToken token) 
        {
            return db.User.FirstOrDefault(p => p.Id == token.UserId);
        }

        public RefreshToken? TokenFind(string token)
        {
            RefreshToken? tkn = db.RefreshToken.SingleOrDefault(p => p.Token == token);

            if (tkn == null 
                || tkn.ExpiryData < DateTime.UtcNow 
                || tkn.Used)
            {
                return null;
            }

            SetUsed(tkn);

            return tkn;
        }

        public bool DeleteToken (string token)
        {
            try
            {
                var tk = db.RefreshToken.SingleOrDefault(p => p.Token == token);
                db.RefreshToken.Remove(tk);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
