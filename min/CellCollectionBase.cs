using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace min {
    public class CellCollectionBase : INotifyPropertyChanged {
        #region ctor
        public CellCollectionBase() {

        }
        public CellCollectionBase(int rows, int columns, int bombs) {
            Generate(rows, columns, bombs);
            Cells2 = new ObservableCollection<CellBase>();
        }
        #endregion

        #region prop
        public int Size { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Bombs { get; set; }
        public int ClosedCells { get; set; }
        public int OpenCells { get; set; }

        public delegate void LossDelegate();
        public event LossDelegate Loss;

        public CellBase[,] Cells {
            get => cells; set {
                cells = value;
                Cells2 = new ObservableCollection<CellBase>();
                foreach (var item in value) {
                    Cells2.Add(item);
                }
                OnPropertyChanged("Cells");
            }
        }
        private CellBase[,] cells;
        public ObservableCollection<CellBase> Cells2;

        //public delegate CellBase Open2();
        //Open2 del;
        #endregion

        #region actions
        public virtual void Click(CellBase cell) {
            throw new NotImplementedException();
        }
        public virtual void Click(int index) {
            throw new NotImplementedException();
        }
        public virtual void Click(int RowIndex, int ColumnIndex) {
            throw new NotImplementedException();
        }
        public void Generate(int bombs) {
            throw new NotImplementedException();
        }
        public virtual void Generate(int rows, int columns, int bombs) {
            Rows = rows;
            Columns = columns;
            Size = Rows * Columns;
            Bombs = bombs;

            Cells = new CellBase[Rows, Columns];
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    Cells[i, j] = new CellBase() {
                        RIndex = i,
                        CIndex = j
                    };
                }
            }          
            generateBombs();
            calculateValues();
            init();
            OnPropertyChanged("Cells");
        }
        private void generateBombs() {
            Random rnd = new Random();
            int bombsCount = Bombs;
            while (bombsCount > 0) {
                int i = rnd.Next(0, Rows);
                int j = rnd.Next(0, Columns);
                if (!Cells[i, j].IsBomb) {
                    Cells[i, j].IsBomb = true;
                    bombsCount--;
                }
            }
        }
        private void generateBombs2() {
            List<int> tempMas = new List<int>();
            for (int i = 0; i < Size; i++) {
                tempMas.Add(i);
            }
            int bombsCount = Bombs;
            Random rnd = new Random();
            while (bombsCount > 0) {
                int index = rnd.Next(0, tempMas.Count);
                tempMas.Remove(index);
                int i = index / Columns;
                int j = index % Columns;
                Cells[i, j].IsBomb = true;
                Cells[i, j].Value = -1;
                bombsCount--;
            }
        }
        private void calculateValues() {
            int[,] map = new int[Rows + 2, Columns + 2];
            for (int i = 1; i <= Rows; i++) {
                for (int j = 1; j <= Columns; j++) {
                    if (Cells[i - 1, j - 1].IsBomb) {
                        map[i, j] = 1;
                    }
                }
            }
            for (int i = 1; i <= Rows; i++) {
                for (int j = 1; j <= Columns; j++) {
                    if (map[i, j] != 1) {
                        int value = 0;
                        value += map[i - 1, j - 1];
                        value += map[i - 1, j];
                        value += map[i - 1, j + 1];
                        value += map[i, j - 1];
                        value += map[i, j + 1];
                        value += map[i + 1, j - 1];
                        value += map[i + 1, j];
                        value += map[i + 1, j + 1];
                        Cells[i - 1, j - 1].Value = value;
                    }
                }
            }
        }

        private void init() {
            foreach (var item in Cells) {
                // del += item.OpenCell;
                item.OpenByCell += OpenCell2;

            }
        }
        private void testOpen() {
            Console.WriteLine($"DEL - ");
        }
        public void OpenCell(CellBase cell) {
            Console.WriteLine("t.value = " + cell.Value);
            int iTemp = -1, jTemp = -1;
            if (cell.IsBomb) {
                Console.WriteLine("Fall");
            } else if (cell.IsEmpty && !cell.IsOpen) {

                //for (int i = 0; i < Rows; i++) {
                //    for (int j = 0; j < Columns; j++) {
                //        if (Cells[i, j] == cell) {
                //            iTemp = i;
                //            jTemp = j;
                //            break;
                //        }
                //        if (iTemp == i) {
                //            break;
                //        }
                //    }
                //}
                iTemp = cell.RIndex;
                jTemp = cell.CIndex;

                CellBase[,] tempMas = new CellBase[Rows + 2, Columns + 2];
                for (int i = 0; i < Rows + 2; i++) {
                    for (int j = 0; j < Columns + 2; j++) {
                        if (i == 0 || j == 0 || i == Rows + 1 || j == Columns + 1) {
                            tempMas[i, j] = new CellBase() { IsOpen = true, IsEmpty = true };
                        } else {
                            tempMas[i, j] = Cells[i - 1, j - 1];
                        }
                    }
                }

                Console.WriteLine($"I = {iTemp}  J = {jTemp}");
                List<CellBase> tempList = new List<CellBase>();
                if (iTemp != -1) {
                    //tempList.Add(Cells[iTemp - 1, jTemp - 1]);
                    //tempList.Add(Cells[iTemp - 1, jTemp]);
                    //tempList.Add(Cells[iTemp - 1, jTemp + 1]);
                    //tempList.Add(Cells[iTemp, jTemp - 1]);
                    //tempList.Add(Cells[iTemp, jTemp + 1]);
                    //tempList.Add(Cells[iTemp + 1, jTemp - 1]);
                    //tempList.Add(Cells[iTemp + 1, jTemp]);
                    //tempList.Add(Cells[iTemp + 1, jTemp + 1]);

                    tempList.Add(tempMas[iTemp, jTemp]);
                    tempList.Add(tempMas[iTemp, jTemp + 1]);
                    tempList.Add(tempMas[iTemp, jTemp + 2]);
                    tempList.Add(tempMas[iTemp + 1, jTemp]);
                    tempList.Add(tempMas[iTemp + 1, jTemp + 2]);
                    tempList.Add(tempMas[iTemp + 2, jTemp]);
                    tempList.Add(tempMas[iTemp + 2, jTemp + 1]);
                    tempList.Add(tempMas[iTemp + 2, jTemp + 2]);

                    foreach (var item in tempList) {
                        if (item.IsEmpty && !item.IsOpen) {
                            item.Open();
                            //item.Value = -5;
                            item.IsOpen = true;
                        }
                    }
                    Console.WriteLine($"I = {iTemp}  J = {jTemp}");
                }
            }


        }

        public void OpenCell2(CellBase cell) {
            Console.WriteLine(cell.ToString());
            cell.IsOpen = true;
            if (cell.IsBomb) {
                Console.WriteLine("Fall");
                //GameOver();
                Restart();
            } else if (cell.Value == 0) {
                cell.IsOpen = true;
                foreach (var item in Cells) {
                    if (item != cell) {
                        if (Math.Abs(item.RIndex - cell.RIndex) <= 1 && Math.Abs(item.CIndex - cell.CIndex) <= 1) {
                            Console.WriteLine(item.ToString());
                            if (item.IsClosed) {
                                item.Open();
                            }
                        }
                    }
                }
            }
        }
        public void GameOver() {
            MessageBox.Show("!");
            Generate(10, 10, 10);
            Console.WriteLine(this.ToString());
            Loss();
        }
        public void Restart() {
            foreach (var item in Cells) {
                item.IsOpen = false;
            }
        }
        #endregion

        #region testing 
        public override string ToString() {
            string str = "";
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Columns; j++) {
                    if (Cells[i, j].IsBomb) {
                        str += "X";
                    } else {
                        str += Cells[i, j].Value.ToString();
                    }
                }
                str += "\n";
            }
            return str;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
