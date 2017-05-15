using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace min {
   public class BoardPanelBase : Panel , INotifyPropertyChanged {

        public BoardPanelBase() {
            Rows = 10;
            Columns = 10;
        }
        public BoardPanelBase(int r, int c) {
            Rows = r;
            Columns = c;
        }
        private int columns;
        public int Columns {
            get { return columns; }
            set { columns = value; }
        }
        private int rows;
        public int Rows {
            get { return rows; }
            set { rows = value; }
        }


        protected override Size ArrangeOverride(Size finalSize) {
            for (int i = 0; i < InternalChildren.Count; i++) {
                double tempX = 50 * (i % Columns);
                double tempY = 50 * (i / Columns);
                InternalChildren[i].Arrange(new Rect(new Point(tempX, tempY), InternalChildren[i].DesiredSize));
            }
            return new Size(5 * Rows, 5 * Columns);
        }
        protected override Size MeasureOverride(Size availableSize) {
            foreach (UIElement child in InternalChildren) {
                child.Measure(availableSize);
            }
            return new Size(Rows * 50 + 10, Columns * 50 + 10);
        }


        //private CellBase[,] mCells2;

        //public CellBase[,] MCells2 {
        //    get { return mCells2; }
        //    set { mCells2 = value;
        //        OnPropertyChanged("MCells2");
        //    }
        //}




        public CellBase[,] mCells {
            get { return (CellBase[,])GetValue(mCellsProperty); }
            set { SetValue(mCellsProperty, value);
                foreach (var item in value) {
                    Children.Add(new CellControl(item));
                }
                OnPropertyChanged("mCells");
            }
        }

        // Using a DependencyProperty as the backing store for mCells.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mCellsProperty =
            DependencyProperty.Register("mCells", typeof(CellBase[,]), typeof(BoardPanelBase), new PropertyMetadata(new CellBase[0,0]));



        public ObservableCollection<CellBase> mCells2 {
            get { return (ObservableCollection<CellBase>)GetValue(mCells2Property); }
            set { SetValue(mCells2Property, value);
                foreach (var item in value) {
                    Children.Add(new CellControl(item));
                }
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty mCells2Property =
            DependencyProperty.Register("mCells2", typeof(ObservableCollection<CellBase>), typeof(BoardPanelBase), new PropertyMetadata(new ObservableCollection<CellBase>()));



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
