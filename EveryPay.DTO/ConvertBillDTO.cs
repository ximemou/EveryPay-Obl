using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.DTO
{
    public class ConvertBillDTO
    {

        public ConvertBillDTO()
        {

        }
        public Bill convertDTO(BillDTO billDTO)
        {
            Bill bill = new Bill();
            bill.Amount = billDTO.Amount;

            bill.Supplier = new Supplier(billDTO.SupplierId);
            return bill;
        }
    }
}
