using EveryPay.Data.Entities;
using EveryPay.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EveryPay.Exceptions;
using EveryPay.DTO;
using System.Web.Http.Cors;

namespace EveryPay.Web.Api.Controllers
{
  
    public class ClientController : ApiController
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        public ClientController()
        {
            this.clientService = new ClientService();
        }

        [Route("api/clients")]
        public IHttpActionResult GetClients()
        {
            IEnumerable<Client> clients = clientService.GetAllClients();

            return Ok(clients);
        }

        [Route("api/clients/{clientId}")]
        public IHttpActionResult GetClient(int clientId)
        {

            try
            {
                Client client = clientService.GetClientById(clientId);
                return Ok(client);
            }
            catch(NotFoundException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }

        }

        [Route("api/clients")]
        public IHttpActionResult PostClient(ClientDTO client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int clientId=  clientService.CreateClient(client);
                return Ok(clientId);
            }
            catch(WrongDataTypeException ex)
            {
                 return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch(NotUniqueException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch(WrongClientIdentification ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        [Route("api/clients/{clientId}")]
        public IHttpActionResult PutClient(int clientId,Client client)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!clientService.UpdateClient(clientId, client))
                {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (NotFoundException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
      

        }

        [Route("api/clients/{clientId}")]
        public IHttpActionResult DeleteClient (int clientId)
        {
            try
            {
                if (!clientService.DeleteClient(clientId))
                {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch(NotFoundException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
          
        }

    }

}
