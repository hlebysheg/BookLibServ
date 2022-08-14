using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookLibServ.Models.EntityModel
{
    public class RefreshToken
    {
        [Key]
        public int JwtId { get; set; }
        public string Token { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpiryData { get; set; }
        public bool Used { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
