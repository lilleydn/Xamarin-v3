using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class DropboxFloatingToolbar : PSPDFGradientView
	{
		public float Margin { get; set; }

		UIButton [] buttons;
		public UIButton [] Buttons { 
			get {
				return buttons;
			} 
			set {
				if (buttons != value) {
					if (buttons != null)
						foreach (var item in buttons)
							item.RemoveFromSuperview ();
					buttons = value;
					UpdateButtons ();
				}
			} 
		}

		public DropboxFloatingToolbar (RectangleF rect) : base (rect)
		{
			Colors = new [] { UIColor.FromWhiteAlpha (0.184f, 1.0f), UIColor.FromWhiteAlpha (0.146f, 1.0f) };
			Margin = 5;
			Layer.BorderWidth = 1;
			Layer.CornerRadius = 4;
			Opaque = false;
		}

		void UpdateButtons ()
		{
			float totalWidth = 0;
			foreach (var button in Buttons) {
				AddSubview (button);
				button.Frame = new RectangleF (totalWidth, 0, 44, 44);
				totalWidth += 44 + Margin;
			}

			// Update Frame
			Frame = new RectangleF (Frame.Location, new SizeF (totalWidth, 44));
		}
	}
}

