using DerbyManagement.App.Services;
using DerbyManagement.Model;

namespace DerbyManagement.App.ViewModels
{
    public class DerbyViewModel : ViewModelBase
    {
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

    }
}
