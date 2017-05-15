using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace min {
    /// <summary>
    /// Логика взаимодействия для CellControl.xaml
    /// </summary>
    public partial class CellControl : UserControl{
        public CellControl(CellBase cell) {
            InitializeComponent();
            _cell = cell;
            DataContext = _cell;
        }
        private CellBase _cell;

        private void tblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            //_cell.Open();
            //_cell.test();           
            _cell.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            _cell.Open();
           // _cell.IsOpen = false;
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.RightButton == MouseButtonState.Pressed) {
                if (_cell.BValue == "?") {
                    _cell.BValue = "!";
                } else if (_cell.BValue == "!") {
                    _cell.BValue = "";
                } else {
                    _cell.BValue = "?";
                }
            }
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
