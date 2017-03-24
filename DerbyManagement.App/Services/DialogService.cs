using DerbyManagement.App.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace DerbyManagement.App.Services
{
    public class DialogService : IDialogService
    {
        MetroWindow racerDetailView = null;
        MetroWindow divisionDetailView = null;

        public void CloseRacerDetailDialog()
        {
            if (racerDetailView != null)
                racerDetailView.Close();
        }

        public void ShowRacerDetailDialog()
        {
            racerDetailView = new RacerDetailView();
            racerDetailView.ShowDialog();
        }

        public void CloseDivisionDetailDialog()
        {
            if (divisionDetailView != null)
                divisionDetailView.Close();
        }

        public void ShowDivisionDetailDialog()
        {
            divisionDetailView = new DivisionDetailView();
            divisionDetailView.ShowDialog();
        }

        public void ShowMessage(object context, string title, string message)
        {
            IDialogCoordinator dialogCoodinator = new DialogCoordinator();
            dialogCoodinator.ShowMessageAsync(context, title, message);
        }

        public async Task<bool> ShowMessageConfirm(object context, string title, string message)
        {
            IDialogCoordinator dialogCoodinator = new DialogCoordinator();

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No"
            };

            MessageDialogResult result = await dialogCoodinator.ShowMessageAsync(context, title, message,
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            return (result == MessageDialogResult.Affirmative);
        }

    }
}
