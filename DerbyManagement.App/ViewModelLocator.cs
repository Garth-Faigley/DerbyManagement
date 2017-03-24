using DerbyManagement.App.Services;
using DerbyManagement.App.ViewModels;
using DerbyManagement.DAL;

namespace DerbyManagement.App
{
    public class ViewModelLocator
    {
        private static IDialogService dialogService = new DialogService();
        private static IDerbyDataService derbyDataService = new DerbyDataService(new DerbyRepository());

        private static DerbyViewModel derbyViewModel = new DerbyViewModel(derbyDataService, dialogService);
        private static DivisionDetailViewModel divisionDetailViewModel = new DivisionDetailViewModel(derbyDataService);
        private static RacerViewModel racerViewModel = new RacerViewModel(derbyDataService, dialogService);
        private static RacerDetailViewModel racerDetailViewModel = new RacerDetailViewModel(derbyDataService);

        public static DerbyViewModel DerbyViewModel
        {
            get { return derbyViewModel; }
        }

        public static DivisionDetailViewModel DivisionDetailViewModel
        {
            get { return divisionDetailViewModel; }
        }

        public static RacerViewModel RacerViewModel
        {
            get { return racerViewModel; }
        }

        public static RacerDetailViewModel RacerDetailViewModel
        {
            get { return racerDetailViewModel; }
        }

    }
}
