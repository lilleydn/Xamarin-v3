using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class DropboxPDFViewController : PSPDFViewController
	{
		public DropboxFloatingToolbar FloatingToolbar { get; set; }

		public DropboxPDFViewController (PSPDFDocument document) : base (document)
		{
			PageTransition = PSPDFPageTransition.ScrollContinuous;
			ScrollDirection = PSPDFScrollDirection.Vertical;
			StatusBarStyleSetting = PSPDFStatusBarStyle.Default;
			ShouldHideStatusBarWithHUD = false;
			RenderAnimationEnabled = false;
			ThumbnailBarMode = PSPDFThumbnailBarMode.None;
			ThumbnailController.FilterOptions = null;
			OutlineButtonItem.AvailableControllerOptions = new NSOrderedSet (PSPDFOutlineBarButtonItemOption.Outline);
			LeftBarButtonItems = null;
			RightBarButtonItems = null;
			Title = document.Title;
			DocumentLabelEnabled = false;

			DidChangeViewMode += (o, s) => UpdateFloatingToolbarAnimated (true);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Add the floating toolbar to the HUD
			FloatingToolbar = new DropboxFloatingToolbar (new RectangleF (20, 20, 0, 0));
			UpdateFloatingToolbarAnimated (false); // will update zise
			HUDView.AddSubview (FloatingToolbar);
		}

		public override PSPDFDocument Document {
			get {
				return base.Document;
			}
			set {
				base.Document = value;
				UpdateFloatingToolbarAnimated (IsViewLoaded);
			}
		}

		void UpdateFloatingToolbarAnimated (bool animated)
		{
			UIView.Animate (animated ? 0.25 : 0, () => {
				bool showToolbar = Document.Valid && ViewMode == PSPDFViewMode.Document;
				FloatingToolbar.Alpha = showToolbar ? 0.8f : 0.0f;
			});

			var floatingToolbarButtons = new List<UIButton> ();

			var thumbnailButton = UIButton.FromType (UIButtonType.Custom);
			thumbnailButton.AccessibilityLabel = "Thumbnails";
			thumbnailButton.SetImage (PSPDFIconGenerator.SharedGenerator.IconForType (PSPDFIconType.Thumbnails), UIControlState.Normal);
			thumbnailButton.TouchUpInside += ThumbnailButtonPressed;
			floatingToolbarButtons.Add (thumbnailButton);

			if (Document.OutlineParser.OutlineAvailable) {
				var outlineButton = UIButton.FromType (UIButtonType.Custom);
				outlineButton.AccessibilityLabel = "Outline";
				outlineButton.SetImage (PSPDFIconGenerator.SharedGenerator.IconForType (PSPDFIconType.Outline), UIControlState.Normal);
				outlineButton.TouchUpInside += OutlineButtonPressed;
				floatingToolbarButtons.Add (outlineButton);
			}

			var searchButton = UIButton.FromType (UIButtonType.Custom);
			searchButton.AccessibilityLabel = "Search";
			searchButton.SetImage (PSPDFIconGenerator.ApplyToolbarShadowToImage (UIImage.FromBundle ("search")), UIControlState.Normal);
			searchButton.TouchUpInside += SearchButtonPressed;
			floatingToolbarButtons.Add (searchButton);

			FloatingToolbar.Buttons = floatingToolbarButtons.ToArray ();
		}

		void ThumbnailButtonPressed (object sender, EventArgs e)
		{
			if (ViewMode == PSPDFViewMode.Document)
				SetViewMode (PSPDFViewMode.Thumbnails, true);
			else
				SetViewMode (PSPDFViewMode.Document, true);
		}

		void OutlineButtonPressed (object sender, EventArgs e)
		{
			var buttonSender = sender as UIButton;
			var outlineViewController = new PSPDFOutlineViewController (Document, null) {
				WeakDelegate = this
			};
			PresentModalOrInPopover (outlineViewController, true, true, true, buttonSender, 
			                         NSDictionary.FromObjectAndKey (NSObject.FromObject (UIPopoverArrowDirection.Up), PSPDFViewController.PresentOptionAllowedPopoverArrowDirections));
		}

		void SearchButtonPressed (object sender, EventArgs e)
		{
			var buttonSender = sender as UIButton;
			var searchController = new PSPDFSearchViewController (Document, null) {
				WeakDelegate = this
			};
			var viewController = new UIViewController (searchController.Handle);
			PresentModalOrInPopover (viewController, false, true, true, buttonSender, 
			                         NSDictionary.FromObjectAndKey (NSObject.FromObject (UIPopoverArrowDirection.Up), PSPDFViewController.PresentOptionAllowedPopoverArrowDirections));

		}
	}
}

