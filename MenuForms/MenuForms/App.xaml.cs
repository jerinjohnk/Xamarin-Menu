using MenuForms.Services.Interfaces;
using MenuForms.ViewModels.Base;
using System.Threading.Tasks;
using MenuForms.Utils;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MenuForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AdaptColorsToHexString();

            if (Device.OS == TargetPlatform.Windows)
            {
                InitNavigation();
            }
        }

        protected override async void OnStart()
        {
            base.OnStart();

            if (Device.OS != TargetPlatform.Windows)
            {
                await InitNavigation();
            }
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        private void AdaptColorsToHexString()
        {
            for (var i = 0; i < this.Resources.Count; i++)
            {
                var key = this.Resources.Keys.ElementAt(i);
                var resource = this.Resources[key];

                if (resource is Color)
                {
                    var color = (Color)resource;
                    this.Resources.Add(key + "HexString", color.ToHexString());
                }
            }
        }
    }
}
