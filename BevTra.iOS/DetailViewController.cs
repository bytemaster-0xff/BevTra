using System;
using System.Drawing;
using System.Collections.Generic;
using Foundation;
using UIKit;
using Facebook.CoreKit;
using Facebook.LoginKit;
using CoreGraphics;
using System.Diagnostics;

namespace BevTra.iOS
{
    public partial class DetailViewController : UIViewController
    {
        UIPopoverController masterPopoverController;
        object detailItem;

        List<string> readPermissions = new List<string> { "public_profile" };

        LoginButton loginButton;
        ProfilePictureView pictureView;
        UILabel nameLabel;

        public DetailViewController(IntPtr handle) : base(handle)
        {
        }

        public void SetDetailItem(object newDetailItem)
        {
            if (detailItem != newDetailItem)
            {
                detailItem = newDetailItem;

                // Update the view
                ConfigureView();
            }

            if (masterPopoverController != null)
                masterPopoverController.Dismiss(true);
        }

        void ConfigureView()
        {
            // Update the user interface for the detail item
            if (IsViewLoaded && detailItem != null)
                detailDescriptionLabel.Text = detailItem.ToString();
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            loginButton = new LoginButton(new CGRect(80, 120, 220, 46))
            {
                LoginBehavior = LoginBehavior.SystemAccount,
                ReadPermissions = readPermissions.ToArray()
            };

            loginButton.Completed += (sender, e) => {
                if (e.Error != null)
                {
                    Debug.WriteLine(e.Error.Description);
                }

                if (e.Result != null && e.Result.IsCancelled)
                {
                    // Handle if the user cancelled the login request
                }

                // Handle your successful login
            };

            // Handle actions once the user is logged out
            loginButton.LoggedOut += (sender, e) => {
                // Handle your logout
                nameLabel.Text = "";
            };

            // The user image profile is set automatically once is logged in
            pictureView = new ProfilePictureView(new CGRect(80, 200, 220, 220));

            // Create the label that will hold user's facebook name
            nameLabel = new UILabel(new CGRect(20, 319, 280, 21))
            {
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear
            };

            // If you have been logged into the app before, ask for the your profile name
            if (AccessToken.CurrentAccessToken != null)
            {
                var request = new GraphRequest("/me?fields=name", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
                request.Start((connection, result, error) => {
                    // Handle if something went wrong with the request
                    if (error != null)
                    {
                        new UIAlertView("Error...", error.Description, null, "Ok", null).Show();
                        return;
                    }

                    // Get your profile name
                    var userInfo = result as NSDictionary;
                    nameLabel.Text = userInfo["name"].ToString();
                });
            }

            // Add views to main view
            View.AddSubview(loginButton);
            View.AddSubview(pictureView);
            View.AddSubview(nameLabel);

            // Perform any additional setup after loading the view, typically from a nib.
            ConfigureView();
        }

        [Export("splitViewController:willHideViewController:withBarButtonItem:forPopoverController:")]
        public void WillHideViewController(UISplitViewController splitController, UIViewController viewController, UIBarButtonItem barButtonItem, UIPopoverController popoverController)
        {
            barButtonItem.Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
            NavigationItem.SetLeftBarButtonItem(barButtonItem, true);
            masterPopoverController = popoverController;
        }

        [Export("splitViewController:willShowViewController:invalidatingBarButtonItem:")]
        public void WillShowViewController(UISplitViewController svc, UIViewController vc, UIBarButtonItem button)
        {
            // Called when the view is shown again in the split view, invalidating the button and popover controller.
            NavigationItem.SetLeftBarButtonItem(null, true);
            masterPopoverController = null;
        }
    }
}