using DerbyManagement.App.Messages;
using DerbyManagement.App.Services;
using DerbyManagement.App.Utility;
using DerbyManagement.Model;
using System.Windows.Input;

namespace DerbyManagement.App.ViewModels
{
    public class DerbyViewModel : ViewModelBase
    {
        private IDerbyDataService _derbyDataService;
        private IDialogService _dialogService;

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

        private Division selectedDivision;
        public Division SelectedDivision
        {
            get { return selectedDivision; }
            set
            {
                selectedDivision = value;
                RaisePropertyChanged("SelectedDivision");
            }
        }

        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public DerbyViewModel(IDerbyDataService derbyDataService, IDialogService dialogService)
        {
            _derbyDataService = derbyDataService;
            _dialogService = dialogService;

            LoadData();
            LoadCommands();

            Messenger.Default.Register<UpdateListMessage>(this, OnUpdateListMessageRecieved);
        }

        private void LoadData()
        {
            Derby = _derbyDataService.GetCurrentDerbyWithDivisions();
        }

        private void LoadCommands()
        {
            EditCommand = new CustomCommand(EditDivision, CanEditDivision);
            AddCommand = new CustomCommand(AddDivision, CanAddDivision);
            DeleteCommand = new CustomCommand(DeleteRacer, CanDeleteDivision);
        }

        private void OnUpdateListMessageRecieved(UpdateListMessage obj)
        {
            LoadData();
            _dialogService.CloseDivisionDetailDialog();
        }
        
        private void EditDivision(object obj)
        {
            Messenger.Default.Send<Division>(selectedDivision);
            _dialogService.ShowDivisionDetailDialog();
        }

        private bool CanEditDivision(object obj)
        {
            return true;
        }

        private void AddDivision(object obj)
        {
            Division newDivision = _derbyDataService.CreateDivision();
            Derby.Divisions.Add(newDivision);
            Messenger.Default.Send<Division>(newDivision);
            _dialogService.ShowDivisionDetailDialog();
        }

        private bool CanAddDivision(object obj)
        {
            return true;
        }

        private async void DeleteRacer(object obj)
        {
            if (await _dialogService.ShowMessageConfirm(this, "Delete Division",
                "Delete Division " + selectedDivision.Name + "?"))
            {
                _derbyDataService.DeleteDivision(selectedDivision);
                LoadData();
            }
        }

        private bool CanDeleteDivision(object obj)
        {
            return true;
        }

    }
}
