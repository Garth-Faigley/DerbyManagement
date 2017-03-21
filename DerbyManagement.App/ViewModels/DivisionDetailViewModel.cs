using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DerbyManagement.App.ViewModels
{
    class DivisionDetailViewModel : ViewModelBase, IDataErrorInfo
    {
        private IDerbyDataService _derbyDataService;
        private bool _isLoading;

        public ICommand SaveCommand { get; set; }
        public ICommand SaveAndAddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private Division _division;

        public int Order
        {
            get { return _division.Sequence; }
            set
            {
                _division.Sequence = value;
                SetDivisionDirty();
                RaisePropertyChanged("Sequence");
            }
        }

        public string Name
        {
            get { return _division.Name; }
            set
            {
                _division.Name = value;
                SetDivisionDirty();
                RaisePropertyChanged("Name");
            }
        }

        public bool IncludeInChampionship
        {
            get { return _division.IncludeInChampionship; }
            set
            {
                _division.IncludeInChampionship = value;
                SetDivisionDirty();
                RaisePropertyChanged("IncludeInChampionship");
            }
        }

        public bool IsChampionship
        {
            get { return _division.IsChampionship; }
            set
            {
                _division.IsChampionship = value;
                SetDivisionDirty();
                RaisePropertyChanged("IsChampionship");
            }
        }

        public string this[string propertyName]
        {
            get
            {
                var result = string.Empty;

                if (propertyName.Equals("Sequence"))
                {
                    var derbyId = _division.DerbyId;
                    if (derbyId == 0)
                        derbyId = _derbyDataService.GetCurrentDerby().DerbyId;

                    var numberOfDivisionsWithThisSequence = _derbyDataService
                        .CheckSequenceNumberUnique(derbyId, _division.DivisionId, _division.Sequence);
                    if (numberOfDivisionsWithThisSequence > 0)
                        result = "Sequence number must be unique.  Please select another sequence number.";
                }

                if (result == string.Empty)
                    result = _division[propertyName];

                IsValid = result == string.Empty;
                return result;
            }
        }

        public DivisionDetailViewModel(IDerbyDataService derbyDataService)
        {
            _derbyDataService = derbyDataService;

            Messenger.Default.Register<Division>(this, LoadDivision);

            SaveCommand = new CustomCommand(SaveDivision, CanSave);
            SaveAndAddCommand = new CustomCommand(SaveAndAddDivision, CanSave);
            CancelCommand = new CustomCommand(CancelDivision, CanCancel);
        }

        private void LoadDivision(Division division)
        {
            _isLoading = true;
            _division = division;
            _isLoading = false;
        }

        #region " Command Handlers "
        private bool CanSave(object obj)
        {
            return _division.IsDirty && IsValid;
        }

        private void SaveDivision(object division)
        {
            SaveDivision();
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }

        private void SaveAndAddDivision(object obj)
        {
            SaveDivision();

            Division newDivision = _derbyDataService.CreateDivision();
            LoadDivision(newDivision);
            RaisePropertyChanged(string.Empty);
        }

        private void SaveDivision()
        {
            if (_division.DerbyId == 0)
                _division.DerbyId = _derbyDataService.GetCurrentDerby().DerbyId;
            _derbyDataService.Save();
        }

        private void CancelDivision(object obj)
        {
            _derbyDataService.Cancel();
            Messenger.Default.Send<UpdateListMessage>(new UpdateListMessage());
        }

        private bool CanCancel(object obj)
        {
            return true;
        }
        #endregion

        public string Error
        {
            get { throw new NotSupportedException("Not supported"); }
        }

        private void SetDivisionDirty()
        {
            if (!_isLoading)
            {
                _division.IsDirty = true;
            }
        }

    }
}
