using DerbyManagement.App.Extensions;
using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;

namespace DerbyManagement.App.ViewModels
{
    public class RacerDetailViewModel : ViewModelBase
    {
        private IDerbyDataService _derbyDataService;
        private bool _isLoading;
        private Racer _selectedRacer { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int CarNumber
        {
            get { return _selectedRacer.CarNumber; }
            set
            {
                _selectedRacer.CarNumber = value;
                SetRacerDirty();
                RaisePropertyChanged("_selectedRacer");
            }
        }

        public string CarName
        {
            get { return _selectedRacer.CarName; }
            set
            {
                _selectedRacer.CarName = value;
                SetRacerDirty();
                RaisePropertyChanged("_selectedRacer");
            }
        }

        public string OwnerFirstName
        {
            get { return _selectedRacer.OwnerFirstName; }
            set
            {
                _selectedRacer.OwnerFirstName = value;
                SetRacerDirty();
                RaisePropertyChanged("OwnerFirstName");
            }
        }

        public string OwnerLastName
        {
            get { return _selectedRacer.OwnerLastName; }
            set
            {
                _selectedRacer.OwnerLastName = value;
                SetRacerDirty();
                RaisePropertyChanged("_selectedRacer");
            }
        }

        public ObservableCollection<Division> Divisions
        {
            get { return _selectedRacer.Divisions.ToObservableCollection(); }
            set
            {
                _selectedRacer.Divisions = value.ToList();
                SetRacerDirty();
                RaisePropertyChanged("_selectedRacer");
            }
        }

        public RacerDetailViewModel(IDerbyDataService derbyDataService)
        {
            _derbyDataService = derbyDataService;

            Messenger.Default.Register<Racer>(this, OnRacerRecieved);

            SaveCommand = new CustomCommand(SaveRacer, CanSaveRacer);
            CancelCommand = new CustomCommand(CancelRacer, CanCancel);
        }

        private void OnRacerRecieved(Racer racer)
        {
            _isLoading = true;
            _selectedRacer = racer;
            _isLoading = false;
        }

        private bool CanSaveRacer(object obj)
        {
            return _selectedRacer.IsDirty;
        }

        private void SaveRacer(object racer)
        {
            _derbyDataService.Save();
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }

        private void CancelRacer(object obj)
        {
            _derbyDataService.Cancel();
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void SetRacerDirty()
        {
            if (!_isLoading)
            {
                _selectedRacer.IsDirty = true;
            }
        }

    }
}
