using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EveryPay.Web.Services;
using EveryPay.Data.Entities;
using EveryPay.Exceptions;
using EveryPay.TokenManagment;
using System.Data.Entity.Validation;
using System.Web.Http.Cors;
using EveryPay.DTO;
using EveryPay.Enumerators;

namespace EveryPay.Web.Api.Controllers
{
  
    public class SuppliersController : ApiController
    {
        private readonly ISupplierService supplierService;

        private ValidateRoles validator;


        public SuppliersController(ISupplierService supplierService)
        {
            this.supplierService = supplierService;
            validator = new ValidateRoles();
        }
        public SuppliersController()
        {
            this.supplierService = new SupplierService();
            validator = new ValidateRoles();
        }

        [Route("api/suppliers")]
        //GET: api/suppliers
        public IHttpActionResult GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }

        [Route("api/suppliers/{supplierId}")]
        //GET: api/suppliers/{supplierId}
        public IHttpActionResult GetSupplierById(int supplierId)
        {

            Supplier supplier = supplierService.GetSupplierById(supplierId);
            if (supplier != null)
            {
                return Ok(supplier);

            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "El proveedor buscado no existe"));
            }
        }



       // [Authorize(Roles = "Administrator")]
        [Route("api/suppliers")]
        public IHttpActionResult PostSupplier( Supplier supplier)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (!ModelState.IsValid)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Datos mal ingresados"));
                    }
                    try
                    {
                        int supplierId = supplierService.CreateSupplier(supplier);

                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, ""+supplierId));
                    }
                    catch (NotUniqueException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch(InvalidCommissionException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch(NotEnoughDataException ex)
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
            }catch(InvalidOperationException)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar el header Authorization"));
            }
            

        }

      //  [Authorize(Roles = "Administrator")]
        [Route("api/suppliers/{idSupplier}")]
        public IHttpActionResult PutSupplier( int idSupplier, Supplier supplier)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (!ModelState.IsValid)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Datos mal ingresados"));
                    }

                    if (!supplierService.UpdateSupplier(idSupplier, supplier))
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontro el proveedor al que se hace referencia"));
                    }
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NoContent, "Proveedor modificado correctamente"));
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
            catch(NotUniqueException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
            catch(InvalidCommissionException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
            }
              
        }

        [Route("api/suppliers/fields/{fieldId}")]
        public IHttpActionResult PutSupplierField(int fieldId, SupplierField supplierField)
        {
            try
            {

                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    if (!supplierService.UpdateSupplierField(fieldId, supplierField))
                    {
                        return NotFound();
                    }
                    return StatusCode(HttpStatusCode.NoContent);
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




        [Route("api/suppliers/{idSupplier}")]
        [HttpDelete]
        public IHttpActionResult DeleteSupplier( int idSupplier)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (supplierService.DeleteSupplier(idSupplier))
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NoContent, "El proveedor ha sido eliminado"));
                    }
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encntro el proveedor al que se hace referencia"));
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


        [Route("api/suppliers/{idSupplier}/fields")]
        public IHttpActionResult PostSupplierFields(int idSupplier, List<SupplierField> fields)
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
                        if (supplierService.AddSupplierfields(idSupplier, fields))
                        {
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "Los campos de los proveedores han sido ingresados correctamente"));
                        }
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontro el proveedor especificado"));
                    }
                    catch (WrongDataTypeException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (NotUniqueException ex)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                    }
                    catch (NullReferenceException)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar todos los datos para los campos particulares"));
                    }
                    catch (ArgumentNullException)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar todos los datos para los campos particulares"));
                    }
                    catch (DbEntityValidationException)
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Debe ingresar todos los datos para los campos particulares"));
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

        [Route("api/suppliers/{supplierId}/fields")]
        //GET: api/suppliers/{supplierId}/fields
        public IHttpActionResult GetSupplierFields(int supplierId)
        {

            try
            {
                IEnumerable<SupplierField> supplierFields = supplierService.GetSuppliersFields(supplierId);
                return Ok(supplierFields);
            }
            catch(NotFoundException ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message));
            }

            
        }


        [Route("api/suppliers/{supplierId}/fields/{fieldId}")]
        public IHttpActionResult DeleteSupplierField(int supplierId,int fieldId)
        {
            try
            {
                if (validator.validate((Request.Headers.GetValues("Authorization").FirstOrDefault()), UserRole.Administrator))
                {
                    if (supplierService.DeleteSupplierField(supplierId, fieldId))
                    {
                        return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NoContent, "El campo particular ha sido eliminado"));
                    }
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Datos erroneos. Verifique datos ingresados"));
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

        [Route("api/suppliersValidation")]
        [HttpPost]
        public IHttpActionResult PostSupplierFieldsValidation(List<SupplierField> fields)
        {

            if (supplierService.suppliersFieldsValidation(fields))
            {
                return Ok();
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Datos erroneos. Verifique datos ingresados"));
            }

        }

    }
}
