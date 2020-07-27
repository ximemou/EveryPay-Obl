using EveryPay.Data.Entities;
using EveryPay.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EveryPay.Exceptions;
using System.Web.Http.Cors;

namespace EveryPay.Web.Api.Controllers
{
  
    public class LogInController : ApiController
    {

        private readonly ILogInService logInService;

        public LogInController(ILogInService logInService)
        {
            this.logInService = logInService;
        }
        public LogInController()
        {
            this.logInService = new LogInService();
        }

        [Route("api/login")]
        [HttpPost]

        public IHttpActionResult GetToken(User user)
        {
            try
            {
                return Ok(logInService.GenerateToken(user.UserName, user.Password));
            }
            catch (NotFoundException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }


    }
}
