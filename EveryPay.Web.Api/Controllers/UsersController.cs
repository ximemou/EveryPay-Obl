using EveryPay.Data.Entities;
using EveryPay.Enumerators;
using EveryPay.Exceptions;
using EveryPay.TokenManagment;
using EveryPay.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EveryPay.Web.Api.Controllers
{
  
    public class UsersController : ApiController
    {
        private readonly IUserService UserService;

        private ValidateRoles validator;


        public UsersController(IUserService UserService)
        {
            this.UserService = UserService;
            validator = new ValidateRoles();
        }

        public UsersController()
        {
            this.UserService = new UserService();
            validator = new ValidateRoles();
        }

        [Route("api/Users")]
        //GET: api/Users
       public IHttpActionResult GetUsers()
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    IEnumerable<User> Users = UserService.GetAllUsers();
                    return Ok(Users);
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No posee los permisos necesarios"));
                }

            }
            catch (InvalidOperationException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar el header Authorization"));
            }
            
        }
        
        [Route("api/Users/{UserId}")]
        //GET: api/Users/{UserId}
        public IHttpActionResult GetUserById(int UserId)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    User user = UserService.GetUserById(UserId);
                    return Ok(user);
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No posee los permisos necesarios"));
                }
            }
            catch (InvalidOperationException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar el header Authorization"));
            }
            

                
        }

        [Route("api/Users/{UserId}")]
        public IHttpActionResult PutUser(int UserId, User newUser)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    if (!UserService.UpdateUser(UserId, newUser))
                    {
                        return NotFound();
                    }
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No posee los permisos necesarios"));
                }
            }catch(InvalidOperationException )
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar el header Authorization"));
            }
            catch (NotUniqueException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (NotValidRoleException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }


        }

        [Route("api/Users")]
        public IHttpActionResult PostUser(User User)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    try
                    {
                        int userId = UserService.CreateUser(User);
                        return Ok(userId);
                    }

                    catch (WrongDataTypeException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message));
                    }
                    catch(NotValidRoleException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (NullReferenceException)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar todos los datos del usuario"));
                    }
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No posee los permisos necesarios"));
                }
            }
            catch (InvalidOperationException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar el header Authorization"));
            }
          
        }
        [Route("api/Users/{UserId}")]
        public IHttpActionResult DeleteUser(int UserId)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (UserService.DeleteUser(UserId))
                    {
                        return StatusCode(HttpStatusCode.NoContent);
                    }
                    return NotFound();
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No posee los permisos necesarios"));
                }
            }
            catch (InvalidOperationException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar el header Authorization"));
            }
        }

    }
}
