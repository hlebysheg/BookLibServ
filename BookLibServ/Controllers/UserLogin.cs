using BookLibServ.Models.EntityModel;
using BookLibServ.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordBook.reposit.Interface;
using WordBook.service.Interface;

namespace WordBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogin : ControllerBase
    {
        private readonly ILogger<UserLogin> _logger;
        private readonly IAuthRep db;
        private readonly IConfiguration _conf;
        private readonly IGenerateJWT _generator;
        public UserLogin(IAuthRep context, IConfiguration config, ILogger<UserLogin>? log, IGenerateJWT generator)
        {
            db = context;
            _conf = config;
            _logger = log;
            _generator = generator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("log")]
        public IActionResult Login([FromBody] UserView logData)
        {
            var user = db.Auth(logData.Name, logData.Password);
            if (user != null)
            {
                var token = _generator.Generate(user);
                var TokenContext = _generator.GenerateRandomStr(25);
                var refTokenToResponse = new RefreshToken
                {
                    Used = false,
                    CreationTime = DateTime.UtcNow,
                    ExpiryData = DateTime.UtcNow.AddMonths(6),
                    UserId = user.Id,
                    User = user,
                    Token = TokenContext + Guid.NewGuid()
                };
                db.Create(refTokenToResponse);

                TokenView resp = new TokenView
                {
                    RefreshToken = refTokenToResponse.Token,
                    AccesToken = _generator.Generate(user)
                };

                _logger.LogInformation(logData.Name + " login now");

                return Ok(resp);
            }
            return NotFound("User not found");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenView token)
        {
            string refreshToken = token.RefreshToken;

            var storedToken = db.TokenFind(refreshToken);
            if(storedToken == null)
            {
                return BadRequest("auth fail");
            }

            var user = db.GetUserByToken(storedToken);

            if (user == null)
            {
                return BadRequest("user not found");
            }
            //generate token
            var TokenContext = _generator.GenerateRandomStr(25);
            var refTokenToResponse = new RefreshToken
            {
                Used = false,
                CreationTime = DateTime.UtcNow,
                ExpiryData = DateTime.UtcNow.AddMonths(6),
                UserId = user.Id,
                User = user,
                Token = TokenContext + Guid.NewGuid()
            };
            db.Create(refTokenToResponse);

            TokenView resp = new TokenView
            {
                RefreshToken = refTokenToResponse.Token,
                AccesToken = _generator.Generate(user)
            };

            return Ok(resp); 
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout([FromBody] TokenView token)
        {
            string refreshToken = token.RefreshToken;

            var storedToken = db.TokenFind(refreshToken);
            if (storedToken == null)
            {
                return NotFound("token not found");
            }

            if (db.DeleteToken(refreshToken))
            {
                return Ok("Logout");
            }

            return BadRequest("token not found");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reg")]
        public IActionResult Register([FromBody] UserView user)
        {
            var isCreated = db.Reg(user.Name, user.Password, user.Email);
            //var isReg = true;
            if(isCreated)
            {
                return Ok("create");
            }
            return BadRequest("try another login or email");
        }
        
    }
}
