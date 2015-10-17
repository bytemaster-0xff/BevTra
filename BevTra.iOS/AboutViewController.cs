using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace BevTra.iOS
{
	partial class AboutViewController : UIViewController
	{
		public AboutViewController (IntPtr handle) : base (handle)
		{
		}

        partial void Close_TouchUpInside(UIButton sender)
        {
            this.DismissModalViewController(true);
        }
    }
}
