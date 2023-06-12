using CurrencyExchange.APIService.ActionFilters;
namespace CurrencyExchange.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManger;
        private readonly ITokenRepository tokenRepository;
        private readonly ILogger<AuthController> logger;

        public AuthController(ILogger<AuthController> logger, UserManager<IdentityUser> userManger, ITokenRepository tokenRepository)
        {
            this.userManger = userManger;
            this.tokenRepository = tokenRepository;
           this.logger = logger;
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            logger.LogInformation("AuthorizeUser started to executing");

            var user = await userManger.FindByEmailAsync(username);
            if (user != null)
            {
                var result = await userManger.CheckPasswordAsync(user, password);
                if (result)
                {
                    //Get Roles for this user
                    var roles = await userManger.GetRolesAsync(user);
                    if (roles != null)
                    {
                        return Ok(tokenRepository.CreateJWTToken(user, roles.ToList()));
                    }
                }
            }
            return BadRequest("User Name or Password Incorrect");
        }


        [HttpPost]
        [Route("Register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.UserName
            };
            var identityResult = await userManger.CreateAsync(identityUser, registerRequest.Password);
            if (identityResult.Succeeded)
            {
                //Add Roles to user
                if (registerRequest.Roles != null)
                {
                    await userManger.AddToRolesAsync(identityUser, registerRequest.Roles);
                }
                return Ok("User registered successfully.");
            }
            return BadRequest("Something went wrong");

        }
    }
}
