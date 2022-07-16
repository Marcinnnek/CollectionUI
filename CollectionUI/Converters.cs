using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CollectionUI
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush FalseColor { get; set; } = Brushes.Black;
        public Brush TrueColor { get; set; } = Brushes.Gray;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = (bool)value;
            return !b ? FalseColor : TrueColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CreateTaskConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var item in values)
            {
                Debug.WriteLine(item+", ");
            }
            
            string description = values[0].ToString();
            DateTime createDate = DateTime.Now;
            DateTime? plannedCompletionDate = (DateTime?)values[1];
            Model.TaskPriority priority = (Model.TaskPriority)(int)values[2];
            if (!string.IsNullOrWhiteSpace(description) && plannedCompletionDate.HasValue)
            {
                return new ViewModel.VMTask(description, createDate, plannedCompletionDate.Value, priority, false);
            }
            else return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
