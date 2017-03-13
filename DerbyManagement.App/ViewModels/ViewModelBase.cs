using System.ComponentModel;

namespace DerbyManagement.App.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _canSave;

        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                _canSave = value;
                RaisePropertyChanged("CanSave");
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
