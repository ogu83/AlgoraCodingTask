using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlgoraCodingTaskClient
{
    public abstract class BaseVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
