namespace SplitAndMerge
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            fileDialogPanel = new Panel();
            selectSingleFileButton = new Button();
            openFileDialog = new OpenFileDialog();
            fileNameLabel = new Label();
            cancelButton = new Button();
            currentActionProgressBar = new ProgressBar();
            selectLabel = new Label();
            fileSizeRadioButton = new RadioButton();
            actionTypePanel = new Panel();
            bitrateRadioButton = new RadioButton();
            label2 = new Label();
            folderBrowserDialog = new FolderBrowserDialog();
            splitMergeProgressLabel = new Label();
            mbBitrateTextBox = new TextBox();
            mbBitrateLabel = new Label();
            progressPanel = new Panel();
            mbBitrateParamsPanel = new Panel();
            splitTypePanel = new Panel();
            limitToTargetcheckBox = new CheckBox();
            splitTypeLabel = new Label();
            fileDialogPanel.SuspendLayout();
            actionTypePanel.SuspendLayout();
            progressPanel.SuspendLayout();
            mbBitrateParamsPanel.SuspendLayout();
            splitTypePanel.SuspendLayout();
            SuspendLayout();
            // 
            // fileDialogPanel
            // 
            fileDialogPanel.Anchor = AnchorStyles.Top;
            fileDialogPanel.Controls.Add(selectSingleFileButton);
            fileDialogPanel.Location = new Point(290, 12);
            fileDialogPanel.Name = "fileDialogPanel";
            fileDialogPanel.Size = new Size(213, 30);
            fileDialogPanel.TabIndex = 0;
            // 
            // selectSingleFileButton
            // 
            selectSingleFileButton.Location = new Point(51, 3);
            selectSingleFileButton.Name = "selectSingleFileButton";
            selectSingleFileButton.Size = new Size(100, 23);
            selectSingleFileButton.TabIndex = 0;
            selectSingleFileButton.Text = "Select file";
            selectSingleFileButton.UseVisualStyleBackColor = true;
            selectSingleFileButton.Click += SelectFile_Click;
            // 
            // fileNameLabel
            // 
            fileNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            fileNameLabel.Font = new Font("Segoe UI", 12F);
            fileNameLabel.Location = new Point(12, 16);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new Size(760, 22);
            fileNameLabel.TabIndex = 1;
            fileNameLabel.Text = "File Name";
            fileNameLabel.TextAlign = ContentAlignment.TopCenter;
            fileNameLabel.Visible = false;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(678, 24);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 21);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // currentActionProgressBar
            // 
            currentActionProgressBar.Location = new Point(0, 24);
            currentActionProgressBar.Name = "currentActionProgressBar";
            currentActionProgressBar.Size = new Size(668, 21);
            currentActionProgressBar.Style = ProgressBarStyle.Continuous;
            currentActionProgressBar.TabIndex = 2;
            // 
            // selectLabel
            // 
            selectLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            selectLabel.Font = new Font("Segoe UI", 8F);
            selectLabel.ForeColor = SystemColors.ControlDarkDark;
            selectLabel.Location = new Point(12, 40);
            selectLabel.Name = "selectLabel";
            selectLabel.Size = new Size(760, 15);
            selectLabel.TabIndex = 1;
            selectLabel.Text = "Select a video to compress";
            selectLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fileSizeRadioButton
            // 
            fileSizeRadioButton.AutoSize = true;
            fileSizeRadioButton.Location = new Point(3, 3);
            fileSizeRadioButton.Name = "fileSizeRadioButton";
            fileSizeRadioButton.Size = new Size(65, 19);
            fileSizeRadioButton.TabIndex = 2;
            fileSizeRadioButton.TabStop = true;
            fileSizeRadioButton.Text = "File size";
            fileSizeRadioButton.UseVisualStyleBackColor = true;
            fileSizeRadioButton.CheckedChanged += FileSizeRadioButton_CheckedChanged;
            // 
            // actionTypePanel
            // 
            actionTypePanel.Controls.Add(bitrateRadioButton);
            actionTypePanel.Controls.Add(fileSizeRadioButton);
            actionTypePanel.Location = new Point(321, 65);
            actionTypePanel.Name = "actionTypePanel";
            actionTypePanel.Size = new Size(145, 27);
            actionTypePanel.TabIndex = 6;
            // 
            // bitrateRadioButton
            // 
            bitrateRadioButton.AutoSize = true;
            bitrateRadioButton.Location = new Point(81, 3);
            bitrateRadioButton.Name = "bitrateRadioButton";
            bitrateRadioButton.Size = new Size(62, 19);
            bitrateRadioButton.TabIndex = 3;
            bitrateRadioButton.TabStop = true;
            bitrateRadioButton.Text = "Bit rate";
            bitrateRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Font = new Font("Segoe UI", 8F);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(12, 95);
            label2.Name = "label2";
            label2.Size = new Size(760, 15);
            label2.TabIndex = 1;
            label2.Text = "Are you targetting a specific file size or bit rate?";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // splitMergeProgressLabel
            // 
            splitMergeProgressLabel.Anchor = AnchorStyles.Top;
            splitMergeProgressLabel.Font = new Font("Segoe UI", 7F);
            splitMergeProgressLabel.Location = new Point(185, 1);
            splitMergeProgressLabel.Name = "splitMergeProgressLabel";
            splitMergeProgressLabel.Size = new Size(295, 20);
            splitMergeProgressLabel.TabIndex = 4;
            splitMergeProgressLabel.Text = "33.33%";
            splitMergeProgressLabel.TextAlign = ContentAlignment.BottomCenter;
            // 
            // mbBitrateTextBox
            // 
            mbBitrateTextBox.Location = new Point(319, 3);
            mbBitrateTextBox.Name = "mbBitrateTextBox";
            mbBitrateTextBox.Size = new Size(87, 23);
            mbBitrateTextBox.TabIndex = 9;
            mbBitrateTextBox.Text = "0";
            mbBitrateTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // mbBitrateLabel
            // 
            mbBitrateLabel.AutoSize = true;
            mbBitrateLabel.Font = new Font("Segoe UI", 7F);
            mbBitrateLabel.Location = new Point(412, 8);
            mbBitrateLabel.Name = "mbBitrateLabel";
            mbBitrateLabel.Size = new Size(20, 12);
            mbBitrateLabel.TabIndex = 4;
            mbBitrateLabel.Text = "MB";
            mbBitrateLabel.TextAlign = ContentAlignment.BottomLeft;
            // 
            // progressPanel
            // 
            progressPanel.Controls.Add(currentActionProgressBar);
            progressPanel.Controls.Add(cancelButton);
            progressPanel.Controls.Add(splitMergeProgressLabel);
            progressPanel.Location = new Point(12, 229);
            progressPanel.Name = "progressPanel";
            progressPanel.Size = new Size(760, 58);
            progressPanel.TabIndex = 10;
            // 
            // mbBitrateParamsPanel
            // 
            mbBitrateParamsPanel.Controls.Add(mbBitrateLabel);
            mbBitrateParamsPanel.Controls.Add(mbBitrateTextBox);
            mbBitrateParamsPanel.Location = new Point(12, 174);
            mbBitrateParamsPanel.Name = "mbBitrateParamsPanel";
            mbBitrateParamsPanel.Size = new Size(760, 32);
            mbBitrateParamsPanel.TabIndex = 11;
            // 
            // splitTypePanel
            // 
            splitTypePanel.Controls.Add(limitToTargetcheckBox);
            splitTypePanel.Location = new Point(318, 113);
            splitTypePanel.Name = "splitTypePanel";
            splitTypePanel.Size = new Size(147, 27);
            splitTypePanel.TabIndex = 8;
            // 
            // limitToTargetcheckBox
            // 
            limitToTargetcheckBox.AutoSize = true;
            limitToTargetcheckBox.Location = new Point(10, 5);
            limitToTargetcheckBox.Name = "limitToTargetcheckBox";
            limitToTargetcheckBox.Size = new Size(128, 19);
            limitToTargetcheckBox.TabIndex = 0;
            limitToTargetcheckBox.Text = "Don't exceed target";
            limitToTargetcheckBox.UseVisualStyleBackColor = true;
            // 
            // splitTypeLabel
            // 
            splitTypeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            splitTypeLabel.Font = new Font("Segoe UI", 8F);
            splitTypeLabel.ForeColor = SystemColors.ControlDarkDark;
            splitTypeLabel.Location = new Point(12, 143);
            splitTypeLabel.Name = "splitTypeLabel";
            splitTypeLabel.Size = new Size(760, 15);
            splitTypeLabel.TabIndex = 7;
            splitTypeLabel.Text = "If ticked, the outcome might undershoot the target by as much as 10%, resulting in lower quality.";
            splitTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 211);
            Controls.Add(splitTypePanel);
            Controls.Add(splitTypeLabel);
            Controls.Add(progressPanel);
            Controls.Add(actionTypePanel);
            Controls.Add(label2);
            Controls.Add(selectLabel);
            Controls.Add(fileNameLabel);
            Controls.Add(fileDialogPanel);
            Controls.Add(mbBitrateParamsPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Compress";
            FormClosing += MainForm_FormClosing;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            fileDialogPanel.ResumeLayout(false);
            actionTypePanel.ResumeLayout(false);
            actionTypePanel.PerformLayout();
            progressPanel.ResumeLayout(false);
            mbBitrateParamsPanel.ResumeLayout(false);
            mbBitrateParamsPanel.PerformLayout();
            splitTypePanel.ResumeLayout(false);
            splitTypePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel fileDialogPanel;
        private OpenFileDialog openFileDialog;
        private Label fileNameLabel;
        private Button cancelButton;
        private ProgressBar currentActionProgressBar;
        private Label selectLabel;
        private RadioButton fileSizeRadioButton;
        private Panel actionTypePanel;
        private RadioButton bitrateRadioButton;
        private Label label2;
        private FolderBrowserDialog folderBrowserDialog;
        private Label splitMergeProgressLabel;
        private TextBox mbBitrateTextBox;
        private Label mbBitrateLabel;
        private Panel progressPanel;
        private Panel mbBitrateParamsPanel;
        private Button selectSingleFileButton;
        private Panel splitTypePanel;
        private Label splitTypeLabel;
        private CheckBox limitToTargetcheckBox;
    }
}