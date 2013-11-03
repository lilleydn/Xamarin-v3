using System;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;

[assembly: LinkerSafe]
[assembly: LinkWith ("PSPDFKit", LinkTarget.Simulator | LinkTarget.ArmV7s | LinkTarget.ArmV7, IsCxx = true, LinkerFlags = "-lz -lxml2 -lsqlite3 -ObjC -fobjc-arc", Frameworks = "CoreText QuartzCore MessageUI ImageIO CoreMedia MediaPlayer CFNetwork AVFoundation AssetsLibrary Security QuickLook AudioToolbox CoreData SystemConfiguration CoreTelephony", SmartLink = true, ForceLoad = true)]