using MachineTest6_1.Model;
using MachineTest6_1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MachineTest6_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _config;

        private readonly ILoginRepository _repository;


        //Dependency injection

        public LoginController(IConfiguration config,
            ILoginRepository logiRepository)
        {
            _config = config;
            _repository = logiRepository;
        }

        #region --validate username and password
        // GET api/<Logins/username/password/5
        [HttpGet("{username}/{password}")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCredentials(string username, string password)
        {
            //variable for tracking unauthorized

            IActionResult response = Unauthorized();  //401
            UserLogin validUser = null;

            //step  1-- authenticate the user by passing username and password

            validUser = await _repository.ValidateUser(username, password);

            //step 2--generate JWT tocken

            if (validUser != null)
            {
                //custom method for generating token

                var tokenString = GenerateJWTToken(validUser);

                response = Ok(new
                {
                    uName = validUser.Username,
                    releId = validUser.RoleId,
                    token = tokenString

                });

            }
            return response;

        }

        #endregion


        #region  Generate JWT token
        private string GenerateJWTToken(UserLogin validUser)
        {
            //1--secret key

            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //2--algorithm

            var credentials = new SigningCredentials(
                secretKey, SecurityAlgorithms.HmacSha256);

            //3--JWT

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials

                );


            //4--writing token

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        #endregion


        // GET: api/UserRegistration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistration>>> GetAll()
        {
            var reg = await _repository.GetAllRegistrations();
            if (reg == null)
            {
                return NotFound("No Details found");
            }

            return Ok(reg);
        }

        // GET: api/UserRegistration/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegistration>> GetById(int id)
        {
            var result = await _repository.GetRegistrationById(id);
            if (result == null)
            {
                return NotFound("No user found with ID ");
            }

            return Ok(result);
        }

        // POST: api/UserRegistration
        [HttpPost]

        public async Task<ActionResult<UserRegistration>> PostReg(UserRegistration registration)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee

                var newUserReg = await _repository.AddRegistration(registration);

                if (newUserReg != null)
                {
                    return Ok(newUserReg);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();
        }



        // PUT: api/UserRegistration/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UserRegistration>> Put(int id, UserRegistration registration)
        {
            if (id != registration.RegistrationId)
            {
                return BadRequest("User ID mismatch.");
            }

            var result = await _repository.UpdateRegistration(registration);
            if (result == null)
            {
                return NotFound($"No user found with ID {id}");
            }

            return Ok(result);
        }

    }
}
