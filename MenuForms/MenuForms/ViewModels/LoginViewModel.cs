using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuForms.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
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

        public LoginViewModel()
        {           
            MainText = "Login";
        }
    }
}
