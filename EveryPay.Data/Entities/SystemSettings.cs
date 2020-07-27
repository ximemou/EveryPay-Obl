using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class SystemSettings
    {
        [Key]
        public int Id { get; set; }
        public int MoneyForPoint { get; set; }

        public SystemSettings()
        {
            MoneyForPoint = 150;
        }

    }
}
