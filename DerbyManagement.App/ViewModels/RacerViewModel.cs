using DerbyManagement.App.Extensions;
using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;

namespace DerbyManagement.App.ViewModels
{
    public class RacerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IDerbyDataService _derbyDataService;
        private IDialogService _dialogService;
        private Derby _currentDerby;

        private ObservableCollection<Racer> racers;
        public ObservableCollection<Racer> Racers
        {
            get { return racers; }
            set
            {
                racers = value;
                RaisePropertyChanged("Racers");
            }
        }

        private Racer selectedRacer;
        public Racer SelectedRacer
        {
            get { return selectedRacer; }
            set
            {
                selectedRacer = value;
                RaisePropertyChanged("SelectedRacer");
            }
        }

        public ICommand EditCommand { get; set; }


        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        public RacerViewModel(IDerbyDataService derbyDataService, IDialogService dialogService)
        {
            _derbyDataService = derbyDataService;
            _dialogService = dialogService;

            // TODO- update the derby (or at least DerbyId) when a new derby is created
            _currentDerby = _derbyDataService.GetCurrentDerby();
            LoadData();

            LoadCommands();

            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageRecieved);
        }

        private void LoadCommands()
        {
            EditCommand = new CustomCommand(EditRacer, CanEditRacer);
        }

        private void OnUpdateListMessageRecieved(UpdateListMessage obj)
        {
            LoadData();
            _dialogService.CloseRacerDetailDialog();
        }

        private void EditRacer(object obj)
        {
            Messenger.Default.Send<Racer>(selectedRacer);
            _dialogService.ShowRacerDetailDialog();
        }

        private bool CanEditRacer(object obj)
        {
            //if (SelectedRacer != null)
            return true;
            //return false;
        }

        private void LoadData()
        {
            Racers = _derbyDataService.GetRacersByDerbyIdWithDivisions(_currentDerby.DerbyId).ToObservableCollection();
        }

    }
}
