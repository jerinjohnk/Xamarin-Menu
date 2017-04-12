using Acr.UserDialogs;
using MenuForms.Services.Interfaces;
using System.Threading.Tasks;

namespace MenuForms.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
    }
}
