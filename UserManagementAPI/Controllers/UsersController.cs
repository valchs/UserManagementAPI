using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using UserManagementLibrary.DataAccess;
using UserManagementLibrary.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;

        public UsersController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                var data = new UserData(_config);
                List<User> users = data.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<List<User>> GetUserById(int id)
        {
            try
            {
                var data = new UserData(_config);
                var user = data.GetUsers(id: id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserInput user)
        {
            try
            {
                var data = new UserData(_config);
                var usr = data.GetUsers(email: user.Email);

                if (usr.Count != 0)
                {
                    ModelState.AddModelError("Email", "User with that email already exists!");
                    return BadRequest(ModelState);
                }

                int id = data.SaveUser(user);
                return Ok($"User {id}: saved!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult ChangeUser(int id, [FromBody] UserInput user)
        {
            try
            {
                var data = new UserData(_config);
                int userId = data.SaveUser(user, id);
                return Ok($"User {userId}: updated!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var data = new UserData(_config);
                data.DeleteUser(id);
                return Ok($"Item {id}: deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
