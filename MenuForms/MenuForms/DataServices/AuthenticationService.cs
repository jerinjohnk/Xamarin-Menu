using MenuForms.DataServices.Interfaces;
using MenuForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuForms.DataServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestProvider _requestProvider;

        // public bool IsAuthenticated => !string.IsNullOrEmpty(Settings.AccessToken);
        public bool IsAuthenticated => true;

        public AuthenticationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            //var auth = new AuthenticationRequest
            //{
            //    UserName = userName,
            //    Credentials = password,
            //    GrantType = "password"
            //};

           // UriBuilder builder = new UriBuilder(GlobalSettings.AuthenticationEndpoint);
           // builder.Path = "api/login";

            //string uri = builder.ToString();

            //AuthenticationResponse authenticationInfo = await _requestProvider.PostAsync<AuthenticationRequest, AuthenticationResponse>(uri, auth);
            //Settings.UserId = authenticationInfo.UserId;
            //Settings.ProfileId = authenticationInfo.ProfileId;
            //Settings.AccessToken = authenticationInfo.AccessToken;

            return true;
        }

        public Task LogoutAsync()
        {
            //Settings.RemoveUserId();
            //Settings.RemoveProfileId();
            //Settings.RemoveAccessToken();
            //Settings.RemoveCurrentBookingId();

            return Task.FromResult(false);
        }

        //public int GetCurrentUserId()
        //{
        //   // return Settings.UserId;
        //}
    }
}

