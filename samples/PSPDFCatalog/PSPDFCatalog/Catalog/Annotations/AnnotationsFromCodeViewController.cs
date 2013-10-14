using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class AnnotationsFromCodeViewController : PSPDFViewController
	{
		PSPDFAnnotation [] annotationsArr;

		public AnnotationsFromCodeViewController (NSData documentData) : base (new PSPDFDocument (documentData))
		{
			Document.Title = "Programmatically create annotations";

			var annotationsList = new List<PSPDFAnnotation> ();
			float maxHeight = Document.PageInfoForPage (0).RotatedPageRect.Size.Height;
			for (int i = 0; i < 5; i++) {
				var note = new PSPDFNoteAnnotation () {
					BoundingBox = new RectangleF (new PointF (100, (50 + i * maxHeight / 5)), PSPDFNoteAnnotation.ViewFixedSize),
					Contents = string.Format ("Note {0}", (5 - i)) // notes are added bottom-up
				};
				annotationsList.Add (note);
			}
			annotationsArr = annotationsList.ToArray ();
			Document.AddAnnotations (annotationsArr);
		}
	}
}

