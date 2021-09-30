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
        private readonly IUserData _db;

        public UsersController(IUserData db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<User>> Get()
        {
            try
            {
                //var data = new UserData(_config);
                List<User> users = _db.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetById(int id)
        {
            try
            {
                //var data = new UserData(_config);
                var user = _db.GetUsers(id: id);

                if (user.Count == 0)
                {
                    return NotFound();
                }

                return Ok(user[0]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] UserInput user)
        {
            try
            {
                //var data = new UserData(_config);
                var usr = _db.GetUsers(email: user.Email);

                if (usr.Count != 0)
                {
                    ModelState.AddModelError("Email", "User with that email already exists!");
                    return BadRequest(ModelState);
                }

                int id = _db.SaveUser(user);
                User newUser = _db.GetUsers(id: id)[0];
                return Created($"users/{id}", newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Change(int id, [FromBody] UserInput user)
        {
            try
            {
                //var data = new UserData(_config);
                var usr = _db.GetUsers(id: id);

                if (usr.Count == 0)
                {
                    return NotFound();
                }

                _db.SaveUser(user, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                //var data = new UserData(_config);
                var usr = _db.GetUsers(id: id);

                if (usr.Count == 0)
                {
                    return NotFound();
                }

                _db.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
