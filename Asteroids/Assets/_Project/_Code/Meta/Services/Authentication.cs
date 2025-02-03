using System;
using System.Threading.Tasks;
using TMPro;
using Zenject;
using Unity.Services.Authentication;
using UnityEngine;

namespace _Project._Code.Meta.Services
{
    public class Authentication : IInitializable
    {
        private ICloudDataControl _cloudDataControl;

        public Authentication(ICloudDataControl cloudDataControl)
        {
            _cloudDataControl = cloudDataControl;
        }
        
        async public void Initialize()
        {
            await SignInAnonymous();
        }

        private async Task SignInAnonymous()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                
                Debug.Log("Sign in Success");
                Debug.Log("Player ID:" + AuthenticationService.Instance.PlayerId);
                
                _cloudDataControl.LoadData();
            }
            catch (Exception e)
            {
                Debug.Log("Sign in failed");
                Debug.LogException(e);
            }
        }
    }
}