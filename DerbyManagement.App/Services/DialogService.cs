using DerbyManagement.App.Views;
using MahApps.Metro.Controls;

namespace DerbyManagement.App.Services
{
    public class DialogService : IDialogService
    {
        MetroWindow racerDetailView = null;

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
    }
}
