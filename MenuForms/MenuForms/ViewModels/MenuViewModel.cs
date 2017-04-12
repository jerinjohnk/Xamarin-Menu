using MenuForms.DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuForms.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {       
        private readonly IAuthenticationService _authenticationService;

        private string _mainText;
        public string MainText
        {
            get
            {
                return _mainText;
            }

            set
            {
                _mainText = value;
                RaisePropertyChanged(() => MainText);
            }
        }

        public MenuViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;            
            MainText = "Menu";
        }
    }
}
