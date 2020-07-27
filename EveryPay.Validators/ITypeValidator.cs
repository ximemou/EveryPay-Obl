using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Validators
{
    public interface ITypeValidator
    {
        void validateTypeMatchesGivenValue(string givenValue);

    }
}
