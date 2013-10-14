using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class LinkEditorViewController : PSPDFViewController
	{
		public LinkEditorViewController (PSPDFDocument document) : base (document)
		{
			Document.EditableAnnotationTypes = new NSOrderedSet (
				(NSObject) PSPDFAnnotation.AnnotationStringLink, // Important!!
				(NSObject) PSPDFAnnotation.AnnotationStringHighlight,
				(NSObject) PSPDFAnnotation.AnnotationStringUnderline,
				(NSObject) PSPDFAnnotation.AnnotationStringSquiggly,
				(NSObject) PSPDFAnnotation.AnnotationStringStrikeOut,
				(NSObject) PSPDFAnnotation.AnnotationStringNote,
				(NSObject) PSPDFAnnotation.AnnotationStringFreeText,
				(NSObject) PSPDFAnnotation.AnnotationStringInk,
				(NSObject) PSPDFAnnotation.AnnotationStringSquare,
				(NSObject) PSPDFAnnotation.AnnotationStringCircle,
				(NSObject) PSPDFAnnotation.AnnotationStringStamp );
		}
	}
}

