using Frapper.API.Filters;
using Frapper.API.Helpers;
using Frapper.Common;
using Frapper.Repository.UserMaster.Queries;
using Frapper.ViewModel.Authenticate.Request;
using Frapper.ViewModel.Authenticate.Response;
using Frapper.ViewModel.UserMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Frapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Hiding Login Api from Swagger documenation 
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LoginController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserMasterQueries _userMasterQueries;
        private readonly IUserTokensQueries _userTokensQueries;
        public LoginController(IOptions<AppSettings> appSettings, IUserMasterQueries userMasterQueries, IUserTokensQueries userTokensQueries)
        {
            _appSettings = appSettings.Value;
            _userMasterQueries = userMasterQueries;
            _userTokensQueries = userTokensQueries;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ValidateModel]
        public IActionResult Authenticate([FromBody] AuthenticateRequest authenticateRequest)
        {
            if (ModelState.IsValid)
            {
                if (!_userMasterQueries.CheckUserExists(authenticateRequest.Username))
                {
                    return BadRequest(new BadRequestResponse("Entered Username or Password is Invalid"));
                }
                else
                {
                    var loggedInuserdetails = _userMasterQueries.GetCommonUserDetailsbyUserName(authenticateRequest.Username);

                    if (loggedInuserdetails == null)
                    {
                        return BadRequest(new BadRequestResponse("Username or password is incorrect"));
                    }

                    var usersalt = _userTokensQueries.GetUserSaltbyUserid(loggedInuserdetails.UserId);
                    if (usersalt == null)
                    {
                        return BadRequest(new BadRequestResponse("Entered Username or Password is Invalid"));
                    }

                    if (loggedInuserdetails.Status == false)
                    {
                        return BadRequest(new BadRequestResponse("Your Account is InActive Contact Administrator"));
                    }

                    var generatepasswordhash = HashHelper.CreateHashSHA512(authenticateRequest.Password).ToLower();
                    var generatedhash = HashHelper.CreateHashSHA512(generatepasswordhash, usersalt.PasswordSalt);

                    if (string.Equals(loggedInuserdetails.PasswordHash, generatedhash, StringComparison.Ordinal))
                    {
                        var response = GenerateJwtToken(loggedInuserdetails);
                        return Ok(new OkResponse("Success", new AuthenticateResponse()
                        {
                            Token = response
                        }));
                    }
                }
            }

            return BadRequest(new BadRequestResponse("Username or password is incorrect"));
        }

        private string GenerateJwtToken(CommonUserDetailsViewModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                IssuedAt = DateTime.Now,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("uid", model.UserId.ToString()),
                    new Claim("roleId", model.RoleId.ToString()),
                    new Claim("role", model.RoleName),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_appSettings.Expires)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
