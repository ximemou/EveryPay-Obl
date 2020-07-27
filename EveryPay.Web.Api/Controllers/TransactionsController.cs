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
 
    public class TransactionsController : ApiController
    {
        private readonly ITransactionService transactionService;

        private ValidateRoles validator;

        public TransactionsController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
            validator = new ValidateRoles();
        }
        public TransactionsController()
        {
            this.transactionService = new TransactionService();
            validator = new ValidateRoles();
        }

        [Route("api/transactions")]
        //GET: api/transactions
        public IHttpActionResult GetTransactions()
        {
            IEnumerable<Transaction> transactions = transactionService.GetAllTransactions();
            return Ok(transactions);
        }

        [Route("api/transactions/{transactionId}")]
        //GET: api/transactions/{transactionId}
        public IHttpActionResult GetTransactionsById(int transactionId)
        {

            Transaction transaction = transactionService.GetTransactionById(transactionId);
           
            return Ok(transaction);
        }



        [Route("api/transactions")]
        public IHttpActionResult PostTransaction(TransactionDTO transaction)
        {
            try
            {

                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator)
                    || validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Cashier))
                {
                    try
                    {
                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        int transactionId = transactionService.CreateTransaction(transaction);

                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "" + transactionId));
                    }
                    catch (DateFormatException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (Exception ex)
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

        [Route("api/transactions/{transactionId}/bills")]
        [HttpPost]
        public IHttpActionResult PostBillsTransaction(int transactionId, List<BillDTO> bills)
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
                        if (!transactionService.AddBillsToTtransaction(transactionId, bills))
                        {
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontro la transaccion a la que se hace referencia"));
                        }
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "Facturas agregadas correctamente a la transaccion"));
                    }

                    catch (NotFoundException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (NoSupplierFieldsException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (Exception ex)
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

        [Route("api/transactions/{transactionId}/bills")]
        [HttpGet]
        public IHttpActionResult GetBillsForTransaction(int transactionId)
        {

            Transaction transaction = transactionService.GetTransactionById(transactionId);
            return Ok(transaction.Bills);
        }


        [Route("api/transactions/{transactionId}/pay")]
        public IHttpActionResult PostPayment(int transactionId,PaymentDTO paymentMethod)
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
                        if (!transactionService.PayTransaction(paymentMethod, transactionId))
                        {
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se puedo realizar el pago, verifique el importe ingresado"));
                        }

                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "Pago realizado exitosamente"));
                    }
                    catch (NonExistingPaymentException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (NotFoundException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (NoSpecificValuesInBillException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (TransactionAlreadyPayException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (NoSupplierFieldsException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (Exception ex)
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
            
     }
           
 }

        

    
