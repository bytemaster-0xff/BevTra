using BevTra.Core.ViewModels;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using Facebook.LoginKit;
using CoreGraphics;
using System.Diagnostics;
using System.Collections.Generic;

namespace BevTra.iOS
{
	partial class StartupViewController : UIViewController
	{
        LoginButton _loginButton;
        List<string> _readPermissions = new List<string> { "public_profile" };

        public StartupViewController (IntPtr handle) : base (handle)
		{
            
        }

        public StartupViewController()
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _loginButton = new LoginButton(new CGRect(80, 120, 220, 46))
            {
                LoginBehavior = LoginBehavior.SystemAccount,
                ReadPermissions = _readPermissions.ToArray()
            };

            _loginButton.Completed += (sender, e) => {
                if (e.Error != null)
                {
                    Debug.WriteLine(e.Error.Description);
                    return;
                }

                if (e.Result != null && e.Result.IsCancelled)
                {
                    return;
                }

                Vm.LoginSuccss();

                // Handle your successful login
            };

            // Handle actions once the user is logged out
            _loginButton.LoggedOut += (sender, e) => {
                // Handle your logout

            };

            View.Add(_loginButton);
        }

        public StartupViewModel Vm { get { return Application.Locator.Startup; } }

        
	}
}
