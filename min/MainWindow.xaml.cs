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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        CellCollectionBase cells;
        List<CellBase> list;

        public MainWindow() {
            InitializeComponent();
             DataContext = cells;
           // DataContext = this;
            cells = new CellCollectionBase(10,10,10);
            cells.Loss += Loss;

            Console.WriteLine(cells.ToString());
            foreach (var item in cells.Cells) {
                Board.Children.Add(new CellControl(item));
            }
            //cells.Generate(10, 10, 10);

            list = new List<CellBase>();

            list.Add(new CellBase());
            list.Add(new CellBase());
            list.Add(new CellBase());
            list.Add(new CellBase());
            list.Add(new CellBase());
            list.Add(new CellBase());

            List<CellBase> list2 = new List<CellBase>();
            list2.Add(list[3]);
            list2[0].IsOpen = true;

            foreach (var item in list) {
                Console.WriteLine(item.IsOpen);
            }
            Console.WriteLine();

            foreach (var item in list2) {
                Console.WriteLine(item.IsOpen);
            }
        }
        public void Loss() {
            Board.Children.Clear();
            //cells.Generate(10, 10, 10);
            foreach (var item in cells.Cells) {
                Board.Children.Add(new CellControl(item));
            }
        }
    }
   
}
