using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.IO;
using MonoTouch.CoreGraphics;
using System.Threading.Tasks;
using MonoTouch.ObjCRuntime;

using PSPDFKit;

namespace PSPDFCatalog
{
	public partial class DVCMenu : DialogViewController
	{
		public static readonly string HackerMonthlyFile = "Pdf/hackermonthly-issue039.pdf";
		public static readonly string ProtectedFile = "Pdf/protected.pdf";

		UIColor barColor;

		public DVCMenu () : base (UITableViewStyle.Grouped, null)
		{
			Root = new RootElement ("PSPDFKit") {
				new Section (PSPDFKitGlobal.VersionString){
					new StringElement ("PSPDFViewController playground", () => {
						var pdfViewer = new PlayGroundViewController (NSUrl.FromFilename (HackerMonthlyFile));
						NavigationController.PushViewController (pdfViewer, true);
					})
				},
				new Section ("Annotations"){
					new StringElement ("Annotations From Code", () => {
						// we use a NSData document here but it'll work even better with a file-based variant.
						NSError err;
						var documentData = NSData.FromUrl (NSUrl.FromFilename (HackerMonthlyFile), NSDataReadingOptions.Mapped, out err);
						var pdfViewer = new AnnotationsFromCodeViewController (documentData);
						NavigationController.PushViewController (pdfViewer, true);
					})
				},
				new Section ("Interface"){
					new StringElement ("Dropbox-like interface", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						var pdfViewer = new DropboxPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					})
				},
				new Section ("Password / Security", "Password is: test123") {
					new StringElement ("Password Preset", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (ProtectedFile));
						document.UnlockWithPassword ("test123");
						var pdfViewer = new PSPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Password Not Preset", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (ProtectedFile));
						var pdfViewer = new PSPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Create Password Protected PDF", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						// Create temp file and password
						var tempPdf = NSUrl.FromFilename (Path.Combine (Path.GetTempPath (), Guid.NewGuid ().ToString () + ".pdf"));
						var password = new NSString ("test123");

						// Lets create the dictionary options needed by the PSPDFProcesor
						// With password protected pages, PSPDFProcessor can only add link annotations.
						// We use a helper class to access the CGPDFContextKeys used by the dictionary
						var processorOptions = new NSMutableDictionary ();
						processorOptions.LowlevelSetObject (password, Helper.CGPDFContextUserPassword);
						processorOptions.LowlevelSetObject (password, Helper.CGPDFContextOwnerPassword);
						processorOptions.LowlevelSetObject (NSNumber.FromInt32 (128), Helper.CGPDFContextEncryptionKeyLength);
						processorOptions.LowlevelSetObject (NSNumber.FromBoolean (true), PSPDFProcessor.AnnotationAsDictionary.Handle);
						processorOptions.LowlevelSetObject (NSNumber.FromObject (PSPDFAnnotationType.Link), PSPDFProcessor.ProcessorAnnotationTypes.Handle);
						// We start a new task so this executes on a separated thread since it is a hevy task and we don't want to block the UI
						Task.Factory.StartNew (()=> {
							NSError err;
							PSPDFProcessor.DefaultProcessor.GeneratePDFFromDocument (document: document, 
							                                                         pageRange: NSIndexSet.FromNSRange (new NSRange (0, (int)document.PageCount)),
							                                                         fileURL: tempPdf,
							                                                         options: (NSDictionary) processorOptions,
							                                                         progressHandler: (currentPage, numberOfProcessedPages, totalPages) => InvokeOnMainThread (()=> PSPDFProgressHUD.ShowProgress (((float)numberOfProcessedPages / (float)totalPages), "Preparing")),
							                                                         error: out err);
						}).ContinueWith ((task) => {
							InvokeOnMainThread (()=> {
								PSPDFProgressHUD.Dismiss ();
								var docToShow = new PSPDFDocument (tempPdf);
								var pdfViewer = new PSPDFViewController (docToShow);
								NavigationController.PushViewController (pdfViewer, true);
							});
						});
					}),
				},
				new Section ("Subclassing", "Examples how to subclass PSPDFKit."){
					new StringElement ("Annotation Link Editor", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						// Need to cast to solve ambiguity between NSObject and string ctor
						document.EditableAnnotationTypes = new NSOrderedSet (
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

						var pdfViewer = new LinkEditorViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Capture Bookmarks", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						document.OverrideClass (new Class (typeof (PSPDFBookmarkParser)), new Class (typeof (CustomBookmarkParser)));
						var pdfViewer = new PSPDFViewController (document);
						pdfViewer.RightBarButtonItems = new NSObject[] { pdfViewer.BookmarkButtonItem, pdfViewer.SearchButtonItem, pdfViewer.OutlineButtonItem, pdfViewer.ViewModeButtonItem };
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Change link background color to red", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						// Note: You can also globally change the color using:
						// PSPDFLinkAnnotationView.SetGlobalBorderColor = UIColor.Green;
						// We don't use this in the example here since it would change the color globally for all examples.
						var pdfViewer = new PSPDFViewController (document);
						pdfViewer.OverrideClass (new Class (typeof (PSPDFLinkAnnotationView)), new Class (typeof (CustomLinkAnnotationView)));
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Custom AnnotationProvider", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						document.SetDidCreateDocumentProviderBlock ((documentProvider)=> {
							documentProvider.AnnotationManager.AnnotationProviders = new NSObject[] { new CustomAnnotationProvider ((int)documentProvider.Document.PageCount), documentProvider.AnnotationManager.FileAnnotationProvider };
						});
						var pdfViewer = new PSPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					})
				},
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (UIDevice.CurrentDevice.CheckSystemVersion (7,0)) {
				barColor = UIColor.FromRGBA (0f, (166f/255f), (240f/255f), 1f);
				UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.LightContent, animated);
				NavigationController.NavigationBar.BarTintColor = barColor;
				NavigationController.Toolbar.TintColor = UIColor.Black;
				NavigationController.View.TintColor = UIColor.White;
				NavigationController.NavigationBar.SetTitleTextAttributes (new UITextAttributes () { TextColor = UIColor.White });
			}
			UIApplication.SharedApplication.SetStatusBarHidden (false, animated ? UIStatusBarAnimation.Fade : UIStatusBarAnimation.None);
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;
			NavigationController.SetToolbarHidden (true, animated);
		}
	}
}
