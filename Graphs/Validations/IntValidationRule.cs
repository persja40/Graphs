using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Graphs.Validations
{
    public class IntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value is string)
                {
                    var str = value as string;
                    int val = int.Parse(str);
                }
            }
            catch
            {
                return new ValidationResult(false, null);
            }

            return new ValidationResult(true, null);
        }
    }
}
