﻿using DerbyManagement.App.Extensions;
using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace DerbyManagement.App.ViewModels
{
    public class RacerDetailViewModel : ViewModelBase, IDataErrorInfo
    {
        private IDerbyDataService _derbyDataService;
        private bool _isLoading;
        private List<Division> _divisions;
        private List<Division> _originalRacerDivisions;
        private Racer _selectedRacer { get; set; }

        public ICommand AddDivisionCommand { get; set; }
        public ICommand RemoveDivisionCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SaveAndAddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int CarNumber
        {
            get { return _selectedRacer.CarNumber; }
            set
            {
                _selectedRacer.CarNumber = value;
                SetRacerDirty();
                RaisePropertyChanged("CarNumber");
            }
        }

        public string CarName
        {
            get { return _selectedRacer.CarName; }
            set
            {
                _selectedRacer.CarName = value;
                SetRacerDirty();
                RaisePropertyChanged("CarName");
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
                RaisePropertyChanged("OwnerLastName");
            }
        }

        public List<Division> Divisions
        {
            get { return _divisions; }
        }

        public ObservableCollection<Division> RacerDivisions
        {
            get { return _selectedRacer.Divisions.ToObservableCollection(); }
            set
            {
                _selectedRacer.Divisions = value.ToList();
                SetRacerDirty();
                RaisePropertyChanged("Divisions");
            }
        }

        public string Error
        {
            get { throw new NotSupportedException("Not supported"); }
        }

        public string this[string propertyName]
        {
            get
            {
                var result = string.Empty;

                if (propertyName.Equals("CarNumber"))
                {
                    var derbyId = _divisions[0].DerbyId;
                    var numberOfCarsWithThisNumber = _derbyDataService.CheckCarNumberUnique(derbyId, 
                        _selectedRacer.RacerId, CarNumber);
                    if (numberOfCarsWithThisNumber > 0)
                        result = "This car number is already taken.  Please select another number.";
                }

                if (result == string.Empty) 
                    result = _selectedRacer[propertyName];

                IsValid = result == string.Empty;
                return result;
            }
        }

        public RacerDetailViewModel(IDerbyDataService derbyDataService)
        {
            _derbyDataService = derbyDataService;

            var currentDerby = _derbyDataService.GetCurrentDerby();
            _divisions = _derbyDataService.GetAllDivisionsExceptChampionship(currentDerby.DerbyId);

            Messenger.Default.Register<Racer>(this, LoadRacer);

            AddDivisionCommand = new CustomCommand(AddDivision, CanAddRemoveDivision);
            RemoveDivisionCommand = new CustomCommand(RemoveDivision, CanAddRemoveDivision);
            SaveCommand = new CustomCommand(SaveRacer, CanSaveRacer);
            SaveAndAddCommand = new CustomCommand(SaveAndAddRacer, CanSaveRacer);
            CancelCommand = new CustomCommand(CancelRacer, CanCancel);
        }

        public void LoadRacer(Racer racer)
        {
            _isLoading = true;
            _selectedRacer = racer;
            _originalRacerDivisions = _selectedRacer.Divisions;
            _isLoading = false;
        }

        #region " Command Handlers "
        private bool CanSaveRacer(object obj)
        {
            return _selectedRacer.IsDirty && IsValid;
        }

        private void SaveRacer(object racer)
        {
            _derbyDataService.Save();
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }

        private void SaveAndAddRacer(object obj)
        {
            _derbyDataService.Save();

            Racer newRacer = _derbyDataService.CreateRacer();
            LoadRacer(newRacer);
            RaisePropertyChanged(string.Empty);
        }

        private void CancelRacer(object obj)
        {
            _selectedRacer.Divisions = _originalRacerDivisions;
            _derbyDataService.Cancel();
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private void AddDivision(object obj)
        {
            if (!(obj is Division))
                throw new ArgumentException("Invalid division");

            var divisionToAdd = (Division)obj;
            var racerDivisions = RacerDivisions;

            racerDivisions.Add(divisionToAdd);
            RacerDivisions = racerDivisions;
        }

        private void RemoveDivision(object obj)
        {
            if (!(obj is Division))
                throw new ArgumentException("Invalid division");

            var divisionToRemove = (Division)obj;
            var racerDivisions = RacerDivisions;

            racerDivisions.Remove(divisionToRemove);
            RacerDivisions = racerDivisions;
        }

        private bool CanAddRemoveDivision(object obj)
        {
            return true;
        }
        #endregion

        private void SetRacerDirty()
        {
            if (!_isLoading)
            {
                _selectedRacer.IsDirty = true;
            }
        }

    }
}
