﻿namespace ScintillaNET.TestApp
{
    partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.scintilla = new ScintillaNET.Scintilla();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel_Version = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.describeKeywordSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.lexersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox_Find = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripMenuItem_Find = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.scintillaDebug = new ScintillaNET.Scintilla();
			this.statusStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "All Files|*.*";
			// 
			// scintilla
			// 
			this.scintilla._ScintillaManagedDragDrop = true;
			this.scintilla.BorderStyle = ScintillaNET.BorderStyle.Fixed3DVisualStyles;
			this.scintilla.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
			this.scintilla.ChangeHistory = ((ScintillaNET.ChangeHistory)((ScintillaNET.ChangeHistory.Enabled | ScintillaNET.ChangeHistory.Markers)));
			this.scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scintilla.EolMode = ScintillaNET.Eol.Lf;
			this.scintilla.FoldLineStripColor = System.Drawing.Color.Gray;
			this.scintilla.Font = new System.Drawing.Font("Consolas", 10.2F);
			this.scintilla.LexerName = null;
			this.scintilla.Location = new System.Drawing.Point(0, 0);
			this.scintilla.Name = "scintilla";
			this.scintilla.Size = new System.Drawing.Size(500, 423);
			this.scintilla.TabIndex = 2;
			this.scintilla.UseTabs = true;
			this.scintilla.SavePointLeft += new System.EventHandler<System.EventArgs>(this.scintilla_SavePointLeft);
			this.scintilla.SavePointReached += new System.EventHandler<System.EventArgs>(this.scintilla_SavePointReached);
			this.scintilla.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.scintilla_UpdateUI);
			this.scintilla.TextChanged += new System.EventHandler(this.scintilla_TextChanged);
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(13, 20);
			this.toolStripStatusLabel.Text = " ";
			// 
			// toolStripStatusLabel_Version
			// 
			this.toolStripStatusLabel_Version.Name = "toolStripStatusLabel_Version";
			this.toolStripStatusLabel_Version.Size = new System.Drawing.Size(886, 20);
			this.toolStripStatusLabel_Version.Spring = true;
			this.toolStripStatusLabel_Version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// statusStrip
			// 
			this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel_Version});
			this.statusStrip.Location = new System.Drawing.Point(0, 454);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(914, 26);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 3;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 27);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(178, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.describeKeywordSetsToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 27);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// describeKeywordSetsToolStripMenuItem
			// 
			this.describeKeywordSetsToolStripMenuItem.Name = "describeKeywordSetsToolStripMenuItem";
			this.describeKeywordSetsToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
			this.describeKeywordSetsToolStripMenuItem.Text = "Describe &Keyword Sets";
			this.describeKeywordSetsToolStripMenuItem.Click += new System.EventHandler(this.describeKeywordSetsToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Enabled = false;
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 27);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Enabled = false;
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
			this.aboutToolStripMenuItem.Text = "&About...";
			// 
			// menuStrip
			// 
			this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.lexersToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.toolStripTextBox_Find,
            this.toolStripMenuItem_Find});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(914, 31);
			this.menuStrip.TabIndex = 1;
			// 
			// lexersToolStripMenuItem
			// 
			this.lexersToolStripMenuItem.Name = "lexersToolStripMenuItem";
			this.lexersToolStripMenuItem.Size = new System.Drawing.Size(64, 27);
			this.lexersToolStripMenuItem.Text = "Lexers";
			// 
			// toolStripTextBox_Find
			// 
			this.toolStripTextBox_Find.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.toolStripTextBox_Find.Name = "toolStripTextBox_Find";
			this.toolStripTextBox_Find.Size = new System.Drawing.Size(124, 27);
			this.toolStripTextBox_Find.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_Find_KeyDown);
			// 
			// toolStripMenuItem_Find
			// 
			this.toolStripMenuItem_Find.Name = "toolStripMenuItem_Find";
			this.toolStripMenuItem_Find.Size = new System.Drawing.Size(51, 27);
			this.toolStripMenuItem_Find.Text = "Find";
			this.toolStripMenuItem_Find.Click += new System.EventHandler(this.toolStripMenuItem_Find_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "All Files|*.*";
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 31);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.scintilla);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.scintillaDebug);
			this.splitContainer.Size = new System.Drawing.Size(914, 423);
			this.splitContainer.SplitterDistance = 500;
			this.splitContainer.SplitterWidth = 10;
			this.splitContainer.TabIndex = 4;
			// 
			// scintillaDebug
			// 
			this.scintillaDebug._ScintillaManagedDragDrop = true;
			this.scintillaDebug.BorderStyle = ScintillaNET.BorderStyle.Fixed3DVisualStyles;
			this.scintillaDebug.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
			this.scintillaDebug.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scintillaDebug.FoldLineStripColor = System.Drawing.Color.Gray;
			this.scintillaDebug.Font = new System.Drawing.Font("Consolas", 10.2F);
			this.scintillaDebug.LexerName = null;
			this.scintillaDebug.Location = new System.Drawing.Point(0, 0);
			this.scintillaDebug.Name = "scintillaDebug";
			this.scintillaDebug.Size = new System.Drawing.Size(404, 423);
			this.scintillaDebug.TabIndex = 3;
			this.scintillaDebug.UseTabs = true;
			this.scintillaDebug.WrapMode = ScintillaNET.WrapMode.Word;
			this.scintillaDebug.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.scintillaHexDebug_UpdateUI);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(914, 480);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
			this.Name = "FormMain";
			this.Text = "Scintilla.NET Test App";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.Shown += new System.EventHandler(this.FormMain_Shown);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Version;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem lexersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem describeKeywordSetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_Find;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Find;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Scintilla scintillaDebug;
    }
}

