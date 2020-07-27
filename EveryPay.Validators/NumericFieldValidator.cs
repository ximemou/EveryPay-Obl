using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Exceptions;

namespace EveryPay.Validators
{
    public class NumericFieldValidator : ITypeValidator
    {
        public void validateTypeMatchesGivenValue(string givenValue)
        {
            try
            {
                float.Parse(givenValue);
            }
            catch(FormatException)
            {
                throw new WrongDataTypeException("Se esperaba un tipo de dato numerico pero se encontro otro tipo");
            }
        }
    }
}
