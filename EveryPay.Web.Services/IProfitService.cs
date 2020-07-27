using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Web.Services
{
   public interface IProfitService
    {
        float GetProfitsInPeriod(string[] dates);
        Dictionary<string,float> GetProfitsForEachSupplier(string[] dates);

    }
}
