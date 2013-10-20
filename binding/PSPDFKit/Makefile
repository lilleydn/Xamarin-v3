MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild

all: PSPDFKit.dll

PSPDFKit.dll: clean
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit.csproj
	cp bin/Release/PSPDFKit.dll PSPDFKit.dll

clean:
	-rm -rf *.dll *.mdb *.sln bin/ obj/
