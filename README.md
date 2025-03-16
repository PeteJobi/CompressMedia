# Compress Media
This repo provides a simple GUI for compressing videos to a specific file size or bit rate, while minimizing the loss to quality. Only supports Windows 10 and 11 (not tested on other versions of Windows). Powered by FFMPEG.

![image](https://github.com/user-attachments/assets/2b3d867a-676e-417f-99fe-afadbbe010f1)
![image](https://github.com/user-attachments/assets/c9ced180-87c6-41f1-b828-b1ac594e1797)

## How to build
You need to have at least .NET 9 runtime installed to build the software. Download the latest runtime [here](https://dotnet.microsoft.com/en-us/download). If you're not sure which one to download, try [.NET 9.0 Version 9.0.201](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-9.0.201-windows-x64-installer)

In the project folder, run the below
```
dotnet publish -p:PublishSingleFile=true -r win-x64 -c Release --self-contained false
```
When that completes, go to `\bin\Release\net<version>-windows\win-x64\publish` and you'll find the **CompressMedia.exe** amongst other files. All the files are necessary for the software to run properly. Run **CompressMedia.exe** to use the software.

## Run without building
You can also just download the release builds if you don't wish to build manually. The assets release contains the assets used by the software. The standard release contains the compiled executable. Download them both, extract the assets to a folder and drop the executable in that folder.

If you wish to run the software without installing the required .NET runtime, download the self-contained release.

## How to use
The file types supported are ".mp4" and ".mkv".

Select **File size** to compress your video to a specified size, give or take a few MBs. Enter the target file size in the MB text box. Select **Bit rate** to compress your video to an average bit rate specified in the Kb/s box.

Check the **Don't exceed target** box to ensure it does not go above the specified file size or bitrate. For best outcomes, it's best to leave this box unchecked.

When you're done with the parameters, click **Select file** to select a video to compress.
