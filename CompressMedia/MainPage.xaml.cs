using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using CompressMediaPage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CompressMedia
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void MainPage_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                GoToCompress(items[0].Path);
            }
        }

        private void MainPage_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void ShowFilePicker(object sender, RoutedEventArgs e)
        {
            string[] allSupportedTypes = [".mkv", ".mp4", "wav", "mp3"];
            var filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            foreach (var supportedType in allSupportedTypes)
            {
                filePicker.FileTypeFilter.Add(supportedType);
            }
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(MainWindow.Window);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);
            var file = await filePicker.PickSingleFileAsync();
            GoToCompress(file.Path);
        }

        private async void GoToCompress(string mediaPath)
        {
            try
            {
                string ffmpegPath;
                try
                {
                    ffmpegPath = Path.Join(Package.Current.InstalledLocation.Path, "Assets/ffmpeg.exe");
                }
                catch (InvalidOperationException)
                {
                    ffmpegPath = "Assets/ffmpeg.exe";
                }
                if (!File.Exists(ffmpegPath))
                {
                    await ErrorDialog.ShowAsync();
                    return;
                }
                Frame.Navigate(typeof(CompressMediaPage.CompressMediaPage), new CompressProps { FfmpegPath = ffmpegPath, MediaPath = mediaPath, TypeToNavigateTo = typeof(MainPage).FullName });
            }
            catch (Exception ex)
            {
                ErrorDialog.Content = $"An error occurred while navigating to the compress media page: {ex.Message}";
                await ErrorDialog.ShowAsync();
                System.Diagnostics.Debug.WriteLine($"Error navigating to CompressMediaPage: {ex.Message}");
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string outputFile)
            {
                Console.WriteLine($"Path of output file is {outputFile}");
            }

            await Task.Delay(100);
            //GoToCompress(@"C:\Users\Peter Egunjobi\Videos\Video Splitter 2025-09-12 19-45-40_SplitVideos\Video Splitter 2025-09-12 19-45-40000_TOURED_12fps.gif");
            //GoToCompress(@"C:\Users\Peter Egunjobi\Videos\NVIDIA\The Last of Us™ Part II Remastered\The Last of Us™ Part II Remastered 2025.09.15 - 23.49.34.23.DVR_SplitVideos\The Last of Us™ Part II Remastered 2025.09.15 - 23.49.34.23.DVR000.mp4");
            //GoToCompress(@"C:\Users\Peter Egunjobi\Videos\NVIDIA\The Last of Us™ Part II Remastered\My gameplay recording.mp4");
            //GoToCompress(@"C:\Users\Peter Egunjobi\Videos\NVIDIA\Indika-Win64-Shipping\Indika-Win64-Shipping 2025.05.08 - 12.19.08.15.DVR.mp4");
            //GoToCompress(@"C:\Users\Peter Egunjobi\Pictures\2592x1944.jpeg");
            //GoToCompress(@"C:\Users\Peter Egunjobi\Pictures\frame00014903.png");
            //GoToCompress(@"C:\Users\Peter Egunjobi\Downloads\Music\yt5s.io - [ANIMEOMO] Nier Automata - Birth of a Wish (All versions continuously) (Edited) - EPIC SOUNDTRACK (128 kbps).mp3");
        }
    }
}
