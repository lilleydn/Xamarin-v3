using System;
using MonoTouch.CoreGraphics;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.UIKit;
using System.Runtime.InteropServices;
using MonoTouch.CoreAnimation;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace PSPDFKit
{
	public partial class PSPDFLicenseManager
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFSetLicenseKey")]
		private static extern uint _SetLicenseKey (IntPtr licenseKey);

		public static PSPDFFeatureMask SetLicenseKey (string licenseKey)
		{
			IntPtr licensePtr;
			PSPDFFeatureMask result;

			licensePtr = Marshal.StringToHGlobalAnsi (licenseKey);
			result = (PSPDFFeatureMask)_SetLicenseKey (licensePtr);

			Marshal.FreeHGlobal (licensePtr);

			return result;
		}
	}

	public partial class PSPDFKitGlobal
	{
		public static NSString ErrorDomain
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				var str = Dlfcn.GetStringConstant (RTLD_MAIN_ONLY, "PSPDFErrorDomain");
				return str;
			}
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFVersionString")]
		private static extern IntPtr _VersionString ();

		public static string VersionString
		{
			get 
			{
				IntPtr ptr = _VersionString();
				string val = (string) (NSString) Runtime.GetNSObject(ptr);
				return val;
			}
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFVersionDate")]
		private static extern IntPtr _VersionDate ();

		public static NSDate VersionDate
		{
			get 
			{
				IntPtr ptr = _VersionDate();
				NSDate val = (NSDate) Runtime.GetNSObject(ptr);
				return val;
			}
		}

		private static PSPDFLogLevelMask PSPDFLogLevel;
		public static PSPDFLogLevelMask LogLevel
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLogLevel");
				PSPDFLogLevel = (PSPDFLogLevelMask) Marshal.ReadInt32(ptr);

				return PSPDFLogLevel;
			}
			set 
			{
				PSPDFLogLevel = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLogLevel");
				Marshal.WriteInt32(ptr, (int)PSPDFLogLevel);
			}
		}

		private static PSPDFAnimate PSPDFAnimateOption;
		public static PSPDFAnimate AnimateOption
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAnimateOption");
				PSPDFAnimateOption = (PSPDFAnimate) Marshal.ReadInt32(ptr);

				return PSPDFAnimateOption;
			}
			set 
			{
				PSPDFAnimateOption = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAnimateOption");
				Marshal.WriteInt32(ptr, (int)PSPDFAnimateOption);
			}
		}

		private static bool PSPDFLowMemoryMode;
		public static bool LowMemoryMode
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLowMemoryMode");

				PSPDFLowMemoryMode = Convert.ToBoolean(Marshal.ReadByte(ptr));

				return PSPDFLowMemoryMode;
			}
			set 
			{
				PSPDFLowMemoryMode = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLowMemoryMode");

				Marshal.WriteByte (ptr, Convert.ToByte (PSPDFLowMemoryMode));
			}
		}

		private static float PSPDFInitialAnnotationLoadDelay;
		public static float InitialAnnotationLoadDelay
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				PSPDFInitialAnnotationLoadDelay = Dlfcn.GetFloat (RTLD_MAIN_ONLY, "PSPDFInitialAnnotationLoadDelay");
				return PSPDFInitialAnnotationLoadDelay;
			}
			set 
			{
				PSPDFInitialAnnotationLoadDelay = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFInitialAnnotationLoadDelay");

				unsafe 
				{
					float m = PSPDFInitialAnnotationLoadDelay;
					Marshal.WriteIntPtr (ptr, *(IntPtr*)&m);
				}
			}
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFSizeAspectRatioEqualToSize")]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		private static extern bool PSPDFSizeAspectRatioEqualToSize (IntPtr containerSize, IntPtr size);

		public static bool SizeAspectRatioEqualToSize (SizeF containerSize, SizeF size)
		{
			IntPtr containerPtr = Marshal.AllocHGlobal (Marshal.SizeOf (containerSize));
			IntPtr sizePtr = Marshal.AllocHGlobal (Marshal.SizeOf (size));
			bool result;

			try {
				Marshal.StructureToPtr ((object)containerSize, containerPtr, true);
				Marshal.StructureToPtr ((object)size, sizePtr, true);

				result = PSPDFSizeAspectRatioEqualToSize (containerPtr, sizePtr);
			}
			finally {
				Marshal.FreeHGlobal (containerPtr);
				Marshal.FreeHGlobal (sizePtr);
			}
			return result;
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFTrimString")]
		private static extern IntPtr _TrimString (IntPtr theString);

		public static string TrimString (string theString)
		{
			if (string.IsNullOrEmpty(theString)) 
				return string.Empty;

			IntPtr ptr = _TrimString(new NSString(theString).Handle);
			string val = (string) (NSString) Runtime.GetNSObject (ptr);
			return val;
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFIndexSetFromArray")]
		private static extern IntPtr _IndexSetFromArray (IntPtr array);

		public static NSIndexSet IndexSetFromArray (NSNumber [] array)
		{
			List<NSObject> obj = new List<NSObject>();

			foreach (var item in array)
				obj.Add (item);

			NSArray arr = NSArray.FromNSObjects (obj.ToArray());

			return new NSIndexSet (_IndexSetFromArray (arr.Handle));
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFIsControllerClassAndVisible")]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		private static extern bool PSPDFIsControllerClassAndVisible (IntPtr controller, IntPtr controllerClass);

		public static bool IsControllerClassAndVisible (NSObject controller, Class controllerClass)
		{
			return PSPDFIsControllerClassAndVisible (controller.Handle, controllerClass.Handle);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFLockRotation")]
		private static extern void _LockRotation();

		[Since(6,0)]
		public static void LockRotation()
		{
			_LockRotation();
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFUnlockRotation")]
		private static extern void _UnlockRotation();

		[Since(6,0)]
		public static void UnlockRotation()
		{
			_UnlockRotation();
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFEqualObjects")]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		private static extern bool PSPDFEqualObjects (IntPtr obj1, IntPtr obj2);

		public static bool EqualObjects (NSObject obj1, NSObject obj2)
		{
			return PSPDFEqualObjects (obj1.Handle, obj2.Handle);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFConvertViewPointToPDFPoint")]
		private static extern IntPtr PSPDFConvertViewPointToPDFPoint (IntPtr viewPoint, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static PointF ConvertViewPointToPDFPoint (PointF viewPoint, RectangleF cropBox, uint rotation, RectangleF bounds)
		{
			IntPtr viewPointPtr = Marshal.AllocHGlobal (Marshal.SizeOf (viewPoint));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			PointF result;

			try {
				Marshal.StructureToPtr ((object)viewPoint, viewPointPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertViewPointToPDFPoint (viewPointPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (PointF)Marshal.PtrToStructure (resultPtr, typeof(PointF));
			}
			finally {
				Marshal.FreeHGlobal (viewPointPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFConvertPDFPointToViewPoint")]
		private static extern IntPtr PSPDFConvertPDFPointToViewPoint (IntPtr pdfPoint, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static PointF ConvertPDFPointToViewPoint (PointF pdfPoint, RectangleF cropBox, uint rotation, RectangleF bounds)
		{
			IntPtr pdfPointPtr = Marshal.AllocHGlobal (Marshal.SizeOf (pdfPoint));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			PointF result;

			try {
				Marshal.StructureToPtr ((object)pdfPoint, pdfPointPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertPDFPointToViewPoint (pdfPointPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (PointF)Marshal.PtrToStructure (resultPtr, typeof(PointF));
			}
			finally {
				Marshal.FreeHGlobal (pdfPointPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFConvertPDFRectToViewRect")]
		private static extern IntPtr PSPDFConvertPDFRectToViewRect (IntPtr pdfRect, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static RectangleF ConvertPDFRectToViewRect (RectangleF pdfRect, RectangleF cropBox, uint rotation, RectangleF bounds)
		{
			IntPtr pdfRectPtr = Marshal.AllocHGlobal (Marshal.SizeOf (pdfRect));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			RectangleF result;

			try {
				Marshal.StructureToPtr ((object)pdfRect, pdfRectPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertPDFRectToViewRect (pdfRectPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (RectangleF)Marshal.PtrToStructure (resultPtr, typeof(RectangleF));
			}
			finally {
				Marshal.FreeHGlobal (pdfRectPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFConvertViewRectToPDFRect")]
		private static extern IntPtr PSPDFConvertViewRectToPDFRect (IntPtr viewRect, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static RectangleF ConvertViewRectToPDFRect (RectangleF viewRect, RectangleF cropBox, uint rotation, RectangleF bounds)
		{
			IntPtr viewRectPtr = Marshal.AllocHGlobal (Marshal.SizeOf (viewRect));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			RectangleF result;

			try {
				Marshal.StructureToPtr ((object)viewRect, viewRectPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertPDFRectToViewRect (viewRectPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (RectangleF)Marshal.PtrToStructure (resultPtr, typeof(RectangleF));
			}
			finally {
				Marshal.FreeHGlobal (viewRectPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}
	}

	public partial class PSPDFWord : NSObject
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFStringFromGlyphs")]
		private static extern IntPtr _StringFromGlyphs (IntPtr glyphs);

		public static string StringFromGlyphs (PSPDFGlyph [] glyphs, CGAffineTransform t, RectangleF boundingBox)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add (glyph);

			var arry = NSArray.FromNSObjects (objs.ToArray());

			var str = new NSString (_StringFromGlyphs (arry.Handle));
			return (string) str;
		}
	}

	public partial class PSPDFGlyph : NSObject
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFRectsFromGlyphs")]
		private static extern RectangleF [] _RectsFromGlyphs(IntPtr glyphs, CGAffineTransform t, RectangleF boundingBox);

		public static RectangleF [] RectsFromGlyphs(PSPDFGlyph [] glyphs, CGAffineTransform t, RectangleF boundingBox)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add(glyph);

			NSArray arry = NSArray.FromNSObjects(objs.ToArray());
			return _RectsFromGlyphs(arry.Handle, t, boundingBox);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFBoundingBoxFromGlyphs")]
		private static extern RectangleF _BoundingBoxFromGlyphs(IntPtr glyphs, CGAffineTransform t);

		public static RectangleF BoundingBoxFromGlyphs(PSPDFGlyph [] glyphs, CGAffineTransform t)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add(glyph);

			NSArray arry = NSArray.FromNSObjects(objs.ToArray());
			return _BoundingBoxFromGlyphs(arry.Handle, t);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFReduceGlyphsToColumn")]
		private static extern IntPtr _ReduceGlyphsToColumn(IntPtr glyphs);

		public static NSArray ReduceGlyphsToColumn (PSPDFGlyph [] glyphs)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add(glyph);

			NSArray arry = NSArray.FromNSObjects(objs.ToArray());
			return new NSArray(_ReduceGlyphsToColumn(arry.Handle));
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFGlyphIsOnSameLineSegmentAsGlyph")]
		[return: MarshalAsAttribute(UnmanagedType.Bool)]
		private static extern bool _IsOnSameLineSegmentAsGlyph (IntPtr glyph1, IntPtr glyph2);

		public static bool IsOnSameLineSegmentAsGlyph (PSPDFGlyph glyph1, PSPDFGlyph glyph2)
		{
			return _IsOnSameLineSegmentAsGlyph (glyph1.Handle, glyph2.Handle);
		}
	}

	public partial class PSPDFAnnotation : PSPDFModel
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFAnnotationTypeTextMarkup")]
		private static extern uint _AnnotationTypeTextMarkup ();

		public static PSPDFAnnotationType AnnotationTypeTextMarkup
		{
			get 
			{
				return (PSPDFAnnotationType) _AnnotationTypeTextMarkup ();
			}
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFStringFromAnnotationType")]
		private static extern IntPtr _StringFromAnnotationType (uint annotationType);

		public static string StringFromAnnotationType (PSPDFAnnotationType annotationType)
		{
			return (string) new NSString (_StringFromAnnotationType ((uint)annotationType));
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFAnnotationTypeFromString")]
		private static extern uint _AnnotationTypeFromString (IntPtr theString);

		public static PSPDFAnnotationType AnnotationTypeFromString (NSString theString)
		{
			return (PSPDFAnnotationType)_AnnotationTypeFromString (theString.Handle);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFAnnotationSupportLegacyFormat")]
		private static extern void _AnnotationSupportLegacyFormat (IntPtr unarchiver);

		public static void AnnotationSupportLegacyFormat (NSKeyedUnarchiver unarchiver)
		{
			_AnnotationSupportLegacyFormat (unarchiver.Handle);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFPostprocessAnnotationInLegacyFormat")]
		private static extern IntPtr _PostprocessAnnotationInLegacyFormat (IntPtr annotations);

		public static NSArray PostprocessAnnotationInLegacyFormat (PSPDFAnnotation [] annotations)
		{
			var objs = new List<NSObject>();

			foreach (var annotation in annotations)
				objs.Add(annotation);

			NSArray arry = NSArray.FromNSObjects(objs.ToArray ());
			var resArray = new NSArray (_PostprocessAnnotationInLegacyFormat (arry.Handle));

			return resArray;
		}
	}

	public partial class PSPDFBookmarkParser : NSObject
	{
		public virtual bool ClearAllBookmarksWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _ClearAllBookmarksWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual PSPDFBookmark [] LoadBookmarksWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				PSPDFBookmark [] ret = _LoadBookmarksWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual bool SaveBookmarksWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _SaveBookmarksWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFFileAnnotationProvider : NSObject
	{
		public virtual PSPDFAnnotation [] AnnotationsForPage (uint page, CGPDFPage pageRef)
		{
			return AnnotationsForPage_ (page, pageRef.Handle);
		}

		public virtual PSPDFAnnotation [] ParseAnnotationsForPage (uint page, CGPDFPage pageRef)
		{
			return ParseAnnotationsForPage_ (page, pageRef.Handle);
		}

		public virtual bool TryLoadAnnotationsFromFileWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _TryLoadAnnotationsFromFileWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual bool SaveAnnotationsWithOptions (NSDictionary options, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _SaveAnnotationsWithOptions (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual NSDictionary LoadAnnotationsWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				NSDictionary ret = _LoadAnnotationsWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFNoteAnnotation : PSPDFAnnotation
	{
		private static SizeF PSPDFNoteAnnotationViewFixedSize;
		public static SizeF ViewFixedSize
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				PSPDFNoteAnnotationViewFixedSize = Dlfcn.GetSizeF(RTLD_MAIN_ONLY, "PSPDFNoteAnnotationViewFixedSize");

				return PSPDFNoteAnnotationViewFixedSize;
			}
			set 
			{
				PSPDFNoteAnnotationViewFixedSize = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym(RTLD_MAIN_ONLY, "PSPDFNoteAnnotationViewFixedSize");
				Marshal.StructureToPtr((object) PSPDFNoteAnnotationViewFixedSize, ptr, true);
			}
		}
	}

	public partial class PSPDFStyleManager : NSObject
	{
		private static Class PSPDFStyleManagerClass;
		public static Class StyleManagerClass
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				var classPtr = Dlfcn.GetIntPtr (RTLD_MAIN_ONLY, "PSPDFStyleManagerClass");

				PSPDFStyleManagerClass = new Class (classPtr);

				return PSPDFStyleManagerClass;
			}
			set 
			{
				PSPDFStyleManagerClass = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFStyleManagerClass");

				Marshal.WriteIntPtr (ptr, PSPDFStyleManagerClass.Handle);
			}
		}
	}

	public partial class PSPDFIconGenerator : NSObject
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFApplyToolbarShadowToImage")]
		private static extern IntPtr _ApplyToolbarShadowToImage (IntPtr oldImage);

		public static UIImage ApplyToolbarShadowToImage (UIImage oldImage)
		{
			var imagePtr = _ApplyToolbarShadowToImage (oldImage.Handle);
			return new UIImage (imagePtr);
		}

		private static Class PSPDFIconGeneratorClass;
		public static Class IconGeneratorClass
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				var classPtr = Dlfcn.GetIntPtr (RTLD_MAIN_ONLY, "PSPDFIconGeneratorClass");

				PSPDFIconGeneratorClass = new Class (classPtr);

				return PSPDFIconGeneratorClass;
			}
			set 
			{
				PSPDFIconGeneratorClass = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFIconGeneratorClass");

				Marshal.WriteIntPtr (ptr, PSPDFIconGeneratorClass.Handle);
			}
		}
	}

	public partial class PSPDFOpenInBarButtonItem : PSPDFBarButtonItem
	{
		private static bool PSPDFCheckIfCompatibleAppsAreInstalled;
		public static bool CheckIfCompatibleAppsAreInstalled
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFCheckIfCompatibleAppsAreInstalled");

				PSPDFCheckIfCompatibleAppsAreInstalled = Convert.ToBoolean (Marshal.ReadByte(ptr));

				return PSPDFCheckIfCompatibleAppsAreInstalled;
			}
			set 
			{
				PSPDFCheckIfCompatibleAppsAreInstalled = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFCheckIfCompatibleAppsAreInstalled");

				Marshal.WriteByte (ptr, Convert.ToByte (PSPDFCheckIfCompatibleAppsAreInstalled));
			}
		}
	}

	public partial class PSPDFMenuItem : UIMenuItem
	{
		private static bool PSPDFAllowImagesForMenuItems;
		public static bool AllowImagesForMenuItems
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");

				PSPDFAllowImagesForMenuItems = Convert.ToBoolean (Marshal.ReadByte(ptr));

				return PSPDFAllowImagesForMenuItems;
			}
			set 
			{
				PSPDFAllowImagesForMenuItems = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");

				Marshal.WriteByte (ptr, Convert.ToByte (PSPDFAllowImagesForMenuItems));
			}
		}
	}

	public partial class PSPDFProcessor : NSObject
	{
		private static RectangleF PSPDFPaperSizeA4;
		public static RectangleF PaperSizeA4
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFPaperSizeA4");
				PSPDFPaperSizeA4 = (RectangleF)Marshal.PtrToStructure(ptr, typeof(RectangleF));

				return PSPDFPaperSizeA4;
			}
		}

		private static RectangleF PSPDFPaperSizeLetter;
		public static RectangleF PaperSizeLetter
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFPaperSizeLetter");
				PSPDFPaperSizeLetter = (RectangleF)Marshal.PtrToStructure(ptr, typeof(RectangleF));

				return PSPDFPaperSizeLetter;
			}
		}
	}

	public partial class PSPDFNavigationController : UINavigationController
	{
		public PSPDFNavigationController (Type navigationBarType, Type toolbarType) : base (navigationBarType, toolbarType)
		{
		}

		public PSPDFNavigationController (UIViewController rootViewController) : base (rootViewController)
		{
		}
	}

	public partial class PSPDFDocument : NSObject
	{
		public static PSPDFDocument FromDataProvider (CGDataProvider dataProvider)
		{
			return FromDataProvider_ (dataProvider.Handle);
		}

		public virtual PSPDFPageInfo PageInfoForPage (uint page, CGPDFPage pageRef)
		{
			return PageInfoForPage_ (page, pageRef.Handle);
		}

		public virtual bool EnsureDataDirectoryExists (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _EnsureDataDirectoryExists (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual bool SaveAnnotationsWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _SaveAnnotationsWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual RectangleF BoxRectForPage (CGPDFBox boxType, uint page, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				RectangleF ret = _BoxRectForPage (boxType, page, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		private static bool PSPDFUseLegacyUIDGenerationMethod;
		public static bool UseLegacyUIDGenerationMethod
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFUseLegacyUIDGenerationMethod");

				PSPDFUseLegacyUIDGenerationMethod = Convert.ToBoolean(Marshal.ReadByte(ptr));

				return PSPDFUseLegacyUIDGenerationMethod;
			}
			set 
			{
				PSPDFUseLegacyUIDGenerationMethod = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFUseLegacyUIDGenerationMethod");

				Marshal.WriteByte (ptr, Convert.ToByte (PSPDFUseLegacyUIDGenerationMethod));
			}
		}
	}

	public partial class PSPDFDocumentProvider : NSObject
	{
		public PSPDFDocumentProvider (CGDataProvider dataProvider, PSPDFDocument document) : this (dataProvider.Handle, document)
		{
		}

		public CGDataProvider DataProvider
		{
			get 
			{
				IntPtr ptr = this.DataProvider_;
				return new CGDataProvider(ptr);
			}
		}

		public virtual PSPDFPageInfo PageInfoForPage (uint page, CGPDFPage pageRef)
		{
			return PageInfoForPage_ (page, pageRef.Handle);
		}

		public virtual NSData DataRepresentationWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				NSData ret = _DataRepresentationWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual bool SaveAnnotationsWithOptions (NSDictionary options, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _SaveAnnotationsWithOptions (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFPageRenderer : NSObject
	{
		public virtual RectangleF RenderPageRef (CGPDFPage page, CGContext context, RectangleF rectangle, PSPDFPageInfo pageInfo, PSPDFAnnotation [] annotations, NSDictionary options)
		{
			return RenderPageRef_ (page.Handle, context, rectangle, pageInfo, annotations, options);
		}

		public virtual SizeF SetupGraphicsContext (CGPDFPage page, CGContext context, PointF point, double zoom, PSPDFPageInfo pageInfo, PSPDFAnnotation [] annotations, NSDictionary options)
		{
			return SetupGraphicsContext_ (page.Handle, context.Handle, point, zoom, pageInfo, annotations, options);
		}

		private static Class PSPDFPageRendererClass;
		public static Class PageRendererClass
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				var classPtr = Dlfcn.GetIntPtr (RTLD_MAIN_ONLY, "PSPDFPageRendererClass");

				PSPDFPageRendererClass = new Class (classPtr);

				return PSPDFPageRendererClass;
			}
			set 
			{
				PSPDFPageRendererClass = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFPageRendererClass");

				Marshal.WriteIntPtr (ptr, PSPDFPageRendererClass.Handle);
			}
		}
	}

	public partial class PSPDFSearchViewController : NSObject
	{
		private static uint PSPDFMinimumSearchLength;
		public static uint MinimumSearchLength
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFMinimumSearchLength");
				PSPDFMinimumSearchLength = (uint)Marshal.PtrToStructure(ptr, typeof(uint));

				return PSPDFMinimumSearchLength;
			}
			set 
			{
				PSPDFMinimumSearchLength = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFMinimumSearchLength");
				byte [] data = BitConverter.GetBytes (PSPDFMinimumSearchLength);
				Marshal.Copy (data, 0, ptr, data.Length); 
			}
		}
	}

	public partial class PSPDFLibrary : NSObject
	{
		private static uint PSPDFLibraryVersion;
		public static uint LibraryVersion
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLibraryVersion");
				PSPDFLibraryVersion = (uint)Marshal.PtrToStructure(ptr, typeof(uint));

				return PSPDFLibraryVersion;
			}
		}
	}

	public partial class PSPDFGoToAction : PSPDFAction
	{
		public PSPDFGoToAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef, NSMutableDictionary /* CFMutableDictionary */ pageCache) : this (actionDictionary.Handle, documentRef.Handle, pageCache)
		{
		}

		public static uint ResolveActionsWithNamedDestinations (PSPDFGoToAction [] actions, CGPDFDocument documentRef)
		{
			return ResolveActionsWithNamedDestinations_ (actions, documentRef.Handle);
		}
	}

	public partial class PSPDFRemoteGoToAction : PSPDFAction
	{
		public PSPDFRemoteGoToAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef, bool launch) : this (actionDictionary.Handle, documentRef.Handle, launch)
		{
		}
	}

	public partial class PSPDFNamedAction : PSPDFAction
	{
		public PSPDFNamedAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef) : this (actionDictionary.Handle, documentRef.Handle)
		{
		}
	}

	public partial class PSPDFJavaScriptAction : PSPDFAction
	{
		public PSPDFJavaScriptAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef) : this (actionDictionary.Handle, documentRef.Handle)
		{
		}
	}

	public partial class PSPDFRenditionAction : PSPDFAction
	{
		public PSPDFRenditionAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef) : this (actionDictionary.Handle, documentRef.Handle)
		{
		}
	}

	public partial class PSPDFRichMediaExecuteAction : PSPDFAction
	{
		public PSPDFRichMediaExecuteAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef) : this (actionDictionary.Handle, documentRef.Handle)
		{
		}
	}

	public partial class PSPDFSubmitFormAction : PSPDFAbstractFormAction
	{
		public PSPDFSubmitFormAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef) : this (actionDictionary.Handle, documentRef.Handle)
		{
		}
	}

	public partial class PSPDFResetFormAction : PSPDFAbstractFormAction
	{
		public PSPDFResetFormAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef) : this (actionDictionary.Handle, documentRef.Handle)
		{
		}
	}

	public partial class PSPDFHideAction : PSPDFAction
	{
		public PSPDFHideAction (CGPDFDictionary actionDictionary, CGPDFDocument documentRef) : this (actionDictionary.Handle, documentRef.Handle)
		{
		}
	}

	public partial class PSPDFTextParser : NSObject
	{
		public PSPDFTextParser (CGPDFPage pageRef, uint page, PSPDFDocument document, NSMutableDictionary fontCache, bool hideGlyphsOutsidePageRect, CGPDFBox pdfBox) : this (pageRef.Handle, page, document, fontCache, hideGlyphsOutsidePageRect, pdfBox)
		{
		}

		public static PSPDFTextParser FromStream (CGPDFStream stream)
		{
			return FromStream_ (stream.Handle);
		}
	}

	public partial class PSPDFAnnotationManager : NSObject
	{
		public virtual PSPDFAnnotation [] AnnotationsForPage (uint page, PSPDFAnnotationType type, CGPDFPage pageRef)
		{
			return AnnotationsForPage_ (page, type, pageRef.Handle);
		}

		public virtual bool SaveAnnotationsWithOptions (NSDictionary options, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _SaveAnnotationsWithOptions (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFFormElement : PSPDFWidgetAnnotation
	{
		public PSPDFFormElement (CGPDFDictionary annotDict, CGPDFDocument documentRef, NSMutableDictionary fieldsAddressMap) : this (annotDict.Handle, documentRef.Handle, fieldsAddressMap)
		{
		}

		public PSPDFFormElement (CGPDFDictionary annotDict, CGPDFDocument documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap) : this (annotDict.Handle, documentRef.Handle, parentFormElement, fieldsAddressMap)
		{
		}

		public static PSPDFFormElement FormElementWithAnnotationDictionary (CGPDFDictionary annotDict, CGPDFDocument documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap)
		{
			return FormElementWithAnnotationDictionary_ (annotDict.Handle, documentRef.Handle, parentFormElement, fieldsAddressMap);
		}
	}

	public partial class PSPDFButtonFormElement : PSPDFFormElement
	{
		public PSPDFButtonFormElement (CGPDFDictionary annotDict, CGPDFDocument documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap) : this (annotDict.Handle, documentRef.Handle, parentFormElement, fieldsAddressMap)
		{
		}
	}

	public partial class PSPDFChoiceFormElement : PSPDFAbstractTextRenderingFormElement
	{
		public PSPDFChoiceFormElement (CGPDFDictionary annotDict, CGPDFDocument documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap) : this (annotDict.Handle, documentRef.Handle, parentFormElement, fieldsAddressMap)
		{
		}
	}

	public partial class PSPDFSignatureFormElement : PSPDFFormElement
	{
		public PSPDFSignatureFormElement (CGPDFDictionary annotDict, CGPDFDocument documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap) : this (annotDict.Handle, documentRef.Handle, parentFormElement, fieldsAddressMap)
		{
		}
	}

	public partial class PSPDFTextFieldFormElement : PSPDFAbstractTextRenderingFormElement
	{
		public PSPDFTextFieldFormElement (CGPDFDictionary annotDict, CGPDFDocument documentRef, PSPDFFormElement parentFormElement, NSMutableDictionary fieldsAddressMap) : this (annotDict.Handle, documentRef.Handle, parentFormElement, fieldsAddressMap)
		{
		}
	}

	public partial class PSPDFFontInfo : NSObject
	{
		public PSPDFFontInfo (CGPDFDictionary font, string fontKey) : this (font.Handle, fontKey)
		{
		}
	}

	public partial class PSPDFAESCryptoDataProvider : NSObject
	{
		public CGDataProvider DataProvider
		{
			get 
			{
				IntPtr ptr = this.DataProvider_;
				return new CGDataProvider(ptr);
			}
		}

		public static bool IsAESCryptoDataProvider (CGDataProvider dataProviderRef)
		{
			return IsAESCryptoDataProvider_ (dataProviderRef.Handle);
		}

		private static uint PSPDFDefaultPBKDFNumberOfRounds;
		public static uint DefaultPBKDFNumberOfRounds
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDefaultPBKDFNumberOfRounds");
				PSPDFDefaultPBKDFNumberOfRounds = (uint)Marshal.PtrToStructure(ptr, typeof(uint));

				return PSPDFDefaultPBKDFNumberOfRounds;
			}
		}
	}

	public partial class PSPDFStampAnnotation : PSPDFAnnotation
	{
		public virtual UIImage LoadImageWithError (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				UIImage ret = _LoadImageWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFSegmentedControl : UISegmentedControl
	{
		public PSPDFSegmentedControl (object[] args) : base (args) 
		{

		}
	}
}