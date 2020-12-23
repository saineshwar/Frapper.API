using Frapper.API.Filters;
using Frapper.API.Helpers;
using Frapper.Common;
using Frapper.Entities.UserMaster;
using Frapper.Repository;
using Frapper.Repository.UserMaster.Queries;
using Frapper.ViewModel.UserMaster.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Frapper.API.Controllers.V1
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/User")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkEntityFramework _unitOfWorkEntityFramework;
        private readonly IUserMasterQueries _userMasterQueries;
        public UserController(IUserMasterQueries userMasterQueries, IUnitOfWorkEntityFramework unitOfWorkEntityFramework)
        {
            _userMasterQueries = userMasterQueries;
            _unitOfWorkEntityFramework = unitOfWorkEntityFramework;
        }

        [Route("Create")]
        [HttpPost]
        [MapToApiVersion("1.0")]
        [ValidateModel]
        public IActionResult Create([FromBody] CreateUserRequest createUserRequest)
        {

            if (_userMasterQueries.CheckUserExists(createUserRequest.UserName))
            {
                return BadRequest(new BadRequestResponse("Entered Username Already Exists"));
            }

            if (_userMasterQueries.CheckEmailExists(createUserRequest.EmailId))
            {
                return BadRequest(new BadRequestResponse("Entered EmailId Already Exists"));
            }

            if (_userMasterQueries.CheckMobileNoExists(createUserRequest.MobileNo))
            {
                return BadRequest(new BadRequestResponse("Entered MobileNo Already Exists"));
            }

            if (!string.Equals(createUserRequest.Password, createUserRequest.ConfirmPassword,
                StringComparison.Ordinal))
            {
                return BadRequest(new BadRequestResponse("Password Does not Match"));
            }

            var generatepasswordhash = HashHelper.CreateHashSHA512(createUserRequest.Password).ToLower();

            var salt = GenerateRandomNumbers.GenerateRandomDigitCode(20);
            var saltedpassword = HashHelper.CreateHashSHA512(generatepasswordhash, salt);

            var userMappedobject = new UserMaster()
            {
                UserId = 0,
                Status = true,
                CreatedOn = DateTime.Now,
                CreatedBy = Convert.ToInt32(1),
                PasswordHash = saltedpassword,
                FirstName = createUserRequest.FirstName,
                EmailId= createUserRequest.EmailId,
                Gender = createUserRequest.Gender,
                MobileNo = createUserRequest.MobileNo,
                IsFirstLogin = false,
                LastName = createUserRequest.LastName,
                UserName = createUserRequest.UserName
            };
         

            AssignedRoles assignedRoles = new AssignedRoles()
            {
                AssignedRoleId = 0,
                CreateDate = DateTime.Now,
                Status = true,
                RoleId = Convert.ToInt32(RolesHelper.Roles.User),
                UserMaster = userMappedobject
            };

            UserTokens userTokens = new UserTokens()
            {
                UserMaster = userMappedobject,
                CreatedOn = DateTime.Now,
                HashId = 0,
                PasswordSalt = salt
            };

            _unitOfWorkEntityFramework.UserMasterCommand.Add(userMappedobject);
            _unitOfWorkEntityFramework.AssignedRolesCommand.Add(assignedRoles);
            _unitOfWorkEntityFramework.UserTokensCommand.Add(userTokens);
            var result = _unitOfWorkEntityFramework.Commit();

            if (result)
            {
                return Ok(new OkResponse("User Registered Successfully !"));
            }
            else
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }
    }
}
