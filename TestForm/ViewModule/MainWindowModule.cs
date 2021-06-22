using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestForm.ViewModule
{
    public class MainWindowModule : INotifyPropertyChanged
    {
        public bool NotifyAnimationEnded { get; set; }

        public int NavWidth { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
