using EveryPay.Data.Entities;
using EveryPay.Exceptions;
using EveryPay.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.DTO
{
    public class ConvertPaymentMethod
    {

        public ConvertPaymentMethod()
        {

        }

        public IPaymentMethod convertPaymentDTO(PaymentDTO paymentDTO)
        {
            if (paymentDTO.PaymentMethodType == "Debit Card")
            {
                IPaymentMethod paymentMethod = new DebitCard();
                return paymentMethod;

            }
            else
            {
                if (paymentDTO.PaymentMethodType == "Cash")
                {
                    IPaymentMethod paymentMethod = new Cash();
                    return paymentMethod;

                }
                else
                {
                    throw new NonExistingPaymentException("El metodo de pago debe ser Debit Card o Cash");
                }
            }
        }
    }    

  }
    

