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
        /// <summary>
        /// API for user login and genereate JWT Token
        ///GET :https://localhost:7035/api/Auth/Login
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            logger.LogInformation("AuthorizeUser started to executing");

            var user = await userManger.FindByEmailAsync(loginRequest.UserName);
            if (user != null)
            {
                var result = await userManger.CheckPasswordAsync(user, loginRequest.Password);
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

        /// <summary>
        /// API to register a new user
        ///GET :https://localhost:7035/api/Auth/Register
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
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
