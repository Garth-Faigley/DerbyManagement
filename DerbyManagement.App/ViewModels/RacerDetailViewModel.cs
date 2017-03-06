using DerbyManagement.App.Extensions;
using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DerbyManagement.App.ViewModels
{
    public class RacerDetailViewModel : ViewModelBase
    {
        private IDerbyDataService _derbyDataService;
        private IDialogService _dialogService;
        private bool _isLoading;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public Racer SelectedRacer { get; set; }

        public int CarNumber
        {
            get { return SelectedRacer.CarNumber; }
            set
            {
                SelectedRacer.CarNumber = value;
                SetRacerDirty();
                RaisePropertyChanged("SelectedRacer");
            }
        }

        public string CarName
        {
            get { return SelectedRacer.CarName; }
            set
            {
                SelectedRacer.CarName = value;
                SetRacerDirty();
                RaisePropertyChanged("SelectedRacer");
            }
        }

        public string OwnerFirstName
        {
            get { return SelectedRacer.OwnerFirstName; }
            set
            {
                SelectedRacer.OwnerFirstName = value;
                SetRacerDirty();
                RaisePropertyChanged("SelectedRacer");
            }
        }

        public string OwnerLastName
        {
            get { return SelectedRacer.OwnerLastName; }
            set
            {
                SelectedRacer.OwnerLastName = value;
                SetRacerDirty();
                RaisePropertyChanged("SelectedRacer");
            }
        }

        public ObservableCollection<Division> Divisions
        {
            get { return SelectedRacer.Divisions.ToObservableCollection(); }
            set
            {
                SelectedRacer.Divisions = value.ToList();
                SetRacerDirty();
                RaisePropertyChanged("SelectedRacer");
            }
        }

        public RacerDetailViewModel(IDerbyDataService derbyDataService, IDialogService dialogService)
        {   
            _derbyDataService = derbyDataService;
            _dialogService = dialogService;

            Messenger.Default.Register<Racer>(this, OnRacerRecieved);

            SaveCommand = new CustomCommand(SaveRacer, CanSaveRacer);
            CancelCommand = new CustomCommand(CancelRacer, CanCancel);
        }

        private void OnRacerRecieved(Racer racer)
        {
            _isLoading = true;
            SelectedRacer = racer;
            _isLoading = false;
        }

        private bool CanSaveRacer(object obj)
        {
            return SelectedRacer.IsDirty;
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
                SelectedRacer.IsDirty = true;
            }
        }

    }
}
