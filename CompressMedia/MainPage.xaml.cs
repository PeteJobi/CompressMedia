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
        private TitleBar _titleBar;
        private string? _ffmpegPath;
        private readonly EventHandler<string> _fileDropped;
        private readonly EventHandler<string> _ffmpegSet;
        public readonly string[] supportedTypes = [".mkv", ".mp4", ".mov", ".avi", ".mp3", ".m4a", ".wav"];

        public MainPage()
        {
            _fileDropped = (_, path) => GoToCompress(path);
            _ffmpegSet = (_, path) => _ffmpegPath = path;
            InitializeComponent();
        }

        private async void GoToCompress(string mediaPath)
        {
            if (_ffmpegPath == null) return;
            Frame.Navigate(typeof(CompressMediaPage.CompressMediaPage), new CompressProps { FfmpegPath = _ffmpegPath, MediaPath = mediaPath, TypeToNavigateTo = typeof(MainPage).FullName });
            _titleBar.IsBackButtonVisible = true;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is TitleBar titleBar)
            {
                _titleBar = titleBar;
                return;
            }
            _titleBar.IsBackButtonVisible = false;

            if (e.Parameter is string outputFile)
            {
                Console.WriteLine($"Path of output file is {outputFile}");
            }

            //await Task.Delay(10);
            //GoToCompress(@"C:\Users\Peter Egunjobi\Pictures\screencapture-github-PeteJobi-CompressMedia-2025-09-25-23_44_33_TOURED_COMPRESSED.mp4");
        }
    }
}
