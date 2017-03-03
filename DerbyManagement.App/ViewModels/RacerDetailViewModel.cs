using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace DerbyManagement.App.ViewModels
{
    public class RacerDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IDerbyDataService _derbyDataService;
        private IDialogService _dialogService;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveCommand { get; set; }

        private Racer selectedRacer;

        public Racer SelectedRacer
        {
            get { return selectedRacer; }
            set {
                selectedRacer = value;
                RaisePropertyChanged("SelectedRacer");
            }
        }

        public RacerDetailViewModel(IDerbyDataService derbyDataService, IDialogService dialogService)
        {
            _derbyDataService = derbyDataService;
            _dialogService = dialogService;

            Messenger.Default.Register<Racer>(this, OnRacerRecieved);

            SaveCommand = new CustomCommand(SaveRacer, CanSaveRacer);
        }

        private void OnRacerRecieved(Racer racer)
        {
            selectedRacer = racer;
        }

        private bool CanSaveRacer(object obj)
        {
            // TODO
            return true;
        }

        private void SaveRacer(object racer)
        {
            _derbyDataService.Save();
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }

    }
}
