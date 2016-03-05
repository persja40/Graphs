using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Graphs.ValueConverters
{
    public class GraphListValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ObservableCollection<int>)
            {
                string _return = "";
                foreach(var item in value as ObservableCollection<int>)
                {
                    _return += item + " ";
                }
                return _return.Trim();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<int> list = new ObservableCollection<int>();
            if(value is string)
            {
                var str = value as string;
                foreach (var item in str.Split(' '))
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
