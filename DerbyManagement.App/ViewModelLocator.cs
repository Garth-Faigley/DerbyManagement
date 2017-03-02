using DerbyManagement.App.Services;
using DerbyManagement.App.ViewModels;
using DerbyManagement.DAL;

namespace DerbyManagement.App
{
    public class ViewModelLocator
    {
        private static IDerbyDataService derbyDataService = new DerbyDataService(new DerbyRepository());

        private static DerbyViewModel derbyViewModel = new DerbyViewModel(derbyDataService);

        public static DerbyViewModel DerbyViewModel
        {
            get
            {
                return derbyViewModel;
            }
        }
    }
}
