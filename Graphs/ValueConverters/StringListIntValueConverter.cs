using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Graphs.ValueConverters
{
    public class StringListIntValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<int>)
            {
                string _return = "";
                foreach (var item in value as ObservableCollection<int>)
                {
                    _return += (item) + " ";
                }
                return _return.Trim();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<int> list = new ObservableCollection<int>();
            
            if (value is string)
            {
                var str = value as string;
                string[] numbers = Regex.Split(str, @"\D+");
                foreach (var item in numbers)
                {
                    if (string.IsNullOrWhiteSpace(item))
                        continue;
                    int parsed = Int32.Parse(item.Trim());
                    list.Add(parsed);
                }
            }
            return list;
        }
    }
}
