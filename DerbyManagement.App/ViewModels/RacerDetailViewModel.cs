using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DerbyManagement.App.ViewModels
{
    public class RacerDetailViewModel : ViewModelBase
    {
        private IDerbyDataService _derbyDataService;
        private IDialogService _dialogService;

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

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
            CancelCommand = new CustomCommand(CancelRacer, CanCancel);
        }

        private void OnRacerRecieved(Racer racer)
        {
            selectedRacer = racer;
        }

        private bool CanSaveRacer(object obj)
        {
            // return selectedRacer.IsDirty;
            return true;
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

    }
}
