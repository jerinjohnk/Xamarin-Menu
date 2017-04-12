using MenuForms.DataServices;
using MenuForms.DataServices.Base;
using MenuForms.DataServices.Interfaces;
using MenuForms.Services;
using MenuForms.Services.Interfaces;
using Microsoft.Practices.Unity;
using System;

namespace MenuForms.ViewModels.Base
{
    public class ViewModelLocator
    {
        private readonly IUnityContainer _unityContainer;

        private static readonly ViewModelLocator _instance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get
            {
                return _instance;
            }
        }

        protected ViewModelLocator()
        {
            _unityContainer = new UnityContainer();

            // providers
            _unityContainer.RegisterType<IRequestProvider, RequestProvider>();
            //_unityContainer.RegisterType<ILocationProvider, LocationProvider>();
            //_unityContainer.RegisterType<IMediaPickerService, MediaPickerService>();

            // services
            _unityContainer.RegisterType<IDialogService, DialogService>();
            RegisterSingleton<INavigationService, NavigationService>();

            // data services
            _unityContainer.RegisterType<IAuthenticationService, AuthenticationService>();
            //_unityContainer.RegisterType<ILeadService, OverdueService>();

            // view models
            // _unityContainer.RegisterType<MenuViewModel>();

            _unityContainer.RegisterType<HomeViewModel>();
            _unityContainer.RegisterType<MainViewModel>();

        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public void Register<T>(T instance)
        {
            _unityContainer.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            _unityContainer.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}