using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Exceptions;

namespace EveryPay.Validators
{
    public class DateFieldValidator : ITypeValidator
    {
        public void validateTypeMatchesGivenValue(string givenValue)
        {
            try
            {
                DateTime.ParseExact(givenValue, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new WrongDataTypeException("Se esperaba un tipo de dato DateTime y se encontro otro tipo");
            }
        }
    }
}
