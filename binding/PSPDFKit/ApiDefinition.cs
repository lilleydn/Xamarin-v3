using System;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreAnimation;
using MonoTouch.MessageUI;
using MonoTouch.CoreText;
using MonoTouch.MediaPlayer;

namespace PSPDFKit
{
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFGlobalLock
	{
		[Static, Export ("sharedGlobalLock")]
		PSPDFGlobalLock SharedGlobalLock { get; }

		[Export ("lock")]
		void Lock ();

		[Export ("unlock")]
		void Unlock ();

		[Export ("registerDocumentProvider:")]
		void RegisterDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("deregisterDocumentProvider:")]
		void DeregisterDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("freeAllDocumentProviders")]
		void FreeAllDocumentProviders ();

		[Export ("allowedOpenDocumentRequests", ArgumentSemantic.Assign)]
		uint AllowedOpenDocumentRequests { get; }

		[Export ("limitOpenDocumentProviders")]
		void LimitOpenDocumentProviders ();
	}

	[BaseType (typeof (UIViewController))]
	interface PSPDFBaseViewController
	{

	}

	delegate void UpdateSettingsForRotationHandler (PSPDFViewController pdfController, UIInterfaceOrientation toInterfaceOrientation);

	[BaseType (typeof (PSPDFBaseViewController),
	Delegates = new string [] {"WeakDelegate", "WeakFormSubmissionDelegate"},
	Events = new Type [] { typeof (PSPDFViewControllerDelegate), typeof (PSPDFFormSubmissionDelegate) })]
	interface PSPDFViewController
	{
		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Wrap ("WeakDelegate")]
		PSPDFViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakFormSubmissionDelegate")]
		PSPDFFormSubmissionDelegate FormSubmissionDelegate { get; set; }

		[Export ("formSubmissionDelegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakFormSubmissionDelegate { get; set; }

		[Export ("document", ArgumentSemantic.Retain)]
		PSPDFDocument Document { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("setPage:animated:")]
		bool SetPage (uint page, bool animated);

		[Export ("scrollToNextPageAnimated:")]
		bool ScrollToNextPage (bool animated);

		[Export ("scrollToPreviousPageAnimated:")]
		bool ScrollToPreviousPage (bool animated);

		[Export ("screenPage", ArgumentSemantic.Assign)]
		uint ScreenPage { get; }

		[Export ("scrollRectToVisible:animated:")]
		void ScrollRectToVisible (RectangleF rect, bool animated);

		[Export ("zoomToRect:animated:")]
		void ZoomToRect (RectangleF rect, bool animated);

		[Export ("setZoomScale:animated:")]
		void SetZoomScale (float scale, bool animated);

		[Export ("viewState", ArgumentSemantic.Retain)]
		PSPDFViewState ViewState { get; set; }

		[Export ("setViewState:animated:")]
		void SetViewState (PSPDFViewState viewState, bool animated);

		[Field ("PSPDFViewControllerSearchHeadlessKey", "__Internal")]
		NSString SearchHeadlessKey { get; }

		[Export ("searchForString:options:animated:")]
		void SearchForString (string searchText, [NullAllowed] NSDictionary options, bool animated);

		[Export ("margin", ArgumentSemantic.Assign)]
		UIEdgeInsets Margin { get; set; }

		[Export ("padding", ArgumentSemantic.Assign)]
		SizeF Padding { get; set; }

		[Export ("renderingMode", ArgumentSemantic.Assign)]
		PSPDFPageRenderingMode RenderingMode { get; set; }

		[Export ("smartZoomEnabled", ArgumentSemantic.Assign)]
		bool SmartZoomEnabled { [Bind ("isSmartZoomEnabled")] get; set; }

		[Export ("scrollingEnabled", ArgumentSemantic.Assign)]
		bool ScrollingEnabled { [Bind ("isScrollingEnabled")] get; set; }

		[Export ("viewLockEnabled", ArgumentSemantic.Assign)]
		bool ViewLockEnabled { [Bind ("isViewLockEnabled")] get; set; }

		[Export ("rotationLockEnabled", ArgumentSemantic.Assign)]
		bool RotationLockEnabled { [Bind ("isRotationLockEnabled")] get; set; }

		[Export ("scrollOnTapPageEndEnabled", ArgumentSemantic.Assign)]
		bool ScrollOnTapPageEndEnabled { [Bind ("isScrollOnTapPageEndEnabled")] get; set; }

		[Export ("scrollOnTapPageEndAnimationEnabled", ArgumentSemantic.Assign)]
		bool ScrollOnTapPageEndAnimationEnabled { [Bind ("isScrollOnTapPageEndAnimationEnabled")] get; set; }

		[Export ("scrollOnTapPageEndMargin", ArgumentSemantic.Assign)]
		float ScrollOnTapPageEndMargin { get; set; }

		[Export ("textSelectionEnabled", ArgumentSemantic.Assign)]
		bool TextSelectionEnabled { [Bind ("isTextSelectionEnabled")] get; set; }

		[Export ("imageSelectionEnabled", ArgumentSemantic.Assign)]
		bool ImageSelectionEnabled { [Bind ("isImageSelectionEnabled")] get; set; }

		[Export ("passwordDialogEnabled", ArgumentSemantic.Assign)]
		bool PasswordDialogEnabled { [Bind ("isPasswordDialogEnabled")] get; set; }

		[Export ("internalTapGesturesEnabled", ArgumentSemantic.Assign)]
		bool InternalTapGesturesEnabled { get; set; }

		[Export ("useParentNavigationBar", ArgumentSemantic.Assign)]
		bool UseParentNavigationBar { get; set; }

		[Export ("shouldRestoreNavigationBarStyle", ArgumentSemantic.Assign)]
		bool ShouldRestoreNavigationBarStyle { get; set; }

		[Export ("linkAction", ArgumentSemantic.Assign)]
		PSPDFLinkAction LinkAction { get; set; }

		[Export ("allowedMenuActions", ArgumentSemantic.Assign)]
		PSPDFTextSelectionMenuAction AllowedMenuActions { get; set; }

		[Export ("HUDView", ArgumentSemantic.Retain)]
		PSPDFHUDView HUDView { get;  }

		[Export ("HUDViewMode", ArgumentSemantic.Assign)]
		PSPDFHUDViewMode HUDViewMode { get; set; }

		[Export ("HUDViewAnimation", ArgumentSemantic.Assign)]
		PSPDFHUDViewAnimation HUDViewAnimation { get; set; }

		[Export ("HUDVisible", ArgumentSemantic.Assign)]
		bool HUDVisible { [Bind ("isHUDVisible")] get; set; }

		[Export ("setHUDVisible:animated:")]
		bool SetHUDVisible (bool show, bool animated);

		[Export ("showControlsAnimated:")]
		bool ShowControlsAnimated (bool animated);

		[Export ("hideControlsAnimated:")]
		bool HideControlsAnimated (bool animated);

		[Export ("hideControlsAndPageElementsAnimated:")]
		bool HideControlsAndPageElementsAnimated (bool animated);

		[Export ("toggleControlsAnimated:")]
		bool ToggleControlsAnimated (bool animated);

		[Export ("toolbarEnabled", ArgumentSemantic.Assign)]
		bool ToolbarEnabled { [Bind ("isToolbarEnabled")] get; set; }

		[Export ("allowToolbarTitleChange", ArgumentSemantic.Assign)]
		bool AllowToolbarTitleChange { get; set; }

		[Export ("thumbnailBarMode", ArgumentSemantic.Assign)]
		PSPDFThumbnailBarMode ThumbnailBarMode { get; set; }

		[Export ("pageLabelEnabled", ArgumentSemantic.Assign)]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; set; }

		[Export ("documentLabelEnabled", ArgumentSemantic.Assign)]
		bool DocumentLabelEnabled { [Bind ("isDocumentLabelEnabled")] get; set; }

		[Export ("renderAnimationEnabled", ArgumentSemantic.Assign)]
		bool RenderAnimationEnabled { [Bind ("isRenderAnimationEnabled")] get; set; }

		[Export ("contentView", ArgumentSemantic.Retain)]
		PSPDFHUDView ContentView { get;  }

		[Export ("pageMode", ArgumentSemantic.Assign)]
		PSPDFPageMode PageMode { get; set; }

		[Export ("pageTransition", ArgumentSemantic.Assign)]
		PSPDFPageTransition PageTransition { get; set; }

		[Export ("scrollDirection", ArgumentSemantic.Assign)]
		PSPDFScrollDirection ScrollDirection { get; set; }

		[Export ("doublePageModeOnFirstPage", ArgumentSemantic.Assign)]
		bool DoublePageModeOnFirstPage { [Bind ("isDoublePageModeOnFirstPage")] get; set; }

		[Export ("zoomingSmallDocumentsEnabled", ArgumentSemantic.Assign)]
		bool ZoomingSmallDocumentsEnabled { [Bind ("isZoomingSmallDocumentsEnabled")] get; set; }

		[Export ("pageCurlDirectionLeftToRight", ArgumentSemantic.Assign)]
		bool PageCurlDirectionLeftToRight { [Bind ("isPageCurlDirectionLeftToRight")] get; set; }

		[Export ("fitToWidthEnabled", ArgumentSemantic.Assign)]
		bool FitToWidthEnabled { [Bind ("isFitToWidthEnabled")] get; set; }

		[Export ("alwaysBouncePages", ArgumentSemantic.Assign)]
		bool AlwaysBouncePages { get; set; }

		[Export ("fixedVerticalPositionForFitToWidthEnabledMode", ArgumentSemantic.Assign)]
		bool FixedVerticalPositionForFitToWidthEnabledMode { get; set; }

		[Export ("clipToPageBoundaries", ArgumentSemantic.Assign)]
		bool ClipToPageBoundaries { get; set; }

		[Export ("minimumZoomScale", ArgumentSemantic.Assign)]
		float MinimumZoomScale { get; set; }

		[Export ("maximumZoomScale", ArgumentSemantic.Assign)]
		float MaximumZoomScale { get; set; }

		[Export ("pagePadding", ArgumentSemantic.Assign)]
		float PagePadding { get; set; }

		[Export ("shadowEnabled", ArgumentSemantic.Assign)]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("statusBarStyleSetting", ArgumentSemantic.Assign)]
		PSPDFStatusBarStyle StatusBarStyleSetting { get; set; }

		[Export ("statusBarStyle", ArgumentSemantic.Assign)]
		UIStatusBarStyle StatusBarStyle { get; set; }

		[Export ("navigationBarStyle", ArgumentSemantic.Assign)]
		UIBarStyle NavigationBarStyle { get; set; }

		[Export ("transparentHUD", ArgumentSemantic.Assign)]
		bool TransparentHUD { [Bind ("isTransparentHUD")] get; set; }

		[Export ("shouldHideNavigationBarWithHUD", ArgumentSemantic.Assign)]
		bool ShouldHideNavigationBarWithHUD { get; set; }

		[Export ("shouldHideStatusBarWithHUD", ArgumentSemantic.Assign)]
		bool ShouldHideStatusBarWithHUD { get; set; }

		[Export ("tintColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor TintColor { get; set; }

		[Export ("shouldTintPopovers", ArgumentSemantic.Assign)]
		bool ShouldTintPopovers { get; set; }

		[Export ("shouldTintAlertView", ArgumentSemantic.Assign)]
		bool ShouldTintAlertView { get; set; }

		[Export ("backgroundColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor BackgroundColor { get; set; }

		[Export ("navigationBarHidden", ArgumentSemantic.Assign)]
		bool NavigationBarHidden { [Bind ("isNavigationBarHidden")] get; set; }

		[Export ("pageViewForPage:")]
		PSPDFPageView PageViewForPage (uint page);

		[Export ("popoverController", ArgumentSemantic.Retain)] [NullAllowed]
		UIPopoverController PopoverController { get; set; }

		[Export ("halfModalController", ArgumentSemantic.Retain)] [NullAllowed]
		UIViewController HalfModalController { get; set; }

		[Export ("pagingScrollView", ArgumentSemantic.Retain)] [NullAllowed]
		UIScrollView PagingScrollView { get; }

		[Export ("viewMode", ArgumentSemantic.Assign)]
		PSPDFViewMode ViewMode { get; set; }

		[Export ("setViewMode:animated:")]
		void SetViewMode (PSPDFViewMode viewMode, bool animated);

		[Export ("thumbnailController", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFThumbnailViewController ThumbnailController { get; set; }

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		SizeF ThumbnailSize { get; set; }

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailMargin { get; set; }

		[Export ("visiblePageNumbers")]
		NSOrderedSet VisiblePageNumbers { get; }

		[Export ("visiblePageViews")]
		PSPDFPageView [] VisiblePageViews { get; }

		[Export ("isDoublePageMode")]
		bool IsDoublePageMode { get; }

		[Export ("isLastPage")]
		bool IsLastPage { get; }

		[Export ("isFirstPage")]
		bool IsFirstPage { get; }

		[Export ("masterViewController")]
		UIViewController MasterViewController { get; }

		// PSPDFViewController (PSPDFPresentation) Category

		[Field ("PSPDFPresentOptionRect", "__Internal")]
		NSString PresentOptionRect { get; }

		[Field ("PSPDFPresentOptionPopoverContentSize", "__Internal")]
		NSString PresentOptionPopoverContentSize { get; }

		[Field ("PSPDFPresentOptionAllowedPopoverArrowDirections", "__Internal")]
		NSString PresentOptionAllowedPopoverArrowDirections { get; }

		[Field ("PSPDFPresentOptionModalPresentationStyle", "__Internal")]
		NSString PresentOptionModalPresentationStyle { get; }

		[Field ("PSPDFPresentOptionAlwaysModal", "__Internal")]
		NSString PresentOptionAlwaysModal { get; }

		[Field ("PSPDFPresentOptionAlwaysPopover", "__Internal")]
		NSString PresentOptionAlwaysPopover { get; }

		[Field ("PSPDFPresentOptionPassthroughViews", "__Internal")]
		NSString PresentOptionPassthroughViews { get; }

		[Field ("PSPDFPresentOptionWillDismissBlock", "__Internal")]
		NSString PresentOptionWillDismissBlock { get; }

		[Field ("PSPDFPresentOptionHalfModalMode", "__Internal")]
		NSString PresentOptionHalfModalMode { get; }

		[Field ("PSPDFPresentOptionPersistentCloseButtonMode", "__Internal")]
		NSString PresentOptionPersistentCloseButtonMode { get; }

		[Export ("presentModalOrInPopover:embeddedInNavigationController:withCloseButton:animated:sender:options:")]
		NSObject PresentModalOrInPopover (UIViewController controller, bool embedded, bool closeButton, bool animated, [NullAllowed] NSObject sender, [NullAllowed] NSDictionary options);


		// PSPDFViewController (Annotations) Category

		[Export ("annotationAnimationDuration", ArgumentSemantic.Assign)]
		float AnnotationAnimationDuration { get; set; }

		[Export ("annotationGroupingEnabled", ArgumentSemantic.Assign)]
		bool AnnotationGroupingEnabled { get; set; }

		[Export ("createAnnotationMenuEnabled", ArgumentSemantic.Assign)]
		bool CreateAnnotationMenuEnabled { [Bind ("isCreateAnnotationMenuEnabled")] get; set; }

		[Export ("showAnnotationMenuAfterCreation", ArgumentSemantic.Assign)]
		bool ShowAnnotationMenuAfterCreation { get; set; }

		[Export ("annotationEntersEditModeAfterSecondTapEnabled", ArgumentSemantic.Assign)]
		bool AnnotationEntersEditModeAfterSecondTapEnabled { get; set; }

		[Export ("autosaveEnabled", ArgumentSemantic.Assign)]
		bool AutosaveEnabled { [Bind ("isAutosaveEnabled")] get; set; }

		[Export ("allowBackgroundSaving", ArgumentSemantic.Assign)]
		bool AllowBackgroundSaving { get; set; }

		// PSPDFViewController (PSPDFToolbar) Category

		[Export ("closeButtonItem", ArgumentSemantic.Retain)]
		PSPDFCloseBarButtonItem CloseButtonItem { get; }

		[Export ("outlineButtonItem", ArgumentSemantic.Retain)]
		PSPDFOutlineBarButtonItem OutlineButtonItem { get; }

		[Export ("searchButtonItem", ArgumentSemantic.Retain)]
		PSPDFSearchBarButtonItem SearchButtonItem { get; }

		[Export ("viewModeButtonItem", ArgumentSemantic.Retain)]
		PSPDFViewModeBarButtonItem ViewModeButtonItem { get; }

		[Export ("printButtonItem", ArgumentSemantic.Retain)]
		PSPDFPrintBarButtonItem PrintButtonItem { get; }

		[Export ("openInButtonItem", ArgumentSemantic.Retain)]
		PSPDFOpenInBarButtonItem OpenInButtonItem { get; }

		[Export ("emailButtonItem", ArgumentSemantic.Retain)]
		PSPDFEmailBarButtonItem EmailButtonItem { get; }

		[Export ("annotationButtonItem", ArgumentSemantic.Retain)]
		PSPDFAnnotationBarButtonItem AnnotationButtonItem { get; }

		[Export ("bookmarkButtonItem", ArgumentSemantic.Retain)]
		PSPDFBookmarkBarButtonItem BookmarkButtonItem { get; }

		[Export ("brightnessButtonItem", ArgumentSemantic.Retain)]
		PSPDFBrightnessBarButtonItem BrightnessButtonItem { get; }

		[Export ("activityButtonItem", ArgumentSemantic.Retain)]
		PSPDFActivityBarButtonItem ActivityButtonItem { get; }

		[Export ("additionalActionsButtonItem", ArgumentSemantic.Retain)]
		PSPDFMoreBarButtonItem AdditionalActionsButtonItem { get; }

		[Export ("leftBarButtonItems", ArgumentSemantic.Copy)]
		NSObject [] LeftBarButtonItems { get; [NullAllowed] set; }

		[Export ("rightBarButtonItems", ArgumentSemantic.Copy)]
		NSObject [] RightBarButtonItems { get; [NullAllowed] set; }

		[Export ("additionalBarButtonItems", ArgumentSemantic.Copy)]
		NSObject [] AdditionalBarButtonItems { get; [NullAllowed] set; }

		[Export ("barButtonItemsAlwaysEnabled", ArgumentSemantic.Copy)]
		NSObject [] BarButtonItemsAlwaysEnabled { get; [NullAllowed] set; }

		[Export ("minLeftToolbarWidth", ArgumentSemantic.Assign)]
		float MinLeftToolbarWidth { get; set; }

		[Export ("minRightToolbarWidth", ArgumentSemantic.Assign)]
		float MinRightToolbarWidth { get; set; }

		[Export ("useBorderedToolbarStyle", ArgumentSemantic.Assign)]
		bool UseBorderedToolbarStyle { get; set; }

		// PSPDFViewController (SubclassingHooks) Category

		[Export ("commonInitWithDocument:")]
		void CommonInitWithDocument (PSPDFDocument document);

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, Class subclass);

		[Export ("createToolbarAnimated:")]
		void CreateToolbarAnimated (bool animated);

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbarAnimated (bool animated);

		[Export ("setLeftBarButtonItems:inNavigationItem:animated:")]
		void SetLeftBarButtonItems (PSPDFBarButtonItem [] leftBarButtonItems, UINavigationItem navigationItem, bool animated);

		[Export ("setRightBarButtonItems:inNavigationItem:animated:")]
		void SetRightBarButtonItems (PSPDFBarButtonItem [] rightBarButtonItems, UINavigationItem navigationItem, bool animated);

		[Export ("updateBarButtonItem:animated:")]
		void UpdateBarButtonItem (UIBarButtonItem barButtonItem, bool animated);

		[Export ("updateSettingsForRotation:")]
		void UpdateSettingsForRotation (UIInterfaceOrientation toInterfaceOrientation);

		[Export ("setUpdateSettingsForRotationBlock:")]
		void SetUpdateSettingsForRotationHandler (UpdateSettingsForRotationHandler handler);

		[Export ("clearHighlightedSearchResults")]
		void ClearHighlightedSearchResults ();

		[Export ("addHighlightSearchResults:")]
		void AddHighlightSearchResults (PSPDFSearchResult [] searchResults);

		[Export ("animateSearchHighlight:")]
		void AnimateSearchHighlight (PSPDFSearchResult searchResult);

		[Export ("updateThumbnailBarFrameAnimated:")]
		void UpdateThumbnailBarFrameAnimated ([NullAllowed]PSPDFSearchResult searchResult);

		[Export ("pageTransitionController", ArgumentSemantic.Retain)]
		UIViewController PageTransitionController { get; }

		[Export ("contentRect")]
		RectangleF ContentRect { get; }

		[Export ("visibleAnnotationToolbar")]
		PSPDFAnnotationToolbar VisibleAnnotationToolbar { get; }

		[Export ("pageLabel", ArgumentSemantic.Retain)]  [NullAllowed]
		PSPDFPageLabelView PageLabel { get; set; }

		[Export ("pageLabelDistance", ArgumentSemantic.Assign)]
		float PageLabelDistance { get; set; }

		[Export ("documentLabel", ArgumentSemantic.Retain)]  [NullAllowed]
		PSPDFDocumentLabelView DocumentLabel { get; set; }

		[Export ("scrobbleBar", ArgumentSemantic.Retain)] 
		PSPDFScrobbleBar ScrobbleBar { get; }

		[Export ("thumbnailBar", ArgumentSemantic.Retain)]
		PSPDFScrobbleBar ThumbnailBar { get; }

		[Export ("annotationViewCache", ArgumentSemantic.Retain)]
		PSPDFAnnotationViewCache AnnotationViewCache { get; }

		[Export ("createNewControllerForDocument:")]
		PSPDFViewController CreateNewControllerForDocument (PSPDFDocument document);

		[Export ("calculatedVisiblePageNumbers")]
		NSObject [] CalculatedVisiblePageNumbers { get; }
	}

	interface IPSPDFViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFViewControllerDelegate
	{
		[Export ("pdfViewController:shouldChangeDocument:"), DelegateName ("PSPDFViewControllerShouldChangeDocument"), NoDefaultValue]
		bool ShouldChangeDocument (PSPDFViewController pdfController, PSPDFDocument document);

		[Export ("pdfViewController:didChangeDocument:"), EventArgs ("PSPDFViewController")]
		void DidChangeDocument (PSPDFViewController pdfController, PSPDFDocument document);

		[Export ("pdfViewController:shouldScrollToPage:"), DelegateName ("PSPDFViewControllerShouldScrollToPage"), NoDefaultValue]
		bool ShouldScrollToPage (PSPDFViewController pdfController, uint page);

		[Export ("pdfViewController:didShowPageView:"), EventArgs ("PSPDFViewControllerPageView")]
		void DidShowPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didRenderPageView:"), EventArgs ("PSPDFViewControllerPageView")]
		void DidRenderPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didLoadPageView:"), EventArgs ("PSPDFViewControllerPageView")]
		void DidLoadPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:willUnloadPageView:"), EventArgs ("PSPDFViewControllerPageView")]
		void WillUnloadPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didBeginPageDragging:"), EventArgs ("PSPDFViewControllerPageDragging")]
		void DidBeginPageDragging (PSPDFViewController pdfController, UIScrollView scrollView);

		[Export ("pdfViewController:didEndPageDragging:willDecelerate:withVelocity:targetContentOffset:"), EventArgs ("PSPDFViewControllerDidPageDragging")]
		void DidEndPageDragging (PSPDFViewController pdfController, UIScrollView scrollView, bool decelerate, PointF velocity, ref PointF targetContentOffset);

		[Export ("pdfViewController:didEndPageScrollingAnimation:"), EventArgs ("PSPDFViewControllerPageDragging")]
		void DidEndPageScrollingAnimation (PSPDFViewController pdfController, UIScrollView scrollView);

		[Export ("pdfViewController:didBeginPageZooming:"), EventArgs ("PSPDFViewControllerPageDragging")]
		void DidBeginPageZooming (PSPDFViewController pdfController, UIScrollView scrollView);

		[Export ("pdfViewController:didEndPageZooming:atScale:"), EventArgs ("PSPDFViewControllerEndPageZooming")]
		void DidEndPageZooming (PSPDFViewController pdfController, UIScrollView scrollView, float scale);

		[Export ("pdfViewController:documentForRelativePath:"), DelegateName ("PSPDFViewControllerDocumentForRelativePath"), NoDefaultValue]
		PSPDFDocument DocumentForRelativePath (PSPDFViewController pdfController, string relativePath);

		[Export ("pdfViewController:didTapOnPageView:atPoint:"), DelegateName ("PSPDFViewControllerDidTapOnPageView"), NoDefaultValue]
		bool DidTapOnPageView (PSPDFViewController pdfController, uint page, PointF viewPoint);

		[Export ("pdfViewController:didLongPressOnPageView:atPoint:gestureRecognizer:"), DelegateName ("PSPDFViewControllerDidLongPressOnPageView"), NoDefaultValue]
		bool DidLongPressOnPageView (PSPDFViewController pdfController, PSPDFPageView pageView, PointF viewPoint, UILongPressGestureRecognizer gestureRecognizer);

		[Export ("pdfViewController:shouldSelectText:withGlyphs:atRect:onPageView:"), DelegateName ("PSPDFViewControllerShouldSelectText"), NoDefaultValue]
		bool ShouldSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph [] glyphs, RectangleF rect, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectText:withGlyphs:atRect:onPageView:"), EventArgs ("PSPDFViewControllerDidSelectText")]
		void DidSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph [] glyphs, RectangleF rect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedText:inRect:onPageView:"), DelegateName ("PSPDFViewControllerShouldShowMenuItemsForSelectedText"), NoDefaultValue]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedText (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, RectangleF rect, string selectedText, RectangleF textRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedImage:inRect:onPageView:"), DelegateName ("PSPDFViewControllerShouldShowMenuItemsForSelectedImage"), NoDefaultValue]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedImage (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, RectangleF rect, PSPDFImageInfo selectedImage, RectangleF textRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forAnnotations:inRect:onPageView:"), DelegateName ("PSPDFViewControllerShouldShowMenuItemsForAnnotations"), NoDefaultValue]
		PSPDFMenuItem [] ShouldShowMenuItemsForAnnotations (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, RectangleF rect, PSPDFAnnotation [] annotations, RectangleF textRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldDisplayAnnotation:onPageView:"), DelegateName ("PSPDFViewControllerShouldDisplayAnnotation"), NoDefaultValue]
		bool ShouldDisplayAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, PSPDFPageView pageView);

		// TODO: Ponder the Switch PSPDFAnnotationViewProtocol annotationView to IPSPDFAnnotationViewProtocol annotationView
		[Export ("pdfViewController:didTapOnAnnotation:annotationPoint:annotationView:pageView:viewPoint:"), DelegateName ("PSPDFViewControllerDidTapOnAnnotation"), NoDefaultValue]
		bool DidTapOnAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, PointF annotationPoint, PSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView, PointF viewPoint);

		[Export ("pdfViewController:shouldSelectAnnotations:onPageView:"), DelegateName ("PSPDFViewControllerShouldSelectAnnotations"), NoDefaultValue]
		bool ShouldSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectAnnotations:onPageView:"), EventArgs ("PSPDFViewControllerDidSelectAnnotations")]
		void DidSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView);

		// TODO: Ponder the Switch PSPDFAnnotationViewProtocol to IPSPDFAnnotationViewProtocol retrun type and param
		[Export ("pdfViewController:annotationView:forAnnotation:onPageView:"), DelegateName ("PSPDFViewControllerAnnotationViewForAnnotation"), NoDefaultValue]
		PSPDFAnnotationViewProtocol AnnotationViewForAnnotation (PSPDFViewController pdfController, PSPDFAnnotationViewProtocol annotationView, PSPDFAnnotation annotation, PSPDFPageView pageView);

		// TODO: Ponder the Switch PSPDFAnnotationViewProtocol annotationView to IPSPDFAnnotationViewProtocol annotationView
		[Export ("pdfViewController:willShowAnnotationView:onPageView:"), EventArgs ("PSPDFViewControllerAnnotationView")]
		void WillShowAnnotationView (PSPDFViewController pdfController, PSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView);

		// TODO: Ponder the Switch PSPDFAnnotationViewProtocol annotationView to IPSPDFAnnotationViewProtocol annotationView
		[Export ("pdfViewController:didShowAnnotationView:onPageView:"), EventArgs ("PSPDFViewControllerAnnotationView")]
		void DidShowAnnotationView (PSPDFViewController pdfController, PSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowController:embeddedInController:options:animated:"), DelegateName ("PSPDFViewControllerShouldShowController"), NoDefaultValue]
		bool ShouldShowController (PSPDFViewController pdfController, NSObject viewController, NSObject embeddedInController, NSDictionary options, bool animated);

		[Export ("pdfViewController:didShowController:embeddedInController:options:animated:"), EventArgs ("PSPDFViewControllerDidShowController")]
		void DidShowController (PSPDFViewController pdfController, NSObject viewController, NSObject embeddedInController, NSDictionary options, bool animated);

		[Export ("pdfViewController:requestsUpdateForBarButtonItem:animated:"), EventArgs ("PSPDFViewControllerRequestsUpdateForBarButtonItem")]
		void RequestsUpdateForBarButtonItem (PSPDFViewController pdfController, UIBarButtonItem barButtonItem, bool animated);

		[Export ("pdfViewController:didChangeViewMode:"), EventArgs ("PSPDFViewControllerDidChangeViewMode")]
		void DidChangeViewMode (PSPDFViewController pdfController, PSPDFViewMode viewMode);

		[Export ("pdfViewControllerWillDismiss:"), EventArgs ("PSPDFViewController")]
		void WillDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewControllerDidDismiss:"), EventArgs ("PSPDFViewController")]
		void DidDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewController:shouldShowHUD:"), DelegateName ("PSPDFViewControllerShouldShowHud"), NoDefaultValue]
		bool ShouldShowHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didShowHUD:"), DelegateName ("PSPDFViewControllerDidShowHud"), NoDefaultValue]
		bool DidShowHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:shouldHideHUD:"), DelegateName ("PSPDFViewControllerShouldHideHud"), NoDefaultValue]
		bool ShouldHideHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didHideHUD:"), EventArgs ("PSPDFViewControllerDidHideHud")]
		void DidHideHud (PSPDFViewController pdfController, bool animated);
	}

	delegate void DidCreateDocumentProviderHandler (PSPDFDocumentProvider documentProvider);
	delegate void SaveAnnotationsCompletionHandler (PSPDFAnnotation [] savedAnnotations, NSError error);
	delegate void DetectingLinkTypesProgressHandler (PSPDFAnnotation [] annotations, uint page, bool stop, NSError error);

	[BaseType (typeof (NSObject),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFDocumentDelegate) })]
	interface PSPDFDocument
	{
		[Static]
		[Export ("document")]
		PSPDFDocument Document { get; }

		[Static]
		[Export ("documentWithURL:")]
		PSPDFDocument FromUrl (NSUrl url);

		[Static]
		[Export ("documentWithData:")]
		PSPDFDocument FromData (NSData data);

		[Static]
		[Export ("documentWithDataArray:")]
		PSPDFDocument FromData (NSData [] data);

		[Static]
		[Export ("documentWithDataProvider:")] [Internal]
		PSPDFDocument FromDataProvider_ (IntPtr /* CGDataProvider */ dataProvider);

		[Static]
		[Export ("documentWithDataProviderArray:")]
		PSPDFDocument FromDataProvider (CGDataProvider [] dataProviders);

		[Static]
		[Export ("documentWithBaseURL:files:")]
		PSPDFDocument FromBaseUrl (NSUrl baseUrl, string [] files);

		[Static]
		[Export ("documentWithBaseURL:fileTemplate:startPage:endPage:")]
		PSPDFDocument FromBaseUrl (NSUrl baseUrl, string fileTemplate, int startPage, int endPage);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithData:")]
		IntPtr Constructor (NSData data);

		[Export ("initWithDataArray:")]
		IntPtr Constructor (NSData [] data);

//		Use the Static Ctor instead
//		[Export ("initWithDataProvider:")] [Internal]
//		IntPtr Constructor (IntPtr /* CGDataProvider */ dataProvider);

		[Export ("initWithDataProviderArray:")]
		IntPtr Constructor (CGDataProvider [] dataProviders);

		[Export ("initWithBaseURL:files:")]
		IntPtr Constructor (NSUrl baseUrl, string [] files);

		[Export ("initWithBaseURL:fileTemplate:startPage:endPage:")]
		IntPtr Constructor (NSUrl baseUrl, string fileTemplate, int startPage, int endPage);

		[Export ("isEqualToDocument:")]
		bool IsEqualToDocument (PSPDFDocument otherDocument);

		[Wrap ("WeakDelegate")]
		PSPDFDocumentDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("appendFile:")]
		void AppendFile ([NullAllowed] string file);

		[Export ("pathForPage:")]
		NSUrl PathForPage (uint page);

		[Export ("fileIndexForPage:")]
		uint FileIndexForPage (uint page);

		[Export ("URLForFileIndex:")]
		NSUrl UrlForFileIndex (int fileIndex);

		[Export ("filesWithBasePath")]
		NSUrl [] FilesWithBasePath { get; }

		[Export ("fileNamesWithDataDictionary")]
		NSDictionary FileNamesWithDataDictionary { get; }

		[Export ("fileNameForPage:")]
		string FileNameForPage (uint pageIndex);

		[Export ("fileName")]
		string FileName { get; }

		[Export ("baseURL", ArgumentSemantic.Retain)][NullAllowed]
		NSUrl BaseUtl { get; set; }

		[Export ("files", ArgumentSemantic.Copy)]
		string [] Files { get; set; }

		[Export ("fileURL", ArgumentSemantic.Retain)][NullAllowed]
		NSUrl FileURL { get; set; }

		[Export ("data", ArgumentSemantic.Copy)]
		NSData Data { get; }

		[Export ("dataArray", ArgumentSemantic.Copy)]
		NSData [] DataArray { get; }

		[Export ("dataProviderArray", ArgumentSemantic.Copy)]
		CGDataProvider [] DataProviderArray { get; }

		[Export ("UID", ArgumentSemantic.Copy)]
		string Uid { get; set; }

		[Export ("valid", ArgumentSemantic.Assign)]
		bool Valid { [Bind ("isValid")] get; }

		[Export ("documentProviderForPage:")]
		PSPDFDocumentProvider DocumentProviderForPage (uint page);

		[Export ("pageOffsetForDocumentProvider:")]
		uint PageOffsetForDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("documentProviders")]
		PSPDFDocumentProvider [] DocumentProviders { get; }

		[Export ("pageCount", ArgumentSemantic.Assign)]
		uint PageCount { get; }

		[Export ("pageRange", ArgumentSemantic.Copy)] [NullAllowed]
		NSIndexSet PageRange { get; set; }

		[Export ("pageWithPageRange:")]
		uint PageWithPageRange (uint page);

		[Export ("PDFPageNumberForPage:")]
		uint PdfPageNumberForPage (uint page);

		[Export ("pageInfoForPage:")]
		PSPDFPageInfo PageInfoForPage (uint page);

		[Export ("displayingPdfController", ArgumentSemantic.Assign)]
		PSPDFViewController DisplayingPdfController { get; set; }

		[Export ("displayingPage", ArgumentSemantic.Assign)]
		uint DisplayingPage { get; }

		[Export ("textSearch", ArgumentSemantic.Retain)]  [NullAllowed]
		PSPDFTextSearch TextSearch { get; set; }

		[Export ("outlineParser", ArgumentSemantic.Retain)]
		PSPDFOutlineParser OutlineParser { get; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; set; }

		// PSPDFDocument (Caching) Category

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("fillCache")]
		void FillCache ();

		[Export ("dataDirectory", ArgumentSemantic.Copy)]
		string DataDirectory { get; set; }

		[Export ("ensureDataDirectoryExistsWithError:")] [Internal]
		bool _EnsureDataDirectoryExists (IntPtr error);

		[Export ("diskCacheStrategy", ArgumentSemantic.Assign)]
		PSPDFDiskCacheStrategy DiskCacheStrategy { get; set; }

		// PSPDFDocument (Security) Category

		[Export ("unlockWithPassword:")]
		bool UnlockWithPassword (string password);

		[Export ("lock")]
		void Lock ();

		[Export ("password", ArgumentSemantic.Copy)]
		string Password { get; set; }

		[Export ("isEncrypted", ArgumentSemantic.Assign)]
		bool IsEncrypted { get; }

		[Export ("encryptionFilter", ArgumentSemantic.Copy)]
		string EncryptionFilter { get; set; }

		[Export ("isLocked", ArgumentSemantic.Assign)]
		bool IsLocked { get; }

		[Export ("allowsPrinting", ArgumentSemantic.Assign)]
		bool AllowsPrinting { get; }

		[Export ("allowsCopying", ArgumentSemantic.Assign)]
		bool AllowsCopying { get; set; }

		// PSPDFDocument (Bookmarks) Category

		[Export ("bookmarksEnabled", ArgumentSemantic.Assign)]
		bool BookmarksEnabled { [Bind ("isBookmarksEnabled")] get; set; }

		[Export ("bookmarkParser", ArgumentSemantic.Retain)]  [NullAllowed]
		PSPDFBookmarkParser BookmarkParser { get; set; }

		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		// PSPDFDocument (PageLabels) Category

		[Export ("pageLabelsEnabled", ArgumentSemantic.Assign)]
		bool PageLabelsEnabled { [Bind ("isPageLabelsEnabled")] get; set; }

		[Export ("pageLabelForPage:substituteWithPlainLabel:")]
		string PageLabelForPage (uint page, bool substitute);

		[Export ("pageForPageLabel:partialMatching:")]
		uint PageLabelForPage (string pageLabel, bool partialMatching);

		// PSPDFDocument (Forms) Category

		[Export ("formsEnabled", ArgumentSemantic.Assign)]
		bool FormsEnabled { [Bind ("isFormsEnabled")] get; set; }

		[Export ("formParser", ArgumentSemantic.Retain)]
		PSPDFFormParser FormParser { get; }

		// PSPDFDocument (Annotations) Category

		[Export ("annotationsEnabled", ArgumentSemantic.Assign)]
		bool AnnotationsEnabled { [Bind ("isAnnotationsEnabled")] get; set; }

		[Export ("annotationsForPage:type:")]
		PSPDFAnnotation [] AnnotationsForPage (uint page, PSPDFAnnotationType type);

		[Export ("addAnnotations:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations);

		[Export ("removeAnnotations:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations);

		[Export ("allAnnotationsOfType:")]
		NSDictionary AllAnnotationsOfType (PSPDFAnnotationType annotationType);

		[Export ("annotationManagerForPage:")]
		PSPDFAnnotationManager AnnotationManagerForPage (uint page);

		// PSPDFDocument (AnnotationSaving) Category

		[Notification]
		[Field ("PSPDFDocumentWillSaveAnnotationsNotification", "__Internal")]
		NSString DocumentWillSaveAnnotationsNotification { get; }

		[Export ("editableAnnotationTypes", ArgumentSemantic.Copy)][NullAllowed]
		NSObject /* HACK: NSOrderedSet */ EditableAnnotationTypes { get; set; }

		[Export ("canEmbedAnnotations", ArgumentSemantic.Assign)]
		bool CanEmbedAnnotations { get; }

		[Export ("annotationSaveMode", ArgumentSemantic.Assign)]
		PSPDFAnnotationSaveMode AnnotationSaveMode { get; set; }

		[Export ("defaultAnnotationUsername", ArgumentSemantic.Copy)][NullAllowed]
		string DefaultAnnotationUsername { get; set; }

		[Field ("PSPDFAnnotationWriteOptionsGenerateAppearanceStreamForTypeKey", "__Internal")]
		NSString WriteOptionsGenerateAppearanceStreamForTypeKey { get; }

		[Export ("annotationWritingOptions", ArgumentSemantic.Copy)][NullAllowed]
		NSDictionary AnnotationWritingOptions { get; set; }

		[Export ("saveAnnotationsWithCompletionBlock:")]
		void SaveAnnotationsWithCompletionHandler (SaveAnnotationsCompletionHandler completionHandler);

		[Export ("saveAnnotationsWithError:")] [Internal]
		bool _SaveAnnotationsWithError (IntPtr error);

		[Export ("hasDirtyAnnotations")]
		bool HasDirtyAnnotations { get; }

		// PSPDFDocument (Rendering) Category

		[Field ("PSPDFPreserveAspectRatio", "__Internal")]
		NSString PreserveAspectRatio { get; }

		[Field ("PSPDFIgnoreDisplaySettings", "__Internal")]
		NSString IgnoreDisplaySettings { get; }

		[Export ("imageForPage:size:clippedToRect:annotations:options:receipt:error:")]
		UIImage ImageForPage (uint page, SizeF size, RectangleF clipRect, PSPDFAnnotation [] annotations, NSDictionary options, out PSPDFRenderReceipt receipt, out NSError error);

		[Export ("renderPage:context:size:clippedToRect:annotations:options:error:")]
		PSPDFRenderReceipt RenderPage (uint page, CGContext context, SizeF size, RectangleF clipRect, PSPDFAnnotation [] annotations, NSDictionary options, out NSError error);

		[Export ("renderOptions", ArgumentSemantic.Copy)][NullAllowed]
		NSDictionary RenderOptions { get; set; }

		[Export ("renderAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType renderAnnotationTypes { get; set; }

		// PSPDFDocument DataDetection Category

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsFromDetectingLinkTypes:pagesInRange:progress:error:")]
		NSDictionary AnnotationsFromDetectingLinkTypes (PSPDFTextCheckingType textLinkTypes, NSIndexSet pageRange, DetectingLinkTypesProgressHandler progressHandler, out NSError error);

		// PSPDFDocument (ObjectFinder) Category

		[Field ("PSPDFObjectsGlyphs", "__Internal")]
		NSString ObjectsGlyphs { get; }

		[Field ("PSPDFObjectsText", "__Internal")]
		NSString ObjectsText { get; }

		[Field ("PSPDFObjectsFullWords", "__Internal")]
		NSString ObjectsFullWords { get; }

		[Field ("PSPDFObjectsTextBlocks", "__Internal")]
		NSString ObjectsTextBlocks { get; }

		[Field ("PSPDFObjectsTextBlocksIgnoreLarge", "__Internal")]
		NSString ObjectsTextBlocksIgnoreLarge { get; }

		[Field ("PSPDFObjectsAnnotationPageBounds", "__Internal")]
		NSString ObjectsAnnotationPageBounds { get; }

		[Field ("PSPDFObjectsImages", "__Internal")]
		NSString ObjectsImages { get; }

		[Field ("PSPDFObjectsSmartSort", "__Internal")]
		NSString ObjectsSmartSort { get; }

		[Field ("PSPDFObjectsTextFlow", "__Internal")]
		NSString ObjectsTextFlow { get; }

		[Field ("PSPDFObjectsFindFirstOnly", "__Internal")]
		NSString ObjectsFindFirstOnly { get; }

		[Field ("PSPDFObjectsTestIntersection", "__Internal")]
		NSString ObjectsTestIntersection { get; }

		// Output categories

		[Field ("PSPDFObjectsGlyphKey", "__Internal")]
		NSString ObjectsGlyphKey { get; }

		[Field ("PSPDFObjectsWordKey", "__Internal")]
		NSString ObjectsWordKey { get; }

		[Field ("PSPDFObjectsTextKey", "__Internal")]
		NSString ObjectsTextKey { get; }

		[Field ("PSPDFObjectsTextBlockKey", "__Internal")]
		NSString ObjectsTextBlockKey { get; }

		[Field ("PSPDFObjectsAnnotationKey", "__Internal")]
		NSString ObjectsAnnotationKey { get; }

		[Field ("PSPDFObjectsImageKey", "__Internal")]
		NSString ObjectsImageKey { get; }

		[Export ("objectsAtPDFPoint:page:options:")]
		NSDictionary ObjectsAtPDFPoint (PointF pdfPoint, uint page, NSDictionary options);

		[Export ("objectsAtPDFRect:page:options:")]
		NSDictionary ObjectsAtPDFRect (RectangleF pdfRect, uint page, NSDictionary options);

		[Export ("textParserForPage:")]
		PSPDFTextParser TextParserForPage (uint page);

		[Export ("hasLoadedTextParserForPage:")]
		bool HasLoadedTextParserForPage (uint page);

		[Export ("textParserHideGlyphsOutsidePageRect", ArgumentSemantic.Assign)]
		bool TextParserHideGlyphsOutsidePageRect { get; set; }

		// PSPDFDocument (Metadata) Category

		[Export ("title", ArgumentSemantic.Copy)]
		string Title { get; set; }

		[Export ("titleLoaded", ArgumentSemantic.Assign)]
		bool TitleLoaded { [Bind ("isTitleLoaded")] get; }

		[Field ("PSPDFMetadataKeyTitle", "__Internal")]
		NSString MetadataKeyTitle { get; }

		[Field ("PSPDFMetadataKeyAuthor", "__Internal")]
		NSString MetadataKeyAuthor { get; }

		[Field ("PSPDFMetadataKeySubject", "__Internal")]
		NSString MetadataKeySubject { get; }

		[Field ("PSPDFMetadataKeyKeywords", "__Internal")]
		NSString MetadataKeyKeywords { get; }

		[Field ("PSPDFMetadataKeyCreator", "__Internal")]
		NSString MetadataKeyCreator { get; }

		[Field ("PSPDFMetadataKeyProducer", "__Internal")]
		NSString MetadataKeyProducer { get; }

		[Field ("PSPDFMetadataKeyCreationDate", "__Internal")]
		NSString MetadataKeyCreationDate { get; }

		[Field ("PSPDFMetadataKeyModDate", "__Internal")]
		NSString MetadataKeyModDate { get; }

		[Field ("PSPDFMetadataKeyTrapped", "__Internal")]
		NSString MetadataKeyTrapped { get; }

		[Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; set; }

		// PSPDFDocument (SubclassingHooks) Category

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, Class subclass);

		[Export ("didCreateDocumentProvider:")]
		PSPDFDocumentProvider DidCreateDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("setDidCreateDocumentProviderBlock:")]
		void SetDidCreateDocumentProviderBlock (DidCreateDocumentProviderHandler handler);

		[Export ("pageContentForPage:")]
		string PageContentForPage (uint page);

		[Export ("backgroundColorForPage:")]
		UIColor BackgroundColorForPage (uint page);

		[Export ("backgroundColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor BackgroundColor { get; set; }

		[Export ("pageInfoForPage:pageRef:")] [Internal]
		PSPDFPageInfo PageInfoForPage_ (uint page, IntPtr /* CGPDFPage */ pageRef);

		[Export ("fileNameForIndex:")]
		string FileNameForIndex (uint fileIndex);

		// PSPDFDocument (Advanced) Category

		[Export ("PDFBox", ArgumentSemantic.Assign)]
		CGPDFBox PdfBox { get; set; }

		[Export ("boxRect:forPage:error:")] [Internal]
		RectangleF _BoxRectForPage (CGPDFBox boxType, uint page, IntPtr error);

		[Export ("aspectRatioVariance")]
		float AspectRatioVariance ();

		[Export ("aspectRatioEqual", ArgumentSemantic.Assign)]
		bool AspectRatioEqual { [Bind ("isAspectRatioEqual")] get; set; }

		[Export ("undoEnabled", ArgumentSemantic.Assign)]
		bool UndoEnabled { [Bind ("isUndoEnabled")] get; set; }

		[Export ("undoController", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFUndoController UndoController { get; set; }

		[Export ("documentProviderRelativePageForPage:")]
		uint DocumentProviderRelativePageForPage (uint page);

		[Export ("documentProviderRelativePageWithPageRangeCompensated:")]
		uint DocumentProviderRelativePageWithPageRangeCompensated (uint page);
	}

	interface IPSPDFDocumentDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFDocumentDelegate
	{
		[Export ("pdfDocument:didRenderPage:inContext:withSize:clippedToRect:annotations:options:"), EventArgs ("PSPDFDocumentDelegateDidRenderPage")]
		void DidRenderPage (PSPDFDocument document, uint page, CGContext context, SizeF fullSize, RectangleF clipRect, PSPDFAnnotation [] annotations, NSDictionary options);

		[Export ("pdfDocument:resolveCustomAnnotationPathToken:"), DelegateName ("PSPDFDocumentDelegateResolveCustomAnnotationPathToken"), NoDefaultValue]
		string ResolveCustomAnnotationPathToken (PSPDFDocument document, string pathToken);

		[Export ("pdfDocument:didSaveAnnotations:"), EventArgs ("PSPDFDocumentDelegateDidSaveAnnotations")]
		void DidSaveAnnotations (PSPDFDocument document, PSPDFAnnotation [] annotations);

		[Export ("pdfDocument:failedToSaveAnnotations:error:"), EventArgs ("PSPDFDocumentDelegateFailedToSaveAnnotations")]
		void FailedToSaveAnnotations (PSPDFDocument document, PSPDFAnnotation [] annotations, NSError error);
	}

	interface IPSPDFOverridable { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFOverridable
	{
		[Export ("classForClass:")]
		Class ClassForClass (Class originalClass);
	}

	interface IPSPDFPageViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFPageViewDelegate : PSPDFOverridable
	{

	}

	[BaseType (typeof (PSPDFHUDView))]
	interface PSPDFAnnotationContainerView
	{

	}

	delegate void PSPDFPageViewUpdateShadowHandler (PSPDFPageView pageView);

	[BaseType (typeof (UIView))]
	interface PSPDFPageView
	{
		[Notification]
		[Field ("PSPDFHidePageHUDElementsNotification", "__Internal")]
		NSString HidePageHudElementsNotification { get; }

		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("initWithFrame:delegate:")]
		IntPtr Constructor (RectangleF frame, IPSPDFPageViewDelegate pageViewDelegate);

		[Export ("displayDocument:page:pageRect:scale:delayPageAnnotations:pdfController:")]
		void DisplayDocument (PSPDFDocument document, uint page, RectangleF pageRect, float scale, bool delayPageAnnotations, PSPDFViewController pdfController);

		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Wrap ("WeakDelegate")]
		PSPDFPageViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("updateRenderView")]
		void UpdateRenderView ();

		[Export ("updateView")]
		void UpdateView ();

		[Export ("annotationViewForAnnotation:")]
		IPSPDFAnnotationViewProtocol AnnotationViewForAnnotation (PSPDFAnnotation annotation);

		[Export ("contentView", ArgumentSemantic.Retain)]
		UIImageView ContentView { get; }

		[Export ("renderView", ArgumentSemantic.Retain)]
		UIImageView RenderView { get; }

		[Export ("annotationContainerView", ArgumentSemantic.Retain)]
		PSPDFAnnotationContainerView AnnotationContainerView { get; }

		[Export ("renderSize", ArgumentSemantic.Assign)]
		SizeF RenderSize { get; set; }

		[Export ("PDFScale", ArgumentSemantic.Assign)]
		float PdfScale { get; }

		[Export ("rendering", ArgumentSemantic.Assign)]
		bool Rendering { [Bind ("isRendering")] get; }

		[Export ("visibleRect", ArgumentSemantic.Assign)]
		RectangleF VisibleRect { get; }

		[Export ("selectionView", ArgumentSemantic.Retain)]
		PSPDFTextSelectionView SelectionView { get; }

		[Export ("renderStatusView", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFRenderStatusView RenderStatusView { get; set; }

		[Export ("renderStatusViewOffset", ArgumentSemantic.Assign)]
		float RenderStatusViewOffset { get; set; }

		[Export ("centerRenderStatusView", ArgumentSemantic.Assign)]
		bool CenterRenderStatusView { get; set; }

		[Export ("textParser", ArgumentSemantic.Retain)]
		PSPDFTextParser TextParser { get; }

		[Export ("convertViewPointToPDFPoint:")]
		PointF ConvertViewPointToPdfPoint (PointF viewPoint);

		[Export ("convertPDFPointToViewPoint:")]
		PointF ConvertPdfPointToViewPoint (PointF pdfPoint);

		[Export ("convertViewRectToPDFRect:")]
		RectangleF ConvertViewRectToPdfRect (RectangleF viewRect);

		[Export ("convertPDFRectToViewRect:")]
		RectangleF ConvertPdfRectToViewRect (RectangleF pdfRect);

		[Export ("convertGlyphRectToViewRect:")]
		RectangleF ConvertGlyphRectToViewRect (RectangleF glyphRect);

		[Export ("convertViewRectToGlyphRect:")]
		RectangleF ConvertViewRectToGlyphRect (RectangleF viewRect);

		[Export ("objectsAtPoint:options:")]
		NSDictionary ObjectsAtPoint (PointF viewPoint, NSDictionary options);

		[Export ("objectsAtRect:options:")]
		NSDictionary ObjectsAtRect (RectangleF viewRect, NSDictionary options);

		[Export ("scrollView")]
		PSPDFScrollView ScrollView { get; }

		[Export ("visibleAnnotationViews")]
		IPSPDFAnnotationViewProtocol [] VisibleAnnotationViews { get; }

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; }

		[Export ("document", ArgumentSemantic.Retain)]
		PSPDFDocument Document { get; }

		[Export ("pageInfo", ArgumentSemantic.Retain)]
		PSPDFPageInfo PageInfo { get; }

		[Export ("rightPage", ArgumentSemantic.Assign)]
		bool RightPage { [Bind ("isRightPage")] get; }

		[Export ("shadowEnabled", ArgumentSemantic.Assign)]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("shadowOpacity", ArgumentSemantic.Assign)]
		float ShadowOpacity { get; set; }

		[Export ("updateShadowAnimated:")]
		void UpdateShadowAnimated (bool animated);

		[Export ("setUpdateShadowBlock:")]
		void SetUpdateShadowHandler (PSPDFPageViewUpdateShadowHandler handler);

		// PSPDFPageView (AnnotationViews) Category

		[Export ("setAnnotation:forAnnotationView:")]
		void SetAnnotation (PSPDFAnnotation annotation, IPSPDFAnnotationViewProtocol annotationView);

		[Export ("annotationForAnnotationView:")]
		PSPDFAnnotation AnnotationForAnnotationView (IPSPDFAnnotationViewProtocol annotationView);

		[Export ("selectedAnnotations", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAnnotation [] SelectedAnnotations { get; set; }

		[Export ("singleTapped:")]
		bool SingleTapped (UITapGestureRecognizer recognizer);

		[Export ("longPress:")]
		bool LongPress (UILongPressGestureRecognizer recognizer);

		[Export ("addAnnotation:animated:")]
		void AddAnnotation (PSPDFAnnotation annotation, bool animated);

		[Export ("removeAnnotation:animated:")]
		bool RemoveAnnotation (PSPDFAnnotation annotation, bool animated);

		// PSPDFPageView (AnnotationMenu) Category

		[Export ("menuItemsForAnnotations:")]
		PSPDFMenuItem [] MenuItemsForAnnotations (PSPDFAnnotation [] annotations);

		[Export ("menuItemsForNewAnnotationAtPoint:")]
		PSPDFMenuItem [] MenuItemsForNewAnnotationAtPoint (PointF point);

		[Export ("colorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] ColorMenuItemsForAnnotation (PSPDFAnnotation annotation);

		[Export ("fillColorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] FillColorMenuItemsForAnnotation (PSPDFAnnotation annotation);

		[Export ("opacityMenuItemForAnnotation:withColor:")]
		PSPDFMenuItem OpacityMenuItemForAnnotation (PSPDFAnnotation annotation, UIColor color);

		[Export ("showMenuForAnnotations:edgeInsets:animated:")]
		void ShowMenuForAnnotations (PSPDFAnnotation [] annotations, UIEdgeInsets edgeInsets, bool animated);

		[Export ("showNoteControllerForAnnotation:showKeyboard:animated:")]
		PSPDFNoteAnnotationViewController ShowNoteControllerForAnnotation (PSPDFAnnotation annotation, bool showKeyboard, bool animated);

		[Export ("showFontPickerForAnnotation:animated:")]
		void ShowFontPickerForAnnotation (PSPDFFreeTextAnnotation annotation, bool animated);

		[Export ("showColorPickerForAnnotation:animated:")]
		void ShowColorPickerForAnnotation (PSPDFAnnotation annotation, bool animated);

		[Export ("showSignatureControllerAtPoint:withTitle:shouldSaveSignature:animated:")]
		void ShowSignatureControllerAtPoint (PointF point, string title, bool shouldSaveSignature, bool animated);

		[Export ("availableFontSizes")]
		NSNumber [] AvailableFontSizes ();

		[Export ("availableLineWidths")]
		NSNumber [] AvailableLineWidths ();

		[Export ("passthroughViewsForPopoverController")]
		NSObject [] PassthroughViewsForPopoverController ();

		// PSPDFPageView (ScrollViewDelegateExtensions) Category

		[Export ("pspdf_scrollView:willZoomToScale:animated:")]
		void PSPdf_ScrollView (UIScrollView scrollView, float scale, bool animated);

		// PSPDFPageView (SubclassingHooks) Category

		[Export ("insertAnnotations:forPage:inDocument:")]
		void InsertAnnotations (PSPDFAnnotation [] annotations, uint page, PSPDFDocument document);

		[Export ("tappableAnnotationsAtPoint:")]
		PSPDFAnnotation [] TappableAnnotationsAtPoint (PointF viewPoint);

		[Export ("singleTappedAtViewPoint:")]
		bool SingleTappedAtViewPoint (PointF viewPoint);

		[Export ("showMenuIfSelectedAnimated:")]
		void ShowMenuIfSelectedAnimated (bool animated);

		[Export ("showNewSignatureMenuAtPoint:animated:")]
		void ShowNewSignatureMenuAtPoint (PointF point, bool animated);

		[Export ("showNewImageMenuAtPoint:animated:")]
		void ShowNewImageMenuAtPoint (PointF point, bool animated);

		[Export ("addNewSoundAnnotationAtPoint:animated:")]
		void AddNewSoundAnnotationAtPoint (PointF point, bool animated);

		[Export ("rectForAnnotations:")]
		RectangleF RectForAnnotations (PSPDFAnnotation [] annotations);

		[Export ("defaultColorOptionsForAnnotationType:")]
		NSMutableDictionary DefaultColorOptionsForAnnotationType (PSPDFAnnotationType annotationType);

		[Export ("useAnnotationInspectorForAnnotations:")]
		bool UseAnnotationInspectorForAnnotations ([NullAllowed] PSPDFAnnotation [] annotations);

		[Export ("selectColorForAnnotation:isFillColor:")]
		void SelectColorForAnnotation (PSPDFAnnotation annotation, bool isFillColor);

		[Export ("renderOptionsDictWithZoomScale:")]
		NSDictionary RenderOptionsDictWithZoomScale (float zoomScale);

		[Export ("annotationSelectionView", ArgumentSemantic.Retain)]
		PSPDFResizableView AnnotationSelectionView { get; }

		[Export ("showLinkPreviewActionSheetForAnnotation:fromRect:animated:")]
		NSObject ShowLinkPreviewActionSheetForAnnotation (PSPDFLinkAnnotation annotation, RectangleF viewRect, bool animated);

		[Export ("centerAnnotation:aroundViewPoint:")]
		void CenterAnnotation (PSPDFAnnotation annotation, PointF viewPoint);

		[Export ("loadPageAnnotationsAnimated:blockWhileParsing:")]
		void LoadPageAnnotationsAnimated (bool animated, bool blockWhileParsing);

		[Export ("scaleForPageView")]
		float ScaleForPageView ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFTextSelectionView
	{
		[Export ("selectedGlyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] SelectedGlyphs { get; set; }

		[Export ("selectedText", ArgumentSemantic.Copy)]
		string SelectedText { get; set; }

		[Export ("selectedImage", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFImageInfo SelectedImage { get; set; }

		[Export ("selectionColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("selectionAlpha", ArgumentSemantic.Assign)]
		float SelectionAlpha { get; set; }

		[Export ("simpleSelectionModeEnabled", ArgumentSemantic.Assign)]
		bool SimpleSelectionModeEnabled { get; set; }

		[Export ("selectionHitTestExtension", ArgumentSemantic.Assign)]
		float SelectionHitTestExtension { get; set; }

		[Export ("trimmedSelectedText", ArgumentSemantic.Copy)]
		string TrimmedSelectedText { get; }

		[Export ("pageView", ArgumentSemantic.Assign)]
		PSPDFPageView PageView { get; set; }

		[Export ("firstLineRect", ArgumentSemantic.Assign)]
		RectangleF FirstLineRect { get; }

		[Export ("lastLineRect", ArgumentSemantic.Assign)]
		RectangleF LastLineRect { get; }

		[Export ("selectionRect", ArgumentSemantic.Assign)]
		RectangleF SelectionRect { get; }

		[Export ("updateMenuAnimated:")]
		bool UpdateMenuAnimated (bool animated);

		[Export ("updateSelection")]
		void UpdateSelection ();

		[Export ("discardSelection")]
		void DiscardSelection ();

		[Export ("hasSelection")]
		bool HasSelection { get; }

		[Static]
		[Export ("isTextSelectionFeatureAvailable")]
		bool IsTextSelectionFeatureAvailable { get; }

		// PSPDFTextSelectionView (Advanced) Category

		[Export ("sortedGlyphs:")]
		PSPDFGlyph [] SortedGlyphs (PSPDFGlyph [] glyphs);

		[Export ("presentWikipediaBrowserForSelectedText")]
		UIViewController PresentWikipediaBrowserForSelectedText ();

		[Export ("dictionaryHasDefinitionForTerm:")]
		bool DictionaryHasDefinitionForTerm (string term);

		// PSPDFTextSelectionView (SubclassingHooks) Category

		[Export ("menuItemsForTextSelection:")]
		PSPDFMenuItem [] MenuItemsForTextSelection (string selectedText);

		[Export ("menuItemsForImageSelection:")]
		PSPDFMenuItem [] MenuItemsForImageSelection (PSPDFImageInfo imageSelection);

		[Export ("addHighlightAnnotationWithType:")]
		void AddHighlightAnnotationWithType (PSPDFAnnotationType highlightType);

		[Export ("showTextFlowData:animated:")]
		void ShowTextFlowData (bool show, bool animated);

		[Export ("longPress:")]
		bool LongPress (UILongPressGestureRecognizer recognizer);

		[Export ("pressRecognizerShouldHandlePressImmediately:")]
		bool PressRecognizerShouldHandlePressImmediately (PSPDFLongPressGestureRecognizer recognizer);

		[Export ("isDragHandleSelected")]
		bool IsDragHandleSelected ();
	}

	delegate void PSPDFKeyboardAvoidingScrollViewKeyboardWillShowHandler (PSPDFKeyboardAvoidingScrollView scrollView, NSNotification notification);
	delegate void PSPDFKeyboardAvoidingScrollViewKeyboardWillHideHandler (PSPDFKeyboardAvoidingScrollView scrollView, NSNotification notification);

	[BaseType (typeof (UIScrollView))]
	interface PSPDFKeyboardAvoidingScrollView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("keyboardVisible", ArgumentSemantic.Assign)]
		bool KeyboardVisible { [Bind ("isKeyboardVisible")] get; }

		[Export ("firstResponderIsTextInput", ArgumentSemantic.Assign)]
		bool FirstResponderIsTextInput { get; }

		[Export ("enableKeyboardAvoidance", ArgumentSemantic.Assign)]
		bool EnableKeyboardAvoidance { get; set; }

		[Export ("resignFirstResponderIfInsideView")]
		bool ResignFirstResponderIfInsideView ();

		[Export ("setKeyboardWillShowBlock:")]
		void SetKeyboardWillShowHandler (PSPDFKeyboardAvoidingScrollViewKeyboardWillShowHandler handler);

		[Export ("setKeyboardWillHideBlock:")]
		void SetKeyboardWillHideHandler (PSPDFKeyboardAvoidingScrollViewKeyboardWillHideHandler handler);

		[Export ("moveScrollViewUpForRect:focusRect:animated:")]
		void MoveScrollViewUpForRect (RectangleF rect, RectangleF focusRect, bool animated);

		[Export ("moveScrollViewDownAnimated:")]
		void MoveScrollViewDownAnimated (bool animated);
	}

	[BaseType (typeof (PSPDFKeyboardAvoidingScrollView))]
	interface PSPDFScrollView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("displayDocument:withPage:")]
		void DisplayDocument (PSPDFDocument document, uint page);

		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("document", ArgumentSemantic.Retain)]
		PSPDFDocument Document { get; }

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; }

		[Export ("leftPage", ArgumentSemantic.Retain)]
		PSPDFPageView LeftPage { get; }

		[Export ("rightPage", ArgumentSemantic.Retain)]
		PSPDFPageView RightPage { get; }

		[Export ("shadowStyle", ArgumentSemantic.Assign)]
		PSPDFShadowStyle ShadowStyle { get; set; }

		[Export ("zoomingEnabled", ArgumentSemantic.Assign)]
		bool ZoomingEnabled { [Bind ("isZoomingEnabled")] get; set; }

		[Export ("doublePageMode", ArgumentSemantic.Assign)]
		bool DoublePageMode { [Bind ("isDoublePageMode")] get; set; }

		[Export ("doublePageModeOnFirstPage", ArgumentSemantic.Assign)]
		bool DoublePageModeOnFirstPage { [Bind ("isDoublePageModeOnFirstPage")] get; set; }

		[Export ("zoomingSmallDocumentsEnabled", ArgumentSemantic.Assign)]
		bool ZoomingSmallDocumentsEnabled { [Bind ("isZoomingSmallDocumentsEnabled")] get; set; }

		[Export ("fitToWidthEnabled", ArgumentSemantic.Assign)]
		bool FitToWidthEnabled { [Bind ("isFitToWidthEnabled")] get; set; }

		[Export ("shadowEnabled", ArgumentSemantic.Assign)]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("scrollOnTapPageEndEnabled", ArgumentSemantic.Assign)]
		bool ScrollOnTapPageEndEnabled { [Bind ("isScrollOnTapPageEndEnabled")] get; set; }

		// PSPDFScrollView (PSPDFSubclassing) Category

		[Export ("singleTapGesture", ArgumentSemantic.Retain)]
		UITapGestureRecognizer SingleTapGesture { get; }

		[Export ("doubleTapGesture", ArgumentSemantic.Retain)]
		UITapGestureRecognizer DoubleTapGesture { get; }

		[Export ("longPressGesture", ArgumentSemantic.Retain)]
		UILongPressGestureRecognizer LongPressGesture { get; }

		[Export ("singleTapped:")]
		void SingleTapped (UITapGestureRecognizer recognizer);

		[Export ("doubleTapped:")]
		void DoubleTapped (UITapGestureRecognizer recognizer);

		[Export ("longPress:")]
		void LongPress (UILongPressGestureRecognizer recognizer);

		[Export ("pathShadowForView:")]
		NSObject PathShadowForView (UIView view);

		[Export ("ensureContentIsCentered")]
		void EnsureContentIsCentered ();

		[Export ("createDoubleTapGesture")]
		UITapGestureRecognizer CreateDoubleTapGesture ();

		[Export ("compoundView", ArgumentSemantic.Retain)]
		UIView CompoundView { get; }

		// PSPDFScrollView (PSPDFAdvanced) Category

		[Export ("selectedAnnotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] SelectedAnnotations { get; }
	}

	delegate void PSPDFDocumentProviderPerformHandler (PSPDFDocumentProvider docProvider, IntPtr documentRef);
	delegate void PSPDFDocumentProviderIterateOverPageRef (PSPDFDocumentProvider provider, IntPtr documentRef, IntPtr pageRef, uint page);

	[BaseType (typeof (NSObject),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFDocumentProviderDelegate) })]
	interface PSPDFDocumentProvider
	{
		[Export ("initWithFileURL:document:")]
		IntPtr Constructor (NSUrl fileURL, PSPDFDocument document);

		[Export ("initWithData:document:")]
		IntPtr Constructor (NSData data, PSPDFDocument document);

		[Export ("initWithDataProvider:document:")] [Internal]
		IntPtr Constructor (IntPtr /* CGDataProvider */ dataProvider, PSPDFDocument document);

		[Export ("fileURL")]
		NSUrl FileURL { get; }

		[Export ("data", ArgumentSemantic.Retain)] [NullAllowed]
		NSData Data { get; }

		[Export ("dataProvider")] [Internal]
		IntPtr /* CGDataProvider */ DataProvider_ { get; }

		[Export ("dataRepresentationWithError:")] [Internal]
		NSData _DataRepresentationWithError (IntPtr error);

		[Export ("fileSize")]
		ulong FileSize { get; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; }

		[Wrap ("WeakDelegate")]
		PSPDFDocumentProviderDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("pageInfoForPage:")]
		PSPDFPageInfo PageInfoForPage (uint page);

		[Export ("pageCount", ArgumentSemantic.Assign)]
		uint PageCount { get; set; }

		[Export ("unlockWithPassword:")]
		bool UnlockWithPassword (string password);

		[Export ("password", ArgumentSemantic.Copy)]
		string Password { get; set; }

		[Export ("allowsPrinting", ArgumentSemantic.Assign)]
		bool AllowsPrinting { get; }

		[Export ("allowsCopying", ArgumentSemantic.Assign)]
		bool AllowsCopying { get; set; }

		[Export ("isEncrypted", ArgumentSemantic.Assign)]
		bool IsEncrypted { get; }

		[Export ("isLocked", ArgumentSemantic.Assign)]
		bool IsLocked { get; }

		[Export ("canEmbedAnnotations", ArgumentSemantic.Assign)]
		bool CanEmbedAnnotations { get; set; }

		[Export ("title", ArgumentSemantic.Copy)]
		string Title { get; }

		[Export ("textParserForPage:")]
		PSPDFTextParser TextParserForPage (uint page);

		[Export ("outlineParser", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFOutlineParser OutlineParser { get; set; }

		[Export ("formParser", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFFormParser FormParser { get; set; }

		[Export ("annotationManager", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAnnotationManager AnnotationManager { get; set; }

		[Export ("labelParser", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFLabelParser LabelParser { get; set; }

		// PSPDFDocumentProvider (PageRange) Category

		[Export ("pageCountUnfiltered", ArgumentSemantic.Assign)]
		uint PageCountUnfiltered { get; }

		[Export ("firstPageIndex", ArgumentSemantic.Assign)]
		uint FirstPageIndex { get; }

		[Export ("pageRange", ArgumentSemantic.Copy)]
		NSIndexSet PageRange { get; }

		[Export ("translateCappedPageToRealPage:")]
		uint TranslateCappedPageToRealPage (uint page);

		[Export ("translateRealPageToCappedPage:")]
		uint TranslateRealPageToCappedPage (uint page);

		// PSPDFDocumentProvider (Advanced) Category

		[Export ("ignoreLocking", ArgumentSemantic.Assign)]
		bool IgnoreLocking { get; set; }

		[Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		[Export ("metadataLoaded", ArgumentSemantic.Assign)]
		bool MetadataLoaded { [Bind ("isMetadataLoaded")] get; }

		// PSPDFDocumentProvider (SubclassingHooks) Category

		[Export ("pageInfoForPage:pageRef:")] [Internal]
		PSPDFPageInfo PageInfoForPage_ (uint page, IntPtr /* CGPDFPage */ pageRef);

		[Export ("setPageInfo:forPage:")]
		void SetPageInfo (PSPDFPageInfo pageInfo, uint page);

		[Export ("saveAnnotationsWithOptions:error:")] [Internal]
		bool _SaveAnnotationsWithOptions (NSDictionary options, IntPtr error);

		[Export ("resolveTokenizedPath:alwaysLocal:")]
		string ResolveTokenizedPath (string path, bool alwaysLocal);
	}

	interface IPSPDFDocumentProviderDelegate { };

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFDocumentProviderDelegate
	{
		[Export ("documentProvider:shouldAppendData:"), DelegateName ("PSPDFDocumentProviderDelegateShouldAppendData"), NoDefaultValue]
		bool ShouldAppendData (PSPDFDocumentProvider documentProvider, NSData data);

		[Export ("documentProvider:didAppendData:"), EventArgs ("PSPDFDocumentProviderDelegateDidAppendData")]
		void DidAppendData (PSPDFDocumentProvider documentProvider, NSData data);
	}

	interface IPSPDFCacheDelegate { };

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFCacheDelegate
	{
		[Export ("didCacheImage:document:page:size:")]
		void DidCacheImage (UIImage image, PSPDFDocument document, uint page, SizeF size);
	}

	delegate IntPtr PSPDFCacheDecryptFromPathHandler (PSPDFDocument document, string path);
	delegate void PSPDFCacheEncryptDataHandler (PSPDFDocument document, NSMutableData data);

	[DisableDefaultCtor]
	[BaseType (typeof (UIScrollView))]
	interface PSPDFCache
	{
		[Static]
		[Export ("sharedCache")]
		PSPDFCache SharedCache { get; }

		[Export ("cacheStatusForImageFromDocument:page:size:options:")]
		PSPDFCacheStatus CacheStatusForImageFromDocument (PSPDFDocument document, uint page, SizeF size, PSPDFCacheOptions options);

		[Export ("imageFromDocument:page:size:options:")]
		UIImage ImageFromDocument (PSPDFDocument document, uint page, SizeF size, PSPDFCacheOptions options);

		[Export ("saveImage:document:page:receipt:")]
		void SaveImage (UIImage image, PSPDFDocument document, uint page, string renderReceipt);

		[Export ("cacheDocument:startAtPage:sizes:diskCacheStrategy:")]
		void CacheDocument (PSPDFDocument document, uint startAtPage, NSObject [] sizes, PSPDFDiskCacheStrategy diskCacheStrategy);

		[Export ("stopCachingDocument:")]
		void StopCachingDocument (PSPDFDocument document);

		[Export ("cancelRequestForImageFromDocument:page:size:")]
		void CancelRequestForImageFromDocument (PSPDFDocument document, uint page, SizeF size);

		[Export ("invalidateImageFromDocument:page:")]
		void InvalidateImageFromDocument (PSPDFDocument document, uint page);

		[Export ("removeCacheForDocument:deleteDocument:error:")]
		bool RemoveCacheForDocument (PSPDFDocument document, bool deleteDocument, out NSError error);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("memoryCache", ArgumentSemantic.Retain)]
		PSPDFMemoryCache MemoryCache { get; }

		[Export ("diskCache", ArgumentSemantic.Retain)]
		PSPDFDiskCache DiskCache { get; }

		[Export ("cacheDirectory", ArgumentSemantic.Copy)]
		string CacheDirectory { get; set; }

		[Export ("diskCacheStrategy", ArgumentSemantic.Assign)]
		PSPDFDiskCacheStrategy DiskCacheStrategy { get; set; }

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		SizeF ThumbnailSize { get; set; }

		[Export ("tinySize", ArgumentSemantic.Assign)]
		SizeF TinySize { get; set; }

		[Export ("pauseCachingForService:")]
		bool PauseCachingForService (NSObject service);

		[Export ("resumeCachingForService:")]
		bool ResumeCachingForService (NSObject service);

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFCacheDelegate aDelegate);

		[Export ("removeDelegate:")]
		bool RemoveDelegate (IPSPDFCacheDelegate aDelegate);

		[Export ("useJPGFormat", ArgumentSemantic.Assign)]
		bool UseJPGFormat { get; set; }

		[Export ("JPGFormatCompression", ArgumentSemantic.Assign)]
		float JPGFormatCompression { get; set; }

		[Export ("allowImageResize", ArgumentSemantic.Assign)]
		bool AllowImageResize { get; set; }

		[Export ("setDecryptFromPathBlock:")]
		void SetDecryptFromPathHandler (PSPDFCacheDecryptFromPathHandler handler);

		[Export ("setEncryptDataBlock:")]
		void SetEncryptDataHandler (PSPDFCacheEncryptDataHandler handler);
	}

	[Internal]
	[BaseType (typeof (UIToolbar))]
	interface PSPDFTransparentToolbar
	{

	}

	[BaseType (typeof (NSObject))]
	interface PSPDFPageRenderer
	{
		[Static]
		[Export ("sharedPageRenderer")]
		PSPDFPageRenderer SharedPageRenderer { get; }

		[Export ("setupGraphicsContext:inRectangle:pageInfo:")]
		void SetupGraphicsContext (CGContext context, RectangleF displayRectangle, PSPDFPageInfo pageInfo);

		[Export ("renderPageRef:inContext:inRectangle:pageInfo:withAnnotations:options:")] [Internal]
		RectangleF RenderPageRef_ (IntPtr /* CGPDFPage */ page, CGContext context, RectangleF rectangle, PSPDFPageInfo pageInfo, PSPDFAnnotation [] annotations, NSDictionary options);

		[Export ("renderPage:inContext:atPoint:withZoom:pageInfo:withAnnotations:options:")] [Internal]
		SizeF SetupGraphicsContext_ (IntPtr /* CGPDFPage */ page, IntPtr /* CGContext */ context, PointF point, double zoom, PSPDFPageInfo pageInfo, PSPDFAnnotation [] annotations, NSDictionary options);

		[Export ("renderAppearanceStream:inContext:error:")]
		bool RenderAppearanceStream (PSPDFAnnotation annotation, CGContext context, out NSError error);

		[Field ("PSPDFRenderPageColor", "__Internal")]
		NSString RenderPageColor { get; }

		[Field ("PSPDFRenderContentOpacity", "__Internal")]
		NSString RenderContentOpacity { get; }

		[Field ("PSPDFRenderInverted", "__Internal")]
		NSString RenderInverted { get; }

		[Field ("PSPDFRenderInterpolationQuality", "__Internal")]
		NSString RenderInterpolationQuality { get; }

		[Field ("PSPDFRenderSkipPageContent", "__Internal")]
		NSString RenderSkipPageContent { get; }

		[Field ("PSPDFRenderOverlayAnnotations", "__Internal")]
		NSString RenderOverlayAnnotations { get; }

		[Field ("PSPDFRenderSkipAnnotationArray", "__Internal")]
		NSString RenderSkipAnnotationArray { get; }

		[Field ("PSPDFRenderSkipAnnotationArray", "__Internal")]
		NSString RenderIgnorePageClip { get; }

		[Field ("PSPDFRenderAllowAntiAliasing", "__Internal")]
		NSString RenderAllowAntiAliasing { get; }

		[Field ("PSPDFRenderBackgroundFillColor", "__Internal")]
		NSString RenderBackgroundFillColor { get; }

		[Field ("PSPDFRenderPDFBox", "__Internal")]
		NSString RenderPDFBox { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFPageInfo
	{
		[Export ("initWithPage:rect:rotation:documentProvider:")]
		IntPtr Constructor (uint page, RectangleF pageRect, int rotation, PSPDFDocumentProvider documentProvider);

		[Export ("pageRect", ArgumentSemantic.Assign)]
		RectangleF PageRect { get; set; }

		[Export ("rotatedPageRect", ArgumentSemantic.Assign)]
		RectangleF RotatedPageRect { get; }

		[Export ("pageRotation", ArgumentSemantic.Assign)]
		uint PageRotation { get; set; }

		[Export ("pageRotationTransform", ArgumentSemantic.Assign)]
		CGAffineTransform PageRotationTransform { get; }

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("isEqualToPageInfo:")]
		bool IsEqualToPageInfo (PSPDFPageInfo otherPageInfo);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFHUDView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);
	}

	interface IPSPDFTransitionProtocol { };

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFTransitionProtocol
	{
		[Abstract]
		[Export ("setPage:animated:")]
		void SetPage (uint page, bool animated);

		[Abstract]
		[Export ("visiblePageNumbers")]
		NSOrderedSet VisiblePageNumbers ();

		[Abstract]
		[Export ("pageViewForPage:")]
		PSPDFPageView PageViewForPage (uint page);

		[Abstract]
		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; }

		[Abstract]
		[Export ("scrollView", ArgumentSemantic.Assign)]
		PSPDFContentScrollView ScrollView { get; set; }

		[Export ("VisiblePageViews")]
		PSPDFPageView [] VisiblePageViews ();

		[Export ("compensatedContentOffset")]
		PointF CompensatedContentOffset ();
	}

	[BaseType (typeof (PSPDFScrollView))]
	interface PSPDFContentScrollView
	{
		[Export ("initWithTransitionViewController:")]
		IntPtr Constructor (IPSPDFTransitionProtocol viewController);

		[Export ("contentController", ArgumentSemantic.Assign)]
		IPSPDFTransitionProtocol ContentController { get; }
	}

	[BaseType (typeof (UIPageViewController))]
	interface PSPDFPageViewController : IPSPDFTransitionProtocol
	{
		[Export ("initWithPDFController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("useSolidBackground", ArgumentSemantic.Assign)]
		bool UseSolidBackground { get; set; }

		[Export ("clipToPageBoundaries", ArgumentSemantic.Assign)]
		bool ClipToPageBoundaries { get; set; }
	}

	interface IPSPDFSinglePageViewControllerDelegate { };

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFSinglePageViewControllerDelegate
	{
		[Export ("singlePageViewControllerReadyForReuse:")]
		void SinglePageViewControllerReadyForReuse (PSPDFSinglePageViewController singlePageViewController);

		[Export ("singlePageViewControllerWillDealloc:")]
		void SinglePageViewControllerWillDealloc (PSPDFSinglePageViewController singlePageViewController);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFSinglePageViewController
	{
		[Export ("initWithPDFController:page:")]
		IntPtr Constructor (PSPDFViewController pdfController, uint page);

		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; }

		[Export ("pageView", ArgumentSemantic.Retain)]
		PSPDFPageView PageView { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("useSolidBackground", ArgumentSemantic.Assign)]
		bool UseSolidBackground { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFSinglePageViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		// PSPDFSinglePageViewController (SubclassingHooks) Category

		[Export ("layoutPage")]
		void LayoutPage ();
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFPagingScrollView
	{

	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFPageScrollViewController
	{
		[Export ("initWithPDFController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; }

		[Export ("pagingScrollView", ArgumentSemantic.Retain)]
		UIScrollView PagingScrollView { get; }

		[Export ("pagePadding", ArgumentSemantic.Assign)]
		float PagePadding { get; set; }

		[Export ("visiblePageNumbers")]
		NSOrderedSet VisiblePageNumbers { get; }

		[Export ("pageViewForPage:")]
		PSPDFPageView PageViewForPage (uint page);

		[Export ("setPage:animated:")]
		void SetPage (uint page, bool animated);

		[Export ("reloadData")]
		void ReloadData ();
	}

	interface IPSPDFMultiDocumentViewControllerDelegate { };

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFMultiDocumentViewControllerDelegate
	{
		[Export ("multiPDFController:shouldChangeDocuments:"), DelegateName ("PSPDFMultiDocumentViewControllerDelegateShouldChangeDocuments"), NoDefaultValue]
		bool ShouldChangeDocuments (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument [] newDocuments);

		[Export ("multiPDFController:didChangeDocuments:"), EventArgs ("PSPDFMultiDocumentViewControllerDelegateDidChangeDocuments")]
		void DidChangeDocuments (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument [] oldDocuments);

		[Export ("multiPDFController:shouldChangeVisibleDocument:"), DelegateName ("PSPDFMultiDocumentViewControllerDelegateShouldChangeVisibleDocument"), NoDefaultValue]
		bool ShouldChangeVisibleDocument (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument newDocument);

		[Export ("multiPDFController:didChangeVisibleDocument:"), EventArgs ("PSPDFMultiDocumentViewControllerDelegateDidChangeVisibleDocuments")]
		void DidChangeVisibleDocument (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument oldDocument);
	}

	[BaseType (typeof (PSPDFBaseViewController),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFMultiDocumentViewControllerDelegate) })]
	interface PSPDFMultiDocumentViewController
	{
		[Export ("initWithPDFViewController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("visibleDocument", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFDocument VisibleDocument { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; set; }

		[Export ("addDocuments:atIndex:")]
		void AddDocuments (PSPDFDocument [] documents, uint index);

		[Export ("removeDocuments:")]
		void RemoveDocuments (PSPDFDocument [] documents);

		[Wrap ("WeakDelegate")]
		PSPDFMultiDocumentViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("enableAutomaticStatePersistence", ArgumentSemantic.Assign)]
		bool EnableAutomaticStatePersistence { get; set; }

		[Export ("persistState")]
		void PersistState ();

		[Export ("restoreState")]
		bool RestoreState ();

		[Export ("restoreStateAndMergeWithDocuments:")]
		bool RestoreStateAndMergeWithDocuments (PSPDFDocument [] documents);

		[Export ("statePersistenceKey", ArgumentSemantic.Copy)]
		string StatePersistenceKey { get; set; }

		[Export ("pdfController", ArgumentSemantic.Retain)]
		PSPDFViewController PdfController { get; }

		[Export ("changeDocumentOnTapPageEndMargin", ArgumentSemantic.Assign)]
		bool ChangeDocumentOnTapPageEndMargin { get; set; }

		[Export ("multiDocumentThumbnails", ArgumentSemantic.Assign)]
		bool MultiDocumentThumbnails { get; set; }

		[Export ("showTitle", ArgumentSemantic.Assign)]
		bool ShowTitle { get; set; }

		// PSPDFMultiDocumentViewController (SubclassingHooks) Category

		[Export ("commonInitWithPDFController:")]
		void CommonInitWithPDFController (PSPDFViewController pdfController);

		[Export ("swizzlePDFController:")]
		void SwizzlePDFController (PSPDFViewController pdfController);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFViewState
	{
		[Export ("initWithPage:")]
		IntPtr Constructor (uint page);

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		float ZoomScale { get; set; }

		[Export ("contentOffset", ArgumentSemantic.Assign)]
		PointF ContentOffset { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }
	}

	interface IPSPDFJSONSerializing { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFJSONSerializing
	{
		[Static]
		[Export ("JSONKeyPathsByPropertyKey")]
		NSDictionary JsonKeyPathsByPropertyKey ();

		[Static]
		[Export ("JSONTransformerForKey:")]
		NSObject JsonTransformerForKey (string key);

		[Static]
		[Export ("classForParsingJSONDictionary:")]
		Class ClassForParsingJsonDictionary (NSDictionary jsonDictionary);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFJSONAdapter
	{
		[Field ("PSPDFJSONAdapterErrorDomain", "__Internal")]
		NSString JsonAdapterErrorDomain { get; }

		[Field ("PSPDFJSONAdapterErrorNoClassFound", "__Internal")]
		NSString JsonAdapterErrorNoClassFound { get; }

		[Export ("model", ArgumentSemantic.Retain)]
		IPSPDFJSONSerializing Model { get; }

		[Static]
		[Export ("modelOfClass:fromJSONDictionary:error:")]
		NSObject ModelOfClass (Class modelClass, NSDictionary jsonDictionary, out NSError error);

		[Static]
		[Export ("JSONDictionaryFromModel:")]
		NSDictionary JsonDictionaryFromModel (IPSPDFJSONSerializing model);

		[Export ("initWithJSONDictionary:modelClass:error:")]
		IntPtr Constructor (NSDictionary jsonDictionary, Class modelClass, out NSError error);

		[Export ("initWithModel:")]
		IntPtr Constructor (PSPDFModel model);

		[Export ("JSONDictionary")]
		NSDictionary JsonDictionary { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAction : IPSPDFJSONSerializing
	{
		[Field ("PSPDFActionOptionModal", "__Internal")]
		NSString ActionOptionModal { get; }

		[Field ("PSPDFActionTypeTransformerName", "__Internal")]
		NSString ActionTypeTransformerName { get; }

		[Static]
		[Export ("actionClassForType:")]
		Class ActionClassForType (PSPDFActionType actionType);

		[Export ("initWithType:")]
		IntPtr Constructor (PSPDFActionType actionType);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFActionType Type { get; }

		[Export ("nextAction", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAction NextAction { get; set; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSDictionary Options { get; set; }

		[Export ("localizedDescription")]
		string LocalizedDescription { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFBookmark
	{
		[Export ("initWithPage:")]
		IntPtr Constructor (uint page);

		[Export ("initWithAction:")]
		IntPtr Constructor (PSPDFAction action);

		[Export ("action", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAction Action { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[Export ("pageOrNameString")]
		string PageOrNameString { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFBookmarkParser
	{
		[Notification]
		[Field ("PSPDFBookmarksChangedNotification", "__Internal")]
		NSString BookmarksChangedNotification { get; }

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("bookmarks", ArgumentSemantic.Copy)]
		PSPDFBookmark [] Bookmarks { get; set; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; set; }

		[Export ("addBookmarkForPage:")]
		bool AddBookmarkForPage (uint page);

		[Export ("removeBookmarkForPage:")]
		bool RemoveBookmarkForPage (uint page);

		[Export ("clearAllBookmarksWithError:")] [Internal]
		bool _ClearAllBookmarksWithError (IntPtr error);

		[Export ("bookmarkForPage:")]
		PSPDFBookmark BookmarkForPage (uint page);

		// PSPDFBookmarkParser (SubclassingHooks) Category

		[Export ("cachePath")]
		string CachePath ();

		[Export ("bookmarkPath")]
		string BookmarkPath ();

		[Export ("loadBookmarksWithError:")] [Internal]
		PSPDFBookmark [] _LoadBookmarksWithError (IntPtr error);

		[Export ("saveBookmarksWithError:")] [Internal]
		bool _SaveBookmarksWithError (IntPtr error);

	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFBrightnessViewController
	{
		[Export ("wantsSoftwareDimming", ArgumentSemantic.Assign)]
		bool WantsSoftwareDimming { get; set; }

		[Export ("wantsAdditionalSoftwareDimming", ArgumentSemantic.Assign)]
		bool WantsAdditionalSoftwareDimming { get; set; }

		[Export ("additionalBrightnessDimmingFactor", ArgumentSemantic.Assign)]
		float AdditionalBrightnessDimmingFactor { get; set; }

		[Export ("maximumAdditionalBrightnessDimmingFactor", ArgumentSemantic.Assign)]
		float MaximumAdditionalBrightnessDimmingFactor { get; set; }

		// PSPDFBrightnessViewController (SubclassingHooks) Category

		[Export ("slider", ArgumentSemantic.Retain)] [NullAllowed]
		UISlider Slider { get; set; }

		[Export ("gradient", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFGradientView Gradient { get; set; }

		[Export ("dimmingView")]
		PSPDFDimmingView DimmingView ();

		[Export ("addDimmingView")]
		PSPDFDimmingView AddDimmingView ();

		[Export ("removeDimmingView")]
		void RemoveDimmingView ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFDimmingView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);

		[Export ("additionalBrightnessDimmingFactor", ArgumentSemantic.Assign)]
		float AdditionalBrightnessDimmingFactor { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGradientView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);

		[Export ("direction", ArgumentSemantic.Assign)]
		PSPDFGradientViewDirection Direction { get; set; }

		[Export ("colors", ArgumentSemantic.Copy)]
		UIColor [] Colors { get; set; }

		[Export ("locations", ArgumentSemantic.Copy)]
		NSNumber [] Locations { get; set; }
	}

	interface IPSPDFRenderDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFRenderDelegate
	{
		[Export ("renderQueue:jobDidFinish:")]
		void RenderQueue (PSPDFRenderQueue renderQueue, PSPDFRenderJob job);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRenderQueue
	{
		[Static]
		[Export ("sharedRenderQueue")]
		PSPDFRenderQueue SharedRenderQueue { get; }

		[Export ("requestRenderedImageForDocument:page:size:clippedToRect:annotations:options:priority:queueAsNext:delegate:")]
		PSPDFRenderJob RequestRenderedImageForDocument (PSPDFDocument document, uint page, SizeF size, RectangleF clipRect, PSPDFAnnotation [] annotations, NSDictionary options, PSPDFRenderQueuePriority priority, bool queueAsNext, IPSPDFRenderDelegate renderDelegate);

		[Export ("renderJobsForDocument:page:delegate:")]
		PSPDFRenderJob [] RenderJobsForDocument (PSPDFDocument document, uint page, IPSPDFRenderDelegate renderDelegate);

		[Export ("hasRenderJobsForDelegate:")]
		bool HasRenderJobsForDelegate (IPSPDFRenderDelegate renderDelegate);

		[Export ("numberOfQueuedJobs")]
		uint NumberOfQueuedJobs { get; }

		[Export ("cancelJob:onlyIfQueued:")]
		bool CancelJob (PSPDFRenderJob job, bool onlyIfQueued);

		[Export ("cancelAllJobs")]
		void CancelAllJobs ();

		[Export ("cancelJobsForDocument:page:delegate:includeRunning:")]
		void CancelJobsForDocument (PSPDFDocument document, uint page, IPSPDFRenderDelegate renderDelegate, bool includeRunning);

		[Export ("cancelJobsForDelegate:")]
		void CancelJobsForDelegate (IPSPDFRenderDelegate renderDelegate);

		[Export ("minimumProcessPriority", ArgumentSemantic.Assign)]
		PSPDFRenderQueuePriority MinimumProcessPriority { get; set; }

		[Export ("concurrentRunningRenderRequests", ArgumentSemantic.Assign)]
		uint ConcurrentRunningRenderRequests { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRenderJob
	{
		[Export ("document", ArgumentSemantic.Retain)]
		PSPDFDocument Document { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint page { get; }

		[Export ("size", ArgumentSemantic.Assign)]
		SizeF Size { get; }

		[Export ("clipRect", ArgumentSemantic.Assign)]
		RectangleF ClipRect { get; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		float ZoomScale { get; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; set; }

		[Export ("priority", ArgumentSemantic.Assign)]
		PSPDFRenderQueuePriority Priority { get; set; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSDictionary Options { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFRenderDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("renderedImage", ArgumentSemantic.Retain)] [NullAllowed]
		UIImage RenderedImage { get; set; }

		[Export ("renderReceipt", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFRenderReceipt RenderReceipt { get; set; }

		[Export ("renderTime", ArgumentSemantic.Assign)]
		ulong RenderTime { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRenderReceipt
	{
		[Export ("initWithDocument:page:size:clipRect:annotations:options:")]
		IntPtr Constructor (PSPDFDocument document, uint page, SizeF size, RectangleF clipRect, PSPDFAnnotation [] annotations, NSDictionary options);

		[Export ("renderFingerprintString", ArgumentSemantic.Copy)]
		string RenderFingerprintString { get; set; }

		[Export ("timeInNanoseconds", ArgumentSemantic.Assign)]
		double TimeInNanoseconds { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStyleManager
	{
		[Field ("PSPDFStyleManagerLastUsedStylesKey", "__Internal")]
		NSString StyleManagerLastUsedStylesKey { get; }

		[Field ("PSPDFStyleManagerGenericStylesKey", "__Internal")]
		NSString StyleManagerGenericStylesKey { get; }

		[Static]
		[Export ("sharedStyleManager")]
		PSPDFRenderQueue SharedStyleManager { get; }

		[Export ("styleKeys", ArgumentSemantic.Copy)]
		NSSet StyleKeys { get; set; }

		[Export ("stylesForKey:")]
		PSPDFAnnotationStyle [] StylesForKey (string key);

		[Export ("addStyle:forKey:")]
		void AddStyle (PSPDFAnnotationStyle style, string key);

		[Export ("removeStyle:forKey:")]
		void RemoveStyle (PSPDFAnnotationStyle style, string key);

		[Export ("lastUsedStyleForKey:")]
		PSPDFAnnotationStyle LastUsedStyleForKey (string key);

		[Export ("lastUsedProperty:forKey:")]
		NSObject LastUsedProperty (string styleProperty, string key);

		[Export ("setLastUsedValue:forProperty:forKey:")]
		void SetLastUsedValue (NSObject value, string styleProperty, string key);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationStyle
	{
		[Export ("initWithName:")]
		IntPtr Constructor (string styleName);

		[Export ("styleName", ArgumentSemantic.Copy)]
		string StyleName { get; set; }

		[Export ("styleDictionary", ArgumentSemantic.Copy)]
		NSDictionary StyleDictionary { get; set; }

		[Export ("setStyle:forKey:")]
		void SetStyle (NSObject style, string key);

		[Export ("applyStyleToAnnotation:")]
		void ApplyStyleToAnnotation (PSPDFAnnotation annotation);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFGoToAction
	{
		[Export ("initWithPageIndex:")]
		IntPtr Constructor (uint pageIndex);

		[Export ("initWithNamedDestination:")]
		IntPtr Constructor (string namedDestination);

		[Export ("initWithPDFDictionary:documentRef:pageCache:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef, NSMutableDictionary /* CFMutableDictionary */ pageCache);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		uint PageIndex { get; set; }

		[Export ("namedDestination", ArgumentSemantic.Copy)]
		string NamedDestination { get; }

		[Export ("resolveNamedDestionationWithDocumentProvider:")]
		bool ResolveNamedDestionationWithDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Static]
		[Export ("resolveActionsWithNamedDestinations:documentRef:")] [Internal]
		uint ResolveActionsWithNamedDestinations_ (PSPDFGoToAction [] actions, IntPtr /* CGPDFDocument */ documentRef);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRemoteGoToAction
	{
		[Export ("initWithRemotePath:pageIndex:")]
		IntPtr Constructor (string remotePath, uint pageIndex);

		[Export ("initWithPDFDictionary:documentRef:isLaunch:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef, bool launch);

		[Export ("relativePath", ArgumentSemantic.Copy)]
		string RelativePath { get; set; }

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		uint PageIndex { get; set; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFURLAction
	{
		[Export ("initWithURLString:")]
		IntPtr Constructor (string urlString);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("URL", ArgumentSemantic.Retain)] [NullAllowed]
		NSUrl Url { get; set; }

		[Export ("updateURLWithAnnotationManager:")]
		bool UpdateURLWithAnnotationManager (PSPDFAnnotationManager annotationManager);

		[Export ("isPSPDFPrefixed", ArgumentSemantic.Assign)]
		bool IsPSPDFPrefixed { get; }

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		uint PageIndex { get; }

		[Export ("modal", ArgumentSemantic.Assign)]
		bool Modal { [Bind ("isModal")] get; set; }

		[Export ("popover", ArgumentSemantic.Assign)]
		bool Popover { [Bind ("isPopover")] get; set; }

		[Export ("size", ArgumentSemantic.Assign)]
		SizeF Size { get; set; }

		[Export ("offset", ArgumentSemantic.Assign)]
		float Offset { get; set; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFNamedAction
	{
		[Field ("PSPDFNamedActionTypeTransformerName", "__Internal")]
		NSString NamedActionTypeTransformerName { get; }

		[Export ("initWithActionNamedString:")]
		IntPtr Constructor (string actionNameString);

		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("namedActionType", ArgumentSemantic.Assign)]
		PSPDFNamedActionType NamedActionType { get; set; }

		[Export ("namedAction", ArgumentSemantic.Copy)]
		string NamedAction { get; set; }

		[Export ("pageIndexWithCurrentPage:fromDocument:")]
		uint PageIndexWithCurrentPage (uint currentPage, PSPDFDocument document);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFJavaScriptAction
	{
		[Export ("initWithScript:")]
		IntPtr Constructor (string script);

		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("script", ArgumentSemantic.Copy)]
		string Script { get; set; }

		[Export ("executeScript")]
		void ExecuteScript ();

		[Export ("pageIndexWithCurrentPage:fromDocument:")]
		uint PageIndexWithCurrentPage (uint currentPage, PSPDFDocument document);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRenditionAction
	{
		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("operation", ArgumentSemantic.Assign)]
		PSPDFRenditionActionType Operation { get; set; }

		[Export ("annotation", ArgumentSemantic.Assign)]
		PSPDFScreenAnnotation Annotation { get; set; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRichMediaExecuteAction
	{
		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("command", ArgumentSemantic.Copy)]
		string Command { get; set; }

		[Export ("argument", ArgumentSemantic.Copy)]
		string Argument { get; set; }

		[Export ("annotation", ArgumentSemantic.Assign)]
		PSPDFRichMediaAnnotation Annotation { get; set; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFAbstractFormAction
	{
		[Export ("fields", ArgumentSemantic.Copy)]
		NSObject [] Fields { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFSubmitFormAction
	{
		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("URL", ArgumentSemantic.Retain)] [NullAllowed]
		NSUrl Url { get; set; }

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFSubmitFormActionFlag Flags { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFResetFormAction
	{
		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFResetFormActionFlag Flags { get; set; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFHideAction
	{
		[Export ("initWithPDFDictionary:documentRef:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ actionDictionary, IntPtr /* CGPDFDocument */ documentRef);

		[Export ("shouldHide", ArgumentSemantic.Assign)]
		bool ShouldHide { get; set; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; set; }
	}

	[BaseType (typeof (PSPDFGradientView))]
	interface PSPDFLabelView
	{
		[Export ("label", ArgumentSemantic.Retain)]
		UILabel Label { get; }

		[Export ("labelMargin", ArgumentSemantic.Assign)]
		float LabelMargin { get; set; }

		[Export ("labelStyle", ArgumentSemantic.Assign)]
		PSPDFLabelStyle LabelStyle { get; set; }

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; }

		[Export ("updateAnimated:")]
		void UpdateAnimated (bool animated);

		// PSPDFLabelView (SubclassingHooks) Category

		[Export ("KVOValues")]
		NSObject [] KvoValues ();
	}

	[BaseType (typeof (PSPDFLabelView))]
	interface PSPDFPageLabelView
	{
		[Export ("showThumbnailGridButton", ArgumentSemantic.Assign)]
		bool ShowThumbnailGridButton { get; set; }

		// PSPDFPageLabelView (SubclassingHooks) Category

		[Export ("updateFrame")]
		void UpdateFrame ();
	}


	[BaseType (typeof (PSPDFLabelView))]
	interface PSPDFDocumentLabelView
	{

	}

	[BaseType (typeof (UIView))]
	interface PSPDFRenderStatusView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);

		[Export ("activityIndicator", ArgumentSemantic.Retain)] [NullAllowed]
		UIActivityIndicatorView ActivityIndicator { get; set; }

		// PSPDFRenderStatusView (SubclassingHooks) Category

		[Export ("loadActivityIndicator")]
		void LoadActivityIndicator ();
	}

	interface IPSPDFPasswordViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFPasswordViewDelegate
	{
		[Abstract]
		[Export ("passwordView:didUnlockWithPassword:"), EventArgs ("PSPDFPasswordViewDelegate")]
		void DidUnlock (PSPDFPasswordView passwordView, string password);

		[Export ("passwordView:didFailToUnlockWithPassword:"), EventArgs ("PSPDFPasswordViewDelegate")]
		void DidFailToUnlock (PSPDFPasswordView passwordView, string password);

		[Export ("passwordView:shouldUnlockWithPassword:"), DelegateName ("PSPDFPasswordViewDelegateShouldUnlock"), NoDefaultValue]
		bool ShouldUnlock (PSPDFPasswordView passwordView, string password);

		[Export ("passwordView:willUnlockWithPassword:"), EventArgs ("PSPDFPasswordViewDelegate")]
		void WillUnlock (PSPDFPasswordView passwordView, string password);
	}

	[BaseType (typeof (UIView),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFPasswordViewDelegate) })]
	interface PSPDFPasswordView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);

		[Export ("becomeFirstResponder")] [New]
		bool BecomeFirstResponder ();

		[Export ("document", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFDocument Document { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFPasswordViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("shakeOnError", ArgumentSemantic.Assign)]
		bool ShakeOnError { get; set; }

		// PSPDFPasswordView (SubclassingHooks) Category

		[Export ("passwordField", ArgumentSemantic.Retain)]
		UITextField PasswordField { get; }
	}

	[BaseType (typeof (UILabel))]
	interface PSPDFActivityLabel
	{
		[Static]
		[Export ("labelWithText:showActivity:")]
		PSPDFActivityLabel FromText (string text, bool showActivity);

		[Export ("showActivity", ArgumentSemantic.Assign)]
		bool ShowActivity { get; set; }
	}

	interface IPSPDFSearchOperationDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFSearchOperationDelegate
	{
		[Abstract]
		[Export ("didUpdateSearchOperation:forString:newSearchResults:forPage:"), EventArgs ("PSPDFSearchOperationDelegate")]
		void DidUpdateSearchOperation (PSPDFSearchOperation operation, string searchTerm, PSPDFSearchResult [] searchResults, uint page);

		[Export ("willStartSearchOperation:forString:isFullSearch:"), EventArgs ("PSPDFSearchOperationDelegateWillStartSearch")]
		void WillStartSearchOperation (PSPDFSearchOperation operation, string searchTerm, bool isFullSearch);
	}

	[BaseType (typeof (NSOperation),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFSearchOperationDelegate) })]
	interface PSPDFSearchOperation
	{
		[Export ("initWithDocument:searchTerm:")]
		IntPtr Constructor (PSPDFDocument document, string searchTerm);

		[Export ("pageRanges", ArgumentSemantic.Copy)] [NullAllowed]
		NSIndexSet PageRanges { get; set; }

		[Export ("shouldSearchAllPages", ArgumentSemantic.Assign)]
		bool ShouldSearchAllPages { get; set; }

		[Export ("maximumNumberOfSearchResults", ArgumentSemantic.Assign)]
		uint MaximumNumberOfSearchResults { get; set; }

		[Export ("searchMode", ArgumentSemantic.Assign)]
		PSPDFSearchMode SearchMode { get; set; }

		[Export ("compareOptions", ArgumentSemantic.Assign)]
		NSStringCompareOptions CompareOptions { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFSearchOperationDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("searchResults", ArgumentSemantic.Copy)]
		PSPDFSearchResult [] SearchResults { get; }

		[Export ("searchTerm", ArgumentSemantic.Copy)]
		string SearchTerm { get; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; }
	}

	interface IPSPDFTextSearchDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFTextSearchDelegate
	{
		[Export ("willStartSearch:term:isFullSearch:"), EventArgs ("PSPDFTextSearchDelegate")]
		void WillStartSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);

		[Export ("didUpdateSearch:term:newSearchResults:page:"), EventArgs ("PSPDFTextSearchDelegateDidUpdateSearch")]
		void DidUpdateSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, uint page);

		[Export ("didFinishSearch:term:newSearchResults:isFullSearch:"), EventArgs ("PSPDFTextSearchDelegateDidFinishSearch")]
		void DidFinishSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, bool isFullSearch);

		[Export ("didCancelSearch:term:isFullSearch:"), EventArgs ("PSPDFTextSearchDelegate")]
		void DidCancelSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);
	}

	[BaseType (typeof (NSObject),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFTextSearchDelegate) })]
	interface PSPDFTextSearch : IPSPDFSearchOperationDelegate
	{
		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("searchForString:")]
		void SearchForString (string searchTerm);

		[Export ("searchForString:inRanges:rangesOnly:")]
		void SearchForString (string searchTerm, NSIndexSet ranges, bool rangesOnly);

		[Export ("cancelAllOperationsAndWait")]
		void CancelAllOperationsAndWait ();

		[Export ("searchMode", ArgumentSemantic.Assign)]
		PSPDFSearchMode SearchMode { get; set; }

		[Export ("compareOptions", ArgumentSemantic.Assign)]
		NSStringCompareOptions CompareOptions { get; set; }

		[Export ("maximumNumberOfSearchResults", ArgumentSemantic.Assign)]
		uint MaximumNumberOfSearchResults { get; set; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; }

		[Wrap ("WeakDelegate")]
		PSPDFTextSearchDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		// PSPDFTextSearch (SubclassingHooks) Category

		[Export ("searchQueue", ArgumentSemantic.Retain)]
		NSOperationQueue SearchQueue { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFTextParser
	{
		[Export ("initWithPDFPage:page:document:fontCache:hideGlyphsOutsidePageRect:PDFBox:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFPage */ pageRef, uint page, PSPDFDocument document, NSMutableDictionary fontCache, bool hideGlyphsOutsidePageRect, CGPDFBox pdfBox);

		[Static]
		[Export ("initWithStream:")] [Internal]
		PSPDFTextParser FromStream_ (IntPtr /* CGPDFStream */ stream);

		[Export ("text", ArgumentSemantic.Retain)] [NullAllowed]
		string Text { get; set; }

		[Export ("glyphs", ArgumentSemantic.Retain)]
		PSPDFGlyph [] Glyphs { get; }

		[Export ("words", ArgumentSemantic.Retain)]
		PSPDFWord [] Words { get; }

		[Export ("lines", ArgumentSemantic.Retain)]
		PSPDFTextLine [] Lines { get; }

		[Export ("images", ArgumentSemantic.Retain)]
		PSPDFImageInfo [] Images { get; }

		[Export ("textBlocks", ArgumentSemantic.Retain)]
		PSPDFTextBlock [] TextBlocks { get; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; set; }

		[Export ("textWithGlyphs:")]
		string TextWithGlyphs (PSPDFGlyph [] glyphs);

		// PSPDFTextParser (SubclassingHooks) Category

		[Export ("transformedText", ArgumentSemantic.Retain)]
		string TransformedText { get; }

		[Export ("parsingQueue", ArgumentSemantic.Assign)]
		DispatchQueue ParsingQueue { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFGlyph
	{
		[Export ("initWithFrame:content:font:")]
		IntPtr Constructor (RectangleF frame, string content, PSPDFFontInfo font);

		[Export ("frame", ArgumentSemantic.Assign)]
		RectangleF Frame { get; set; }

		[Export ("content", ArgumentSemantic.Retain)] [NullAllowed]
		string Content { get; set; }

		[Export ("font", ArgumentSemantic.Assign)]
		PSPDFFontInfo Font { get; set; }

		[Export ("lineBreaker", ArgumentSemantic.Assign)]
		bool LineBreaker { get; set; }

		[Export ("isWordBreaker", ArgumentSemantic.Assign)]
		bool IsWordBreaker { get; }

		[Export ("isWordOrLineBreaker", ArgumentSemantic.Assign)]
		bool IsWordOrLineBreaker { get; }

		[Export ("indexOnPage", ArgumentSemantic.Assign)]
		int IndexOnPage { get; set; }

		[Export ("compareByXPosition:")]
		NSComparisonResult CompareByXPosition (PSPDFGlyph glyph);

		[Export ("isOnSameLineAs:")]
		bool IsOnSameLineAs (PSPDFGlyph glyph);

		[Export ("isEqualToGlyph:")]
		bool IsEqualToGlyph (PSPDFGlyph otherGlyph);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFWord
	{
		[Export("initWithGlyphs:")]
		IntPtr Constructor (PSPDFGlyph [] wordGlyphs);

		[Export("initWithFrame:")]
		IntPtr Constructor (RectangleF wordFrame);

		[Export("stringValue")]
		string Text { get; }

		[Export ("frame", ArgumentSemantic.Assign)]
		RectangleF Frame { get; set; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; set; }

		[Export ("lineBreaker", ArgumentSemantic.Assign)]
		bool LineBreaker { get; set; }

		[Export("isOnSameLineAs:")]
		bool IsOnSameLineAs (PSPDFWord word);

		[Export("compareByLayout:")]
		NSComparisonResult CompareByLayout (PSPDFWord word);

		[Export("isEqualToWord:")]
		bool IsEqualToWord (PSPDFWord otherWord);
	}

	[BaseType (typeof (PSPDFWord))]
	interface PSPDFTextLine
	{
		[Export ("prevLine", ArgumentSemantic.Assign)]
		PSPDFTextLine PrevLine { get; }

		[Export ("nextLine", ArgumentSemantic.Assign)]
		PSPDFTextLine NextLine { get; }

		[Export ("borderType", ArgumentSemantic.Assign)]
		PSPDFTextLineBorder BorderType { get; }

		[Export ("blockID", ArgumentSemantic.Assign)]
		int BlockID { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFTextBlock
	{
		[Export("initWithGlyphs:")]
		IntPtr Constructor (PSPDFGlyph [] glyphs);

		[Export ("frame", ArgumentSemantic.Assign)]
		RectangleF Frame { get; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; set; }

		[Export ("words", ArgumentSemantic.Copy)]
		PSPDFWord [] Words { get; }

		[Export ("content", ArgumentSemantic.Copy)]
		string Content { get; }

		[Export("isEqualToTextBlock:")]
		bool IsEqualToTextBlock (PSPDFTextBlock otherBlock);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFImageInfo
	{
		[Export ("imageID", ArgumentSemantic.Copy)]
		string ImageID { get; set; }

		[Export ("pixelWidth", ArgumentSemantic.Assign)]
		int PixelWidth { get; set; }

		[Export ("pixelHeight", ArgumentSemantic.Assign)]
		int PixelHeight { get; set; }

		[Export ("bitsPerComponent", ArgumentSemantic.Assign)]
		int BitsPerComponent { get; set; }

		[Export ("displayWidth", ArgumentSemantic.Assign)]
		double DisplayWidth { get; set; }

		[Export ("displayHeight", ArgumentSemantic.Assign)]
		double DisplayHeight { get; set; }

		[Export ("horizontalResolution", ArgumentSemantic.Assign)]
		double HorizontalResolution { get; set; }

		[Export ("verticalResolution", ArgumentSemantic.Assign)]
		double VerticalResolution { get; set; }

		[Export ("ctm", ArgumentSemantic.Assign)]
		CGAffineTransform Ctm { get; set; }

		[Export ("vertices", ArgumentSemantic.Assign)]
		PointF Vertices { get; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export("isPointInImage:")]
		bool IsPointInImage (PointF point);

		[Export ("boundingBox")]
		RectangleF BoundingBox { get; }

		[Export("imageWithError:")]
		UIImage ImageWithError (out NSError error);

		[Export("imageInRGBColorSpaceWithError:")]
		UIImage ImageInRGBColorSpaceWithError (out NSError error);
	}

	interface IPSPDFSearchViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFSearchViewControllerDelegate : PSPDFTextSearchDelegate, PSPDFOverridable
	{
		[Export ("searchViewController:didTapSearchResult:"), EventArgs ("PSPDFSearchViewControllerDelegate")]
		void DidTapSearchResult (PSPDFSearchViewController searchController, PSPDFSearchResult searchResult);

		[Export ("searchViewControllerDidClearAllSearchResults:"), EventArgs ("PSPDFTextSearchDelegateDidUpdateSearch")]
		void DidClearAllSearchResults (PSPDFSearchViewController searchController);

		[Export ("searchViewControllerGetVisiblePages:"), DelegateName ("PSPDFSearchViewControllerDelegateGetVisiblePages"), NoDefaultValue]
		NSObject [] GetVisiblePages (PSPDFSearchViewController searchController);
	}

	[BaseType (typeof (NSObject),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFSearchViewControllerDelegate) })]
	interface PSPDFSearchViewController
	{
		[Export("initWithDocument:delegate:")]
		IntPtr Constructor (PSPDFDocument document, [NullAllowed] PSPDFSearchViewControllerDelegate controllerDelegate);

		[Export ("searchText", ArgumentSemantic.Copy)]
		string SearchText { get; set; }

		[Export ("showsCancelButton", ArgumentSemantic.Assign)]
		bool ShowsCancelButton { get; set; }

		[Export ("searchBar", ArgumentSemantic.Retain)]
		UISearchBar SearchBar { get; }

		[Export ("searchStatus", ArgumentSemantic.Assign)]
		PSPDFSearchStatus SearchStatus { get; }

		[Export ("clearHighlightsWhenClosed", ArgumentSemantic.Assign)]
		bool ClearHighlightsWhenClosed { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed", ArgumentSemantic.Assign)]
		uint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("searchVisiblePagesFirst", ArgumentSemantic.Assign)]
		bool SearchVisiblePagesFirst { get; set; }

		[Export ("textSearch", ArgumentSemantic.Retain)]
		PSPDFTextSearch TextSearch { get; }

		[Wrap ("WeakDelegate")]
		PSPDFSearchViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export("updateResultCell:searchResult:")]
		void UpdateResultCell (PSPDFSearchResultCell cell, PSPDFSearchResult searchResult);

		// PSPDFSearchViewController (SubclassingHooks) Category

		[Export("filterContentForSearchText:scope:")]
		void FilterContentForSearchText (string searchText, string scope);

		[Export("setSearchStatus:updateTable:")]
		void SetSearchStatus (PSPDFSearchStatus searchStatus, bool updateTable);

		[Export("searchResultForIndexPath:")]
		PSPDFSearchResult SearchResultForIndexPath (NSIndexPath indexPath);

		[Export ("searchResults", ArgumentSemantic.Copy)]
		PSPDFSearchResult [] SearchResults { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSearchResult
	{
		[Export ("pageIndex", ArgumentSemantic.Assign)]
		uint PageIndex { get; set; }

		[Export ("previewText", ArgumentSemantic.Copy)]
		string PreviewText { get; set; }

		[Export ("selection", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFTextBlock Selection { get; set; }

		[Export ("range", ArgumentSemantic.Assign)]
		NSRange Range { get; set; }

		[Export ("rangeInPreviewText", ArgumentSemantic.Assign)]
		NSRange RangeInPreviewText { get; set; }

		[Export ("cachedOutlineTitle", ArgumentSemantic.Copy)]
		string CachedOutlineTitle { get; set; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; set; }

		[Export("isEqualToSearchResult:")]
		bool IsEqualToSearchResult (PSPDFSearchResult otherSearchResult);
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFSearchResultCell
	{
		[Export ("searchPreviewLabel", ArgumentSemantic.Retain)] [NullAllowed]
		NSObject /* PSPDFAttributedLabel */ SearchPreviewLabel { get; set; }

		[Export ("rotatedPageRect", ArgumentSemantic.Assign)]
		RectangleF RotatedPageRect { get; set; }

		[Export ("pagePreviewImage", ArgumentSemantic.Retain)] [NullAllowed]
		UIImage PagePreviewImage { get; set; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		// PSPDFSearchResultCell (SubclassingHooks) Category

		[Export("placeholderImage")]
		UIImage PlaceholderImage ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFSearchHighlightView : IPSPDFAnnotationViewProtocol
	{
		[Export("initWithSearchResult:")]
		IntPtr Constructor (PSPDFSearchResult searchResult);

		[Export("popupAnimation")]
		void PopupAnimation ();

		[Export ("searchResult", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFSearchResult SearchResult { get; set; }

		[Export ("selectionBackgroundColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor SelectionBackgroundColor { get; set; }
	}

	interface IPSPDFThumbnailViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFThumbnailViewControllerDelegate
	{
		[Export ("thumbnailViewController:didSelectPage:inDocument:"), EventArgs ("PSPDFThumbnailViewControllerDelegate")]
		void DidSelectPage (PSPDFThumbnailViewController thumbnailViewController, uint page, PSPDFDocument document);
	}

	[BaseType (typeof (PSTCollectionViewController),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFThumbnailViewControllerDelegate) })]
	interface PSPDFThumbnailViewController
	{
		[Export("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("collectionView", ArgumentSemantic.Retain)] [NullAllowed] [New]
		PSTCollectionView CollectionView { get; set; }

		[Export ("document", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFDocument Document { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFThumbnailViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export("cellForPage:")]
		PSTCollectionViewCell CellForPage (uint page);

		[Export("scrollToPage:animated:")]
		void ScrollToPage (uint page, bool animated);

		[Export("stopScrolling")]
		void StopScrolling ();

		[Export("updateFilterAndVisibleCells")]
		void UpdateFilterAndVisibleCells ();

		[Export ("fixedItemSizeEnabled", ArgumentSemantic.Assign)]
		bool FixedItemSizeEnabled { get; set; }

		[Export ("stickyHeaderEnabled", ArgumentSemantic.Assign)]
		bool StickyHeaderEnabled { get; set; }

		[Export ("filterOptions", ArgumentSemantic.Copy)] [NullAllowed]
		NSObject /* HACK: NSOrderedSet */  FilterOptions { get; set; }

		[Export ("activeFilter", ArgumentSemantic.Assign)]
		PSPDFThumbnailViewFilter ActiveFilter { get; }

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		SizeF ThumbnailSize { get; set; }

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailMargin { get; set; }

		[Export ("thumbnailRowMargin", ArgumentSemantic.Assign)]
		float ThumbnailRowMargin { get; set; }

		[Export ("thumbnailCellClass", ArgumentSemantic.Retain)] [NullAllowed]
		Class ThumbnailCellClass { get; set; }

		// PSPDFThumbnailViewController (SubclassingHooks) Category

		[Export("configureCell:forIndexPath:")]
		void ConfigureCell (PSPDFThumbnailGridViewCell cell, NSIndexPath indexPath);

		[Export ("layout", ArgumentSemantic.Retain)] [NullAllowed]
		PSTCollectionViewLayout Layout { get; set; }

		[Export ("filterSegment", ArgumentSemantic.Retain)] [NullAllowed]
		UISegmentedControl FilterSegment { get; set; }

		[Export("updateFilterSegment")]
		void UpdateFilterSegment ();

		[Export("updateEmptyView")]
		void UpdateEmptyView ();

		[Export ("emptyView", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFCenteredLabelView EmptyView { get; set; }
	}

	[BaseType (typeof (PSPDFThumbnailViewController))]
	interface PSPDFMultiDocumentThumbnailViewController
	{
		[Export("initWithDocuments:")]
		IntPtr Constructor (PSPDFDocument [] documents);

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; set; }

		[Export ("firstPageOnly", ArgumentSemantic.Assign)]
		bool FirstPageOnly { get; set; }
	}

	[BaseType (typeof (PSTCollectionViewCell))]
	interface PSPDFThumbnailGridViewCell : IPSPDFCacheDelegate
	{
		[Export ("document", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }

		[Export ("shadowEnabled", ArgumentSemantic.Assign)]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("pageLabelEnabled", ArgumentSemantic.Assign)]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; set; }

		[Export ("selectedBackgroundView", ArgumentSemantic.Retain)] [NullAllowed] [New]
		UIView SelectedBackgroundView { get; set; }

		[Export("updateCell")]
		void UpdateCell ();

		// PSPDFThumbnailGridViewCell (SubclassingHooks) Category

		[Export ("imageView", ArgumentSemantic.Retain)] [NullAllowed]
		UIImageView ImageView { get; set; }

		[Export ("pageLabel", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFRoundedLabel PageLabel { get; set; }

		[Export ("bookmarkImageView", ArgumentSemantic.Retain)]
		UIImageView BookmarkImageView { get; }

		[Export("pathShadowForView:")]
		NSObject PathShadowForView (UIView imgView);

		[Export("setImage:animated:")]
		void SetImage (UIView imgView, bool animated);

		[Export("setImageSize:")]
		void SetImageSize (SizeF imageSize);

		[Export("updatePageLabel")]
		void UpdatePageLabel ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFScrobbleBar : IPSPDFCacheDelegate
	{
		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; }

		[Export("updateToolbarAnimated:")]
		void UpdateToolbarAnimated (bool animated);

		[Export("updateToolbarPositionAnimated:")]
		void UpdateToolbarPositionAnimated (bool animated);

		[Export("updateToolbarForced")]
		void UpdateToolbarForced ();

		[Export("updatePageMarker")]
		void UpdatePageMarker ();

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("toolbar", ArgumentSemantic.Retain)] [NullAllowed]
		UIToolbar Toolbar { get; set; }

		[Export ("leftBorderMargin", ArgumentSemantic.Assign)]
		float LeftBorderMargin { get; set; }

		[Export ("rightBorderMargin", ArgumentSemantic.Assign)]
		float RightBorderMargin { get; set; }

		[Export ("allowTapsOutsidePageArea", ArgumentSemantic.Assign)]
		bool AllowTapsOutsidePageArea { get; set; }

		// PSPDFScrobbleBar (SubclassingHooks) Category

		[Export("setToolbarFrameAndVisibility:animated:")]
		void SetToolbarFrameAndVisibility (bool shouldShow, bool animated);

		[Export ("viewLocked", ArgumentSemantic.Assign)]
		bool ViewLocked { [Bind ("isViewLocked")] get; set; }

		[Export ("isSmallToolbar")]
		bool IsSmallToolbar { get; }

		[Export ("scrobbleBarHeight")]
		float ScrobbleBarHeight { get; }

		[Export ("scrobbleBarThumbSize")]
		SizeF ScrobbleBarThumbSize { get; }

		[Export ("emptyThumbnailImageView")]
		UIImageView EmptyThumbnailImageView { get; }

		[Export("processTouch:animated:")]
		bool ProcessTouch (UITouch touch, bool animated);

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		float ThumbnailMargin { get; set; }

		[Export ("pageMarkerSizeMultiplier", ArgumentSemantic.Assign)]
		float PageMarkerSizeMultiplier { get; set; }
	}

	interface IPSPDFThumbnailBarDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFThumbnailBarDelegate
	{
		[Export ("thumbnailBar:didSelectPage:"), EventArgs ("PSPDFThumbnailBarDelegate")]
		void DidSelectPage (PSPDFThumbnailBar thumbnailBar, uint page);
	}

	[BaseType (typeof (PSTCollectionViewController),
	Delegates=new string [] {"WeakThumbnailBarDelegate"},
	Events=new Type [] { typeof (PSPDFThumbnailBarDelegate) })]
	interface PSPDFThumbnailBar
	{
		[Export("initWithDocuments:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFDocument Document { get; set; }

		[Wrap ("WeakThumbnailBarDelegate")]
		PSPDFThumbnailBarDelegate ThumbnailBarDelegate { get; set; }

		[Export ("thumbnailBarDelegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakThumbnailBarDelegate { get; set; }

		[Export("scrollToPage:animated:")]
		void ScrollToPage (uint page, bool animated);

		[Export ("stopScrolling")]
		void StopScrolling ();

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		SizeF ThumbnailSize { get; set; }

		[Export ("thumbnailBarHeight", ArgumentSemantic.Assign)]
		float ThumbnailBarHeight { get; set; }

		[Export ("thumbnailCellClass", ArgumentSemantic.Retain)] [NullAllowed]
		Class ThumbnailCellClass { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFOutlineParser
	{
		[Export("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export("outlineElementForPage:exactPageOnly:")]
		PSPDFOutlineElement OutlineElementForPage (uint page, bool exactPageOnly);

		[Export ("outline", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFOutlineElement Outline { get; set; }

		[Export ("outlineParsed", ArgumentSemantic.Assign)]
		bool OutlineParsed { [Bind ("isOutlineParsed")] get; set; }

		[Export ("outlineAvailable", ArgumentSemantic.Assign)]
		bool OutlineAvailable { [Bind ("isOutlineAvailable")] get; set; }

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("firstVisibleElement", ArgumentSemantic.Assign)]
		uint FirstVisibleElement { get; set; }

		[Export ("namedDestinationResolveThreshold", ArgumentSemantic.Assign)]
		uint NamedDestinationResolveThreshold { get; set; }
	}

	[BaseType (typeof (PSPDFBookmark))]
	interface PSPDFOutlineElement
	{
		[Export("initWithTitle:action:children:level:")]
		IntPtr Constructor (string title, PSPDFAction action, PSPDFOutlineElement [] children, uint level);

		[Export ("flattenedChildren")]
		PSPDFOutlineElement [] FlattenedChildren { get; }

		[Export ("allFlattenedChildren")]
		PSPDFOutlineElement [] AllFlattenedChildren { get; }

		[Export ("title", ArgumentSemantic.Copy)]
		string Title { get; set; }

		[Export ("children", ArgumentSemantic.Copy)]
		PSPDFOutlineElement [] Children { get; }

		[Export ("level", ArgumentSemantic.Assign)]
		uint Level { get; set; }

		[Export ("expanded", ArgumentSemantic.Assign)]
		bool Expanded { [Bind ("isExpanded")] get; set; }
	}

	[BaseType (typeof (UITableViewController))]
	interface PSPDFBaseTableViewController
	{
		[Export ("automaticallyAdjustsTableViewInsets", ArgumentSemantic.Assign)]
		bool AutomaticallyAdjustsTableViewInsets { get; set; }

		[Export ("automaticallyResizesPopover", ArgumentSemantic.Assign)]
		bool AutomaticallyResizesPopover { get; set; }

		[Export ("minimumHeightForAutomaticallyResizingPopover", ArgumentSemantic.Assign)]
		float MinimumHeightForAutomaticallyResizingPopover { get; set; }

		[Export ("userInfo", ArgumentSemantic.Copy)] [NullAllowed]
		NSDictionary UserInfo { get; set; }

		// PSPDFBaseTableViewController (SubclassingHooks) Category

		[Export("updatePopoverSize")]
		void UpdatePopoverSize ();

		[Export("requiredPopoverSize")]
		SizeF RequiredPopoverSize ();
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFStatefulTableViewController
	{
		[Export ("emptyView", ArgumentSemantic.Retain)] [NullAllowed]
		UIView EmptyView { get; set; }

		[Export ("loadingView", ArgumentSemantic.Retain)] [NullAllowed]
		UIView LoadingView { get; set; }

		[Export ("controllerState", ArgumentSemantic.Assign)]
		PSPDFStatefulTableViewState ControllerState { get; set; }
	}

	interface IPSPDFOutlineViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFOutlineViewControllerDelegate
	{
		[Abstract]
		[Export ("outlineController:didTapAtElement:"), DelegateName ("PSPDFOutlineViewControllerDelegateDidSelectPage"), DefaultValue (true)]
		bool DidSelectPage (PSPDFOutlineViewController outlineController, PSPDFOutlineElement outlineElement);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController),
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFOutlineViewControllerDelegate) })]
	interface PSPDFOutlineViewController : IUISearchDisplayDelegate, IPSPDFStyleable
	{
		[Export("initWithDocument:delegate:")]
		IntPtr Constructor (PSPDFDocument document, [NullAllowed] PSPDFOutlineViewControllerDelegate controllerDelegate);

		[Export ("allowCopy", ArgumentSemantic.Assign)]
		bool AllowCopy { get; set; }

		[Export ("searchEnabled", ArgumentSemantic.Assign)]
		bool SearchEnabled { get; set; }

		[Export ("showPageLabels", ArgumentSemantic.Assign)]
		bool ShowPageLabels { get; set; }

		[Export ("maximumNumberOfLines", ArgumentSemantic.Assign)]
		uint MaximumNumberOfLines { get; set; }

		[Export ("outlineIntentLeftOffset", ArgumentSemantic.Assign)]
		float OutlineIntentLeftOffset { get; set; }

		[Export ("outlineIndentMultiplier", ArgumentSemantic.Assign)]
		float OutlineIndentMultiplier { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFOutlineViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; set; }

		// PSPDFOutlineViewController (SubclassingHooks) Category

		[Export("outlineCellDidTapDisclosureButton:")]
		void UpdatePopoverSize (PSPDFOutlineCell cell);

		[Export ("outlineCellClass", ArgumentSemantic.Retain)] [NullAllowed]
		Class OutlineCellClass { get; set; }
	}

	interface IPSPDFOutlineCellDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFOutlineCellDelegate
	{
		[Abstract]
		[Export ("outlineCellDidTapDisclosureButton:"), DelegateName ("PSPDFOutlineCellDelegateDidTapDisclosureButton"), DefaultValue (true)]
		bool DidTapDisclosureButton (PSPDFOutlineCell outlineCell);
	}

	[BaseType (typeof (UITableViewCell),
	 Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (PSPDFOutlineCellDelegate) })]
	interface PSPDFOutlineCell
	{
		[Static]
		[Export ("heightForCellWithOutlineElement:constrainedToSize:outlineIntentLeftOffset:outlineIntentMultiplier:showPageLabel:")]
		float HeightForCellWithOutlineElement (PSPDFOutlineElement outlineElement, SizeF constraintSize, float leftOffset, float multiplier, bool showPageLabel);

		[Wrap ("WeakDelegate")]
		PSPDFOutlineCellDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("outlineElement", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFOutlineElement OutlineElement { get; set; }

		[Export ("showExpandCollapseButton", ArgumentSemantic.Assign)]
		bool ShowExpandCollapseButton { get; set; }

		[Export ("showPageLabel", ArgumentSemantic.Assign)]
		bool ShowPageLabel { get; set; }

		// PSPDFOutlineCell (SubclassingHooks) Category

		[Export ("disclosureButton", ArgumentSemantic.Retain)] [NullAllowed]
		UIButton DisclosureButton { get; set; }

		[Export ("pageLabel", ArgumentSemantic.Retain)] [NullAllowed]
		UILabel PageLabel { get; set; }

		[Static]
		[Export ("fontForOutlineElement:")]
		UIFont FontForOutlineElement (PSPDFOutlineElement outlineElement);

		[Export("updateDisclosureButton")]
		void UpdateDisclosureButton ();

		[Export("expandOrCollapse")]
		void ExpandOrCollapse ();

		[Export ("outlineIntentLeftOffset", ArgumentSemantic.Assign)]
		float OutlineIntentLeftOffset { get; set; }

		[Export ("outlineIndentMultiplier", ArgumentSemantic.Assign)]
		float OutlineIndentMultiplier { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFLabelParser
	{
		[Export("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export("parseDocument")]
		NSDictionary ParseDocument ();

		[Export("pageLabelForPage:")]
		string PageLabelForPage (uint page);

		[Export("pageForPageLabel:partialMatching:")]
		uint PageForPageLabel (string pageLabel, bool partialMatching);

		[Export ("labels", ArgumentSemantic.Copy)]
		NSDictionary Labels { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationManager : IPSPDFAnnotationProviderChangeNotifier
	{
		[Notification]
		[Field ("PSPDFAnnotationsAddedNotification", "__Internal")]
		NSString AnnotationsAddedNotification { get; }

		[Notification]
		[Field ("PSPDFAnnotationsRemovedNotification", "__Internal")]
		NSString AnnotationsRemovedNotification { get; }

		[Export("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("annotationProviders", ArgumentSemantic.Copy)]
		NSObject [] AnnotationProviders { get; set; }

		[Export ("fileAnnotationProvider", ArgumentSemantic.Retain)]
		PSPDFFileAnnotationProvider FileAnnotationProvider { get; }

		[Export("annotationsForPage:type:")]
		PSPDFAnnotation [] AnnotationsForPage (uint page, PSPDFAnnotationType type);

		[Export("allAnnotationsOfType:")]
		NSDictionary AllAnnotationsOfType (PSPDFAnnotationType annotationType);

		[Export("hasLoadedAnnotationsForPage:")]
		bool HasLoadedAnnotationsForPage (uint page);

		[Export("annotationViewClassForAnnotation:")]
		Class AnnotationViewClassForAnnotation (PSPDFAnnotation annotation);

		[Export("addAnnotations:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations);

		[Export("removeAnnotations:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations);

		[Export("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, NSObject [] keyPaths, NSDictionary options);

		[Export("saveAnnotationsWithOptions:error:")] [Internal]
		bool _SaveAnnotationsWithOptions (NSDictionary options, IntPtr error);

		[Export("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations ();

		[Export("updateAnnotations:animated:")]
		void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Export ("protocolString", ArgumentSemantic.Copy)]
		string ProtocolString { get; set; }

		[Export ("fileTypeTranslationTable", ArgumentSemantic.Copy)]
		NSDictionary FileTypeTranslationTable { get; set; }

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		// PSPDFAnnotationManager (SubclassingHooks) Category

		[Export("annotationsForPage:type:pageRef:")] [Internal]
		PSPDFAnnotation [] AnnotationsForPage_ (uint page, PSPDFAnnotationType type, IntPtr /* CGPDFPage */ pageRef);

		[Export("dirtyAnnotations")]
		NSDictionary DirtyAnnotations { get; }

		[Export("mediaFileTypes")]
		NSObject [] MediaFileTypes { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotation : IPSPDFUndoProtocol, IPSPDFJSONSerializing
	{
		[Field ("PSPDFAnnotationStringLink", "__Internal")]
		NSString AnnotationStringLink { get; }

		[Field ("PSPDFAnnotationStringHighlight", "__Internal")]
		NSString AnnotationStringHighlight { get; }

		[Field ("PSPDFAnnotationStringUnderline", "__Internal")]
		NSString AnnotationStringUnderline { get; }

		[Field ("PSPDFAnnotationStringStrikeOut", "__Internal")]
		NSString AnnotationStringStrikeOut { get; }

		[Field ("PSPDFAnnotationStringSquiggly", "__Internal")]
		NSString AnnotationStringSquiggly { get; }

		[Field ("PSPDFAnnotationStringNote", "__Internal")]
		NSString AnnotationStringNote { get; }

		[Field ("PSPDFAnnotationStringFreeText", "__Internal")]
		NSString AnnotationStringFreeText { get; }

		[Field ("PSPDFAnnotationStringInk", "__Internal")]
		NSString AnnotationStringInk { get; }

		[Field ("PSPDFAnnotationStringSquare", "__Internal")]
		NSString AnnotationStringSquare { get; }

		[Field ("PSPDFAnnotationStringCircle", "__Internal")]
		NSString AnnotationStringCircle { get; }

		[Field ("PSPDFAnnotationStringLine", "__Internal")]
		NSString AnnotationStringLine { get; }

		[Field ("PSPDFAnnotationStringPolygon", "__Internal")]
		NSString AnnotationStringPolygon { get; }

		[Field ("PSPDFAnnotationStringPolyLine", "__Internal")]
		NSString AnnotationStringPolyLine { get; }

		[Field ("PSPDFAnnotationStringStamp", "__Internal")]
		NSString AnnotationStringStamp { get; }

		[Field ("PSPDFAnnotationStringSound", "__Internal")]
		NSString AnnotationStringSound { get; }

		[Field ("PSPDFAnnotationStringImage", "__Internal")]
		NSString AnnotationStringImage { get; }

		[Field ("PSPDFAnnotationStringWidget", "__Internal")]
		NSString AnnotationStringWidget { get; }

		[Field ("PSPDFAnnotationStringFile", "__Internal")]
		NSString AnnotationStringFile { get; }

		[Field ("PSPDFAnnotationStringRichMedia", "__Internal")]
		NSString AnnotationStringRichMedia { get; }

		[Field ("PSPDFAnnotationStringScreen", "__Internal")]
		NSString AnnotationStringScreen { get; }

		[Field ("PSPDFAnnotationStringCaret", "__Internal")]
		NSString AnnotationStringCaret { get; }

		[Field ("PSPDFAnnotationStringPopup", "__Internal")]
		NSString AnnotationStringPopup { get; }

		[Field ("PSPDFAnnotationStringTextFieldFormElement", "__Internal")]
		NSString AnnotationStringTextFieldFormElement { get; }

		[Field ("PSPDFAnnotationStringButtonFormElement", "__Internal")]
		NSString AnnotationStringButtonFormElement { get; }

		[Field ("PSPDFAnnotationStringChoiceFormElement", "__Internal")]
		NSString AnnotationStringChoiceFormElement { get; }

		[Field ("PSPDFAnnotationStringSignatureFormElement", "__Internal")]
		NSString AnnotationStringSignatureFormElement { get; }

		[Static]
		[Export ("supportedTypes")]
		string [] SupportedTypes { get; }

		[Static]
		[Export ("annotationFromJSONDictionary:document:error:")]
		PSPDFAnnotation AnnotationFromJsonDictionary (NSDictionary jsonDictionary, PSPDFDocument document, out NSError error);

		[Static]
		[Export ("isWriteable")]
		bool IsWriteable { get; }

		[Export ("isMovable")]
		bool IsMovable { get; }

		[Export ("isResizable")]
		bool IsResizable { get; }

		[Export ("shouldMaintainAspectRatio")]
		bool ShouldMaintainAspectRatio { get; }

		[Export ("minimumSize")]
		SizeF MinimumSize { get; }

		[Export ("initWithType:")]
		IntPtr Constructor (PSPDFAnnotationType annotationType);

		[Export ("hitTest:")]
		bool HitTest (PointF point);

		[Export ("boundingBoxForPageRect:")]
		RectangleF BoundingBoxForPageRect (RectangleF pageRect);

		[Export ("drawInContext:")]
		void DrawInContext (CGContext context);

		[Field ("PSPDFAnnotationDrawFlattened", "__Internal")]
		NSString AnnotationDrawFlattened { get; }

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, NSDictionary options);

		[Field ("PSPDFAnnotationDrawCentered", "__Internal")]
		NSString AnnotationDrawCentered { get; }

		[Field ("PSPDFAnnotationMargin", "__Internal")]
		NSString AnnotationMargin { get; }

		[Export ("imageWithSize:withOptions:")]
		UIImage ImageWithSize (SizeF size, NSDictionary options);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFAnnotationType Type { get; }

		[Export ("overlay", ArgumentSemantic.Assign)]
		bool Overlay { [Bind ("isOverlay")] get; set; }

		[Export ("editable", ArgumentSemantic.Assign)]
		bool Editable { [Bind ("isEditable")] get; set; }

		[Export ("typeString", ArgumentSemantic.Copy)]
		string TypeString { get; set; }

		[Export ("alpha", ArgumentSemantic.Assign)]
		float Alpha { get; set; }

		[Export ("color", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor Color { get; set; }

		[Export ("colorWithAlpha", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor ColorWithAlpha { get; }

		[Export ("fillColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor FillColor { get; set; }

		[Export ("fillColorWithAlpha", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor FillColorWithAlpha { get; }

		[Export ("contents", ArgumentSemantic.Copy)]
		string Contents { get; set; }

		[Export ("value", ArgumentSemantic.Copy)]
		NSObject Value { get; set; }

		[Export ("flags", ArgumentSemantic.Assign)]
		uint Flags { get; set; }

		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; set; }

		[Export ("group", ArgumentSemantic.Copy)]
		string Group { get; set; }

		[Export ("lastModified", ArgumentSemantic.Retain)] [NullAllowed]
		NSDate LastModified { get; set; }

		[Export ("creationDate", ArgumentSemantic.Retain)] [NullAllowed]
		NSDate CreationDate { get; set; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		float LineWidth { get; set; }

		[Export ("borderStyle", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderStyle BorderStyle { get; set; }

		[Export ("dashArray", ArgumentSemantic.Copy)]
		NSValue [] DashArray { get; set; }

		[Export ("deleted", ArgumentSemantic.Assign)]
		bool Deleted { [Bind ("isDeleted")] get; set; }

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		RectangleF BoundingBox { get; set; }

		[Export ("rotation", ArgumentSemantic.Assign)]
		uint Rotation { get; set; }

		[Export ("rects", ArgumentSemantic.Copy)]
		NSValue [] Rects { get; set; }

		[Export ("points", ArgumentSemantic.Copy)]
		NSValue [] Points { get; set; }

		[Export ("user", ArgumentSemantic.Copy)]
		string User { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("absolutePage", ArgumentSemantic.Assign)]
		uint AbsolutePage { get; set; }

		[Export ("dirty", ArgumentSemantic.Assign)]
		bool Dirty { [Bind ("isDirty")] get; set; }

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; }

		[Export ("hasAppearanceStream", ArgumentSemantic.Assign)]
		bool HasAppearanceStream { get; }

		[Export ("indexOnPage")]
		int IndexOnPage { get; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; set; }

		[Export ("subject", ArgumentSemantic.Copy)]
		string Subject { get; set; }

		[Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary AdditionalActions { get; set; }

		[Export ("localizedDescription")]
		string LocalizedDescription { get; }

		[Export ("annotationIcon")]
		UIImage AnnotationIcon { get; }

		[Export ("isEqualToAnnotation:")]
		bool IsEqualToAnnotation (PSPDFAnnotation otherAnnotation);

		[Export ("copyToClipboard")]
		bool CopyToClipboard ();

		[Field ("PSPDFBorderStyleTransformerName", "__Internal")]
		NSString BorderStyleTransformerName { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationSet
	{
		[Export ("initWithAnnotations:")]
		IntPtr Constructor (PSPDFAnnotation [] annotations);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, [NullAllowed] NSDictionary options);

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		RectangleF BoundingBox { get; set; }

		[Export ("copyToClipboard")]
		void CopyToClipboard ();

		[Static]
		[Export ("unarchiveFromClipboard")]
		PSPDFAnnotationSet UnarchiveFromClipboard ();
	}

	interface IPSPDFAnnotationProvider { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationProvider
	{
		[Abstract]
		[Export ("annotationsForPage:")]
		PSPDFAnnotation [] AnnotationsForPage (uint page);

		[Export ("hasLoadedAnnotationsForPage:")]
		bool HasLoadedAnnotationsForPage (uint page);

		[Export ("annotationViewClassForAnnotation:")]
		Class AnnotationViewClassForAnnotation (PSPDFAnnotation annotation);

		[Export ("addAnnotations:")]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations);

		[Export ("removeAnnotations:")]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations);

		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotationsWithOptions (NSDictionary options, out NSError error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations ();

		[Export ("dirtyAnnotations")]
		NSDictionary DirtyAnnotations ();

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, NSObject [] keyPaths, NSDictionary options);

		[Export ("providerDelegate", ArgumentSemantic.Assign)]
		IPSPDFAnnotationProviderChangeNotifier ProviderDelegate { get; set; }
	}

	interface IPSPDFAnnotationProviderChangeNotifier { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationProviderChangeNotifier
	{
		[Abstract]
		[Export ("updateAnnotations:animated:")]
		void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Abstract]
		[Export ("parentDocumentProvider")]
		PSPDFDocumentProvider ParentDocumentProvider ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFFileAnnotationProvider : IPSPDFAnnotationProvider
	{
		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[Export ("defaultAnnotationUsername", ArgumentSemantic.Copy)]
		string DefaultAnnotationUsername { get; set; }

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsForPage:pageRef:")] [Internal]
		PSPDFAnnotation [] AnnotationsForPage_ (uint page, IntPtr /* CGPDFPage */ pageRef);

		[Export ("setAnnotations:forPage:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, uint page);

		[Export ("addAnnotations:")]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations);

		[Export ("removeAnnotations:")]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("tryLoadAnnotationsFromFileWithError:")] [Internal]
		bool _TryLoadAnnotationsFromFileWithError (IntPtr error);

		// PSPDFFileAnnotationProvider (Advanced) Category

		[Export ("saveableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SaveableTypes { get; set; }

		[Export ("annotationsPath", ArgumentSemantic.Copy)] [NullAllowed]
		string AnnotationsPath { get; set; }

		// PSPDFFileAnnotationProvider (SubclassingHooks) Category

		[Export("parseAnnotationsForPage:pageRef:")] [Internal]
		PSPDFAnnotation [] ParseAnnotationsForPage_ (uint page, IntPtr /* CGPDFPage */ pageRef);

		[Export("saveAnnotationsWithOptions:error:")] [Internal]
		bool _SaveAnnotationsWithOptions (NSDictionary options, IntPtr error);

		[Export("loadAnnotationsWithError:")]
		NSDictionary _LoadAnnotationsWithError (IntPtr error);

		[Export("parseAnnotationLinkTarget:")]
		void ParseAnnotationLinkTarget (PSPDFLinkAnnotation linkAnnotation);

		[Static]
		[Export("resolvePath:forDocument:page:")]
		NSUrl ResolvePath (string path, PSPDFDocument document, uint page);

		[Export("removeDeletedAnnotations")]
		uint RemoveDeletedAnnotations ();
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractTextOverlayAnnotation
	{
		[Static]
		[Export ("TextOverlayAnnotationWithGlyphs:pageRotationTransform:")]
		PSPDFAbstractTextOverlayAnnotation TextOverlayAnnotationWithGlyphs (PSPDFGlyph [] glyphs, CGAffineTransform pageRotationTransform);

		[Export ("highlightedString")]
		string HighlightedString { get; }
	}

	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFHighlightAnnotation
	{

	}

	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFUnderlineAnnotation
	{

	}

	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFStrikeOutAnnotation
	{

	}

	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFSquigglyAnnotation
	{

	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFreeTextAnnotation
	{
		[Export ("fontName", ArgumentSemantic.Copy)]
		string FontName { get; set; }

		[Export ("fontSize", ArgumentSemantic.Assign)]
		float FontSize { get; set; }

		[Export ("lineHeight", ArgumentSemantic.Assign)]
		float LineHeight { get; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[Export ("verticalTextAlignment", ArgumentSemantic.Assign)]
		PSPDFVerticalAlignment VerticalTextAlignment { get; set; }

		[Export ("defaultFontSize")]
		float DefaultFontSize { get; }

		[Export ("defaultFontName")]
		string DefaultFontName { get; }

		[Export ("defaultFont")]
		UIFont DefaultFont { get; }

		[Export ("sizeToFit")]
		void SizeToFit ();

		[Export ("sizeWithConstraints:")]
		SizeF SizeWithConstraints (SizeF constraints);

		[Export ("enableVerticalResizing", ArgumentSemantic.Assign)]
		bool EnableVerticalResizing { get; set; }

		[Export ("enableHorizontalResizing", ArgumentSemantic.Assign)]
		bool EnableHorizontalResizing { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFNoteAnnotation
	{
		[Export ("iconName", ArgumentSemantic.Copy)]
		string IconName { get; set; }

		[Export ("hitTest:withViewBounds:")]
		bool HitTest (PointF point, RectangleF bounds);

		[Export ("boundingBoxForPageViewBounds:")]
		RectangleF BoundingBoxForPageViewBounds (RectangleF pageBounds);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFInkAnnotation
	{
		[Export ("lines", ArgumentSemantic.Copy)]
		NSValue [] Lines { get; [NullAllowed] set; }

		[Export ("bezierPath", ArgumentSemantic.Copy)]
		UIBezierPath BezierPath { get; }

		[Export ("setBoundingBox:transformLines:")]
		void SetBoundingBox (RectangleF boundingBox, bool transformLines);

		[Export ("copyLinesByApplyingTransform:")]
		NSValue [] CopyLinesByApplyingTransform (CGAffineTransform transform);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractLineAnnotation
	{
		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("bezierPath", ArgumentSemantic.Retain)]
		UIBezierPath BezierPath { get; }

		[Export ("setBoundingBox:transformPoints:")]
		void SetBoundingBox (RectangleF boundingBox, bool transformPoints);
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFLineAnnotation
	{
		[Export ("point1", ArgumentSemantic.Assign)]
		PointF Point1 { get; set; }

		[Export ("point2", ArgumentSemantic.Assign)]
		PointF Point2 { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFLinkAnnotation
	{
		[Export ("initWithLinkAnnotationType:")]
		IntPtr Constructor (PSPDFLinkAnnotationType linkAnotationType);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithURLString:")]
		IntPtr Constructor (string urlString);

		[Export ("initWithPage:")]
		IntPtr Constructor (uint page);

		[Export ("linkType", ArgumentSemantic.Assign)]
		PSPDFLinkAnnotationType LinkType { get; set; }

		[Export ("action", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAction Action { get; set; }

		[Export ("URLAction")]
		PSPDFURLAction UrlAction { get; }

		[Export ("URL", ArgumentSemantic.Retain)] [NullAllowed]
		NSUrl Url { get; set; }

		[Export ("showAsLinkView", ArgumentSemantic.Assign)]
		bool ShowAsLinkView { get; }

		[Export ("multimediaExtension", ArgumentSemantic.Assign)]
		bool MultimediaExtension { [Bind ("isMultimediaExtension")] get; }

		[Export ("controlsEnabled", ArgumentSemantic.Assign)]
		bool ControlsEnabled { get; set; }

		[Export ("autoplayEnabled", ArgumentSemantic.Assign)]
		bool AutoplayEnabled { [Bind ("isAutoplayEnabled")] get; set; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; set; }

		[Export ("targetString")]
		string TargetString { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFSquareAnnotation
	{
		[Export ("bezierPath", ArgumentSemantic.Retain)] [NullAllowed]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFCircleAnnotation
	{
		[Export ("bezierPath", ArgumentSemantic.Retain)] [NullAllowed]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFStampAnnotation
	{
		[Static]
		[Export ("stampColorForSubject:")]
		UIColor StampColorForSubject (string subject);

		[Export ("initWithSubject:")]
		IntPtr Constructor (string subject);

		[Export ("subtext", ArgumentSemantic.Copy)]
		string Subtext { get; set; }

		[Export ("image", ArgumentSemantic.Retain)] [NullAllowed]
		UIImage Image { get; set; }

		[Export ("loadImageWithError:")] [Internal]
		UIImage _LoadImageWithError (IntPtr error);

		[Export ("imageTransform", ArgumentSemantic.Assign)]
		CGAffineTransform ImageTransform { get; set; }

		[Export ("sizeThatFits:")]
		SizeF SizeThatFits (SizeF size);

		[Export ("sizeToFit")]
		void SizeToFit ();
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFCaretAnnotation
	{

	}
	
	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFPopupAnnotation
	{

	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFWidgetAnnotation
	{
		[Export ("action", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAction Action { get; set; }

		[Export ("handleTapInView:")]
		bool HandleTapInView (PSPDFPageView pdfPageView);

		[Export ("shouldRenderAppearanceStream", ArgumentSemantic.Assign)]
		bool ShouldRenderAppearanceStream { get; set; }
	}

	[BaseType (typeof (PSPDFLinkAnnotation))]
	interface PSPDFScreenAnnotation
	{

	}

	[BaseType (typeof (PSPDFLinkAnnotation))]
	interface PSPDFRichMediaAnnotation
	{

	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFileAnnotation
	{
		[Export ("URL", ArgumentSemantic.Retain)] [NullAllowed]
		NSUrl Url { get; set; }

		[Export ("fileName", ArgumentSemantic.Copy)]
		string FileName { get; set; }

		[Export ("appearanceName", ArgumentSemantic.Copy)]
		string AppearanceName { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFSoundAnnotation
	{
		[Export ("initWithRate:channels:bits:encoding:")]
		IntPtr Constructor (uint rate, uint channels, uint bits, string encoding);

		[Export ("iconName", ArgumentSemantic.Copy)]
		string IconName { get; set; }

		[Export ("soundURL", ArgumentSemantic.Retain)] [NullAllowed]
		NSUrl SoundUrl { get; set; }

		[Export ("bits", ArgumentSemantic.Assign)]
		uint Bits { get; set; }

		[Export ("rate", ArgumentSemantic.Assign)]
		uint Rate { get; set; }

		[Export ("channels", ArgumentSemantic.Assign)]
		uint Channels { get; set; }

		[Export ("encoding", ArgumentSemantic.Copy)]
		string Encoding { get; set; }

		[Export ("soundStreamData")]
		NSData SoundStreamData ();

		[Export ("setStreamPropertiesWithDescription:")]
		void SetStreamPropertiesWithDescription (NSDictionary streamDescription);
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolygonAnnotation
	{
		[Export ("points", ArgumentSemantic.Copy)] [New]
		NSValue [] Points { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolyLineAnnotation
	{
		[Export ("points", ArgumentSemantic.Copy)] [New]
		NSValue [] Points { get; set; }
	}

	interface IPSPDFAnnotationViewProtocol { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationViewProtocol
	{
		[Export ("annotation", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAnnotation Annotation { get; set; }

		[Export ("zIndex", ArgumentSemantic.Assign)]
		uint ZIndex { get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		float ZoomScale { get; set; }

		[Export ("PDFScale", ArgumentSemantic.Assign)]
		float PdfScale { get; set; }

		[Export ("willShowPage:")]
		void WillShowPage (uint page);

		[Export ("didShowPage:")]
		void DidShowPage (uint page);

		[Export ("willHidePage:")]
		void WillHidePage (uint page);

		[Export ("didHidePage:")]
		void DidHidePage (uint page);

		[Export ("didChangePageBounds:")]
		void DidChangePageBounds (RectangleF bounds);

		[Export ("viewController", ArgumentSemantic.Assign)] [NullAllowed]
		UIViewController ViewController { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFLinkAnnotationBaseView : IPSPDFAnnotationViewProtocol
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("linkAnnotation", ArgumentSemantic.Retain)]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("zIndex", ArgumentSemantic.Assign)]
		uint ZIndex { get; set; }
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFLinkAnnotationView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Static]
		[Export ("setGlobalBorderColor:")]
		void SetGlobalBorderColor (UIColor color);

		[Static]
		[Export ("getGlobalBorderColor")]
		UIColor GetGlobalBorderColor { get; }

		[Export ("borderColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor BorderColor { get; set; }

		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		float CornerRadius { get; set; }

		[Export ("strokeWidth", ArgumentSemantic.Assign)]
		float StrokeWidth { get; set; }

		[Export ("hideSmallLinks", ArgumentSemantic.Assign)]
		bool HideSmallLinks { [Bind ("shouldHideSmallLinks")] get; set; }

		[Export ("overspan", ArgumentSemantic.Assign)]
		SizeF Overspan { get; set; }

		[Export ("allowToDisableRoundedCorners", ArgumentSemantic.Assign)]
		bool AllowToDisableRoundedCorners { get; set; }

		[Export ("disableRoundedCorners", ArgumentSemantic.Assign)]
		bool DisableRoundedCorners { get; set; }
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFHighlightAnnotationView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF frame);

		[Export ("button")]
		UIButton Button { get; }

		[Export ("touchDown")]
		void TouchDown ();

		[Export ("touchUp")]
		void TouchUp ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGenericAnnotationView : IPSPDFAnnotationViewProtocol
	{
		[Export ("annotation", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAnnotation Annotation { get; set; }

		// PSPDFGenericAnnotationView (SubclassingHooks) Category

		[Export ("annotationsChanged:")]
		void AnnotationsChanged (NSNotification notification);
	}

	[BaseType (typeof (PSPDFGenericAnnotationView))]
	interface PSPDFHostingAnnotationView : IPSPDFRenderDelegate
	{
		[Export ("annotationImageView", ArgumentSemantic.Retain)]
		UIImageView AnnotationImageView { get; }
	}

	[BaseType (typeof (PSPDFHostingAnnotationView))]
	interface PSPDFFreeTextAnnotationView
	{
		[Export ("beginEditing")]
		void BeginEditing ();

		[Export ("endEditing")]
		void EndEditing ();

		[Export ("textView", ArgumentSemantic.Retain)]
		UITextView TextView { get; }

		[Export ("resizableView", ArgumentSemantic.Assign)]
		PSPDFResizableView ResizableView { get; set; }

		// PSPDFFreeTextAnnotationView (SubclassingHooks) Category

		[Export ("textViewForEditing")]
		UITextView TextViewForEditing ();
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFVideoAnnotationView
	{
		[Export ("URL", ArgumentSemantic.Retain)] [NullAllowed]
		NSUrl Url { get; set; }

		[Export ("autoplayEnabled", ArgumentSemantic.Assign)]
		bool AutoplayEnabled { [Bind ("isAutoplayEnabled")] get; set; }

		[Export ("player", ArgumentSemantic.Retain)]
		MPMoviePlayerController Player { get; }

		[Export ("coverView", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFVideoAnnotationCoverView CoverView { get; set; }

		[Export ("zIndex", ArgumentSemantic.Assign)] [New]
		uint ZIndex { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFVideoAnnotationCoverView
	{
		[Export ("coverImage", ArgumentSemantic.Retain)] [NullAllowed]
		UIImageView CoverImage { get; set; }

		[Export ("playButton", ArgumentSemantic.Retain)] [NullAllowed]
		UIButton PlayButton { get; set; }

		// PSPDFVideoAnnotationView (SubclassingHooks) Category

		[Export ("coverURL")]
		NSUrl CoverURL ();

		[Export ("addCoverView")]
		void AddCoverView ();
	}

	[BaseType (typeof (PSPDFGenericAnnotationView))]
	interface PSPDFNoteAnnotationView
	{
		[Export ("initWithAnnotation:")]
		IntPtr Constructor (PSPDFAnnotation noteAnnotation);

		[Export ("annotationImageView", ArgumentSemantic.Retain)] [NullAllowed]
		UIImageView AnnotationImageView { get; set; }

		// PSPDFNoteAnnotationView (SubclassingHooks) Category

		[Export ("renderNoteImage")]
		UIImage RenderNoteImage ();
	}

	delegate void PSPDFWebViewControllerDelegateHandleExternalUrlHandler (PSPDFAlertView alert, uint buttonIndex);

	interface IPSPDFWebViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFWebViewControllerDelegate
	{
		[Abstract]
		[Export ("masterViewController")]
		UIViewController MasterViewController ();

		[Export ("handleExternalURL:buttonCompletionBlock:")]
		bool HandleExternalUrl (NSUrl url, PSPDFWebViewControllerDelegateHandleExternalUrlHandler completionHandler);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFWebViewController : IPSPDFStyleable
	{
		[Static]
		[Export ("modalWebViewWithURL:")]
		UINavigationController ModalWebViewWithUrl (NSUrl url);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithURLRequest:")]
		IntPtr Constructor (NSUrlRequest urlRequest);

		[Export ("availableActions", ArgumentSemantic.Assign)]
		PSPDFWebViewControllerAvailableActions AvailableActions { get; set; }

		[Export ("webView", ArgumentSemantic.Retain)]
		UIWebView WebView { get; }

		[Export ("popoverController", ArgumentSemantic.Retain)] [NullAllowed]
		UIPopoverController PopoverController { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFWebViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("updateGlobalActivityIndicator", ArgumentSemantic.Assign)]
		bool UpdateGlobalActivityIndicator { get; set; }

		[Export ("useCustomErrorPage", ArgumentSemantic.Assign)]
		bool UseCustomErrorPage { get; set; }

		[Export ("useActivitySheetIfAvailable", ArgumentSemantic.Assign)]
		bool UseActivitySheetIfAvailable { get; set; }

		// PSPDFWebViewController_SubclassingHooks

		[Export ("setActivityIndicatorEnabled:")]
		void SetActivityIndicatorEnabled (bool enabled);

		[Export ("showHTMLWithError:")]
		void ShowHTMLWithError (NSError error);

		[Export ("createDefaultActivityViewController")]
		UIActivityViewController CreateDefaultActivityViewController ();

		[Export ("goBack:")]
		void GoBack (NSObject sender);

		[Export ("goForward:")]
		void GoForward (NSObject sender);

		[Export ("reload:")]
		void Reload (NSObject sender);

		[Export ("stop:")]
		void Stop (NSObject sender);

		[Export ("action:")]
		void Action (NSObject sender);

		[Export ("doneButtonClicked:")]
		void DoneButtonClicked (NSObject sender);
	}

	interface IPSPDFSelectionViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFSelectionViewDelegate
	{
		[Export ("selectionView:shouldStartSelectionAtPoint:")]
		bool ShouldStartSelectionAtPoint (PSPDFSelectionView selectionView, PointF point);

		[Export ("selectionView:updateSelectedRect:")]
		void UpdateSelectedRect (PSPDFSelectionView selectionView, RectangleF rect);

		[Export ("selectionView:finishedWithSelectedRect:")]
		void FinishedWithSelectedRect (PSPDFSelectionView selectionView, RectangleF rect);

		[Export ("selectionView:cancelledWithSelectedRect:")]
		void CancelledWithSelectedRect (PSPDFSelectionView selectionView, RectangleF rect);

		[Export ("selectionView:singleTappedWithGestureRecognizer:")]
		void SingleTappedWithGestureRecognizer (PSPDFSelectionView selectionView, UITapGestureRecognizer gestureRecognizer);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFSelectionView
	{
		[Export ("initWithFrame:delegate:")]
		IntPtr Constructor (RectangleF frame, PSPDFSelectionViewDelegate viewDelegate);

		[Wrap ("WeakDelegate")]
		PSPDFSelectionViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("selectionColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("wordSelectionColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor WordSelectionColor { get; set; }

		[Export ("rects", ArgumentSemantic.Retain)] [NullAllowed]
		NSValue [] Rects { get; set; }
	}

	interface IPSPDFDrawViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFDrawViewDelegate
	{
		[Export ("drawViewDidBeginDrawing:")]
		void DidBeginDrawing (PSPDFDrawView drawView);

		[Export ("drawView:didChange:isNew:")]
		void DidChange (PSPDFDrawView drawView, PSPDFDrawAction drawAction, bool isNew);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFDrawView
	{
		[Export ("strokeColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor StrokeColor { get; set; }

		[Export ("fillColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor FillColor { get; set; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		float LineWidth { get; set; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("strokePath", ArgumentSemantic.Retain)]
		UIBezierPath StrokePath { get; }

		[Export ("actionList", ArgumentSemantic.Retain)]
		PSPDFDrawAction [] ActionList { get; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		float ZoomScale { get; set; }

		[Export ("scale", ArgumentSemantic.Assign)]
		float Scale { get; set; }

		[Export ("annotationType", ArgumentSemantic.Assign)]
		PSPDFAnnotationType AnnotationType { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFDrawViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("canUndo")]
		bool CanUndo { get; }

		[Export ("undo")]
		bool Undo ();

		[Export ("canRedo")]
		bool CanRedo { get; }

		[Export ("redo")]
		bool Redo ();

		[Export ("clearAllActions")]
		void ClearAllActions ();

		// PSPDFDrawView (SubclassingHooks) Category

		[Export ("strokeLayer", ArgumentSemantic.Retain)]
		CAShapeLayer StrokeLayer { get; }

		[Export ("fillLayer", ArgumentSemantic.Retain)]
		CAShapeLayer FillLayer { get; }

		[Export ("previewLayer", ArgumentSemantic.Retain)]
		CALayer PreviewLayer { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDrawAction
	{
		[Export ("points", ArgumentSemantic.Copy)]
		NSValue [] Points { get; }

		[Export ("strokeColor", ArgumentSemantic.Retain)]
		UIColor StrokeColor { get; }

		[Export ("fillColor", ArgumentSemantic.Retain)]
		UIColor FillColor { get; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		float LineWidth { get; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; }
	}

	interface IPSPDFAnnotationToolbarDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationToolbarDelegate
	{
		[Export ("annotationToolbarWillShow:")]
		void WillShow (PSPDFAnnotationToolbar annotationToolbar);

		[Export ("annotationToolbarDidShow:")]
		void DidShow (PSPDFAnnotationToolbar annotationToolbar);

		[Export ("annotationToolbarWillHide:")]
		void WillHide (PSPDFAnnotationToolbar annotationToolbar);

		[Export ("annotationToolbarDidHide:")]
		void DidHide (PSPDFAnnotationToolbar annotationToolbar);

		[Export ("annotationToolbar:didChangeMode:")]
		void DidChangeMode (PSPDFAnnotationToolbar annotationToolbar, string newMode);
	}

	delegate void PSPDFAnnotationToolbarCompletionHandler (bool finished);

	[BaseType (typeof (UIToolbar))]
	interface PSPDFAnnotationToolbar : IPSPDFDrawViewDelegate, IPSPDFSelectionViewDelegate
	{
		[Notification]
		[Field ("PSPDFAnnotationToolbarWillHideNotification", "__Internal")]
		NSString AnnotationToolbarWillHideNotification { get; }

		[Field ("PSPDFAnnotationStringEraser", "__Internal")]
		NSString AnnotationStringEraser { get; }

		[Field ("PSPDFAnnotationStringSelectionTool", "__Internal")]
		NSString AnnotationStringSelectionTool { get; }

		[Field ("PSPDFAnnotationStringSavedAnnotations", "__Internal")]
		NSString AnnotationStringSavedAnnotations { get; }

		[Export ("initWithPDFController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("showToolbarInRect:animated:")]
		void ShowToolbarInRect (RectangleF rect, bool animated);

		[Export ("hideToolbarAnimated:completion:")]
		void HideToolbarAnimated (bool animated, PSPDFAnnotationToolbarCompletionHandler completionHandler);

		[Export ("flashToolbar")]
		void FlashToolbar ();

		[Wrap ("WeakDelegate")][New]
		PSPDFAnnotationToolbarDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed][New]
		NSObject WeakDelegate { get; set; }

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; }

		[Export ("toolbarMode", ArgumentSemantic.Copy)]
		string ToolbarMode { get; set; }

		[Export ("drawColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor DrawColor { get; set; }

		[Export ("fillColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor FillColor { get; set; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		float LineWidth { get; set; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("fontName", ArgumentSemantic.Copy)]
		string FontName { get; set; }

		[Export ("fontSize", ArgumentSemantic.Assign)]
		float FontSize { get; set; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[Export ("hideAfterDrawingDidFinish", ArgumentSemantic.Assign)]
		bool HideAfterDrawingDidFinish { get; set; }

		[Export ("saveAfterToolbarHiding", ArgumentSemantic.Assign)]
		bool SaveAfterToolbarHiding { get; set; }

		[Export ("fadeToolbar", ArgumentSemantic.Assign)]
		bool FadeToolbar { get; set; }

		[Export ("slideToolbar", ArgumentSemantic.Assign)]
		bool SlideToolbar { get; set; }

		[Export ("combineInk", ArgumentSemantic.Assign)]
		bool CombineInk { get; set; }

		[Field ("PSPDFAnnotationGroupKeyChoice", "__Internal")]
		NSString AnnotationGroupKeyChoice { get; }

		[Field ("PSPDFAnnotationGroupKeyGroup", "__Internal")]
		NSString AnnotationGroupKeyGroup { get; }

		[Export ("annotationGroups", ArgumentSemantic.Copy)] [NullAllowed]
		NSObject [] AnnotationGroups { get; set; }

		[Export ("additionalBarButtonItems", ArgumentSemantic.Copy)] [NullAllowed]
		UIBarButtonItem [] AdditionalBarButtonItems { get; set; }

		// PSPDFAnnotationToolbar (SubclassingHooks) Category

		[Export ("showDrawingToolbarWithMode:")]
		void ShowDrawingToolbarWithMode (string mode);

		[Export ("textButtonPressed:")]
		void TextButtonPressed (NSObject sender);

		[Export ("highlightButtonPressed:")]
		void HighlightButtonPressed (NSObject sender);

		[Export ("strikeoutButtonPressed:")]
		void StrikeoutButtonPressed (NSObject sender);

		[Export ("underlineButtonPressed:")]
		void UnderlineButtonPressed (NSObject sender);

		[Export ("squigglyButtonPressed:")]
		void SquigglyButtonPressed (NSObject sender);

		[Export ("inkButtonPressed:")]
		void InkButtonPressed (NSObject sender);

		[Export ("squareButtonPressed:")]
		void SquareButtonPressed (NSObject sender);

		[Export ("circleButtonPressed:")]
		void CircleButtonPressed (NSObject sender);

		[Export ("lineButtonPressed:")]
		void LineButtonPressed (NSObject sender);

		[Export ("polygonButtonPressed:")]
		void PolygonButtonPressed (NSObject sender);

		[Export ("polylineButtonPressed:")]
		void PolylineButtonPressed (NSObject sender);

		[Export ("freetextButtonPressed:")]
		void FreetextButtonPressed (NSObject sender);

		[Export ("signatureButtonPressed:")]
		void SignatureButtonPressed (NSObject sender);

		[Export ("stampButtonPressed:")]
		void StampButtonPressed (NSObject sender);

		[Export ("imageButtonPressed:")]
		void ImageButtonPressed (NSObject sender);

		[Export ("soundButtonPressed:")]
		void SoundButtonPressed (NSObject sender);

		[Export ("eraserButtonPressed:")]
		void EraserButtonPressed (NSObject sender);

		[Export ("selectiontoolButtonPressed:")]
		void SelectiontoolButtonPressed (NSObject sender);

		[Export ("showStylePicker:")]
		void ShowStylePicker (NSObject sender);

		[Export ("doneButtonPressed:")]
		void DoneButtonPressed (NSObject sender);

		[Export ("cancelDrawingAnimated:")]
		void CancelDrawingAnimated (bool animated);

		[Export ("doneDrawingAnimated:")]
		void DoneDrawingAnimated (bool animated);

		[Export ("selectStrokeColor:")]
		void SelectStrokeColor (NSObject sender);

		[Export ("undoDrawing:")]
		void UndoDrawing (NSObject sender);

		[Export ("redoDrawing:")]
		void RedoDrawing (NSObject sender);

		[Export ("updateDrawingUndoRedoButtons")]
		void UpdateDrawingUndoRedoButtons ();

		[Export ("canUndoDrawing")]
		bool CanUndoDrawing ();

		[Export ("canRedoDrawing")]
		bool CanRedoDrawing ();

		[Export ("updateToolbarButtonsAnimated:")]
		void UpdateToolbarButtonsAnimated (bool animated);

		[Export ("allowedButtonCount")]
		uint AllowedButtonCount ();

		[Export ("hideAndRemoveToolbar")]
		void HideAndRemoveToolbar ();

		[Export ("hideAndRemoveToolbarAnimated:completion:")]
		void HideAndRemoveToolbarAnimated (bool animated, [NullAllowed] PSPDFAnnotationToolbarCompletionHandler completionHandler);

		[Export ("setLastUsedColor:forAnnotationType:")]
		void SetLastUsedColor (UIColor lastUsedDrawColor, string annotationType);

		[Export ("lastUsedColorForAnnotationTypeString:")]
		UIColor LastUsedColorForAnnotationTypeString (string annotationType);

		[Export ("finishDrawingAnimated:saveAnnotation:")]
		void FinishDrawingAnimated (bool animated, bool saveAnnotation);

		[Export ("annotationsWithActionList:bounds:page:")]
		PSPDFAnnotation [] AnnotationsWithActionList (PSPDFAction [] actionList, RectangleF bounds, uint page);

		[Export ("drawViews", ArgumentSemantic.Retain)]
		NSDictionary DrawViews { get; }

		// PSPDFAnnotationToolbar (Advanced) Category

		[Since (7,0)]
		[Export ("barPosition", ArgumentSemantic.Assign)]
		UIBarPosition BarPosition { get; set; }

		[Export ("editableAnnotationTypes", ArgumentSemantic.Copy)] [NullAllowed]
		NSObject /* HACK: NSOrderedSet */ EditableAnnotationTypes { get; set; }

		[Export ("undoButtonItem", ArgumentSemantic.Retain)]
		UIBarButtonItem UndoButtonItem { get; }

		[Export ("redoButtonItem", ArgumentSemantic.Retain)]
		UIBarButtonItem RedoButtonItem { get; }
	}

	interface IPSPDFSignatureViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFSignatureViewControllerDelegate : PSPDFOverridable
	{
		[Export ("signatureViewControllerDidCancel:"), EventArgs ("PSPDFSignatureViewControllerDelegate")]
		void DidCancel (PSPDFSignatureViewController signatureController);

		[Export ("signatureViewControllerDidSave:"), EventArgs ("PSPDFSignatureViewControllerDelegate")]
		void DidSave (PSPDFSignatureViewController signatureController);
	}

	[BaseType (typeof (PSPDFBaseViewController),
	Delegates = new string [] {"WeakDelegate"},
	Events = new Type [] { typeof (PSPDFSignatureViewControllerDelegate) })]
	interface PSPDFSignatureViewController : IPSPDFStyleable
	{
		[Export ("lines", ArgumentSemantic.Retain)]
		NSObject [] Lines { get; }

		[Wrap ("WeakDelegate")]
		PSPDFSignatureViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("drawView", ArgumentSemantic.Retain)]
		PSPDFDrawView DrawView { get; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; set; }

		// PSPDFSignatureViewController (SubclassingHooks) Category

		[Export ("cancel:")]
		void Cancel (NSObject sender);

		[Export ("done:")]
		void Done (NSObject sender);
	}

	[BaseType (typeof (PSPDFAnnotationGridViewController))]
	interface PSPDFStampViewController
	{
		[Static]
		[Export ("defaultStampAnnotations")]
		PSPDFStampAnnotation [] DefaultStampAnnotations { get; set; }

		[Export ("initWithDelegate:")]
		IntPtr Constructor (PSPDFAnnotationGridViewControllerDelegate controllerDelegate);

		[Export ("stamps", ArgumentSemantic.Copy)]
		NSObject [] Stamps { get; set; }

		[Export ("customStampEnabled", ArgumentSemantic.Assign)]
		bool CustomStampEnabled { get; set; }
	}

	interface IPSPDFFontSelectorViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFFontSelectorViewControllerDelegate : PSPDFOverridable
	{
		[Abstract]
		[Export ("fontSelectorViewController:didSelectFont:"), EventArgs ("PSPDFFontSelectorViewControllerDelegate")]
		void DidSelectFont (PSPDFFontSelectorViewController fontSelectorViewController, UIFont selectedFont);
	}

	[BaseType (typeof (PSPDFBaseTableViewController),
	Delegates = new string [] {"WeakDelegate"},
	Events = new Type [] { typeof (PSPDFFontSelectorViewControllerDelegate) })]
	interface PSPDFFontSelectorViewController
	{
		[Static]
		[Export ("defaultBlocklist")] [NullAllowed]
		string [] DefaultBlocklist { get; set; }

		[Export ("fontFamilyDescriptors", ArgumentSemantic.Copy)]
		UIFontDescriptor [] FontFamilyDescriptors { get; set; }

		[Export ("selectedFont", ArgumentSemantic.Retain)] [NullAllowed]
		UIFont SelectedFont { get; set; }

		[Export ("searchEnabled", ArgumentSemantic.Assign)]
		bool SearchEnabled { get; set; }

		[Export ("showDownloadableFonts", ArgumentSemantic.Assign)]
		bool ShowDownloadableFonts { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFFontSelectorViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }
	}

	interface IPSPDFColorSelectionViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFColorSelectionViewControllerDelegate
	{
		[Abstract]
		[Export ("colorSelectionControllerSelectedColor:context:")]
		UIColor SelectedColor (UIViewController controller, NSObject context);

		[Abstract]
		[Export ("colorSelectionController:didSelectColor:finishedSelection:context:")]
		void DidSelectFont (UIViewController controller, UIColor color, bool finishedSelection, NSObject context);
	}

	[BaseType (typeof (UIViewController))]
	interface PSPDFColorSelectionViewController
	{
		[Static]
		[Export ("defaultColorPickerWithTitle:wantTransparency:delegate:context:")]
		PSPDFSimplePageViewController DefaultColorPickerWithTitle (string title, bool wantTransparency, IPSPDFColorSelectionViewControllerDelegate controlleDelegate, NSObject context);

		[Static]
		[Export ("setDefaultColorPickerStyles:")]
		void SetDefaultColorPickerStyles (NSNumber [] colorPickerStyles);

		[Static]
		[Export ("monoChromeSelectionViewController")]
		PSPDFColorSelectionViewController GetMonoChromeSelectionViewController { get; }

		[Static]
		[Export ("modernColorsSelectionViewController")]
		PSPDFColorSelectionViewController GetModernColorsSelectionViewController { get; }

		[Static]
		[Export ("vintageColorsSelectionViewController")]
		PSPDFColorSelectionViewController GetVintageColorsSelectionViewController { get; }

		[Static]
		[Export ("rainbowSelectionViewController")]
		PSPDFColorSelectionViewController GetRainbowSelectionViewController { get; }

		[Static]
		[Export ("colorSelectionViewControllerFromColors:addDarkenedVariants:")]
		PSPDFColorSelectionViewController GetColorSelectionViewControllerFromColors (UIColor [] colorsArray, bool darkenedVariants);

		[Static]
		[Export ("colorsFromPaletteURL:addDarkenedVariants:")]
		UIColor [] ColorsFromPaletteURL (NSUrl paletteURL, bool darkenedVariants);

		[Export ("initWithColors:")]
		IntPtr Constructor (UIColor [] colors);

		[Export ("colors", ArgumentSemantic.Copy)]
		UIColor [] Colors { get; }

		[Wrap ("WeakDelegate")]
		PSPDFColorSelectionViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		// PSPDFColorSelectionViewController (SubclassingHooks) Category

		[Static]
		[Export ("colorPickerForType:")]
		UIViewController ColorPickerForType (PSPDFColorPickerStyle type);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFLineEndSelectionViewController
	{
		[Export ("initWithTitle:annotation:isStart:delegate:context:")]
		IntPtr Constructor (string title, PSPDFAnnotation annotation, bool isStart, PSPDFLineEndSelectionViewControllerDelegate controllerDelegate, NSObject context);

		[Wrap ("WeakDelegate")]
		PSPDFLineEndSelectionViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }
	}

	interface IPSPDFLineEndSelectionViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFLineEndSelectionViewControllerDelegate
	{
		[Abstract]
		[Export ("lineEndSelectionControllerSelectedLineEnd:isStart:context:")]
		PSPDFLineEndType SelectedLineEnd (UIViewController controller, bool isStart, NSObject context);

		[Abstract]
		[Export ("lineEndSelectionController:didSelectLineEnd:isStart:context:")]
		void DidSelectLineEnd (UIViewController controller, PSPDFLineEndType lineEnd, bool isStart, NSObject context);
	}

	interface IPSPDFNoteAnnotationViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFNoteAnnotationViewControllerDelegate : PSPDFOverridable
	{
		[Export ("noteAnnotationController:didDeleteAnnotation:")]
		void DidDeleteAnnotation (PSPDFNoteAnnotationViewController noteAnnotationController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didClearContentsForAnnotation:")]
		void DidClearContents (PSPDFNoteAnnotationViewController noteAnnotationController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didChangeAnnotation:")]
		void DidChangeAnnotation (PSPDFNoteAnnotationViewController noteAnnotationController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:willDismissWithAnnotation:")]
		void WillDismissWithAnnotation (PSPDFNoteAnnotationViewController noteAnnotationController, PSPDFAnnotation annotation);
	}

	delegate void PSPDFNoteAnnotationViewControllerCustomizationHandler (PSPDFNoteAnnotationViewController controller);

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFNoteAnnotationViewController : IPSPDFStyleable
	{
		[Export ("initWithAnnotation:editable:")]
		IntPtr Constructor (PSPDFAnnotation annotation, bool allowEditing);

		[Export ("initWithAnnotation:")]
		IntPtr Constructor (PSPDFAnnotation annotation);

		[Export ("annotation", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAnnotation Annotation { get; set; }

		[Export ("allowEditing", ArgumentSemantic.Assign)]
		bool AllowEditing { get; set; }

		[Export ("showColorAndIconOptions", ArgumentSemantic.Assign)]
		bool ShowColorAndIconOptions { get; set; }

		[Export ("showCopyButton", ArgumentSemantic.Assign)]
		bool ShowCopyButton { get; set; }

		[Export ("textView", ArgumentSemantic.Retain)] [NullAllowed]
		UITextView TextView { get; }

		[Static]
		[Export ("setTextViewCustomizationBlock:")]
		void SetTextViewCustomizationBlock ([NullAllowed] PSPDFNoteAnnotationViewControllerCustomizationHandler handler);

		[Wrap ("WeakDelegate")]
		PSPDFNoteAnnotationViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		// PSPDFNoteAnnotationViewController (SubclassingHooks) Category

		[Export ("deleteAnnotation:")]
		void DeleteAnnotation (UIBarButtonItem barButtonItem);

		[Export ("deleteAnnotationActionTitle")]
		string DeleteAnnotationActionTitle ();

		[Export ("backgroundView", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFGradientView BackgroundView { get; set; }

		[Export ("optionsView", ArgumentSemantic.Retain)] [NullAllowed]
		UIView OptionsView { get; set; }

		[Export ("tapGesture", ArgumentSemantic.Retain)] [NullAllowed]
		UITapGestureRecognizer TapGesture { get; set; }
	}

	interface IPSPDFAnnotationTableViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationTableViewControllerDelegate : PSPDFOverridable
	{
		[Abstract]
		[Export ("annotationTableViewController:didSelectAnnotation:")]
		void DidSelectAnnotation (PSPDFAnnotationTableViewController annotationController, PSPDFAnnotation annotation);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFAnnotationTableViewController : IPSPDFStyleable
	{
		[Export ("initWithDocument:delegate:")]
		IntPtr Constructor (PSPDFDocument document, [NullAllowed] PSPDFAnnotationTableViewControllerDelegate controllerDelegate);

		[Export ("document", ArgumentSemantic.Assign)]
		PSPDFDocument Document { get; set; }

		[Export ("visibleAnnotationTypes", ArgumentSemantic.Copy)] [NullAllowed]
		NSOrderedSet VisibleAnnotationTypes { get; set; }

		[Export ("allowCopy", ArgumentSemantic.Assign)]
		bool AllowCopy { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFAnnotationTableViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		// PSPDFAnnotationTableViewController (SubclassingHooks) Category

		[Export ("annotationsForPage:")]
		PSPDFAnnotation [] AnnotationsForPage (uint page);

		[Export ("targetTableViewStyle")]
		UITableViewStyle TargetTableViewStyle ();
	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFAnnotationCell
	{
		[Export ("annotation", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAnnotation Annotation { get; set; }
	}

	interface IPSPDFAnnotationSetStore { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationSetStore
	{
		[Abstract]
		[Export ("annotationSets", ArgumentSemantic.Copy)]
		PSPDFAnnotationSet [] AnnotationSets { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFKeychainAnnotationSetsStore : IPSPDFAnnotationSetStore
	{

	}

	[BaseType (typeof (PSPDFAnnotationGridViewController))]
	interface PSPDFSavedAnnotationsViewController
	{
		[Static]
		[Export ("sharedAnntationStore")]
		IPSPDFAnnotationSetStore SharedAnntationStore { get; }

		[Export ("initWithDelegate:")]
		IntPtr Constructor (PSPDFAnnotationGridViewControllerDelegate controllerDelegate);

		[Export ("annotationStore", ArgumentSemantic.Retain)] [NullAllowed]
		IPSPDFAnnotationSetStore AnnotationStore { get; set; }

		// PSPDFSavedAnnotationsViewController (SubclassingHooks) Category

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbarAnimated (bool animated);
	}

	interface IPSPDFContainerViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFContainerViewControllerDelegate
	{
		[Abstract]
		[Export ("containerViewController:didUpdateSelectedIndex:")]
		void DidUpdateSelectedIndex (PSPDFContainerViewController controller, uint selectedIndex);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFContainerViewController : IPSPDFStyleable
	{
		[Wrap ("WeakDelegate")]
		PSPDFContainerViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("addViewController:withTitle:")]
		void AddViewController (UIViewController controller, string title);

		[Export ("addViewController:")]
		void AddViewController (UIViewController controller);

		[Export ("removeViewController:")]
		void RemoveViewController (UIViewController controller);

		[Export ("viewControllers")]
		UIViewController [] ViewControllers { get; }

		[Export ("visibleViewControllerIndex", ArgumentSemantic.Assign)]
		uint VisibleViewControllerIndex { get; set; }

		[Export ("setVisibleViewControllerIndex:animated:")]
		void SetVisibleViewControllerIndex (uint visibleViewControllerIndex, bool animated);

		// PSPDFContainerViewController (SubclassingHooks) Category

		[Export ("filterSegment", ArgumentSemantic.Retain)]
		UISegmentedControl FilterSegment { get; }
	}

	interface IPSPDFTabbedViewControllerDelegate { }

	[BaseType (typeof (PSPDFMultiDocumentViewControllerDelegate))]
	[Model]
	[Protocol]
	interface PSPDFTabbedViewControllerDelegate
	{
		[Export ("tabbedPDFController:shouldChangeDocuments:")]
		bool ShouldChangeDocuments (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument [] newDocuments);

		[Export ("tabbedPDFController:didChangeDocuments:")]
		void DidChangeDocuments (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument [] oldDocuments);

		[Export ("tabbedPDFController:shouldChangeVisibleDocument:")]
		bool ShouldChangeVisibleDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument newDocument);

		[Export ("tabbedPDFController:didChangeVisibleDocument:")]
		void DidChangeVisibleDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument oldDocument);

		[Export ("tabbedPDFController:shouldCloseDocument:")]
		bool ShouldCloseDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument closedDocument);

		[Export ("tabbedPDFController:didCloseDocument:")]
		void DidCloseDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument closedDocument);
	}

	[BaseType (typeof (PSPDFMultiDocumentViewController))]
	interface PSPDFTabbedViewController
	{
		[Export ("initWithPDFViewController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("addDocuments:atIndex:animated:")]
		void AddDocuments (PSPDFDocument [] documents, uint index, bool animated);

		[Export ("removeDocuments:animated:")]
		void AddDocuments (PSPDFDocument [] documents, bool animated);

		[Wrap ("WeakDelegate")][New]
		PSPDFTabbedViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed][New]
		NSObject WeakDelegate { get; set; }

		[Export ("statePersistenceKey", ArgumentSemantic.Copy)][New]
		string StatePersistenceKey { get; set; }

		[Export ("minTabWidth", ArgumentSemantic.Assign)]
		float MinTabWidth { get; set; }

		[Export ("openDocumentActionInNewTab", ArgumentSemantic.Assign)]
		bool OpenDocumentActionInNewTab { get; set; }

		[Export ("tabBar", ArgumentSemantic.Retain)]
		PSPDFTabBarView TabBar { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFTabBarView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("selectTabAtIndex:animated:")]
		void SelectTabAtIndex (uint index, bool animated);

		[Export ("scrollToTabAtIndex:animated:")]
		void ScrollToTabAtIndex (uint index, bool animated);

		[Export ("selectedTabIndex")]
		uint SelectedTabIndex { get; }

		[Export ("minTabWidth", ArgumentSemantic.Assign)]
		float MinTabWidth { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFTabBarViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakDataSource")]
		PSPDFTabBarViewDataSource DataSource { get; set; }

		[Export ("dataSource", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDataSource { get; set; }

		// PSPDFTabBarView (SubclassingHooks) Category

		[Export ("selectTabAtIndex:animated:callDelegate:")]
		void SelectTabAtIndex (uint index, bool animated, bool callDelegate);

		[Export ("scrollView", ArgumentSemantic.Retain)]
		UIScrollView ScrollView { get; }
	}

	interface IPSPDFTabBarViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFTabBarViewDelegate
	{
		[Abstract]
		[Export ("tabBarView:didSelectTabAtIndex:")]
		void DidSelectTabAtIndex (PSPDFTabBarView tabBarView, uint index);

		[Abstract]
		[Export ("tabBarView:didSelectCloseButtonOfTabAtIndex:")]
		void DidSelectCloseButtonOfTabAtIndex (PSPDFTabBarView tabBarView, uint index);
	}

	interface IPSPDFTabBarViewDataSource { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFTabBarViewDataSource
	{
		[Abstract]
		[Export ("numberOfTabsInTabBarView:")]
		int NumberOfTabsInTabBarView (PSPDFTabBarView tabBarView);

		[Abstract]
		[Export ("tabBarView:titleForTabAtIndex:")]
		string TitleForTabAtIndex (PSPDFTabBarView tabBarView, uint index);
	}

	[BaseType (typeof (UIButton))]
	interface PSPDFTabBarCloseButton
	{

	}

	[BaseType (typeof (UIButton))]
	interface PSPDFTabBarButton
	{
		[Export ("selected", ArgumentSemantic.Assign)][New]
		bool Selected { [Bind ("isSelected")] get; set; }

		[Export ("setSelected:animated:")]
		void SetSelected (bool selected, bool animated);

		[Export ("closeButton", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFTabBarCloseButton CloseButton { get; set; }

		[Export ("showCloseButton", ArgumentSemantic.Assign)]
		bool ShowCloseButton { get; set; }

		[Export ("minTabWidth", ArgumentSemantic.Assign)]
		float MinTabWidth { get; set; }

		[Export ("maxTabWidth", ArgumentSemantic.Assign)]
		float MaxTabWidth { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFIconGenerator
	{
		[Static]
		[Export ("sharedGenerator")]
		PSPDFIconGenerator SharedGenerator { get; }

		[Export ("iconForType:")]
		UIImage IconForType (PSPDFIconType iconType);

		[Export ("iconForType:barButtonStyle:")]
		UIImage IconForType (PSPDFIconType iconType, UIBarButtonItemStyle style);

		[Export ("iconForType:shadowOffset:shadowColor:")]
		UIImage IconForType (PSPDFIconType iconType, SizeF shadowOffset, UIColor shadowColor);
	}

	[BaseType (typeof (UIBarButtonItem))]
	interface PSPDFBarButtonItem
	{
		[Static]
		[Export ("dismissPopoverAnimated:completion:")]
		bool DismissPopoverAnimated (bool animated, NSAction completion);

		[Static]
		[Export ("isPopoverVisible")]
		bool IsPopoverVisible { get; }

		[Export ("initWithPDFViewController:")]
		IntPtr Constructor (PSPDFViewController pdfViewController);

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; }

		[Export ("customView")] [New]
		UIView CustomView { get; }

		[Export ("image")] [New]
		UIImage Image { get; }

		[Export ("systemItem")]
		UIBarButtonSystemItem SystemItem ();

		[Export ("landscapeImagePhone")] [New]
		UIImage LandscapeImagePhone { get; }

		[Export ("actionName")]
		string ActionName ();

		[Export ("itemStyle")]
		UIBarButtonItemStyle ItemStyle ();

		[Export ("isAvailable")]
		bool IsAvailable ();

		[Export ("isLongPressActionAvailable")]
		bool IsLongPressActionAvailable ();

		[Export ("updateBarButtonItem")]
		void UpdateBarButtonItem ();

		[Export ("isLongPressActionActive")]
		bool IsLongPressActionActive ();

		[Export ("presentAnimated:sender:")]
		NSObject PresentAnimated (bool animated, NSObject sender);

		[Export ("dismissAnimated:completion:")]
		bool DismissAnimated (bool animated, NSAction completion);

		[Export ("didDismiss")]
		void DidDismiss ();

		[Export ("setPresentedObject:sender:")]
		void SetPresentedObject (NSObject presentedObject, NSObject sender);

		[Export ("presentModalOrInPopover:sender:")]
		NSObject PresentModalOrInPopover (UIViewController viewController, NSObject sender);

		[Export ("dismissModalOrPopoverAnimated:completion:")]
		bool DismissModalOrPopoverAnimated (bool animated, NSAction completion);

		[Static]
		[Export ("popoverControllerForObject:")]
		UIPopoverController PopoverControllerForObject (NSObject obj);

		[Export ("action:")] [New]
		void Action (NSObject sender);

		[Export ("longPressAction:")]
		void LongPressAction (PSPDFBarButtonItem sender);

		[Export ("actionSheet", ArgumentSemantic.Retain)] [NullAllowed]
		UIActionSheet ActionSheet { get; set; }

		[Export ("dismissingSheet", ArgumentSemantic.Assign)]
		bool DismissingSheet { [Bind ("isDismissingSheet")] get; set; }
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFCloseBarButtonItem
	{

	}

	delegate void PSPDFEmailBarButtonItemMailComposeViewControllerCustomizationHandler (MFMailComposeViewController controller);
	
	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFEmailBarButtonItem : IPSPDFDocumentSharingViewControllerDelegate, IMFMailComposeViewControllerDelegate
	{
		[Export ("sendOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions SendOptions { get; set; }

		[Export ("setMailComposeViewControllerCustomizationBlock:")]
		void SetMailComposeViewControllerCustomizationHandler (PSPDFEmailBarButtonItemMailComposeViewControllerCustomizationHandler handler);

		// PSPDFEmailBarButtonItem (SubclassingHooks) Category

		[Export ("showEmailControllerWithSendOptions:dataArray:fileNames:sender:annotationSummary:animated:")]
		void ShowEmailController (PSPDFDocumentSharingOptions sendOptions, NSObject [] dataArray, string [] fileNames, NSObject sender, string annotationSummary, bool animated);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFOpenInBarButtonItem : IPSPDFDocumentSharingViewControllerDelegate
	{
		[Export ("showPrintAction", ArgumentSemantic.Assign)]
		bool ShowPrintAction { get; set; }

		[Export ("openOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions OpenOptions { get; set; }

		[Export ("documentInteractionController", ArgumentSemantic.Retain)]
		UIDocumentInteractionController DocumentInteractionController { get; }

		// PSPDFOpenInBarButtonItem (SubclassingHooks) Category

		[Export ("fileURLForDocument:")]
		NSUrl FileURLForDocument (PSPDFDocument document);

		[Export ("interactionControllerWithURL:")]
		UIDocumentInteractionController InteractionControllerWithURL (NSUrl fileURL);

		[Export ("showOpenInControllerWithOptions:fileURL:animated:sender:")]
		void ShowOpenInControllerWithOptions (PSPDFDocumentSharingOptions options, NSUrl fileURL, bool animated, NSObject sender);

		[Export ("presentOpenInMenuFromBarButtonItem:animated:")]
		bool PresentOpenInMenuFromBarButtonItem (NSObject sender, bool animated);

		[Export ("presentOpenInMenuFromRect:inView:animated:")]
		bool PresentOpenInMenuFromRect (RectangleF senderRect, NSObject sender, bool animated);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFPrintBarButtonItem : IUIPrintInteractionControllerDelegate
	{
		[Export ("printOptions", ArgumentSemantic.Assign)]
		PSPDFPrintOptions PrintOptions { get; set; }
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFSearchBarButtonItem
	{

	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFMoreBarButtonItem
	{
		[Export ("actionSheet", ArgumentSemantic.Retain)] [NullAllowed] [New]
		UIActionSheet ActionSheet { get; set; }
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFViewModeBarButtonItem
	{
		[Export ("viewModeStyle", ArgumentSemantic.Assign)]
		PSPDFViewModeBarButtonStyle ViewModeStyle { get; set; }

		// PSPDFViewModeBarButtonItem (SubclassingHooks) Category

		[Export ("viewModeSegment", ArgumentSemantic.Retain)]
		PSPDFSegmentedControl ViewModeSegment { get; }
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFAnnotationBarButtonItem : IPSPDFAnnotationToolbarDelegate
	{
		[Export ("isAvailableBlocking")]
		bool IsAvailableBlocking ();

		[Export ("targetToolbarForBarButtonItem:")]
		UIView TargetToolbarForBarButtonItem (UIBarButtonItem barButtonItem);

		[Export ("annotationToolbar", ArgumentSemantic.Retain)]
		PSPDFAnnotationToolbar AnnotationToolbar { get; }
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFBookmarkBarButtonItem
	{
		[Export ("showBookmarkControllerOnLongPress", ArgumentSemantic.Assign)]
		bool ShowBookmarkControllerOnLongPress { get; set; }

		[Export ("tapChangesBookmarkStatus", ArgumentSemantic.Assign)]
		bool TapChangesBookmarkStatus { get; set; }

		// PSPDFBookmarkBarButtonItem (SubclassingHooks) Category

		[Export ("bookmarkNumberForVisiblePages")]
		NSNumber BookmarkNumberForVisiblePages ();
	}

	delegate void PSPDFBrightnessBarButtonItemBrightnessControllerCustomizationHandler (PSPDFBrightnessViewController controller);

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFBrightnessBarButtonItem
	{
		[Export ("setBrightnessControllerCustomizationBlock:")]
		void SetBrightnessControllerCustomizationHandler (PSPDFBrightnessBarButtonItemBrightnessControllerCustomizationHandler handler);
	}

	delegate void PSPDFOutlineBarButtonItemDidCreateControllerHandler (UIViewController controller, PSPDFOutlineBarButtonItemOption option);

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFOutlineBarButtonItem
	{
		[Export ("isAvailableBlocking")]
		bool IsAvailableBlocking ();

		[Export ("availableControllerOptions", ArgumentSemantic.Copy)]
		NSObject /* HACK: NSOrderedSet */ AvailableControllerOptions { get; set; }

		[Export ("setDidCreateControllerBlock:")]
		void SetDidCreateControllerHandler (PSPDFOutlineBarButtonItemDidCreateControllerHandler handler);

		// PSPDFOutlineBarButtonItem (SubclassingHooks) Category

		[Export ("titleForOption:")]
		string TitleForOption (PSPDFOutlineBarButtonItemOption option);

		[Export ("controllerForOption:")]
		UIViewController ControllerForOption (PSPDFOutlineBarButtonItemOption option);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFActivityBarButtonItem
	{
		[Export ("applicationActivities", ArgumentSemantic.Copy)]
		UIActivity [] ApplicationActivities { get; set; }

		[Export ("excludedActivityTypes", ArgumentSemantic.Copy)]
		UIActivity [] ExcludedActivityTypes { get; set; }

		[Export ("activityController", ArgumentSemantic.Retain)] [NullAllowed]
		UIActivityViewController ActivityController { get; }
	}

	delegate void PSPDFAlertViewCancelButtonHandler (PSPDFAlertView alert, int buttonIndex);
	delegate void PSPDFAlertViewAddButtonHandler (PSPDFAlertView alert, int buttonIndex);

	[BaseType (typeof (UIAlertView))]
	interface PSPDFAlertView
	{
		[Export ("initWithTitle:")]
		IntPtr Constructor (string title);

		[Export ("initWithTitle:message:")]
		IntPtr Constructor (string title, string message);

		[Export ("setCancelButtonWithTitle:block:")]
		int SetCancelButton (string title, NSAction cancelAction);

		[Export ("setCancelButtonWithTitle:extendedBlock:")]
		int SetCancelButton (string title, PSPDFAlertViewCancelButtonHandler cancelAction);

		[Export ("addButtonWithTitle:block:")]
		int AddButton (string title, NSAction buttonAction);

		[Export ("addButtonWithTitle:extendedBlock:")]
		int AddButton (string title, PSPDFAlertViewAddButtonHandler buttonAction);
	}

	delegate void PSPDFActionSheetCancelButtonHandler (PSPDFActionSheet sheet, int buttonIndex);
	delegate void PSPDFActionSheetDestructiveButtonHandler (PSPDFActionSheet sheet, int buttonIndex);
	delegate void PSPDFActionSheetAddButtonHandler (PSPDFActionSheet sheet, int buttonIndex);
	delegate void PSPDFActionSheetWillDismissHandler (PSPDFActionSheet sheet, int buttonIndex);
	delegate void PSPDFActionSheetDidDismissHandler (PSPDFActionSheet sheet, int buttonIndex);


	[BaseType (typeof (UIActionSheet))]
	interface PSPDFActionSheet
	{
		[Export ("initWithTitle:")]
		IntPtr Constructor (string title);

		[Export ("setCancelButtonWithTitle:block:")]
		void SetCancelButton (string title, NSAction cancelAction);

		[Export ("setCancelButtonWithTitle:extendedBlock:")]
		void SetCancelButton (string title, PSPDFActionSheetCancelButtonHandler cancelAction);

		[Export ("setDestructiveButtonWithTitle:block:")]
		void SetDestructiveButton (string title, NSAction cancelAction);

		[Export ("setDestructiveButtonWithTitle:extendedBlock:")]
		void SetDestructiveButton (string title, PSPDFActionSheetDestructiveButtonHandler DestructiveAction);

		[Export ("addButtonWithTitle:block:")]
		void AddButton (string title, NSAction cancelAction);

		[Export ("addButtonWithTitle:extendedBlock:")]
		void AddButton (string title, PSPDFActionSheetAddButtonHandler buttonAction);

		[Export ("buttonCount")] [New]
		uint ButtonCount { get; }

		[Export ("showWithSender:fallbackView:animated:")]
		void ShowWithSender ([NullAllowed] NSObject sender, UIView view, bool animated);

		[Export ("destroy")]
		void Destroy ();

		[Export ("setWillDismissBlock:")]
		void SetWillDismissHandler (PSPDFActionSheetWillDismissHandler handler);

		[Export ("setDidDismissBlock:")]
		void SetDidDismissHandler (PSPDFActionSheetDidDismissHandler handler);
	}

	[BaseType (typeof (UIMenuItem))]
	interface PSPDFMenuItem
	{
		[Export ("initWithTitle:block:")]
		IntPtr Constructor (string title, [NullAllowed] NSAction action);

		[Export ("initWithTitle:block:identifier:")]
		IntPtr Constructor (string title, [NullAllowed] NSAction action, string identifier);

		[Export ("initWithTitle:image:block:identifier:")]
		IntPtr Constructor (string title, UIImage image, [NullAllowed] NSAction action, string identifier);

		[Export ("initWithTitle:image:block:identifier:allowImageColors:")]
		IntPtr Constructor (string title, UIImage image, [NullAllowed] NSAction action, string identifier, bool allowImageColors);

		[Export ("enabled", ArgumentSemantic.Assign)]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		[Export ("identifier", ArgumentSemantic.Copy)]
		string Identifier { get; set; }

		[Export ("title", ArgumentSemantic.Copy)] [New]
		string Title { get; set; }

		[Export ("ps_image", ArgumentSemantic.Copy)] [NullAllowed]
		UIImage PSImage { get; set; }

		[Export ("setActionBlock:")]
		void SetActionHandler (NSAction action);

		[Static]
		[Export ("installMenuHandlerForObject:")]
		void InstallMenuHandlerForObject (NSObject obj);
	}

	delegate void PSPDFProcessorProgressHandler (uint currentPage, uint numberOfProcessedPages, uint totalPages);
	delegate void PSPDFProcessorPdfFromUrlHandler (NSUrl fileURL, NSError error);


	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFProcessor
	{
		[Field ("PSPDFProcessorAnnotationTypes", "__Internal")]
		NSString ProcessorAnnotationTypes { get; }

		[Field ("PSPDFProcessorAnnotationDict", "__Internal")]
		NSString ProcessorAnnotationDict { get; }

		[Field ("PSPDFProcessorAnnotationAsDictionary", "__Internal")]
		NSString AnnotationAsDictionary { get; }

		[Field ("PSPDFProcessorPageRect", "__Internal")]
		NSString ProcessorPageRect { get; }

		[Field ("PSPDFProcessorNumberOfPages", "__Internal")]
		NSString ProcessorNumberOfPages { get; }

		[Field ("PSPDFProcessorPageBorderMargin", "__Internal")]
		NSString ProcessorPageBorderMargin { get; }

		[Field ("PSPDFProcessorAdditionalDelay", "__Internal")]
		NSString ProcessorAdditionalDelay { get; }

		[Field ("PSPDFProcessorStripEmptyPages", "__Internal")]
		NSString ProcessorStripEmptyPages { get; }

		[Field ("PSPDFProcessorDocumentTitle", "__Internal")]
		NSString ProcessorDocumentTitle { get; }

		[Static]
		[Export ("defaultProcessor")]
		PSPDFProcessor DefaultProcessor { get; }

		[Export ("generatePDFFromDocument:pageRange:outputFileURL:options:progressBlock:error:")]
		bool GeneratePDFFromDocument (PSPDFDocument document, NSIndexSet pageRange, NSUrl fileURL, [NullAllowed] NSDictionary options, PSPDFProcessorProgressHandler progressHandler, out NSError error);

		[Export ("generatePDFFromDocument:pageRange:options:progressBlock:error:")]
		NSData GeneratePDFFromDocument (PSPDFDocument document, NSIndexSet pageRange, [NullAllowed] NSDictionary options, PSPDFProcessorProgressHandler progressHandler, out NSError error);

		[Export ("generatePDFFromHTMLString:outputFileURL:options:")]
		bool GeneratePDFFromHTMLString (string html, NSUrl filerUrl, [NullAllowed] NSDictionary options);

		[Export ("generatePDFFromHTMLString:options:")]
		NSData GeneratePDFFromHTMLString (string html, [NullAllowed] NSDictionary options);

		[Export ("generatePDFFromURL:outputFileURL:options:completionBlock:")]
		PSPDFConversionOperation GeneratePDFFromURL (NSUrl inputURL, NSUrl outputURL, [NullAllowed] NSDictionary options, PSPDFProcessorPdfFromUrlHandler completionHandler);

		[Static]
		[Export ("conversionOperationQueue")]
		NSOperationQueue ConversionOperationQueue { get; }
	}

	delegate void PSPDFConversionOperationHandler (NSUrl fileURL, NSError error);
	delegate void PSPDFConversionOperationDataHandler (NSData fileData, NSError error);

	[BaseType (typeof (NSOperation))]
	interface PSPDFConversionOperation
	{
		[Export ("initWithURL:outputFileURL:options:completionBlock:")]
		IntPtr Constructor (NSUrl inputURL, NSUrl outputFileURL, [NullAllowed] NSDictionary options, PSPDFConversionOperationHandler handler);

		[Export ("initWithURL:options:completionBlock:")]
		IntPtr Constructor (NSUrl inputURL, [NullAllowed] NSDictionary options, PSPDFConversionOperationDataHandler handler);

		[Export ("inputURL", ArgumentSemantic.Retain)]
		NSUrl InputURL { get; }

		[Export ("outputFileURL", ArgumentSemantic.Retain)]
		NSUrl OutputFileURL { get; }

		[Export ("outputData", ArgumentSemantic.Retain)]
		NSData OutputData { get; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSDictionary Options { get; }

		[Export ("error", ArgumentSemantic.Retain)]
		NSError Error { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFProgressHUD
	{
		[Notification]
		[Field ("PSPDFProgressHUDDidReceiveTouchEventNotification", "__Internal")]
		NSString HudDidReceiveTouchEventNotification { get; }

		[Notification]
		[Field ("PSPDFProgressHUDWillDisappearNotification", "__Internal")]
		NSString HudWillDisappearNotification { get; }

		[Notification]
		[Field ("PSPDFProgressHUDDidDisappearNotification", "__Internal")]
		NSString HudDidDisappearNotification { get; }

		[Field ("PSPDFProgressHUDStatusUserInfoKey", "__Internal")]
		NSString ProgressHUDStatusUserInfoKey { get; }

		[Export ("hudBackgroundColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor HudBackgroundColor { get; set; }

		[Export ("hudForegroundColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor HudForegroundColor { get; set; }

		[Export ("hudStatusShadowColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor HudStatusShadowColor { get; set; }

		[Export ("hudFont", ArgumentSemantic.Retain)] [NullAllowed]
		UIFont HudFont { get; set; }

		[Static]
		[Export ("show")]
		void Show ();

		[Static]
		[Export ("showWithMaskType:")]
		void Show (PSPDFProgressHUDMaskType maskType);

		[Static]
		[Export ("showWithStatus:")]
		void Show (string status);

		[Static]
		[Export ("showWithStatus:maskType:")]
		void Show (string status, PSPDFProgressHUDMaskType maskType);

		[Static]
		[Export ("showProgress:")]
		void ShowProgress (float progress);

		[Static]
		[Export ("showProgress:status:")]
		void ShowProgress (float progress, string status);

		[Static]
		[Export ("showProgress:status:maskType:")]
		void ShowProgress (float progress, string status, PSPDFProgressHUDMaskType maskType);

		[Static]
		[Export ("setStatus:")]
		void SetStatus (string status);

		[Static]
		[Export ("showSuccessWithStatus:")]
		void ShowSuccess (string status);

		[Static]
		[Export ("showErrorWithStatus:")]
		void ShowError (string status);

		[Static]
		[Export ("showImage:status:")]
		void ShowImage (UIImage image, string status);

		[Static]
		[Export ("popActivity")]
		void PopActivity ();

		[Static]
		[Export ("dismiss")]
		void Dismiss ();

		[Static]
		[Export ("isVisible")]
		bool IsVisible { get; }
	}

	[BaseType (typeof (UIButton))]
	interface PSPDFColorButton
	{
		[Export ("color", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor Color { get; set; }

		[Export ("displayAsEllipse", ArgumentSemantic.Assign)]
		bool DisplayAsEllipse { get; set; }

		[Export ("borderWidth", ArgumentSemantic.Assign)]
		float BorderWidth { get; set; }

		[Export ("indicatorSize", ArgumentSemantic.Assign)]
		float IndicatorSize { get; set; }
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFHSVColorPickerController
	{
		[Export ("selectionColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("margin", ArgumentSemantic.Assign)]
		UIEdgeInsets Margin { get; set; }

		[Export ("colorPicker", ArgumentSemantic.Retain)]
		PSPDFColorPickerView ColorPicker { get; }

		[Export ("brightnessSlider", ArgumentSemantic.Retain)]
		PSPDFBrightnessSlider BrightnessSlider { get; }

		[Wrap ("WeakDelegate")]
		PSPDFColorSelectionViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }
	}

	[BaseType (typeof (UINavigationController))]
	interface PSPDFNavigationController
	{
		[Export ("rotationForwardingEnabled", ArgumentSemantic.Assign)]
		bool RotationForwardingEnabled { [Bind ("isRotationForwardingEnabled")] get; set; }

		[Export ("setNavigationControllerWillDismissAction:")]
		void SetNavigationControllerWillDismissAction (NSAction action);

		[Export ("persistentCloseButtonMode", ArgumentSemantic.Assign)]
		PSPDFPersistentCloseButtonMode PersistentCloseButtonMode { get; set; }

		[Export ("persistentCloseButton", ArgumentSemantic.Retain)] [NullAllowed]
		UIBarButtonItem PersistentCloseButton { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFModel
	{
		[Export ("modelWithDictionary:error:")]
		IntPtr Constructor (NSDictionary dictionaryValue, out NSError error);

		[Static]
		[Export ("propertyKeys")]
		NSOrderedSet PropertyKeys { get; }

		[Static]
		[Export ("cachedPropertyKeys")]
		NSObject [] CachedPropertyKeys { get; }

		[Static]
		[Export ("cachedPropertyKeySet")]
		NSObject CachedPropertyKeySet { get; }

		[Static]
		[Export ("propertyKeysWithReferentialEquality")]
		NSOrderedSet PropertyKeysWithReferentialEquality { get; }

		[Export ("dictionaryValue", ArgumentSemantic.Copy)]
		NSDictionary DictionaryValue { get; }

		[Export ("mergeValueForKey:fromModel:")]
		void MergeValueForKey (string key, PSPDFModel model);

		[Export ("mergeValuesForKeysFromModel:")]
		void MergeValuesForKeysFromModel (PSPDFModel model);

		// PSPDFModel (Validation) Category

		[Export ("validate:")]
		bool Validate (out NSError error);
	}

	delegate void PSPDFLibraryUIDsMatchingString (string searchString, NSDictionary resultSet);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFLibrary
	{
		[Notification]
		[Field ("PSPDFLibraryWillStartIndexingDocumentNotification", "__Internal")]
		NSString WillStartIndexingDocumentNotification { get; }

		[Notification]
		[Field ("PSPDFLibraryDidFinishIndexingDocumentNotification", "__Internal")]
		NSString DidFinishIndexingDocumentNotification { get; }

		[Notification]
		[Field ("PSPDFLibraryDidFailIndexingDocumentNotification", "__Internal")]
		NSString DidFailIndexingDocumentNotification { get; }

		[Static]
		[Export ("defaultLibrary")]
		PSPDFLibrary DefaultLibrary { get; }

		[Static]
		[Export ("libraryWithPath:")]
		PSPDFLibrary FromPath (string path);

		[Export ("path", ArgumentSemantic.Copy)]
		string Path { get; }

		[Export ("tokenizer", ArgumentSemantic.Copy)]
		string Tokenizer { get; set; }

		[Export ("saveGlyphPositions", ArgumentSemantic.Assign)]
		bool SaveGlyphPositions { get; set; }

		[Export ("saveReversedPageText", ArgumentSemantic.Assign)]
		bool SaveReversedPageText { get; set; }

		[Field ("PSPDFLibraryMaximumSearchResultsTotalKey", "__Internal")]
		NSString LibraryMaximumSearchResultsTotalKey { get; }

		[Field ("PSPDFLibraryMaximumSearchResultsPerDocumentKey", "__Internal")]
		NSString LibraryMaximumSearchResultsPerDocumentKey { get; }

		[Export ("documentUIDsMatchingString:options:completionHandler:")]
		void DocumentUIDsMatchingString (string searchString, NSDictionary options, PSPDFLibraryUIDsMatchingString completionHandler);

		[Export ("indexStatusForUID:withProgress:")]
		PSPDFLibraryIndexStatus IndexStatusForUID (string uid, out float outProgress);

		[Export ("isIndexing")]
		bool IsIndexing { get; }

		[Export ("queuedUIDs")]
		string [] QueuedUIDs { get; }

		[Export ("enqueueDocuments:")]
		void EnqueueDocuments (PSPDFDocument [] documents);

		[Export ("removeIndexForUID:")]
		void RemoveIndexForUID (string uid);

		[Export ("clearAllIndexes")]
		void ClearAllIndexes ();
	}

	interface IPSPDFDocumentPickerControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFDocumentPickerControllerDelegate : PSPDFOverridable
	{
		[Abstract]
		[Export ("documentPickerController:didSelectDocument:page:searchString:")]
		void DidSelectDocument (PSPDFDocumentPickerController controller, PSPDFDocument document, uint pageIndex, string searchString);

		[Export ("documentPickerControllerWillBeginSearch:")]
		void WillBeginSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerDidBeginSearch:")]
		void DidBeginSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerWillEndSearch:")]
		void WillEndSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerDidEndSearch:")]
		void DidEndSearch (PSPDFDocumentPickerController controller);
	}

	[BaseType (typeof (UITableViewController))]
	interface PSPDFDocumentPickerController : IUISearchDisplayDelegate, IUISearchBarDelegate
	{
		[Static]
		[Export ("documentsFromDirectory:includeSubdirectories:")]
		PSPDFDocument [] DocumentsFromDirectory (string directoryName, bool includeSubdirectories);

		[Export ("initWithDirectory:includeSubdirectories:library:delegate:")]
		IntPtr Constructor (string directory, bool includeSubdirectories, PSPDFLibrary library, PSPDFDocumentPickerControllerDelegate controllerDelegate);

		[Export ("initWithDocuments:library:delegate:")]
		IntPtr Constructor (PSPDFDocument [] documents, PSPDFLibrary library, PSPDFDocumentPickerControllerDelegate controllerDelegate);

		[Wrap ("WeakDelegate")]
		PSPDFDocumentPickerControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; }

		[Export ("directory", ArgumentSemantic.Copy)]
		string Directory { get; }

		[Export ("useDocumentTitles", ArgumentSemantic.Assign)]
		bool UseDocumentTitles { get; set; }

		[Export ("showSectionIndexes", ArgumentSemantic.Assign)]
		bool ShowSectionIndexes { get; set; }

		[Export ("alwaysShowDocuments", ArgumentSemantic.Assign)]
		bool AlwaysShowDocuments { get; set; }

		[Export ("fullTextSearchEnabled", ArgumentSemantic.Assign)]
		bool FullTextSearchEnabled { get; set; }

		[Export ("isSearchingIndex", ArgumentSemantic.Assign)]
		bool IsSearchingIndex { get; }

		[Export ("showSearchPageResults", ArgumentSemantic.Assign)]
		bool ShowSearchPageResults { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed", ArgumentSemantic.Assign)]
		uint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("maximumNumberOfSearchResultsPerDocument", ArgumentSemantic.Assign)]
		uint MaximumNumberOfSearchResultsPerDocument { get; set; }

		[Export ("library", ArgumentSemantic.Retain)]
		PSPDFLibrary Library { get; }

		// PSPDFDocumentPickerController (SubclassingHooks) Category

		[Export ("updateStatusCell:")]
		void UpdateStatusCell (PSPDFDocumentPickerIndexStatusCell cell);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFParser
	{
		[Export ("initWithInputStream:documentProvider:")]
		IntPtr Constructor (NSInputStream inputStream, PSPDFDocumentProvider documentProvider);

		[Export ("parseWithError:")]
		PSPDFAnnotation [] ParseWithError (out NSError error);

		[Export ("annotations")]
		PSPDFAnnotation [] Annotations ();

		[Export ("parsingEnded", ArgumentSemantic.Assign)]
		bool ParsingEnded { get; }

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("inputStream", ArgumentSemantic.Retain)]
		NSInputStream InputStream { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFWriter
	{
		[Export ("writeAnnotations:toOutputStream:documentProvider:error:")]
		bool WriteAnnotations (PSPDFAnnotation [] annotations, NSOutputStream outputStream, PSPDFDocumentProvider documentProvider, out NSError error);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFAnnotationProvider : IPSPDFAnnotationProvider
	{
		[Export ("initWithDocumentProvider:fileURL:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider, NSUrl xfdfFileUrl);

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[Export ("fileURL", ArgumentSemantic.Retain)]
		NSUrl FileUrl { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAESCryptoDataProvider
	{
		[Export ("initWithURL:passphrase:salt:rounds:")]
		IntPtr Constructor (NSUrl url, string passphrase, string salt, uint rounds);

		[Export ("initWithURL:passphraseData:salt:rounds:")]
		IntPtr Constructor (NSUrl url, NSData passphraseData, NSData saltData, uint rounds);

		[Export ("initWithURL:binaryKey:")]
		IntPtr Constructor (NSUrl url, NSData key);

		[Export ("dataProvider")] [Internal]
		IntPtr /* CGDataProvider */ DataProvider_ { get; }

		[Static]
		[Export ("isAESCryptoDataProvider:")] [Internal]
		bool IsAESCryptoDataProvider_ (IntPtr /* CGDataProvider */ dataProviderRef);

		[Static]
		[Export ("isAESCryptoFeatureAvailable")]
		bool IsAESCryptoFeatureAvailable { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFCryptor
	{
		[Field ("PSPDFCryptorErrorDomain", "__Internal")]
		NSString CryptorErrorDomain { get; }

		[Export ("keyFromPassphrase:salt:")]
		NSData KeyFromPassphrase (string passphrase, string salt);

		[Export ("encryptFromURL:toURL:key:error:")]
		bool EncryptFromURL (NSUrl sourceURL, NSUrl targetURL, NSData key, out NSError error);

		[Export ("decryptFromURL:toURL:key:error:")]
		bool DecryptFromURL (NSUrl sourceURL, NSUrl targetURL, NSData key, out NSError error);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFFormParser
	{
		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("documentProvider", ArgumentSemantic.Assign)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[Export ("forms", ArgumentSemantic.Copy)]
		NSObject [] Forms { get; set; }

		[Export ("findAnnotationWithFieldName:")]
		PSPDFFormElement FindAnnotationWithFieldName (string fieldName);
	}

	[BaseType (typeof (PSPDFWidgetAnnotation))]
	interface PSPDFFormElement
	{
		[Export ("initWithAnnotationDictionary:documentRef:fieldsAddressMap:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ annotDict, IntPtr /* CGPDFDocument */ documentRef, NSMutableDictionary fieldsAddressMap);

		[Export ("initWithAnnotationDictionary:documentRef:parent:fieldsAddressMap:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ annotDict, IntPtr /* CGPDFDocument */ documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap);

		[Static]
		[Export ("formElementWithAnnotationDictionary:documentRef:parent:fieldsAddressMap:")] [Internal]
		PSPDFFormElement FormElementWithAnnotationDictionary_ (IntPtr /* CGPDFDictionary */ annotDict, IntPtr /* CGPDFDocument */ documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap);

		[Export ("parent", ArgumentSemantic.Assign)]
		PSPDFFormElement Parent { get; set; }

		[Export ("kids", ArgumentSemantic.Copy)]
		PSPDFFormElement [] Kids { get; set; }

		[Export ("fieldType", ArgumentSemantic.Copy)]
		string FieldType { get; set; }

		[Export ("fieldName", ArgumentSemantic.Copy)]
		string FieldName { get; set; }

		[Export ("mappingName", ArgumentSemantic.Copy)]
		string MappingName { get; set; }

		[Export ("alternateFieldName", ArgumentSemantic.Copy)]
		string AlternateFieldName { get; set; }

		[Export ("fieldFlags", ArgumentSemantic.Assign)]
		uint FieldFlags { get; set; }

		[Export ("value", ArgumentSemantic.Retain)] [NullAllowed] [New]
		NSObject Value { get; set; }

		[Export ("defaultValue", ArgumentSemantic.Retain)] [NullAllowed]
		NSObject DefaultValue { get; set; }

		[Export ("appearanceState", ArgumentSemantic.Retain)] [NullAllowed]
		string AppearanceState { get; set; }

		[Export ("exportValue")]
		NSObject ExportValue { get; }

		[Export ("next", ArgumentSemantic.Assign)]
		PSPDFFormElement Next { get; set; }

		[Export ("previous", ArgumentSemantic.Assign)]
		PSPDFFormElement Previous { get; set; }

		[Export ("tabbingPage", ArgumentSemantic.Assign)]
		uint TabbingPage { get; set; }

		[Export ("tabbingStructureIndex", ArgumentSemantic.Assign)]
		uint TabbingStructureIndex { get; set; }

		[Export ("tabbingManualIndex", ArgumentSemantic.Assign)]
		uint TabbingManualIndex { get; set; }

		[Export ("structParent", ArgumentSemantic.Assign)]
		uint StructParent { get; set; }

		[Export ("tabOrder", ArgumentSemantic.Copy)]
		string TabOrder { get; set; }

		[Export ("isReadOnly")]
		bool IsReadOnly { get; }

		[Export ("isRequired")]
		bool IsRequired { get; }

		[Export ("isNoExport")]
		bool IsNoExport { get; }

		[Export ("fullyQualifiedFieldName")]
		string FullyQualifiedFieldName { get; }

		[Export ("handleTapInView:")] [New]
		bool HandleTapInView (PSPDFPageView pdfPageView);

		[Export ("appendCommonFormElementPDFData:")]
		void AppendCommonFormElementPDFData (NSMutableData pdfData);

		[Export ("appendFieldValuePDFData:")]
		void AppendFieldValuePDFData (NSMutableData pdfData);

		[Export ("resetWithAction:")]
		void ResetWithAction (PSPDFResetFormAction action);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFButtonFormElement
	{
		[Export ("initWithAnnotationDictionary:documentRef:parent:fieldsAddressMap:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ annotDict, IntPtr /* CGPDFDocument */ documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap);

		[Export ("isPushButton")]
		bool IsPushButton { get; }

		[Export ("isCheckBox")]
		bool IsCheckBox { get; }

		[Export ("isRadioButton")]
		bool IsRadioButton { get; }

		[Export ("isSelected")]
		bool IsSelected { get; }

		[Export ("select")]
		void Select ();

		[Export ("deselect")]
		void Deselect ();

		[Export ("opt", ArgumentSemantic.Retain)] [NullAllowed]
		NSObject [] Opt { get; set; }

		[Export ("onState", ArgumentSemantic.Retain)] [NullAllowed]
		string OnState { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractTextRenderingFormElement))]
	interface PSPDFChoiceFormElement
	{
		[Export ("initWithAnnotationDictionary:documentRef:parent:fieldsAddressMap:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ annotDict, IntPtr /* CGPDFDocument */ documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap);

		[Export ("isCombo")]
		bool IsCombo { get; }

		[Export ("isEdit")]
		bool IsEdit { get; }

		[Export ("isMultiSelect")]
		bool IsMultiSelect { get; }

		[Export ("options", ArgumentSemantic.Retain)] [NullAllowed]
		NSObject [] Options { get; set; } 

		[Export ("selectedIndices", ArgumentSemantic.Retain)] [NullAllowed]
		NSMutableIndexSet SelectedIndices { get; set; } 

		[Export ("customSelection", ArgumentSemantic.Assign)]
		bool CustomSelection { get; set; } 

		[Export ("topIndex", ArgumentSemantic.Assign)]
		uint TopIndex { get; set; } 

		[Export ("numberOfOptions")]
		uint NumberOfOptions { get; }

		[Export ("optionTextAtIndex:")]
		string OptionTextAtIndex (uint index);

		[Export ("optionExportValueAtIndex:")]
		string OptionExportValueAtIndex (uint index);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFSignatureFormElement
	{
		[Export ("initWithAnnotationDictionary:documentRef:parent:fieldsAddressMap:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ annotDict, IntPtr /* CGPDFDocument */ documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap);
	}

	[BaseType (typeof (PSPDFAbstractTextRenderingFormElement))]
	interface PSPDFTextFieldFormElement
	{
		[Export ("initWithAnnotationDictionary:documentRef:parent:fieldsAddressMap:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ annotDict, IntPtr /* CGPDFDocument */ documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap);

		[Export ("isMultiline")] [New]
		bool IsMultiline { get; }

		[Export ("isPassword")]
		bool IsPassword { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFCenteredLabelView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);

		[Export ("setCenteredLabelText:")]
		void SetCenteredLabelText (string text);

		[Export ("centeredLabel", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFActivityLabel CenteredLabel { get; set; }
	}

	delegate PSPDFCacheInfo PSPDFCacheInfoSelector (NSOrderedSet info);
	delegate NSObject [] PSPDFCacheInfoArraySelector (NSOrderedSet info);
	delegate IntPtr PSPDFCacheDecryptionHelper (string path);
	delegate NSData PSPDFCacheEncryptionHelper (UIImage image);
	delegate void PSPDFCacheCompletionHandler (UIImage image, PSPDFCacheInfo cacheInfo);

	[BaseType (typeof (NSObject))]
	interface PSPDFDiskCache
	{
		[Export ("initWithCacheDirectory:fileFormat:")]
		IntPtr Constructor (string cacheDirectory, string fileFormat);

		[Export ("CacheInfoForImageWithUID:page:size:infoSelector:")]
		PSPDFCacheInfo CacheInfoForImageWithUID (string uid, uint page, SizeF size, PSPDFCacheInfoSelector infoSelector);

		[Export ("imageWithUID:page:size:infoSelector:decryptionHelper:cacheInfo:")]
		UIImage ImageWithUID (string uid, uint page, SizeF size, PSPDFCacheInfoSelector infoSelector, PSPDFCacheDecryptionHelper decryptionHelper, out PSPDFCacheInfo outCacheInfo);

		[Export ("scheduleLoadImageWithUID:page:size:infoSelector:decryptionHelper:completionBlock:")]
		PSPDFCacheInfo ScheduleLoadImageWithUID (string uid, uint page, SizeF size, PSPDFCacheInfoSelector infoSelector, PSPDFCacheDecryptionHelper decryptionHelper, PSPDFCacheCompletionHandler completionHandler);

		[Export ("storeImage:UID:page:encryptionHelper:receipt:")]
		void StoreImage (UIImage image, string uid, uint page, PSPDFCacheEncryptionHelper encryptionHelper, string renderReceipt);

		[Export ("invalidateAllImagesWithUID:")]
		bool InvalidateAllImagesWithUID (string uid);

		[Export ("invalidateAllImagesWithUID:page:infoArraySelector:")]
		bool InvalidateAllImagesWithUID (string uid, uint page, PSPDFCacheInfoArraySelector infoSelector);

		[Export ("cancelWriteRequestsWithUID:page:infoArraySelector:")]
		void CancelWriteRequestsWithUID (string uid, uint page, PSPDFCacheInfoArraySelector infoSelector);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("allowedDiskSpace", ArgumentSemantic.Assign)]
		ulong AllowedDiskSpace { get; set; }

		[Export ("usedDiskSpace", ArgumentSemantic.Assign)]
		ulong UsedDiskSpace { get; }

		[Export ("freeDiskSpace", ArgumentSemantic.Assign)]
		ulong FreeDiskSpace { get; }

		[Export ("fileFormat", ArgumentSemantic.Copy)]
		string FileFormat { get; set; }
	}

	[BaseType (typeof (UISegmentedControl))]
	interface PSPDFSegmentedControl
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (RectangleF rect);

		[Export ("setSelectedImage:forSegmentAtIndex:")]
		void SetSelectedImage (UIImage image, uint segment);

		[Export ("selectedImageForSegmentAtIndex:")]
		UIImage SelectedImageForSegmentAtIndex (uint segment);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFMemoryCache
	{
		[Export ("CacheInfoForImageWithUID:page:size:infoSelector:")]
		PSPDFCacheInfo CacheInfoForImageWithUID (string uid, uint page, SizeF size, PSPDFCacheInfoSelector infoSelector);

		[Export ("invalidateAllImagesWithUID:")]
		bool InvalidateAllImagesWithUID (string uid);

		[Export ("invalidateAllImagesWithUID:page:infoArraySelector:")]
		bool InvalidateAllImagesWithUID (string uid, uint page, PSPDFCacheInfoArraySelector infoSelector);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("count")]
		uint Count { get; }

		[Export ("numberOfPixels", ArgumentSemantic.Assign)]
		uint NumberOfPixels { get; }

		[Export ("maxNumberOfPixels", ArgumentSemantic.Assign)]
		uint MaxNumberOfPixels { get; }

		[Export ("maxNumberOfPixelsUnderStress", ArgumentSemantic.Assign)]
		uint MaxNumberOfPixelsUnderStress { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFCacheInfo
	{
		[Export ("initWithUID:page:size:receipt:")]
		IntPtr Constructor (string uid, uint page, SizeF size, string renderReceipt);

		[Export ("UID", ArgumentSemantic.Copy)]
		string Uid { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; }

		[Export ("size", ArgumentSemantic.Assign)]
		SizeF Size { get; }

		[Export ("renderFingerprintString", ArgumentSemantic.Retain)] [NullAllowed]
		string RenderFingerprintString { get; set; }

		[Export ("lastAccessTime", ArgumentSemantic.Retain)] [NullAllowed]
		NSDate LastAccessTime { get; set; }

		[Export ("diskSize", ArgumentSemantic.Assign)]
		uint DiskSize { get; }

		[Export ("image", ArgumentSemantic.Retain)] [NullAllowed]
		UIImage Image { get; set; }

		[Export ("canBeScaledDownToSize:")]
		bool CanBeScaledDownToSize (SizeF size);
	}

	interface IPSPDFSimplePageViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFSimplePageViewControllerDelegate
	{
		[Export ("shouldDelayContentTouches")]
		bool ShouldDelayContentTouches ();
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFSimplePageViewController
	{
		[Export ("initWithViewControllers:")]
		IntPtr Constructor (UIViewController [] viewControllers);

		[Export ("page", ArgumentSemantic.Assign)]
		uint Page { get; set; }

		[Export ("setPage:animated:")]
		void SetPage (uint page, bool animated);

		// PSPDFSimplePageViewController (SubclassingHooks)

		[Export ("scrollView", ArgumentSemantic.Retain)]
		UIScrollView ScrollView { get; }

		[Export ("pageControl", ArgumentSemantic.Retain)]
		UIPageControl PageControl { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFUndoController
	{
		[Notification]
		[Field ("PSPDFUndoControllerAddedUndoActionNotification", "__Internal")]
		NSString AddedUndoActionNotification { get; }

		[Export ("initWithUndoEnabled:")]
		IntPtr Constructor (bool undoEnabled);

		[Export ("isWorking")]
		bool IsWorking { get; }

		[Export ("canUndo")]
		bool CanUndo { get; }

		[Export ("canRedo")]
		bool CanRedo { get; }

		[Export ("undo")]
		void Undo ();

		[Export ("redo")]
		void Redo ();

		[Export ("beginUndoGrouping")]
		void BeginUndoGrouping ();

		[Export ("endUndoGroupingWithName:")]
		void EndUndoGroupingWithName (string groupName);

		[Export ("endUndoGroupingWithProperty:ofObject:")]
		void EndUndoGroupingWithName (string changedProperty, NSObject obj);

		[Export ("removeAllActions")]
		void RemoveAllActions ();

		[Export ("registerObjectForUndo:")]
		void RegisterObjectForUndo (IPSPDFUndoProtocol obj);

		[Export ("unregisterObjectForUndo:")]
		void UnregisterObjectForUndo (IPSPDFUndoProtocol obj);

		[Export ("isObjectRegisteredForUndo:")]
		bool IsObjectRegisteredForUndo (IPSPDFUndoProtocol obj);

		[Export ("performBlockAsGroup:name:")]
		void PerformActionAsGroup (NSAction action, string groupName);

		[Export ("performBlockWithoutUndo:")]
		void PerformActionWithoutUndo (NSAction action);

		[Export ("undoEnabled", ArgumentSemantic.Assign)]
		bool UndoEnabled { [Bind ("isUndoEnabled")] get; } 

		[Export ("undoManager", ArgumentSemantic.Retain)]
		NSUndoManager UndoManager { get; } 

		[Export ("timedCoalescingInterval", ArgumentSemantic.Assign)]
		double TimedCoalescingInterval { get; set; }

		[Export ("levelsOfUndo", ArgumentSemantic.Assign)]
		uint LevelsOfUndo { get; set; }
	}

	interface IPSPDFUndoProtocol { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFUndoProtocol
	{
		[Static]
		[Export ("keysForValuesToObserveForUndo")]
		NSSet KeysForValuesToObserveForUndo ();

		[Static]
		[Export ("localizedUndoActionNameForKey:")]
		string LocalizedUndoActionNameForKey (string key);

		[Static]
		[Export ("undoCoalescingForKey:")]
		PSPDFUndoCoalescing UndoCoalescingForKey (string key);

		[Export ("insertUndoObjects:forKey:")]
		void InsertUndoObjects (NSSet objects, string key);

		[Export ("removeUndoObjects:forKey:")]
		void RemoveUndoObjects (NSSet objects, string key);

		[Export ("didUndoOrRedoChange:")]
		void DidUndoOrRedoChange (string key);

		// PSPDFUndoController (TimeCoalescingSupport) Category

		[Export ("commitIncompleteUndoActions")]
		void CommitIncompleteUndoActions ();

		[Export ("hasIncompleteUndoActions")]
		bool HasIncompleteUndoActions ();

		[Export ("incompleteUndoActionName")]
		string IncompleteUndoActionName ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationViewCache
	{
		[Export ("initWithPDFController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("prepareAnnotationViewForAnnotation:frame:pageView:")]
		IPSPDFAnnotationViewProtocol PrepareAnnotationViewForAnnotation (PSPDFAnnotation annotation, RectangleF annotationRect, PSPDFPageView pageView);

		[Export ("handleTouchUpForAnnotationIgnoredByDelegate:")]
		void HandleTouchUpForAnnotationIgnoredByDelegate (PSPDFLinkAnnotationView annotationView);

		[Export ("recycleAnnotationView:")]
		void RecycleAnnotationView (IPSPDFAnnotationViewProtocol annotationView);

		[Export ("dequeueViewFromCacheForAnnotation:class:")]
		IPSPDFAnnotationViewProtocol DequeueViewFromCacheForAnnotation (PSPDFAnnotation annotation, Class annotationViewClass);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("pdfController", ArgumentSemantic.Assign)]
		PSPDFViewController PdfController { get; set; } 

		// PSPDFAnnotationViewCache (SubclassingHooks) Category

		[Export ("createAnnotationViewForAnnotation:frame:")]
		IPSPDFAnnotationViewProtocol CreateAnnotationViewForAnnotation (PSPDFAnnotation annotation, RectangleF annotationRect);
	}


	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFGalleryAnnotationView
	{
		[Export ("galleryViewController", ArgumentSemantic.Retain)]
		PSPDFGalleryViewController GalleryViewController { get; } 

		[Export ("viewController", ArgumentSemantic.Assign)]
		UIViewController ViewController { get; } 
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContainerView
	{
		[Export ("galleryView", ArgumentSemantic.Retain)]
		PSPDFGalleryView GalleryView { get; set; } 

		[Export ("errorView", ArgumentSemantic.Retain)]
		PSPDFGalleryErrorView ErrorView { get; set; } 

		[Export ("loadingView", ArgumentSemantic.Retain)]
		PSPDFGalleryLoadingView LoadingView { get; set; } 
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentCaptionView
	{
		[Export ("label", ArgumentSemantic.Retain)]
		UILabel Label { get; set; } 

		[Export ("contentInset", ArgumentSemantic.Assign)]
		UIEdgeInsets ContentInset { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentLoadingView
	{
		[Export ("progress", ArgumentSemantic.Assign)]
		float Progress { get; set; }

		[Export ("color", ArgumentSemantic.Retain)]
		UIColor Color { get; set; } 
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentView
	{
		[Export ("initWithReuseIdentifier:")]
		IntPtr Constructor (string reuseIdentifier);

		[Export ("imageView", ArgumentSemantic.Retain)]
		UIImageView ImageView { get; } 

		[Export ("loadingView", ArgumentSemantic.Retain)]
		PSPDFGalleryContentLoadingView LoadingView { get; } 

		[Export ("captionView", ArgumentSemantic.Retain)]
		PSPDFGalleryContentCaptionView CaptionView { get; } 

		[Export ("errorView", ArgumentSemantic.Retain)]
		PSPDFGalleryErrorView ErrorView { get; } 

		[Export ("reuseIdentifier", ArgumentSemantic.Retain)]
		string ReuseIdentifier { get; } 

		[Export ("caption", ArgumentSemantic.Copy)]
		string Caption { get; set; } 

		[Export ("image", ArgumentSemantic.Retain)]
		UIImage Image { get; set; } 

		[Export ("loading", ArgumentSemantic.Retain)]
		bool Loading { [Bind ("isLoading")] get; set; } 

		[Export ("error", ArgumentSemantic.Retain)]
		NSError Error { get; set; }

		[Export ("prepareForReuse")]
		void PrepareForReuse ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryErrorView
	{
		[Export ("titleLabel", ArgumentSemantic.Retain)]
		UILabel TitleLabel { get; set; }

		[Export ("subtitleLabel", ArgumentSemantic.Retain)]
		UILabel SubtitleLabel { get; set; } 
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryItem
	{
		[Static]
		[Export ("itemsFromJSONData:error:")]
		PSPDFGalleryItem [] FromJsonData (NSData data, out NSError error);

		[Export ("initWithDictionary:error:")]
		IntPtr Constructor (NSDictionary dictionary, out NSError error);

		[Export ("initWithContentURL:caption:")]
		IntPtr Constructor (NSUrl contentUrl, string caption);

		[Export ("caption", ArgumentSemantic.Copy)]
		string Caption { get; }

		[Export ("contentURL", ArgumentSemantic.Retain)]
		NSUrl ContentUrl { get; } 

		[Export ("remoteContent", ArgumentSemantic.Retain)]
		UIImage RemoteContent { get; set; } 

		[Export ("loadingRemoteContent", ArgumentSemantic.Assign)]
		bool LoadingRemoteContent { [Bind ("isLoadingRemoteContent")] get; set; } 

		[Export ("remoteContentProgress", ArgumentSemantic.Assign)]
		float RemoteContentProgress { get; set; }

		[Export ("remoteContentError", ArgumentSemantic.Retain)]
		NSError RemoteContentError { get; set; } 
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryLoadingView
	{
		[Export ("activityIndicatorView", ArgumentSemantic.Retain)]
		UIActivityIndicatorView ActivityIndicatorView { get; set; }
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFGalleryView
	{
		[Wrap ("WeakDataSource")]
		PSPDFGalleryViewDataSource DataSource { get; set; }

		[Export ("dataSource", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDataSource { get; set; }

		[Export ("currentItemIndex", ArgumentSemantic.Assign)]
		uint CurrentItemIndex { get; set; }

		[Export ("contentPadding", ArgumentSemantic.Assign)]
		float ContentPadding { get; set; }

		[Wrap ("WeakDelegate")] [New]
		PSPDFGalleryViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed][New]
		NSObject WeakDelegate { get; set; }

		[Export ("reload")]
		void Reload ();

		[Export ("contentViewForItemAtIndex:")]
		PSPDFGalleryContentView ContentViewForItemAtIndex (uint index);

		[Export ("itemIndexForContentView:")]
		uint ItemIndexForContentView (PSPDFGalleryContentView contentView);

		[Export ("dequeueReusableContentViewWithIdentifier:")]
		PSPDFGalleryContentView DequeueReusableContentView (string identifier);

		[Export ("setCurrentItemIndex:animated:")]
		void SetCurrentItemIndex (uint currentItemIndex, bool animated);

		[Export ("activeContentViews")]
		NSSet ActiveContentViews ();
	}

	interface IPSPDFGalleryViewDataSource { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFGalleryViewDataSource
	{
		[Export ("numberOfItemsInGalleryView:")]
		uint NumberOfItemsInGalleryView (PSPDFGalleryView galleryView);

		[Export ("galleryView:contentViewForItemAtIndex:")]
		PSPDFGalleryContentView ContentViewForItemAtIndex (PSPDFGalleryView galleryView, uint index);
	}

	interface IPSPDFGalleryViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFGalleryViewDelegate
	{
		[Export ("galleryView:didScrollToItemAtIndex:")]
		void DidScrollToItemAtIndex (PSPDFGalleryView galleryView, uint index);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFGalleryViewController : PSPDFOverridable
	{
		[Export ("initWithLinkAnnotation:")]
		IntPtr Constructor (PSPDFLinkAnnotation annotation);

		[Export ("maxConcurrentDownloads", ArgumentSemantic.Assign)]
		uint MaxConcurrentDownloads { get; set; }

		[Export ("maxPrefetchDownloads", ArgumentSemantic.Assign)]
		uint MaxPrefetchDownloads { get; set; }

		[Export ("allowFullscreen", ArgumentSemantic.Assign)]
		bool AllowFullscreen { get; set; }

		[Export ("fullscreenDismissPanTreshold", ArgumentSemantic.Assign)]
		float FullscreenDismissPanTreshold { get; set; }

		[Export ("state", ArgumentSemantic.Assign)]
		PSPDFGalleryViewControllerState State { get; }

		[Export ("items", ArgumentSemantic.Copy)]
		PSPDFGalleryItem [] Items { get; }

		[Export ("linkAnnotation", ArgumentSemantic.Retain)]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("fullscreen", ArgumentSemantic.Retain)]
		bool Fullscreen { [Bind ("isFullscreen")] get; set; }

		[Export ("setFullscreen:animated:")]
		void SetFullscreen (bool fullscreen, bool animated);
	}

	interface IPSPDFFormSubmissionDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFFormSubmissionDelegate
	{
		[Export ("pdfViewController:shouldSubmitFormRequest:"), DelegateName ("PSPDFFormSubmissionDelegateShouldSubmitFormRequest"), NoDefaultValue]
		bool ShouldSubmitFormRequest (PSPDFViewController viewController, PSPDFFormRequest formRequest);

		[Export ("pdfViewController:willSubmitFormValues:"), EventArgs ("PSPDFFormSubmissionDelegateWillSubmitFormValues")]
		void WillSubmitFormValues (PSPDFViewController viewController, PSPDFFormRequest formRequest);

		[Export ("pdfViewController:didReceiveResponseData:"), EventArgs ("PSPDFFormSubmissionDelegateDidReceiveResponseData")]
		void DidReceiveResponseData (PSPDFViewController viewController, NSData responseData);

		[Export ("pdfViewController:didFailWithError:"), EventArgs ("PSPDFFormSubmissionDelegateDidFailWithError")]
		void DidFailWithError (PSPDFViewController viewController, NSError error);

		[Export ("pdfViewControllerShouldPresentResponseInWebView:"), EventArgs ("PSPDFFormSubmissionDelegateShouldPresentResponseInWebView")]
		void ShouldPresentResponseInWebView (PSPDFViewController viewController);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFFormRequest
	{
		[Export ("initWithFormat:values:request:")]
		IntPtr Constructor (PSPDFSubmitFormActionFormat format, NSDictionary values, NSUrlRequest request);

		[Export ("submissionFormat", ArgumentSemantic.Assign)]
		PSPDFSubmitFormActionFormat SubmissionFormat { get; } 

		[Export ("formValues", ArgumentSemantic.Copy)]
		NSDictionary FormValues { get; } 

		[Export ("request", ArgumentSemantic.Retain)]
		NSUrlRequest Request { get; } 
	}

	interface IPSPDFTextOptionsProtocol { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFTextOptionsProtocol
	{
		[Abstract]
		[Export ("fontSize")]
		float FontSize ();

		[Abstract]
		[Export ("boundingBox")]
		RectangleF BoundingBox ();

		[Abstract]
		[Export ("lineWidth")]
		float LineWidth ();

		[Abstract]
		[Export ("fillColorWithAlpha")]
		UIColor FillColorWithAlpha ();

		[Abstract]
		[Export ("textAlignment")]
		UITextAlignment TextAlignment ();

		[Abstract]
		[Export ("colorWithAlpha")]
		UIColor ColorWithAlpha ();

		[Abstract]
		[Export ("rotation")]
		uint Rotation ();

		[Abstract]
		[Export ("pageRotation")]
		uint PageRotation ();

		[Abstract]
		[Export ("createFontRef")]
		IntPtr /* CTFont */ CreateFontRef ();

		[Abstract]
		[Export ("edgeInsets")]
		UIEdgeInsets EdgeInsets ();

		[Abstract]
		[Export ("clippingBox")]
		RectangleF ClippingBox ();

		[Abstract]
		[Export ("isMultiline")]
		bool IsMultiline ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFTextOptions
	{
		[Export ("fontSize", ArgumentSemantic.Assign)]
		float FontSize { get; set; } 

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		RectangleF BoundingBox { get; set; } 

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		float LineWidth { get; set; } 

		[Export ("fillColorWithAlpha", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor FillColorWithAlpha { get; set; } 

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; } 

		[Export ("colorWithAlpha", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor ColorWithAlpha { get; set; } 

		[Export ("rotation", ArgumentSemantic.Assign)]
		uint Rotation { get; set; } 

		[Export ("pageRotation", ArgumentSemantic.Assign)]
		uint PageRotation { get; set; } 

		[Export ("fontRef", ArgumentSemantic.Assign)]
		IntPtr /* CTFont */ FontRef { get; set; } 

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; } 

		[Export ("clippingBox", ArgumentSemantic.Assign)]
		RectangleF ClippingBox { get; set; } 

		[Export ("isMultiline", ArgumentSemantic.Assign)]
		bool IsMultiline { get; set; } 

		[Export ("initWithOptions:")]
		IntPtr Constructor (PSPDFTextOptionsProtocol options);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFAbstractTextRenderingFormElement
	{
		[Export ("fontName", ArgumentSemantic.Copy)]
		string FontName { get; set; } 

		[Export ("fontSize", ArgumentSemantic.Assign)]
		float FontSize { get; set; } 

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; } 

		[Export ("font", ArgumentSemantic.Assign)]
		UIFont Font { get; set; } 

		[Export ("maxLength", ArgumentSemantic.Assign)]
		uint MaxLength { get; set; } 

		[Export ("defaultFontName")]
		string DefaultFontName ();

		[Export ("defaultFontSize")]
		float DefaultFontSize ();

		[Export ("defaultFont")]
		UIFont DefaultFont ();

		[Export ("createFontRef")]
		IntPtr /* CTFont */ CreateFontRef ();

		[Export ("autoFontSizingFudgeFactor")]
		float AutoFontSizingFudgeFactor ();

		[Export ("autoFontSizingHeightCorrection")]
		float AutoFontSizingHeightCorrection ();

		[Export ("boundingBox", ArgumentSemantic.Assign)] [New]
		RectangleF BoundingBox { get; set; } 

		[Export ("lineWidth", ArgumentSemantic.Assign)] [New]
		float LineWidth { get; set; } 

		[Export ("fillColorWithAlpha", ArgumentSemantic.Retain)] [NullAllowed] [New]
		UIColor FillColorWithAlpha { get; set; } 

		[Export ("colorWithAlpha", ArgumentSemantic.Retain)] [NullAllowed] [New]
		UIColor ColorWithAlpha { get; set; } 

		[Export ("rotation", ArgumentSemantic.Assign)] [New]
		uint Rotation { get; set; } 

		[Export ("pageRotation", ArgumentSemantic.Assign)]
		uint PageRotation { get; set; } 

		[Export ("fontRef", ArgumentSemantic.Assign)]
		IntPtr /* CTFont */ FontRef { get; set; } 

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; } 

		[Export ("clippingBox", ArgumentSemantic.Assign)]
		RectangleF ClippingBox { get; set; } 

		[Export ("isMultiline", ArgumentSemantic.Assign)]
		bool IsMultiline { get; set; } 

		[Export ("drawText:inContext:")]
		void DrawText (string contents, CGContext context);

		[Export ("drawText:withCombLength:inContext:")]
		void DrawText (string contents, uint combLength, CGContext context);

		[Export ("drawText:inContext:withOptions:")]
		void DrawText (string contents, CGContext context, IPSPDFTextOptionsProtocol options);

		[Export ("drawText:withCombLength:inContext:withOptions:")]
		void DrawText (string contents, uint combLength, CGContext context, IPSPDFTextOptionsProtocol options);
	}

	[BaseType (typeof (PSPDFSpinnerCell))]
	interface PSPDFDocumentPickerIndexStatusCell
	{

	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFSpinnerCell
	{
		// PSPDFSpinnerCell_SubclassingHooks

		[Export ("spinner", ArgumentSemantic.Retain)]
		UIActivityIndicatorView Spinner { get; }

		[Export ("alignTextLabel")]
		void AlignTextLabel ();
	}

	interface IPSPDFColorPickerViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFColorPickerViewDelegate
	{
		[Export ("colorPickerDidChangeSelection:finishedSelection:")]
		void FinishedSelection (PSPDFColorPickerView colorPicker, bool finished);
	}

	[BaseType (typeof (UIControl))]
	interface PSPDFColorPickerView
	{
		[Export ("selection", ArgumentSemantic.Assign)]
		PointF Selection { get; }

		[Export ("cropToCircle", ArgumentSemantic.Assign)]
		bool CropToCircle { get; set; }

		[Export ("isOrthogonal", ArgumentSemantic.Assign)]
		bool IsOrthogonal { get; set; }

		[Export ("loupeEnabled", ArgumentSemantic.Assign)]
		bool LoupeEnabled { [Bind ("isLoupeEnabled")] get; set; }

		[Export ("selectionColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("brightness", ArgumentSemantic.Assign)]
		float Brightness { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFColorPickerViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("brightnessSlider", ArgumentSemantic.Assign)]
		PSPDFBrightnessSlider BrightnessSlider { get; set; }

		[Export ("selectionToHue:saturation:brightness:")]
		void SelectionToHue (float hue, float saturation, float brightness);

		[Export ("colorAtPoint:")]
		UIColor ColorAtPoint (PointF point);
	}

	[BaseType (typeof (UISlider))]
	interface PSPDFBrightnessSlider
	{
		[Export ("backgroundStyle", ArgumentSemantic.Assign)]
		PSPDFSliderBackgroundStyle BackgroundStyle { get; set; }

		[Export ("colorPicker", ArgumentSemantic.Assign)]
		PSPDFColorPickerView ColorPicker { get; set; }

		[Export ("updateBackground")]
		void UpdateBackground ();
	}

	[BaseType (typeof (UILabel))]
	interface PSPDFRoundedLabel
	{
		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		float CornerRadius { get; set; }

		[Export ("rectColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor RectColor { get; set; }
	}

	interface IPSPDFDocumentSharingViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFDocumentSharingViewControllerDelegate : PSPDFOverridable
	{
		[Abstract]
		[Export ("documentSharingViewController:didFinishWithSelectedOptions:resultingObjects:fileNames:annotationSummary:error:")]
		void DidFinish (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions selectedSharingOption, NSObject [] resultingObjects, string [] fileNames, string annotationSummary, NSError error);

		[Export ("documentSharingViewControllerDidCancel:")]
		void DidCancel (PSPDFDocumentSharingViewController shareController);

		[Export ("documentSharingViewController:shouldPrepareWithSelectedOptions:selectedPages:")]
		bool ShouldPrepare (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions selectedSharingOption, NSIndexSet selectedPages);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFDocumentSharingViewController : IPSPDFStyleable
	{
		[Export ("initWithDocument:visiblePages:allowedSharingOptions:delegate:")]
		IntPtr Constructor (PSPDFDocument document, [NullAllowed] NSOrderedSet visiblePages, PSPDFDocumentSharingOptions sharingOptions, PSPDFDocumentSharingViewControllerDelegate controllerDelegate);

		[Export ("checkIfControllerHasOptionsAvailableAndCallDelegateIfNot")]
		bool CheckIfControllerHasOptionsAvailableAndCallDelegateIfNot ();

		[Export ("document", ArgumentSemantic.Retain)]
		PSPDFDocument Document { get; }

		[Export ("visiblePages", ArgumentSemantic.Copy)] [NullAllowed]
		NSOrderedSet VisiblePages { get; set; }

		[Export ("sharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions SharingOptions { get; set; }

		[Export ("commitButtonTitle", ArgumentSemantic.Copy)]
		string CommitButtonTitle { get; set; }

		[Wrap ("WeakDelegate")]
		PSPDFDocumentSharingViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("isInPopover", ArgumentSemantic.Assign)]
		bool IsInPopover { get; set; }

		// PSPDFDocumentSharingViewController (SubclassingHooks) Category

		[Static]
		[Export ("annotationSummaryForDocument:pages:")]
		string AnnotationSummaryForDocument (PSPDFDocument document, NSIndexSet pages);
	}

	interface IPSPDFSignatureSelectorViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFSignatureSelectorViewControllerDelegate : PSPDFOverridable
	{
		[Export ("signatureSelectorViewController:didSelectSignature:")]
		void DidSelectSignature (PSPDFSignatureSelectorViewController signatureSelectorController, PSPDFInkAnnotation signature);

		[Export ("signatureSelectorViewControllerWillCreateNewSignature:")]
		void WillCreateNewSignature (PSPDFSignatureSelectorViewController signatureSelectorController);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFSignatureSelectorViewController : IPSPDFStyleable
	{
		[Export ("initWithSignatures:")]
		IntPtr Constructor (PSPDFInkAnnotation [] signature);

		[Export ("signatures", ArgumentSemantic.Copy)]
		PSPDFInkAnnotation [] Signatures { get; }

		[Wrap ("WeakDelegate")]
		PSPDFSignatureSelectorViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }
	}

	interface IPSPDFStyleable { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFStyleable
	{
		[Export ("tintColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor TintColor { get; set; }

		[Export ("barStyle", ArgumentSemantic.Assign)]
		UIBarStyle BarStyle { get; set; }

		[Export ("isBarTranslucent", ArgumentSemantic.Assign)]
		bool IsBarTranslucent { get; set; }

		[Export ("isInPopover", ArgumentSemantic.Assign)]
		bool IsInPopover { get; set; }

		[Export ("shouldTintToolbarButtons", ArgumentSemantic.Assign)]
		bool ShouldTintToolbarButtons { get; set; }

		[Export ("shouldTintAlertView", ArgumentSemantic.Assign)]
		bool ShouldTintAlertView { get; set; }
	}

	interface IPSPDFResizableViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFResizableViewDelegate
	{
		[Export ("resizableViewDidBeginEditing:")]
		void DidBeginEditing (PSPDFResizableView resizableView);

		[Export ("resizableViewChangedFrame:")]
		void ChangedFrame (PSPDFResizableView resizableView);

		[Export ("resizableViewDidEndEditing:")]
		void DidEndEditing (PSPDFResizableView resizableView);
	}

	interface IPSPDFResizableTrackedViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFResizableTrackedViewDelegate
	{
		[Export ("resizableView", ArgumentSemantic.Assign)]
		PSPDFResizableView ResizableView { get; set; }

		[Export ("annotation", ArgumentSemantic.Retain)]
		PSPDFAnnotation Annotation { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFResizableView : IPSPDFLongPressGestureRecognizerDelegate
	{
		[Export ("initWithTrackedView:")]
		IntPtr Constructor (UIView trackedView);

		[Export ("trackedViews", ArgumentSemantic.Copy)]
		NSSet TrackedViews { get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		float ZoomScale { get; set; }

		[Export ("innerEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets InnerEdgeInsets { get; set; }

		[Export ("outerEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets OuterEdgeInsets { get; set; }

		[Export ("effectiveInnerEdgeInsets")]
		UIEdgeInsets EffectiveInnerEdgeInsets { get; }

		[Export ("effectiveOuterEdgeInsets")]
		UIEdgeInsets EffectiveOuterEdgeInsets { get; }

		[Export ("allowEditing", ArgumentSemantic.Assign)]
		bool AllowEditing { get; set; }

		[Export ("allowResizing", ArgumentSemantic.Assign)]
		bool AllowResizing { get; set; }

		[Export ("allowAdjusting", ArgumentSemantic.Assign)]
		bool AllowAdjusting { get; set; }

		[Export ("enableResizingGuides", ArgumentSemantic.Assign)]
		bool EnableResizingGuides { get; set; }

		[Export ("guideSnapAllowance", ArgumentSemantic.Assign)]
		float GuideSnapAllowance { get; set; }

		[Export ("minWidth", ArgumentSemantic.Assign)]
		float MinWidth { get; set; }

		[Export ("minHeight", ArgumentSemantic.Assign)]
		float MinHeight { get; set; }

		[Export ("preventsPositionOutsideSuperview", ArgumentSemantic.Assign)]
		bool PreventsPositionOutsideSuperview { get; set; }

		[Export ("selectionBorderColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor SelectionBorderColor { get; set; }

		[Export ("guideBorderColor", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor GuideBorderColor { get; set; }

		[Export ("longPress:")]
		bool LongPress (UILongPressGestureRecognizer recognizer);

		[Wrap ("WeakDelegate")]
		PSPDFResizableViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("contentFrame", ArgumentSemantic.Assign)]
		RectangleF ContentFrame { get; set; }

		[Export ("mode", ArgumentSemantic.Assign)]
		PSPDFResizableViewMode Mode { get; set; }

		[Export ("pageView", ArgumentSemantic.Assign)]
		PSPDFPageView PageView { get; set; }

		// PSPDFResizableView (SubclassingHooks) Category

		[Export ("knobTopLeft", ArgumentSemantic.Retain)]
		UIImageView KnobTopLeft { get; }

		[Export ("knobTopMiddle", ArgumentSemantic.Retain)]
		UIImageView KnobTopMiddle { get; }

		[Export ("knobTopRight", ArgumentSemantic.Retain)]
		UIImageView KnobTopRight { get; }

		[Export ("knobMiddleLeft", ArgumentSemantic.Retain)]
		UIImageView KnobMiddleLeft { get; }

		[Export ("knobMiddleRight", ArgumentSemantic.Retain)]
		UIImageView KnobMiddleRight { get; }

		[Export ("knobBottomLeft", ArgumentSemantic.Retain)]
		UIImageView KnobBottomLeft { get; }

		[Export ("knobBottomMiddle", ArgumentSemantic.Retain)]
		UIImageView KnobBottomMiddle { get; }

		[Export ("knobBottomRight", ArgumentSemantic.Retain)]
		UIImageView KnobBottomRight { get; }

		[Export ("outerKnobImage", ArgumentSemantic.Retain)]
		UIImage OuterKnobImage { get; }

		[Export ("innerKnobImage", ArgumentSemantic.Retain)]
		UIImage InnerKnobImage { get; }

		[Export ("trackedAnnotation", ArgumentSemantic.Retain)]
		PSPDFAnnotation TrackedAnnotation { get; }

		[Export ("updateKnobsAnimated:")]
		void UpdateKnobsAnimated (bool animated);
	}

	[BaseType (typeof (UILongPressGestureRecognizer))]
	interface PSPDFLongPressGestureRecognizer
	{

	}

	interface IPSPDFLongPressGestureRecognizerDelegate { }

	[BaseType (typeof (UIGestureRecognizerDelegate))]
	[Model]
	[Protocol]
	interface PSPDFLongPressGestureRecognizerDelegate
	{
		[Abstract]
		[Export ("pressRecognizerShouldHandlePressImmediately:")]
		bool PressRecognizerShouldHandlePressImmediately (PSPDFLongPressGestureRecognizer recognizer);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFFontInfo
	{
		[Export ("name", ArgumentSemantic.Copy)]
		string Name { get; }

		[Export ("fontKey", ArgumentSemantic.Copy)]
		string FontKey { get; }

		[Export ("ascent", ArgumentSemantic.Assign)]
		float Ascent { get; }

		[Export ("descent", ArgumentSemantic.Assign)]
		float Descent { get; }

		[Export ("encodingArray", ArgumentSemantic.Retain)]
		NSObject [] EncodingArray { get; }

		[Export ("toUnicodeMap", ArgumentSemantic.Retain)]
		NSObject ToUnicodeMap { get; }

		[Export ("fontCMap", ArgumentSemantic.Retain)]
		NSObject FontCMap { get; }

		[Export ("ucsCMap", ArgumentSemantic.Retain)]
		NSObject UcsCMap { get; }

		[Export ("initWithFontDictionary:fontKey:")] [Internal]
		IntPtr Constructor (IntPtr /* CGPDFDictionary */ font, string fontKey);

		[Export ("widthForCharacter:")]
		float WidthForCharacter (ushort character);

		[Export ("isMultiByteFont", ArgumentSemantic.Assign)]
		bool IsMultiByteFont { get; }

		[Static]
		[Export ("glyphNames")]
		NSDictionary GlyphNames { get; }

		[Static]
		[Export ("standardFontWidths")]
		NSDictionary StandardFontWidths { get; }

		[Export ("isEqualToFontInfo:")]
		bool IsEqualToFontInfo (PSPDFFontInfo otherFontInfo);

		[Export ("unicodeStringForCharacter:")]
		string UnicodeStringForCharacter (ushort character);
	}

	interface IPSPDFAnnotationGridViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationGridViewControllerDelegate : PSPDFOverridable
	{
		[Export ("annotationGridViewControllerDidCancel:")]
		void DidCancel (PSPDFAnnotationGridViewController annotationGridController);

		[Export ("annotationGridViewController:didSelectAnnotationSet:")]
		void DidSelectAnnotationSet (PSPDFAnnotationGridViewController annotationGridController, PSPDFAnnotationSet annotationSet);
	}

	interface IPSPDFAnnotationGridViewControllerDataSource { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFAnnotationGridViewControllerDataSource
	{
		[Abstract]
		[Export ("numberOfSectionsInAnnotationGridViewController:")]
		int NumberOfSections (PSPDFAnnotationGridViewController annotationGridController);

		[Abstract]
		[Export ("annotationGridViewController:numberOfAnnotationsInSection:")]
		int NumberOfAnnotationsInSection (PSPDFAnnotationGridViewController annotationGridController, int section);

		[Abstract]
		[Export ("annotationGridViewController:annotationSetForIndexPath:")]
		PSPDFAnnotationSet AnnotationSetForIndexPath (PSPDFAnnotationGridViewController annotationGridController, NSIndexPath indexPath);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFAnnotationGridViewController : IPSPDFStyleable
	{
		[Export ("initWithDelegate:")]
		IntPtr Constructor (PSPDFAnnotationGridViewControllerDelegate controllerDelegate);

		[Wrap ("WeakDelegate")]
		PSPDFAnnotationGridViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakDataSource")]
		PSPDFAnnotationGridViewControllerDataSource DataSource { get; set; }

		[Export ("dataSource", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDataSource { get; set; }

		[Export ("userInfo", ArgumentSemantic.Copy)] [NullAllowed]
		NSDictionary UserInfo { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		// PSPDFAnnotationGridViewController (SubclassingHooks) Category

		[Export ("close:")]
		void Close (NSObject sender);

		[Export ("configureCell:forIndexPath:")]
		void ConfigureCell (PSPDFAnnotationSetCell annotationSetCell, NSIndexPath indexPath);

		[Export ("gridView", ArgumentSemantic.Retain)] [NullAllowed]
		PSTCollectionView GridView { get; set; }
	}

	[BaseType (typeof (PSTCollectionViewCell))]
	interface PSPDFAnnotationSetCell
	{
		[Export ("annotationSet", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFAnnotationSet AnnotationSet { get; set; }
	}

	interface IPSPDFTextStampViewControllerDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSPDFTextStampViewControllerDelegate : PSPDFOverridable
	{
		[Export ("textStampViewController:didCreateAnnotation:")]
		void DidCreateAnnotation (PSPDFTextStampViewController stampController, PSPDFStampAnnotation stampAnnotation);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFTextStampViewController
	{
		[Export ("initWithStampAnnotation:")]
		IntPtr Constructor (PSPDFStampAnnotation stampAnnotation);

		[Wrap ("WeakDelegate")]
		PSPDFTextStampViewControllerDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		[Export ("stampAnnotation", ArgumentSemantic.Retain)]
		PSPDFStampAnnotation StampAnnotation { get; }

		[Export ("defaultStampText", ArgumentSemantic.Copy)] [NullAllowed]
		string DefaultStampText { get; set; }
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFStaticTableViewController
	{
		[Export ("sections", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFSectionModel [] Sections { get; set; }

		[Export ("updateVisibleCells")]
		void UpdateVisibleCells ();
	}

	delegate void PSPDFCellModelActionHandler (PSPDFStaticTableViewController viewController, UITableViewCell cell);
	delegate void PSPDFCellModelUpdateHandler (PSPDFStaticTableViewController viewController, UITableViewCell cell);
	delegate void PSPDFCellModelCreateHandler (PSPDFStaticTableViewController viewController, UITableViewCell cell);

	[BaseType (typeof (NSObject))]
	interface PSPDFCellModel
	{
		[Static]
		[Export ("cellWithTitle:")]
		PSPDFCellModel FromTitle (string title);

		[Export ("section", ArgumentSemantic.Assign)]
		PSPDFSectionModel Section { get; set; }

		[Export ("title", ArgumentSemantic.Copy)]
		string Title { get; set; }

		[Export ("subtitle", ArgumentSemantic.Copy)]
		string Subtitle { get; set; }

		[Export ("accessoryView", ArgumentSemantic.Retain)] [NullAllowed]
		UIView AccessoryView { get; set; }

		[Export ("cellClass", ArgumentSemantic.Retain)] [NullAllowed]
		Class CellClass { get; set; }

		[Export ("selectionStyle", ArgumentSemantic.Assign)]
		UITableViewCellSelectionStyle SelectionStyle { get; set; }

		[Export ("accessoryType", ArgumentSemantic.Assign)]
		UITableViewCellAccessory AccessoryType { get; set; }

		[Export ("height", ArgumentSemantic.Assign)]
		float Height { get; set; }

		[Export ("userInfo", ArgumentSemantic.Copy)] [NullAllowed]
		NSDictionary UserInfo { get; set; }

		[Export ("checked", ArgumentSemantic.Assign)]
		bool Checked { [Bind ("isChecked")] get; set; }

		[Export ("setActionBlock:")]
		void SetActionHandler (PSPDFCellModelActionHandler actionHandler);

		[Export ("setUpdateBlock:")]
		void SetUpdateHandler (PSPDFCellModelUpdateHandler updateHandler);

		[Export ("setCreateBlock:")]
		void SetCreateHandler (PSPDFCellModelCreateHandler createHandler);

		[Export ("heightForWidth:")]
		float HeightForWidth (float width);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSectionModel
	{
		[Static]
		[Export ("sectionWithTitle:")]
		PSPDFSectionModel FromTitle (string title);

		[Export ("title", ArgumentSemantic.Copy)]
		string Title { get; set; }

		[Export ("cells", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFCellModel [] Cells { get; set; }
	}

	[BaseType (typeof (PSPDFNeverAnimatingTableViewCell))]
	interface PSPDFColorCell
	{
		[Export ("color", ArgumentSemantic.Retain)] [NullAllowed]
		UIColor Color { get; set; }
	}

	[BaseType (typeof (PSPDFColorCell))]
	interface PSPDFLineEndCell
	{
		[Export ("setLineEnd:annotation:forStart:")]
		void SetLineEnd (PSPDFLineEndType lineEnd, PSPDFAnnotation annotation, bool isStart);
	}

	[BaseType (typeof (PSPDFSectionModel))]
	interface PSPDFCheckboxSectionModel
	{
		[Export ("checkedCellModel", ArgumentSemantic.Retain)] [NullAllowed]
		PSPDFCellModel CheckedCellModel { get; set; }
	}

	[BaseType (typeof (PSPDFCellModel))]
	interface PSPDFCheckBoxCellModel
	{

	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFTableViewCell
	{
		[Export ("performBlockWithoutAnimationIfResizingPopover:")]
		void PerformBlockWithoutAnimationIfResizingPopover (NSAction handler);
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFNonAnimatingTableViewCell
	{

	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFNeverAnimatingTableViewCell
	{

	}

//	PSPDFAttributedLabel Bind????

	// PSTCollectionView

	delegate void PSTCollectionViewPerformBatchUpdatesCompletionHandler (bool finished);

	[BaseType (typeof (UIScrollView))]
	interface PSTCollectionView
	{
		[Export("initWithFrame:collectionViewLayout:")]
		IntPtr Constructor (RectangleF rect, PSTCollectionViewLayout layout);

		[Export ("collectionViewLayout", ArgumentSemantic.Retain)] [NullAllowed]
		PSTCollectionViewLayout CollectionViewLayout { get; set; }

		[Wrap ("WeakDelegate")] [New]
		PSTCollectionViewDelegate Delegate { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed] [New]
		NSObject WeakDelegate { get; set; }

		[Wrap ("WeakDataSource")]
		PSTCollectionViewDataSource DataSource { get; set; }

		[Export ("dataSource", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDataSource { get; set; }

		[Export ("backgroundView", ArgumentSemantic.Retain)] [NullAllowed]
		UIView BackgroundView { get; set; }

		[Export("registerClass:forCellWithReuseIdentifier:")]
		void RegisterClass (Class cellClass, string identifier);

		[Export("registerClass:forSupplementaryViewOfKind:withReuseIdentifier:")]
		void RegisterClass (Class cellClass,string elementKind, string identifier);

		[Export("registerNib:forCellWithReuseIdentifier:")]
		void RegisterNib (UINib nib, string identifier);

		[Export("registerNib:forSupplementaryViewOfKind:withReuseIdentifier:")]
		void RegisterNib (UINib nib, string kind, string identifier);

		[Export("dequeueReusableCellWithReuseIdentifier:forIndexPath:")]
		NSObject DequeueReusableCellWithReuseIdentifier (string identifier, NSIndexPath indexPath);

		[Export("dequeueReusableSupplementaryViewOfKind:withReuseIdentifier:forIndexPath:")]
		NSObject DequeueReusableSupplementaryViewOfKind (string elementKind, string identifier, NSIndexPath indexPath);

		[Export ("allowsSelection")]
		bool AllowsSelection { get; set; }

		[Export ("allowsMultipleSelection")]
		bool AllowsMultipleSelection { get; set; }

		[Export ("indexPathsForSelectedItems")]
		NSIndexPath [] IndexPathsForSelectedItems { get; }

		[Export ("selectItemAtIndexPath:animated:scrollPosition:")]
		void SelectItemAtIndexPath (NSIndexPath indexPath, bool animated, PSTCollectionViewScrollPosition scrollPosition);

		[Export ("deselectItemAtIndexPath:animated:")]
		void DeselectItemAtIndexPath (NSIndexPath indexPath, bool animated);

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("setCollectionViewLayout:animated:")]
		void SetCollectionViewLayout (PSTCollectionViewLayout layout, bool animated);

		[Export ("numberOfSections")]
		int NumberOfSections ();

		[Export ("numberOfItemsInSection:")]
		int NumberOfItemsInSection (int section);

		[Export ("layoutAttributesForItemAtIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForItemAtIndexPath (NSIndexPath indexPath);

		[Export ("layoutAttributesForSupplementaryElementOfKind:atIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForSupplementaryElementOfKind (string kind, NSIndexPath indexPath);

		[Export ("indexPathForItemAtPoint:")]
		NSIndexPath IndexPathForItemAtPoint (PointF point);

		[Export ("indexPathForCell:")]
		NSIndexPath IndexPathForCell (PSTCollectionViewCell cell);

		[Export ("cellForItemAtIndexPath:")]
		PSTCollectionViewCell CellForItemAtIndexPath (NSIndexPath indexPath);

		[Export ("visibleCells")]
		NSObject [] VisibleCells ();

		[Export ("indexPathsForVisibleItems")]
		NSIndexPath [] IndexPathsForVisibleItems ();

		[Export ("scrollToItemAtIndexPath:atScrollPosition:animated:")]
		void ScrollToItemAtIndexPath (NSIndexPath indexPath, PSTCollectionViewScrollPosition scrollPosition, bool animated);

		[Export ("insertSections:")]
		void InsertSections (NSIndexSet sections);

		[Export ("deleteSections:")]
		void DeleteSections (NSIndexSet sections);

		[Export ("reloadSections:")]
		void ReloadSections (NSIndexSet sections);

		[Export ("moveSection:toSection:")]
		void MoveSection (int section, int newSection);

		[Export ("insertItemsAtIndexPaths:")]
		void InsertItemsAtIndexPaths (NSIndexPath [] indexPaths);

		[Export ("deleteItemsAtIndexPaths:")]
		void DeleteItemsAtIndexPaths (NSIndexPath [] indexPaths);

		[Export ("reloadItemsAtIndexPaths:")]
		void ReloadItemsAtIndexPaths (NSIndexPath [] indexPaths);

		[Export ("moveItemAtIndexPath:toIndexPath:")]
		void MoveItemAtIndexPath (NSIndexPath indexPath, NSIndexPath newIndexPath);

		[Export ("performBatchUpdates:completion:")]
		void MoveItemAtIndexPath (NSAction updates, PSTCollectionViewPerformBatchUpdatesCompletionHandler completion);
	}

	[BaseType (typeof (UIView))]
	interface PSTCollectionReusableView
	{
		[Export ("reuseIdentifier", ArgumentSemantic.Copy)]
		string ReuseIdentifier { get; }

		[Export("prepareForReuse")]
		void PrepareForReuse ();

		[Export("applyLayoutAttributes:")]
		void ApplyLayoutAttributes (PSTCollectionViewLayoutAttributes layoutAttributes);

		[Export("willTransitionFromLayout:toLayout:")]
		void WillTransitionFromLayout (PSTCollectionViewLayout oldLayout, PSTCollectionViewLayout newLayout);

		[Export("didTransitionFromLayout:toLayout:")]
		void DidTransitionFromLayout (PSTCollectionViewLayout oldLayout, PSTCollectionViewLayout newLayout);
	}

	[BaseType (typeof (PSTCollectionReusableView))]
	interface PSTCollectionViewCell
	{
		[Export ("contentView", ArgumentSemantic.Copy)]
		UIView ContentView { get; }

		[Export ("selected", ArgumentSemantic.Assign)]
		bool Selected { [Bind ("isSelected")] get; set; }

		[Export ("highlighted", ArgumentSemantic.Assign)]
		bool Highlighted { [Bind ("isHighlighted")] get; set; }

		[Export ("backgroundView", ArgumentSemantic.Retain)] [NullAllowed]
		UIView BackgroundView { get; set; }

		[Export ("selectedBackgroundView", ArgumentSemantic.Retain)] [NullAllowed]
		UIView SelectedBackgroundView { get; set; }
	}

	[BaseType (typeof (UIViewController))]
	interface PSTCollectionViewController
	{
		[Export("initWithCollectionViewLayout:")]
		IntPtr Constructor (PSTCollectionViewLayout layout);

		[Export ("collectionView", ArgumentSemantic.Retain)] [NullAllowed]
		PSTCollectionView CollectionView { get; set; }

		[Export ("clearsSelectionOnViewWillAppear", ArgumentSemantic.Assign)]
		bool ClearsSelectionOnViewWillAppear { get; set; }
	}

	interface IPSTCollectionViewDelegateFlowLayout { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSTCollectionViewDelegateFlowLayout : PSTCollectionViewDelegate
	{
		[Export ("collectionView:layout:sizeForItemAtIndexPath:")]
		SizeF SizeForItemAtIndexPath (PSTCollectionView collectionView, PSTCollectionViewLayout collectionViewLayout, NSIndexPath indexPath);

		[Export ("collectionView:layout:insetForSectionAtIndex:")]
		UIEdgeInsets InsetForSectionAtIndex (PSTCollectionView collectionView, PSTCollectionViewLayout collectionViewLayout, int section);

		[Export ("collectionView:layout:minimumLineSpacingForSectionAtIndex:")]
		float MinimumLineSpacingForSectionAtIndex (PSTCollectionView collectionView, PSTCollectionViewLayout collectionViewLayout, int section);

		[Export ("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
		float MinimumInteritemSpacingForSectionAtIndex (PSTCollectionView collectionView, PSTCollectionViewLayout collectionViewLayout, int section);

		[Export ("collectionView:layout:referenceSizeForHeaderInSection:")]
		SizeF ReferenceSizeForHeaderInSection (PSTCollectionView collectionView, PSTCollectionViewLayout collectionViewLayout, int section);

		[Export ("collectionView:layout:referenceSizeForFooterInSection:")]
		SizeF ReferenceSizeForFooterInSection (PSTCollectionView collectionView, PSTCollectionViewLayout collectionViewLayout, int section);

		[Export ("layoutAttributesForSupplementaryViewOfKind:atIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForSupplementaryViewOfKind (string kind, NSIndexPath indexPath);
	}

	[BaseType (typeof (PSTCollectionViewLayout))]
	interface PSTCollectionViewFlowLayout
	{
		[Field ("PSTCollectionElementKindSectionHeader", "__Internal")]
		NSString ElementKindSectionHeader { get; set; }

		[Field ("PSTCollectionElementKindSectionFooter", "__Internal")]
		NSString ElementKindSectionFooter { get; set; }

		[Export ("minimumLineSpacing")]
		float MinimumLineSpacing { get; set; }

		[Export ("minimumInteritemSpacing")]
		float MinimumInteritemSpacing { get; set; }

		[Export ("itemSize")]
		SizeF ItemSize { get; set; }

		[Export ("scrollDirection")]
		PSTCollectionViewScrollDirection ScrollDirection { get; set; }

		[Export ("headerReferenceSize")]
		SizeF HeaderReferenceSize { get; set; }

		[Export ("footerReferenceSize")]
		SizeF FooterReferenceSize { get; set; }

		[Export ("sectionInset")]
		UIEdgeInsets SectionInset { get; set; }

		[Export ("rowAlignmentOptions", ArgumentSemantic.Retain)] [NullAllowed]
		NSDictionary RowAlignmentOptions { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSTCollectionViewLayoutAttributes
	{
		[Export ("frame")]
		RectangleF Frame { get; set; }

		[Export ("center")]
		PointF Center { get; set; }

		[Export ("size")]
		SizeF Size { get; set; }

		[Export ("transform3D")]
		CATransform3D Transform3D { get; set; }

		[Export ("alpha")]
		float Alpha { get; set; }

		[Export ("zIndex")]
		int ZIndex { get; set; }

		[Export ("hidden")]
		bool Hidden { [Bind ("isHidden")] get; set; }

		[Export ("indexPath", ArgumentSemantic.Retain)] [NullAllowed]
		NSIndexPath IndexPath { get; set; }

		[Static]
		[Export("layoutAttributesForCellWithIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForCellWithIndexPath (NSIndexPath indexPath);

		[Static]
		[Export("layoutAttributesForSupplementaryViewOfKind:withIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForSupplementaryViewOfKind (string elementKind, NSIndexPath indexPath);

		[Static]
		[Export("layoutAttributesForDecorationViewOfKind:withIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForDecorationViewOfKind (string kind, NSIndexPath indexPath);
	}

	[BaseType (typeof (NSObject))]
	interface PSTCollectionViewLayout
	{
		[Export ("collectionView", ArgumentSemantic.Assign)]
		PSTCollectionView CollectionView { get; }

		[Export ("invalidateLayout")]
		void InvalidateLayout ();

		[Export ("registerClass:forDecorationViewOfKind:")]
		void RegisterClass (Class viewClass, string kind);

		[Export ("registerNib:forDecorationViewOfKind:")]
		void RegisterNib (UINib nib, string kind);

		// PSTCollectionViewLayout (SubclassingHooks) Category

		[Static]
		[Export ("layoutAttributesClass")]
		Class LayoutAttributesClass ();

		[Export ("prepareLayout")]
		void PrepareLayout ();

		[Export ("layoutAttributesForElementsInRect:")]
		PSTCollectionViewLayoutAttributes [] LayoutAttributesForElementsInRect (RectangleF rect);

		[Export ("layoutAttributesForItemAtIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForItemAtIndexPath (NSIndexPath indexPath);

		[Export ("layoutAttributesForSupplementaryViewOfKind:atIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForSupplementaryViewOfKind (string kind, NSIndexPath indexPath);

		[Export ("layoutAttributesForDecorationViewOfKind:atIndexPath:")]
		PSTCollectionViewLayoutAttributes LayoutAttributesForDecorationViewOfKind (string kind, NSIndexPath indexPath);

		[Export ("shouldInvalidateLayoutForBoundsChange:")]
		bool ShouldInvalidateLayoutForBoundsChange (RectangleF newBounds);

		[Export ("targetContentOffsetForProposedContentOffset:withScrollingVelocity:")]
		PointF TargetContentOffsetForProposedContentOffset (PointF proposedContentOffset, PointF velocity);

		[Export ("collectionViewContentSize")]
		SizeF CollectionViewContentSize ();

		// PSTCollectionViewLayout (UpdateSupportHooks) Category

		[Export ("prepareForCollectionViewUpdates:")]
		void PrepareForCollectionViewUpdates (PSTCollectionViewUpdateItem [] updateItems);

		[Export ("finalizeCollectionViewUpdates")]
		void FinalizeCollectionViewUpdates ();

		[Export ("initialLayoutAttributesForAppearingItemAtIndexPath:")]
		PSTCollectionViewLayoutAttributes InitialLayoutAttributesForAppearingItemAtIndexPath (NSIndexPath itemIndexPath);

		[Export ("finalLayoutAttributesForDisappearingItemAtIndexPath:")]
		PSTCollectionViewLayoutAttributes FinalLayoutAttributesForDisappearingItemAtIndexPath (NSIndexPath itemIndexPath);

		[Export ("initialLayoutAttributesForInsertedSupplementaryElementOfKind:atIndexPath:")]
		PSTCollectionViewLayoutAttributes InitialLayoutAttributesForInsertedSupplementaryElementOfKind (string elementKind, NSIndexPath elementIndexPath);

		[Export ("finalLayoutAttributesForDeletedSupplementaryElementOfKind:atIndexPath:")]
		PSTCollectionViewLayoutAttributes FinalLayoutAttributesForDeletedSupplementaryElementOfKind (string elementKind, NSIndexPath elementIndexPath);
	}

	[BaseType (typeof (NSObject))]
	interface PSTCollectionViewUpdateItem
	{
		[Export ("indexPathBeforeUpdate", ArgumentSemantic.Retain)]
		NSIndexPath IndexPathBeforeUpdate { get; }

		[Export ("indexPathAfterUpdate", ArgumentSemantic.Retain)]
		NSIndexPath IndexPathAfterUpdate { get; }

		[Export ("updateAction", ArgumentSemantic.Assign)]
		PSTCollectionUpdateAction UpdateAction { get; set; }

		[Export("initWithInitialIndexPath:finalIndexPath:updateAction:")]
		IntPtr Constructor (NSIndexPath arg1, NSIndexPath arg2, PSTCollectionUpdateAction arg3);

		[Export("initWithAction:forIndexPath:")]
		IntPtr Constructor (PSTCollectionUpdateAction arg1, NSIndexPath arg2);

		[Export("initWithOldIndexPath:newIndexPath:")]
		IntPtr Constructor (NSIndexPath arg1, NSIndexPath arg2);

		[Export("compareIndexPaths:")]
		NSComparisonResult CompareIndexPaths (PSTCollectionViewUpdateItem otherItem);

		[Export("inverseCompareIndexPaths:")]
		NSComparisonResult InverseCompareIndexPaths (PSTCollectionViewUpdateItem otherItem);
	}

	interface IPSTCollectionViewDataSource { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSTCollectionViewDataSource
	{
		[Abstract]
		[Export ("collectionView:numberOfItemsInSection:")]
		int DidSelectPage (PSTCollectionView collectionView, int section);

		[Abstract]
		[Export ("collectionView:cellForItemAtIndexPath:")]
		PSTCollectionViewCell CellForItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("numberOfSectionsInCollectionView:")]
		int NumberOfSectionsInCollectionView (PSTCollectionView collectionView);

		[Export ("collectionView:viewForSupplementaryElementOfKind:atIndexPath")]
		PSTCollectionReusableView ViewForSupplementaryElementOfKind (PSTCollectionView collectionView, string kind, NSIndexPath indexPath);
	}

	interface IPSTCollectionViewDelegate { }

	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface PSTCollectionViewDelegate : IUIScrollViewDelegate
	{
		[Export ("collectionView:shouldHighlightItemAtIndexPath:")]
		bool ShouldHighlightItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:didHighlightItemAtIndexPath:")]
		void DidHighlightItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:didUnhighlightItemAtIndexPath:")]
		void DidUnhighlightItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:shouldSelectItemAtIndexPath:")]
		bool ShouldSelectItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:shouldDeselectItemAtIndexPath:")]
		bool ShouldDeselectItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:didSelectItemAtIndexPath:")]
		void DidSelectItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:didDeselectItemAtIndexPath:")]
		void DidDeselectItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:didEndDisplayingCell:forItemAtIndexPath:")]
		void DidEndDisplayingCell (PSTCollectionView collectionView, PSTCollectionViewCell cell, NSIndexPath indexPath);

		[Export ("collectionView:didEndDisplayingSupplementaryView:forElementOfKind:atIndexPath:")]
		void DidEndDisplayingSupplementaryView (PSTCollectionView collectionView, PSTCollectionReusableView view, string elementKind, NSIndexPath indexPath);

		[Export ("collectionView:shouldShowMenuForItemAtIndexPath:")]
		bool ShouldShowMenuForItemAtIndexPath (PSTCollectionView collectionView, NSIndexPath indexPath);

		[Export ("collectionView:canPerformAction:forItemAtIndexPath:withSender:")]
		bool CanPerformAction (PSTCollectionView collectionView, Selector action, NSIndexPath indexPath, NSObject sender);

		[Export ("collectionView:performAction:forItemAtIndexPath:withSender:")]
		void PerformAction (PSTCollectionView collectionView, Selector action, NSIndexPath indexPath, NSObject sender);
	}
}