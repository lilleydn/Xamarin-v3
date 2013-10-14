using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class CustomLinkAnnotationView : PSPDFLinkAnnotationView
	{
		// MUST HAVE ctor when Subclassing!!! It will crash otherwise.
		public CustomLinkAnnotationView (IntPtr handle) : base (handle)
		{
		}

		// You must manually export Constructors when you need them, this will be called
		// by PSPDFKit when creating LinkAnnotationViews is needed
		[Export ("initWithFrame:")]
		public CustomLinkAnnotationView (RectangleF frame) : base (frame)
		{
			BorderColor = UIColor.Red.ColorWithAlpha (0.5f);
		}
	}
}

