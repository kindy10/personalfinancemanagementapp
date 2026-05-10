using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        //Event automatically triggered when property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        //Helper method to notify UI

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
