using EveryPay.Data.Entities;
using EveryPay.DTO;
using EveryPay.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EveryPay.Exceptions;
using EveryPay.TokenManagment;
using System.Web.Http.Cors;
using EveryPay.Enumerators;

namespace EveryPay.Web.Api.Controllers
{
   
    public class BillsController : ApiController
    {

        private readonly IBillService billService;

        private ValidateRoles validator;

        public BillsController(IBillService billService)
        {
            this.billService = billService;
            validator = new ValidateRoles();
        }
        public BillsController()
        {
            this.billService = new BillService();
            validator = new ValidateRoles();
        }

        [Route("api/bills")]
        //GET: api/bills
        public IHttpActionResult GetBills()
        {
            IEnumerable<Bill> bills = billService.GetAllBills();
            return Ok(bills);
        }

        [Route("api/bills/{billId}")]
        //GET: api/bills/{billId}
        public IHttpActionResult GetBillsById(int billId)
        {

            Bill bill = billService.GetBillById(billId);
            return Ok(bill);
        }

        [Route("api/bills")]
        public IHttpActionResult PostBill(Bill bill)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator)
                   || validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Cashier))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    int billId = billService.CreateBill(bill);

                    return Ok(billId);
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

        [Route("api/bills/{billId}/values")]
        public IHttpActionResult PostValuesToBill(int billId, List<SpecificFieldValueDTO> values)
        {
            try
            {

                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator)
                   || validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Cashier))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    try
                    {
                        if (!billService.AddValuesToBill(billId, values))
                        {
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No existe la factura especificada"));
                        }


                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "Se ingresaron correctamente "));
                    }
                    catch (NotFoundException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message));
                    }
                    catch (PaymentAlreadyRegisteredException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (WrongDataTypeException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
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

        [Route("api/bills/{billId}/values")]
        //GET: api/bills/{billId}/values
        public IHttpActionResult GetValuesForBill(int billId)
        {
            try
            {
                IEnumerable<SpecificFieldValue> values = billService.GetAllFieldValuesForBill(billId);
                return Ok(values);
            }
            catch (NotFoundException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message));
            }
        }

        [Route("api/bills/{billId}")]
        public IHttpActionResult DeleteBill(int billId)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator)
                  || validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Cashier))
                {
                    if (!billService.DeleteBill(billId))
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No existe la factura que se quiere eliminar"));
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NoContent, "Factura eliminada correctamente"));
                    }
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No posee los permisos necesarios"));
                }
            }
            catch (TransactionAlreadyPayException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch (InvalidOperationException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar el header Authorization"));
            }
        }
    }
    
}
