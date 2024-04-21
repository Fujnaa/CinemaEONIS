using CinemaBackend.Models.DTOs.CustomerDTOs;
using CinemaBackend.Models.DTOs.UserDTOs;
using CinemaBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinemaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public static UserHashedDto user = new UserHashedDto();
        private readonly IConfiguration _configuration;
        private ICustomerService _customerService;
        private IWorkerService _workerService;

        public AuthController(IConfiguration configuration, ICustomerService customerService ,IWorkerService workerService)
        {
            _configuration = configuration;
            _customerService = customerService;
            _workerService = workerService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserHashedDto>> Register(UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.UserPassword);

            Customer toRegister = new Customer();

            toRegister.CustomerName = request.UserName;
            toRegister.CustomerEmailAdress = request.UserEmailAdress;
            toRegister.CustomerPhoneNumber = request.UserPhoneNumber;
            toRegister.CustomerPasswordHash = passwordHash;
            toRegister.CustomerMembershipLevel = "Bronze";

            Customer registered = await _customerService.CreateCustomer(toRegister);

            return Ok(registered);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserHashedDto>> Login(UserLoginDto request)
        {

            UserHashedDto userHashedDto = new UserHashedDto();

            Customer loggedCustomer = await _customerService.GetCustomerByEmail(request.UserEmailAdress);

            if(loggedCustomer != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.UserPassword, loggedCustomer.CustomerPasswordHash))
                {
                    return BadRequest("Wrong password.");
                }

                userHashedDto.UserName = loggedCustomer.CustomerName;
                userHashedDto.UserEmailAdress = loggedCustomer.CustomerEmailAdress;
                userHashedDto.UserPhoneNumber = loggedCustomer.CustomerPhoneNumber!;

                string tokenCustomer = CreateToken(userHashedDto, "Customer");

                return Ok(tokenCustomer);

            }

            Worker loggedWorker = await _workerService.GetWorkerByEmail(request.UserEmailAdress);

            if (loggedWorker != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.UserPassword, loggedWorker.WorkerPasswordHash))
                {
                    return BadRequest("Wrong password.");
                }

                userHashedDto.UserName = loggedWorker.WorkerName;
                userHashedDto.UserEmailAdress = loggedWorker.WorkerEmailAdress;
                userHashedDto.UserPhoneNumber = loggedWorker.WorkerPhoneNumber!;

            } else return NotFound("User not found.");



            string token = CreateToken(userHashedDto, "Worker");

            return Ok(token);
        }

        private string CreateToken(UserHashedDto user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserEmailAdress),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
