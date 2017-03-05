using System.Threading.Tasks;

namespace DerbyManagement.App.Services
{
    public interface IDialogService
    {
        void CloseRacerDetailDialog();
        void ShowRacerDetailDialog();

        // For the MahApps dialogs to work, need to ass these two attributes to the hosting view: 
        //     xmlns:Dialog="clr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro" 
        //     Dialog:DialogParticipation.Register="{Binding}"
        void ShowMessage(object context, string title, string message);
        Task<bool> ShowMessageConfirm(object context, string title, string message);
    }
}
