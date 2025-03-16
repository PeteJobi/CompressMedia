using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SplitAndMerge
{
    public partial class MainForm : Form
    {
        static readonly string[] allowedExts = { ".mkv", ".mp4" };
        const int PROGRESS_MAX = 1_000_000;
        const string ffmpegPath = "ffmpeg.exe";
        Process? currentProcess;
        bool hasBeenKilled;
        bool isPaused;
        string[] filesCreated = Array.Empty<string>();

        public MainForm()
        {
            InitializeComponent();
            fileSizeRadioButton.Checked = true;
            currentActionProgressBar.Maximum = PROGRESS_MAX;
            Reset(null, EventArgs.Empty);
        }

        private void Reset(object? sender, EventArgs e)
        {
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;
            cancelButton.Click -= Reset;
            fileDialogPanel.Show();
            selectLabel.Show();
            fileNameLabel.Hide();
            currentActionProgressBar.Value = 0;
            actionTypePanel.Enabled = true;
            splitTypePanel.Enabled = true;
            splitTypePanel.Visible = true;
            splitTypeLabel.Visible = true;
            mbBitrateParamsPanel.Enabled = true;
            ResetProgressUI(true);
            Height = 250;
        }

        void ResetProgressUI(bool show)
        {
            currentActionProgressBar.Value = 0;
            splitMergeProgressLabel.Visible = show;
            splitMergeProgressLabel.Text = string.Empty;
        }

        async Task PrepareFiles(string[] fileNames)
        {
            fileDialogPanel.Hide();
            selectLabel.Hide();
            fileNameLabel.Show();
            actionTypePanel.Enabled = false;
            splitTypePanel.Enabled = false;
            mbBitrateParamsPanel.Enabled = false;
            Height = 326;
            fileNameLabel.Text = Path.GetFileName(fileNames[0]);
            await Compress(fileNames[0]);
        }

        async Task Compress(string fileName)
        {
            var duration = TimeSpan.MinValue;
            var parsedAudioBitrate = 0;
            await StartProcess(ffmpegPath, $"-i \"{fileName}\"", null, (sender, args) =>
            {
                if (string.IsNullOrWhiteSpace(args.Data) || hasBeenKilled) return;
                if (duration == TimeSpan.MinValue)
                {
                    var matchCollection = Regex.Matches(args.Data, @"\s*Duration:\s(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                    if (matchCollection.Count == 0) return;
                    duration = TimeSpan.Parse(matchCollection[0].Groups[1].Value);
                }
                if (parsedAudioBitrate == 0)
                {
                    var matchCollection = Regex.Matches(args.Data, @"\s*Stream .+: Audio: .+ (\d+) kb/s.+");
                    if (matchCollection.Count == 0) return;
                    parsedAudioBitrate = int.Parse(matchCollection[0].Groups[1].Value);
                }
            });

            var mbBitrate = int.Parse(mbBitrateTextBox.Text);
            long targetSize = mbBitrate * 1000 * 1000 * 8;
            var totalBitrate = fileSizeRadioButton.Checked ? targetSize / duration.TotalSeconds : mbBitrate;
            var audioBitrate = parsedAudioBitrate * 1000;
            var videoBitrate = totalBitrate - audioBitrate;
            var limitToTarget = limitToTargetcheckBox.Checked ? $"-maxrate:v {videoBitrate} -bufsize:v {totalBitrate}" : string.Empty;
            var outputFileName = GetOutputName(fileName);
            File.Delete(outputFileName);
            await StartProcess(ffmpegPath, $"-i \"{fileName}\" -b:v {videoBitrate} -b:a {audioBitrate} {limitToTarget} \"{outputFileName}\"", null, (sender, args) =>
            {
                Debug.WriteLine(args.Data);
                if (string.IsNullOrWhiteSpace(args.Data) || hasBeenKilled) return;
                if (args.Data.StartsWith("frame"))
                {
                    if (CheckNoSpaceDuringBreakMerge(args.Data)) return;
                    MatchCollection matchCollection = Regex.Matches(args.Data, @"^frame=\s*\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+");
                    if (matchCollection.Count == 0) return;
                    IncrementProgress(TimeSpan.Parse(matchCollection[0].Groups[1].Value), duration);
                }
            });
            if (HasBeenKilled()) return;
            AllDone();
        }

        string GetOutputName(string path)
        {
            string inputName = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            string parentFolder = Path.GetDirectoryName(path) ?? throw new FileNotFoundException($"The specified path does not exist: {path}");
            return Path.Combine(parentFolder, $"{inputName}_COMPRESSED{extension}");
        }

        void IncrementProgress(TimeSpan currentTime, TimeSpan totalDuration)
        {
            Invoke(() =>
            {
                double fraction = currentTime / totalDuration;
                currentActionProgressBar.Value = Math.Max(0, Math.Min((int)(fraction * PROGRESS_MAX), PROGRESS_MAX));
                splitMergeProgressLabel.Text = $"{Math.Round(fraction * 100, 2)} %";
            });
        }

        bool CheckNoSpaceDuringBreakMerge(string line)
        {
            if (!line.EndsWith("No space left on device") && !line.EndsWith("I/O error")) return false;
            SuspendProcess(currentProcess);
            MessageBox.Show($"Process failed.\nError message: {line}");
            Invoke(() => Cancel(true));
            return true;
        }

        void AllDone()
        {
            currentProcess = null;
            currentActionProgressBar.Value = currentActionProgressBar.Maximum;
            splitMergeProgressLabel.Text = "100 %";
            cancelButton.Text = "Retry";
            cancelButton.Click -= CancelButton_Click;
            cancelButton.Click += Reset;
        }

        bool HasBeenKilled()
        {
            if (!hasBeenKilled) return false;
            hasBeenKilled = false;
            return true;
        }

        void Cancel(bool dontWaitForExit = false)
        {
            currentProcess.Kill();
            if (!dontWaitForExit) currentProcess.WaitForExit();
            hasBeenKilled = true;
            cancelButton.Click -= CancelButton_Click;
            Reset(null, EventArgs.Empty);
            currentProcess = null;
            foreach (string path in filesCreated)
            {
                if (Directory.Exists(path)) Directory.Delete(path, true);
                else if (File.Exists(path)) File.Delete(path);
            }
        }

        bool ConfirmCancel()
        {
            const string message = "Are you sure that you would like to cancel the process?";
            string caption = $"Cancel compress task";
            if (!isPaused) SuspendProcess(currentProcess);
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            bool confirm = result == DialogResult.Yes;
            if (!confirm && !isPaused) ResumeProcess(currentProcess);
            return confirm;
        }

        private async void SelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Select a video";
            openFileDialog.Filter = "Video File|" + string.Join(";", allowedExts.Select(e => $"*{e}"));
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            await PrepareFiles(openFileDialog.FileNames);
        }

        private void FileSizeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            mbBitrateLabel.Text = fileSizeRadioButton.Checked ? "MB" : "Kb/s";
        }

        private void CancelButton_Click(object? sender, EventArgs e)
        {
            if (ConfirmCancel()) Cancel();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentProcess == null) return;

            if (ConfirmCancel()) Cancel();
            else e.Cancel = true;
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (!fileDialogPanel.Visible) return;
            e.Effect = DragDropEffects.All;
        }

        private async void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!fileDialogPanel.Visible) return;
            string[]? files = ((string[]?)e.Data?.GetData(DataFormats.FileDrop, false));
            if (files?.Length < 1) return;
            if (Directory.Exists(files[0]))
            {
                files = Directory.GetFiles(files[0]);
            }
            files = files.Where(p => allowedExts.Contains(Path.GetExtension(p).ToLower())).ToArray();
            await PrepareFiles(files);
        }

        async Task StartProcess(string processFileName, string arguments, DataReceivedEventHandler? outputEventHandler, DataReceivedEventHandler? errorEventHandler)
        {
            Process ffmpeg = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = processFileName,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                },
                EnableRaisingEvents = true
            };
            ffmpeg.OutputDataReceived += outputEventHandler;
            ffmpeg.ErrorDataReceived += errorEventHandler;
            ffmpeg.Start();
            ffmpeg.BeginErrorReadLine();
            ffmpeg.BeginOutputReadLine();
            currentProcess = ffmpeg;
            await ffmpeg.WaitForExitAsync();
            ffmpeg.Dispose();
        }

        [Flags]
        public enum ThreadAccess : int
        {
            SUSPEND_RESUME = (0x0002)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        private static void SuspendProcess(Process process)
        {
            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                SuspendThread(pOpenThread);

                CloseHandle(pOpenThread);
            }
        }

        public static void ResumeProcess(Process process)
        {
            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);

                if (pOpenThread == IntPtr.Zero)
                {
                    continue;
                }

                int suspendCount;
                do
                {
                    suspendCount = ResumeThread(pOpenThread);
                } while (suspendCount > 0);

                CloseHandle(pOpenThread);
            }
        }
    }
}