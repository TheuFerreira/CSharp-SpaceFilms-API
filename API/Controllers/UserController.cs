using API.Domain.User.Cases;
using API.Domain.User.Errors;
using API.Domain.User.Models;
using API.Domain.User.Repositories;
using API.Domain.User.Requests;
using API.Domain.User.Responses;
using API.Domain.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UserController : ControllerBase
    {
        private readonly CreateUserCase createUserCase;
        private readonly GenerateUserTokenCase generateUserTokenCase;
        private readonly GetUserByEmailAndPasswordCase getUserByEmailAndPasswordCase;
        private readonly ResetUserPasswordCase resetUserPasswordCase;
        private readonly GetUserByIdCase getUserByIdCase;

        public UserController(ITokenService tokenService, IUserRepository userRepository, Token token)
        {
            createUserCase = new CreateUserCase(userRepository);
            generateUserTokenCase = new GenerateUserTokenCase(tokenService, token);
            getUserByEmailAndPasswordCase = new GetUserByEmailAndPasswordCase(userRepository);
            resetUserPasswordCase = new ResetUserPasswordCase(userRepository);
            getUserByIdCase = new GetUserByIdCase(userRepository);
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register(RegisterUserRequest request)
        {
            try
            {
                createUserCase.Execute(request);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (UserException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("SignIn")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserSignInResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SignIn(UserSignInRequest request)
        {
            try
            {
                UserModel user = getUserByEmailAndPasswordCase.Execute(request.Email, request.Password);
                UserSignInResponse response = generateUserTokenCase.Execute(user.UserId);
                return Ok(response);
            }
            catch (UserEmailNotCheckedException)
            {
                return BadRequest();
            }
            catch (UserException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                resetUserPasswordCase.Execute(request.Email);
                return Ok();
            }
            catch (UserEmailNotExistsException)
            {
                return NotFound(request);
            }
        }

        [HttpGet]
        [Route("Info")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInfoResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetInfo()
        {
            ClaimsIdentity identity = (ClaimsIdentity)HttpContext.User.Identity!;
            int userId = int.Parse(identity.FindFirst("UserId")!.Value);

            try
            {
                UserModel user = getUserByIdCase.Execute(userId);
                UserInfoResponse response = new()
                {
                    Name = user.Name,
                };

                return Ok(response);
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
        }
    }
}