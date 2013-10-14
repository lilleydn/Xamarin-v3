using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace PSPDFCatalog
{
	public static class Helper
	{
		static IntPtr kCGPDFContextUserPassword;
		public static IntPtr CGPDFContextUserPassword {
			get {
				if (kCGPDFContextUserPassword == IntPtr.Zero) {
					IntPtr h = Dlfcn.dlopen (MonoTouch.Constants.CoreGraphicsLibrary, 0);
					try {
						kCGPDFContextUserPassword = Dlfcn.GetIntPtr (h, "kCGPDFContextUserPassword");
					} finally {
						Dlfcn.dlclose (h);
					}
				}
				return kCGPDFContextUserPassword;
			}
		}

		static IntPtr kCGPDFContextOwnerPassword;
		public static IntPtr CGPDFContextOwnerPassword {
			get {
				if (kCGPDFContextOwnerPassword == IntPtr.Zero) {
					IntPtr h = Dlfcn.dlopen (MonoTouch.Constants.CoreGraphicsLibrary, 0);
					try {
						kCGPDFContextOwnerPassword = Dlfcn.GetIntPtr (h, "kCGPDFContextOwnerPassword");
					} finally {
						Dlfcn.dlclose (h);
					}
				}
				return kCGPDFContextOwnerPassword;
			}
		}

		static IntPtr kCGPDFContextEncryptionKeyLength;
		public static IntPtr CGPDFContextEncryptionKeyLength {
			get {
				if (kCGPDFContextEncryptionKeyLength == IntPtr.Zero) {
					IntPtr h = Dlfcn.dlopen (MonoTouch.Constants.CoreGraphicsLibrary, 0);
					try {
						kCGPDFContextEncryptionKeyLength = Dlfcn.GetIntPtr (h, "kCGPDFContextEncryptionKeyLength");
					} finally {
						Dlfcn.dlclose (h);
					}
				}
				return kCGPDFContextEncryptionKeyLength;
			}
		}

	}
}

