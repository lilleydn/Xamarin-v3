using System;

namespace PSPDFKit
{
	[Flags]
	public enum PSPDFLogLevelMask
	{
		Nothing  = 0,
		Error    = 1 << 0,
		Warning  = 1 << 1,
		Info     = 1 << 2,
		Verbose  = 1 << 3,
		All      = int.MaxValue
	}

	public enum PSPDFAnimate
	{
		Never,
		ModernDevices,
		Everywhere
	}

	public enum PSPDFPageTransition
	{
		ScrollPerPage,
		ScrollContinuous,
		Curl
	}

	public enum PSPDFViewMode
	{
		Document,
		Thumbnails
	}

	public enum PSPDFPageMode
	{
		Single,
		Double,
		Automatic
	}

	public enum PSPDFScrollDirection
	{
		Horizontal,
		Vertical
	}

	public enum PSPDFStatusBarStyle
	{
		Inherit,             /// Don't change status bar style, but show/hide status bar on HUD events.
		SmartBlack,          /// UIStatusBarStyleBlackOpaque on iPad, UIStatusBarStyleBlackTranslucent on iPhone.
		SmartBlackHideOnIpad,/// Similar to SmartBlack (iOS 7: white), but also hides statusBar on iPad.
		BlackOpaque,         /// Opaque Black everywhere. (iOS 7: white)
		Default,             /// Default status bar (iOS 6: white on iPhone/black on iPad iOS 7: Black).
		Disable              /// Never show status bar.
	}

	public enum PSPDFHUDViewMode
	{
		Always,
		Automatic,
		AutomaticNoFirstLastPage,
		Never
	}

	public enum PSPDFHUDViewAnimation
	{
		None,
		Fade,
		Slide
	}

	public enum PSPDFThumbnailBarMode
	{
		None,
		ScrobbleBar,
		Scrollable
	}

	public enum PSPDFLinkAction
	{
		None,
		AlertView,
		Scrollable,
		OpenSafari,
		InlineBrowser
	}

	public enum PSPDFPageRenderingMode
	{
		ThumbnailThenFullPage,
		ThumbnailIfInMemoryThenFullPage,
		FullPage,
		FullPageBlocking,
		ThumbnailThenRender,
		Render
	}

	[Flags]
	public enum PSPDFTextSelectionMenuAction : uint
	{
		Search              = 1 << 0, /// Allow search from selected text.
		Define              = 1 << 1, /// Starting with iOS 7, this will also offer Wikipedia in the controller.
		WikipediaAsFallback = 1 << 2, /// Only displayed if Define fails/is missing. Ignored as of iOS 7.
		All                 = uint.MaxValue
	}

	public enum PSPDFAnnotationSaveMode
	{
		Disabled,
		ExternalFile,
		Embedded,
		EmbeddedWithExternalFileAsFallback
	}

	[Flags]
	public enum PSPDFTextCheckingType : uint
	{
		None        = 0,
		Link        = 1 << 0,  // URLs
		PhoneNumber = 1 << 1,  // Phone numbers
		All			= uint.MaxValue
	}

	public enum PSPDFShadowStyle
	{
		Flat,
		Curl
	}

	public enum PSPDFCacheStatus : uint
	{
		NotCached,
		InMemory,
		OnDisk
	}

	public enum PSPDFDiskCacheStrategy
	{
		Nothing,    // No files are saved. (slowest)
		Thumbnails, // Only thumbnails are cached to disk.
		NearPages,  // Only a few files are saved and all thumbnails.
		Everything  // The whole PDF document is converted to images and saved. (fastest)
	}

	[Flags]
	public enum PSPDFCacheOptions : uint
	{
		MemoryStoreIfVisible      = 0,       // Default. Store into the memory cache if document is visible.
		MemoryStoreAlways         = 1,       // Always store into the memory cache.
		MemoryStoreNever          = 2,       // Never store into memory cache (unless it's already there)

		DiskLoadAsyncAndPreload   = 0 << 3,  // Default. Queue disk load and preload.
		DiskLoadAsync             = 1 << 3,  // Queue disk load, don't decompress JPG.
		DiskLoadSyncAndPreload    = 2 << 3,  // Load image on current thread + decompress.
		DiskLoadSync              = 3 << 3,  // Load image on current thread.
		DiskLoadSkip              = 4 << 3,  // Don't access the disk cache.

		RenderQueue               = 0 << 6,  // Default. Queue up request.
		RenderQueueBackground     = 1 << 6,  // Queue, but with a very low priority.
		RenderSync                = 2 << 6,  // If needed, render on current thread.
		RenderSkip                = 3 << 6,  // Don't render, don't queue.

		ActualityCheckAndRequest  = 0 << 9,  // Default. Return image, potentially queue for re-render.
		ActualityIgnore           = 1 << 9,  // Ignore cache actuality, simply return an image.

		SizeRequireAboutExact     = 0 << 12,  // Default. Requires the exact size, allows 2 pixel tolerance/rounding errors.
		SizeRequireExact          = 1 << 12,  // Requires the exact size.
		SizeAllowLarger           = 2 << 12,  // Allow downscaling of larger sizes.
		SizeAllowLargerScaleSync  = 3 << 12,  // Resizes the image if size is substantially different, sync.
		SizeAllowLargerScaleAsync = 4 << 12,  // Resizes the image if size is substantially different, async.
		SizeGetLargestAvailable   = 5 << 12,  // Returns the largest available image.
		SizeAllowSmaller          = 6 << 12,  // Returns an image equal to or smaller to given size.
	}
	
	public enum PSPDFActionType : byte
	{
		Url,
		GoTo,
		RemoteGoTo,
		Named,
		Launch,
		JavaScript,
		Rendition,
		RichMediaExecute, // See Adobe® Supplement to the ISO 32000, Page 40ff.
		SubmitForm,
		ResetForm,

		// Unimplemented actions
		Sound,
		Movie,
		Hide,   // Implemented
		Thread,
		ImportData,
		SetOCGState,
		Trans,
		GoTo3DView,
		GoToEmbedded,

		Unknown = byte.MaxValue
	}

	public enum PSPDFGradientViewDirection
	{
		Horizontal,
		Vertical
	}

	[Flags]
	public enum PSPDFFeatureMask : uint
	{
		None              = 0,
		PDFViewer         = 1 << 0,
		TextSelection     = 1 << 1,
		StrongEncryption  = 1 << 2, // Not available in the demo.
		PDFCreation       = 1 << 3,
		AnnotationEditing = 1 << 4,
		AcroForms         = 1 << 5,
		IndexedFTS        = 1 << 6,
		All               = uint.MaxValue
	}

	public enum PSPDFRenderQueuePriority : uint
	{
		VeryLow,  // Used to re-render annotation changes.
		Low,      // Low and ReallyLow are used from within PSPDFCache.
		Normal,   // Life page renderings.
		High,     // Zoomed renderings.
		VeryHigh  // Highest priority. Unused.
	}

	public enum PSPDFNamedActionType : uint
	{
		None,
		NextPage,
		PreviousPage,
		FirstPage,
		LastPage,
		GoBack,
		GoForward,
		GoToPage, // not implemented
		Find,
		Print,    // not implemented
		Outline,
		Search,
		Brightness,
		ZoomIn,   // not implemented
		ZoomOut,  // not implemented
		SaveAs,   // Will simply trigger document.SaveChangedAnnotations
		Unknown = uint.MaxValue
	}

	public enum PSPDFRenditionActionType : uint
	{
		PlayStop,
		Stop,
		Pause,
		Resume,
		Play
	}

	[Flags]
	public enum PSPDFSubmitFormActionFlag : uint
	{
		IncludeExclude       = 1 << (1-1),
		IncludeNoValueFields = 1 << (2-1),
		ExportFormat         = 1 << (3-1),
		GetMethod            = 1 << (4-1),
		SubmitCoordinates    = 1 << (5-1),
		XFDF                 = 1 << (6-1),
		IncludeAppendSaves   = 1 << (7-1),
		IncludeAnnotations   = 1 << (8-1),
		SubmitPDF            = 1 << (9-1),
		CanonicalFormat      = 1 << (10-1),
		ExclNonUserAnnots    = 1 << (11-1),
		ExclFKey             = 1 << (12-1),
		EmbedForm            = 1 << (14-1),
	}

	[Flags]
	public enum PSPDFResetFormActionFlag : uint
	{
		IncludeExclude = 1 << (1-1)
	}

	public enum PSPDFLabelStyle : uint
	{
		Flat,     // <= iOS6 default.
		Bordered,
		Modern,   // iOS7 and newer.
	}

	public enum PSPDFSearchMode
	{
		Basic,        // don't show highlight positions
		Highlighting, // show highlights
	}

	public enum PSPDFTextLineBorder
	{
		Undefined = 0,
		TopDown,  // 1
		BottomUp, // 2
		None      // 3
	}

	public enum PSPDFSearchStatus
	{
		Idle,
		Active,
		Finished,
		Cancelled
	}

	public enum PSPDFAttributedLabelVerticalAlignment
	{
		Center   = 0,
		Top      = 1,
		Bottom   = 2
	}

	public enum PSPDFThumbnailViewFilter : uint
	{
		ShowAll,     // Show all thumbnails.
		Bookmarks,   // Show bookmarked thumbnails.
		Annotations, // All annotation types except links. PSPDFKit Annotate only.
	}

	public enum PSPDFStatefulTableViewState : uint
	{
		Loading,  // Controller is querying data
		Empty,    // Controller finished loading, has no data.
		Finished  // Controller has data.
	}

	[Flags]
	public enum PSPDFAnnotationType : uint
	{
		None        = 0,
		Undefined   = 1 << 0,  // Any annotation whose type couldn't be recognized.
		Link        = 1 << 1,  // Links and PSPDFKit multimedia extensions.
		Highlight   = 1 << 2,
		StrikeOut   = 1 << 17,
		Underline   = 1 << 18,
		Squiggly    = 1 << 19,
		FreeText    = 1 << 3,
		Ink         = 1 << 4,  // Ink (includes Signatures)
		Square      = 1 << 5,
		Circle      = 1 << 20,
		Line        = 1 << 6,
		Note        = 1 << 7,
		Stamp       = 1 << 8,  // A stamp can be an image as well.
		Caret       = 1 << 9,
		RichMedia   = 1 << 10, // Embedded PDF video
		Screen      = 1 << 11, // Embedded PDF video
		Widget      = 1 << 12, // Widget (includes special links all form types)
		File        = 1 << 13, // FileAttachment
		Sound       = 1 << 14,
		Polygon     = 1 << 15,
		PolyLine    = 1 << 16,
		Popup       = 1 << 21, // Not yet supported.
		All         = uint.MaxValue
	}

	public enum PSPDFAnnotationBorderStyle : uint
	{
		None,
		Solid,
		Dashed,    // Not yet supported.
		Belved,    // Not yet supported.
		Inset,     // Not yet supported.
		Underline, // Not yet supported.
		Unknown
	}

	[Flags]
	public enum PSPDFAnnotationFlags : uint
	{
		Invisible      = 1 << 0, // If set, ignore annotation AP stream if there is no handler available.
		Hidden         = 1 << 1, // If set, do not display or print the annotation or allow it to interact with the user.
		Print          = 1 << 2, // [IGNORED] If set, print the annotation when the page is printed. Default value.
		NoZoom         = 1 << 3, // [IGNORED] If set, don't scale the annotation’s appearance to match the magnification of the page.
		NoRotate       = 1 << 4, // [IGNORED] If set, don't rotate the annotation’s appearance to match the rotation of the page.
		NoView         = 1 << 5, // [IGNORED] If set, don't display the annotation on the screen. (But printing might be allowed)
		ReadOnly       = 1 << 6, // [IGNORED] If set, don't allow the annotation to interact with the user. Ignored for Widget.
		Locked         = 1 << 7, // [IGNORED] If set, don't allow the annotation to be deleted or properties modified (except contents)
		ToggleNoView   = 1 << 8, // [IGNORED] If set, invert the interpretation of the NoView flag for certain events.
		LockedContents = 1 << 9, // [IGNORED] If set, don't allow the contents of the annotation to be modified by the user.
	}

	public enum PSPDFAnnotationTriggerEvent : byte
	{
		CursorEnters,  // E
		CursorExits,   // X
		MouseDown,     // D
		MouseUp,       // U
		ReceiveFocus,  // Fo
		LooseFocus,    // Bl
		PageOpened,    // PO
		PageClosed,    // PC
		PageVisible,   // PV
		PageInvisible, // PI
	}

	public enum PSPDFUndoCoalescing : uint
	{
		/// Does not coalesce events with the same key at all but rather creates one new undo event for
		/// every single change.
		None,

		/// Coalesces events with the same key by time. Assuming that a key changes a number of times over a
		/// short period of time, only the initial value will be recorded.
		Timed,

		/// Puts all subsequent changes to one key into the same group. This means that constant changes
		/// of the same value will result in exactly one event, which restores the property to its initial value.
		All
	}

	public enum PSPDFLineEndType
	{
		None,
		Square,
		Circle,
		Diamond,
		OpenArrow,
		ClosedArrow,
		Butt,
		ReverseOpenArrow,
		ReverseClosedArrow,
		Slash
	}

	public enum PSPDFLinkAnnotationType : byte
	{
		Page = 0,
		WebURL,  // 1
		Document,// 2
		Video,   // 3
		YouTube, // 4
		Audio,   // 5
		Image,   // 6
		Browser, // 7
		Custom   // any annotation format that is not recognized is custom (e.g. tel://)
	}

	public enum PSPDFResizableViewMode : uint
	{
		Idle,   /// Nothing is currently happening.
		Move,   /// The annotation is being moved.
		Resize, /// The annotation is being resized.
		Adjust  /// The shape of the annotation is being adjusted.
	}

	[Flags]
	public enum PSPDFWebViewControllerAvailableActions : uint
	{
		None             = 0,
		OpenInSafari     = 1 << 0,
		MailLink         = 1 << 1,
		CopyLink         = 1 << 2,
		Print            = 1 << 3,
		StopReload       = 1 << 4,
		Back             = 1 << 5,
		Forward          = 1 << 6,
		// Following actions can only be used on iOS6+ with UIActivityViewController.
		Facebook         = 1 << 7,
		Twitter          = 1 << 8,
		Message          = 1 << 9,
		All              = 0xFFFFFF
	}

	public enum PSPDFColorPickerStyle : uint
	{
		Rainbow,
		Modern,
		Vintage,
		Monochrome,
		HSVPicker
	}

	public enum PSPDFIconType
	{
		Outline,
		Page,
		Thumbnails,
		BackArrow,
		BackArrowSmall,
		ForwardArrow,
		Email
	}

	[Flags]
	public enum PSPDFDocumentSharingOptions : uint
	{
		CurrentPageOnly              = 1<<0, // Only page set in .page of PSPDFViewController.
		VisiblePages                 = 1<<1, // All visible pages. (ignored if only one visible)
		AllPages                     = 1<<2, // Send whole document.

		EmbedAnnotations             = 1<<3, // Save annotations in the PDF.
		FlattenAnnotations           = 1<<4, // Render annotations into the PDF.
		AnnotationsSummary           = 1<<5, // Save annotations + add summary.
		RemoveAnnotations            = 1<<6, // Remove all annotations.

		OfferMergeFiles              = 1<<8, // Allow to choose between multiple files or merging.
		ForceMergeFiles              = 2<<8, // Forces file merging.
	}

	[Flags]
	public enum PSPDFPrintOptions : uint
	{
		DocumentOnly       = 1<<0,
		IncludeAnnotations = 1<<1
	}

	public enum PSPDFViewModeBarButtonStyle : uint
	{
		Toggle,
		Switch
	}

	public enum PSPDFOutlineBarButtonItemOption : uint
	{
		Outline,     // The outline (Table of Contents) controller.
		Bookmarks,   // Bookmark list controller.
		Annotations, // Annotation list controller. PSPDFKit Annotate only.
	}

	public enum PSPDFProgressHUDMaskType : uint
	{
		None = 1, // allow user interactions while HUD is displayed
		Clear,    // don't allow
		Black,    // don't allow and dim the UI in the back of the HUD
		Gradient  // don't allow and dim the UI with a a-la-alert-view bg gradient
	}

	public enum PSPDFSliderBackgroundStyle
	{
		Default = 0,
		Grayscale,
		Colorful,
	}

	public enum PSPDFPersistentCloseButtonMode : uint
	{
		None,
		Left,
		Right
	}

	public enum PSPDFLibraryIndexStatus : uint
	{
		Unknown, // Not in library
		Queued,
		Partial,
		PartialAndIndexing,
		Finished
	}

	public enum PSPDFCryptorErrorCode
	{
		FailedToInitCryptor = 100,
		FailedToProcessFile = 110,
		ErrorInvalidIV           = 200,
		ErrorWritingOutputFile   = 600
	}

	[Flags]
	public enum PSPDFFormElementFlag : uint
	{
		ReadOnly = 1 << (1-1),
		Required = 1 << (2-1),
		NoExport = 1 << (3-1)
	}

	[Flags]
	public enum PSPDFButtonFlag : uint
	{
		NoToggleToOff  = 1 << (15-1),
		Radio          = 1 << (16-1),
		PushButton     = 1 << (17-1),
		RadiosInUnison = 1 << (26-1)
	}

	[Flags]
	public enum PSPDFChoiceFlag : uint
	{
		Combo             = 1 << (18-1),
		Edit              = 1 << (19-1),
		Sort              = 1 << (20-1),
		MultiSelect       = 1 << (22-1),
		DoNotSpellCheck   = 1 << (23-1),
		CommitOnSelChange = 1 << (27-1)
	}

	[Flags]
	public enum PSPDFTextFieldFlag : uint
	{
		Multiline       = 1 << (13-1),
		Password        = 1 << (14-1),
		FileSelect      = 1 << (21-1),
		DoNotSpellCheck = 1 << (23-1),
		DoNotScroll     = 1 << (24-1),
		Comb            = 1 << (25-1),
		RichText        = 1 << (26-1)
	}

	public enum PSPDFTextInputFormat : uint
	{
		Normal,
		Number,
		Date,
		Time
	}

	public enum PSPDFSubmitFormActionFormat : uint
	{
		FDF,
		XFDF,
		HTML,
		PDF
	}

	public enum PSPDFErrorCode
	{
		PageInvalid = 100,
		UnableToOpenPDF = 200,
		UnableToGetPageReference = 210,
		UnableToGetStream = 211,
		PageRenderSizeIsEmpty = 220,
		PageRenderClipRectTooLarge = 230,
		PageRenderGraphicsContextNil = 240,
		DocumentLocked = 300,
		FailedToLoadAnnotations = 400,
		FailedToWriteAnnotations = 410,
		CannotEmbedAnnotations = 420,
		FailedToLoadBookmarks = 450,
		OutlineParser = 500,
		UnableToConvertToDataRepresentation = 600,
		RemoveCacheError = 700,
		FailedToConvertToPDF = 800,
		FailedToGeneratePDFInvalidArguments = 810,
		FailedToGeneratePDFDocumentInvalid = 820,
		FailedToUpdatePageObject = 850,
		MicPermissionNotGranted = 900,
		XFDFParserLackingInputStream = 1000,
		XFDFParserAlreadyCompleted = 1010,
		XFDFParserAlreadyStarted = 1020,
		XMLParserError = 1100,
		XFDFWriterCannotWriteToStream = 1200,
		FDFWriterCannotWriteToStream = 1250,
		SoundEncoderInvalidInput = 1300,
		GalleryInvalidManifest = 1400,
		InvalidRemoteContent = 1500,
		FeatureNotEnabled = 100000,
		Unknown = int.MaxValue
	}

	[Flags]
	public enum PSPDFFontInfoType : uint
	{
		Simple    = 1 << (1-1),
		Composite = 1 << (2-1)
	}

	public enum PSPDFGalleryViewControllerState : uint
	{
		// The view controller is currently not doing anything.
		Idle,

		// The manifest file is currently downloaded.
		Loading,

		// The manifest file has been downloaded and the view controller is ready.
		Ready,

		// The view controller could not download the manifest file because of a connection error.
		ConnectionError,

		// The view controller could download the manifest file but could not parse it.
		ManifestError
	}

	public enum PSPDFVerticalAlignment : uint
	{
		Top,
		Center,
		Bottom
	}







	[Flags]
	public enum PSTCollectionViewScrollPosition : uint
	{
		None                 = 0,

		// The vertical positions are mutually exclusive to each other, but are bitwise or-able with the horizontal scroll positions.
		// Combining positions from the same grouping (horizontal or vertical) will result in an NSInvalidArgumentException.
		Top                  = 1 << 0,
		CenteredVertically   = 1 << 1,
		Bottom               = 1 << 2,

		// Likewise, the horizontal positions are mutually exclusive to each other.
		Left                 = 1 << 3,
		CenteredHorizontally = 1 << 4,
		Right                = 1 << 5
	}

	public enum PSTCollectionElementCategory : uint
	{
		Cell,
		SupplementaryView,
		DecorationView
	}

	public enum PSTCollectionViewScrollDirection
	{
		Vertical,
		Horizontal
	}

	public enum PSTCollectionViewItemType : uint
	{
		Cell,
		SupplementaryView,
		DecorationView
	}

	public enum PSTCollectionUpdateAction
	{
		Insert,
		Delete,
		Reload,
		Move,
		None
	}
}

