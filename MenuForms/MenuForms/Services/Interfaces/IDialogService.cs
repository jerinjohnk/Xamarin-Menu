using System.Threading.Tasks;

namespace MenuForms.Services.Interfaces
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}

