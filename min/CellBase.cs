using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace min {
    public class CellBase : INotifyPropertyChanged {

        public CellBase() {
            IsOpen = false;
        }

        public delegate void OpenCell(CellBase cb);
        public event OpenCell OpenByCell;

        private bool isClosed;

        public bool IsClosed {
            get { return isClosed; }
            set {
                isClosed = value;
                OnPropertyChanged("IsClosed");
            }
        }

        public bool IsOpen {
            get {
                return isOpen;
            }
            set {
                isOpen = value;
                IsClosed = !value;
                OnPropertyChanged("IsOpen");
                OnPropertyChanged("IsClosed");
            }
        }
        private bool isOpen;
        public bool IsEmpty { get; set; }
        private bool _isBomb;
        public bool IsBomb {
            get { return _isBomb; }
            set {
                Value = -1;
                _isBomb = value;
            }
        }

        private int rIndex;
        public int RIndex {
            get { return rIndex; }
            set { rIndex = value; }
        }

        private int cIndex;

        public int CIndex {
            get { return cIndex; }
            set { cIndex = value; }
        }
        private int _value;
        public int Value {
            get { return _value; }
            set {
                if (value == 0)
                    IsEmpty = true;
                else
                    IsEmpty = false;
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        private string bValue;
        public string BValue {
            get { return bValue; }
            set { bValue = value;
                OnPropertyChanged("BValue");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }



        public bool Loss() {
            return false;
        }

        public void Open() {
            OpenByCell(this);
        }


        public override string ToString() {
            return $"RI = {RIndex} CI = {CIndex}  B = {IsBomb}  O = {isOpen}  E = {IsEmpty}  CL = {IsClosed}";
        }
    }
}
