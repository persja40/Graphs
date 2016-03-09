using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Graphs.Validations
{
    public class GraphListValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                List<int> list = new List<int>();
                if (value is string)
                {
                    var str = value as string;
                    foreach (var item in str.Split(' ')) 
                    {
                        if (string.IsNullOrWhiteSpace(item))
                            continue;
                        int parsed = Int32.Parse(item.Trim());
                        if (parsed < 0)
                            return new ValidationResult(false, null);
                        if (list.Contains(parsed))
                            return new ValidationResult(false, null);
                    }
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
