using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("PSPDFKit", LinkTarget.Simulator | LinkTarget.ArmV7s | LinkTarget.ArmV7, IsCxx = true, LinkerFlags = "-lz -lxml2 -lsqlite3", Frameworks = "CoreText QuartzCore MessageUI ImageIO CoreMedia MediaPlayer CFNetwork AVFoundation AssetsLibrary Security QuickLook AudioToolbox CoreData", ForceLoad = true)]