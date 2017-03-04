using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.ComponentModel;

namespace DerbyManagement.App.ViewModels
{
    public class DerbyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IDerbyDataService _derbyDataService;

        private Derby derby;

        public Derby Derby
        {
            get { return derby; }
            set {
                derby = value;
                RaisePropertyChanged("Derby");

                // TODO- send message when new derby created
            }
        }

        public string Test
        {
            get { return "test"; }
            set { string test = value; }
        }


        public DerbyViewModel(IDerbyDataService derbyDataService)
        {
            _derbyDataService = derbyDataService;

            LoadData();
            // LoadCommands();

            // TODO add Messanger
        }

        private void LoadData()
        {
            Derby = _derbyDataService.GetCurrentDerbyWithDivisions();
        }

        //private void LoadCommands()
        //{
        //    throw new NotImplementedException();
        //}

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
