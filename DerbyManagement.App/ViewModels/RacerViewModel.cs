using DerbyManagement.App.Extensions;
using DerbyManagement.App.Services;
using DerbyManagement.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DerbyManagement.App.ViewModels
{
    public class RacerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IDerbyDataService _derbyDataService;

        private ObservableCollection<Racer> racers;

        public ObservableCollection<Racer> Racers
        {
            get { return racers; }
            set {
                racers = value;
                RaisePropertyChanged("Racers");
            }
        }

        private Racer selectedRacer;

        public Racer SelectedRacer
        {
            get { return selectedRacer; }
            set {
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


        public RacerViewModel(IDerbyDataService derbyDataService)
        {
            _derbyDataService = derbyDataService;
            // TODO- add messanger service

            LoadData();

            //LoadCommands();
        }

        //private void LoadCommands()
        //{
        //    EditCommand = new CustomCommand(EditRacer, CanEditRacer);
        //}

        // TODO
        //private void OnUpdateListMessageReceived(UpdateListMessage obj)
        //{
        //    LoadData();
        //    dialogService.CloseDetailDialog();
        //}

        private void EditRacer(object obj)
        {
            // TODO 

            //Messenger.Default.Send<Racer>(selectedRacer);
            //dialogService.ShowDetailDialog();
        }

        //private bool CanEditRacer(object obj)
        //{
        //    if (SelectedRacer != null)
        //        return true;
        //    return false;
        //}

        private void LoadData()
        {
            Racers = _derbyDataService.GetRacersByDerbyIdWithDivisions(2).ToObservableCollection();
        }

    }
}
