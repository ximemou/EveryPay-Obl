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
  
    public class ProfitsController : ApiController
    {

        private readonly IProfitService profitService;

        private ValidateRoles validator;

        public ProfitsController(IProfitService profitService)
        {
            this.profitService = profitService;
            validator = new ValidateRoles();
        }
        public ProfitsController()
        {
            this.profitService = new ProfitService();
            validator = new ValidateRoles();
        }

        [Route("api/profits")]
        [HttpPost]
        public IHttpActionResult PostProfit(string[] dates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            float profits = profitService.GetProfitsInPeriod(dates);

            return Ok(profits);
        }

        [Route("api/profits-supplier")]
        [HttpPost]
        public IHttpActionResult PostProfitsSuppliers(string[] dates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dictionary<string, float> earnings = profitService.GetProfitsForEachSupplier(dates);

            return Ok(earnings);
        }

    }
}
