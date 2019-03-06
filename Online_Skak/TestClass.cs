using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel;
=======
>>>>>>> e36a4b8492046abf13b9066a544ff2231fd8f428
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Skak
{
<<<<<<< HEAD
    class TestClass : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
=======
    class 
    {
>>>>>>> e36a4b8492046abf13b9066a544ff2231fd8f428
    }
}
