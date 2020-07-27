using EveryPay.Data.Entities;
using EveryPay.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Web.Services
{
    public interface IBillService
    {
        Bill GetBillById(int billId);
        IEnumerable<Bill> GetAllBills();
        int CreateBill( Bill aBill);
        bool UpdateBill(int billId, Bill aBill);
        bool DeleteBill(int billId);
        bool AddValuesToBill(int billId, List<SpecificFieldValueDTO> values);
        IEnumerable<SpecificFieldValue> GetAllFieldValuesForBill(int billId);
    }
}
