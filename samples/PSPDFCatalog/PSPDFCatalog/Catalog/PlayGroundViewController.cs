using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class PlayGroundViewController : PSPDFViewController
	{
		public PlayGroundViewController (PSPDFDocument document) : base (document)
		{
		}

		public PlayGroundViewController (NSUrl documentPath) : this (new PSPDFDocument (documentPath))
		{
		}
	}
}

