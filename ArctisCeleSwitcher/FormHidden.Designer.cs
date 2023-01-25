namespace ArctisCeleSwitcher {
    partial class FormHidden {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHidden));
            this.timerRefreshStatus = new System.Windows.Forms.Timer(this.components);
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.impostazioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.esciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboPlaybackArctis = new System.Windows.Forms.ComboBox();
            this.comboPlaybackSpeaker = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.autoSwapCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboRecordSpeaker = new System.Windows.Forms.ComboBox();
            this.comboRecordArctis = new System.Windows.Forms.ComboBox();
            this.trayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerRefreshStatus
            // 
            this.timerRefreshStatus.Interval = 1000;
            this.timerRefreshStatus.Tick += new System.EventHandler(this.timerRefreshStatus_Tick);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "ArctisCeleSwitcher";
            this.trayIcon.Visible = true;
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripSeparator2,
            this.impostazioniToolStripMenuItem,
            this.toolStripSeparator1,
            this.esciToolStripMenuItem});
            this.trayMenu.Name = "contextMenuStrip1";
            this.trayMenu.Size = new System.Drawing.Size(135, 82);
            this.trayMenu.Opened += new System.EventHandler(this.trayMenu_Opened);
            // 
            // statusLabel
            // 
            this.statusLabel.Enabled = false;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(134, 22);
            this.statusLabel.Text = "HS: ? BAT: ?";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(131, 6);
            // 
            // impostazioniToolStripMenuItem
            // 
            this.impostazioniToolStripMenuItem.Name = "impostazioniToolStripMenuItem";
            this.impostazioniToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.impostazioniToolStripMenuItem.Text = "Settings";
            this.impostazioniToolStripMenuItem.Click += new System.EventHandler(this.impostazioniToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // esciToolStripMenuItem
            // 
            this.esciToolStripMenuItem.Name = "esciToolStripMenuItem";
            this.esciToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.esciToolStripMenuItem.Text = "Exit";
            this.esciToolStripMenuItem.Click += new System.EventHandler(this.esciToolStripMenuItem_Click);
            // 
            // comboPlaybackArctis
            // 
            this.comboPlaybackArctis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPlaybackArctis.FormattingEnabled = true;
            this.comboPlaybackArctis.Location = new System.Drawing.Point(12, 27);
            this.comboPlaybackArctis.Name = "comboPlaybackArctis";
            this.comboPlaybackArctis.Size = new System.Drawing.Size(580, 23);
            this.comboPlaybackArctis.TabIndex = 0;
            // 
            // comboPlaybackSpeaker
            // 
            this.comboPlaybackSpeaker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboPlaybackSpeaker.FormattingEnabled = true;
            this.comboPlaybackSpeaker.Location = new System.Drawing.Point(12, 71);
            this.comboPlaybackSpeaker.Name = "comboPlaybackSpeaker";
            this.comboPlaybackSpeaker.Size = new System.Drawing.Size(580, 23);
            this.comboPlaybackSpeaker.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Arctis Headphones";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Speakers";
            // 
            // save
            // 
            this.save.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.save.Location = new System.Drawing.Point(12, 134);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(1162, 23);
            this.save.TabIndex = 4;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // autoSwapCheckBox
            // 
            this.autoSwapCheckBox.AutoSize = true;
            this.autoSwapCheckBox.Location = new System.Drawing.Point(12, 100);
            this.autoSwapCheckBox.Name = "autoSwapCheckBox";
            this.autoSwapCheckBox.Size = new System.Drawing.Size(242, 19);
            this.autoSwapCheckBox.TabIndex = 5;
            this.autoSwapCheckBox.Text = "Auto switch device based on arctis status";
            this.autoSwapCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(598, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Speakers Microphone";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(598, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Arctis Microphone";
            // 
            // comboRecordSpeaker
            // 
            this.comboRecordSpeaker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboRecordSpeaker.FormattingEnabled = true;
            this.comboRecordSpeaker.Location = new System.Drawing.Point(598, 71);
            this.comboRecordSpeaker.Name = "comboRecordSpeaker";
            this.comboRecordSpeaker.Size = new System.Drawing.Size(576, 23);
            this.comboRecordSpeaker.TabIndex = 7;
            // 
            // comboRecordArctis
            // 
            this.comboRecordArctis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboRecordArctis.FormattingEnabled = true;
            this.comboRecordArctis.Location = new System.Drawing.Point(598, 27);
            this.comboRecordArctis.Name = "comboRecordArctis";
            this.comboRecordArctis.Size = new System.Drawing.Size(576, 23);
            this.comboRecordArctis.TabIndex = 6;
            // 
            // FormHidden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 169);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboRecordSpeaker);
            this.Controls.Add(this.comboRecordArctis);
            this.Controls.Add(this.autoSwapCheckBox);
            this.Controls.Add(this.save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboPlaybackSpeaker);
            this.Controls.Add(this.comboPlaybackArctis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHidden";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ArctisCeleSwitcher Configuration";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHidden_FormClosing);
            this.trayMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerRefreshStatus;
        private NotifyIcon trayIcon;
        private ComboBox comboPlaybackArctis;
        private ComboBox comboPlaybackSpeaker;
        private Label label1;
        private Label label2;
        private Button save;
        private ContextMenuStrip trayMenu;
        private ToolStripMenuItem impostazioniToolStripMenuItem;
        private ToolStripMenuItem esciToolStripMenuItem;
        private ToolStripMenuItem tESTToolStripMenuItem;
        private ToolStripMenuItem statusLabel;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private CheckBox autoSwapCheckBox;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem sendToneToolStripMenuItem;
        private Label label3;
        private Label label4;
        private ComboBox comboRecordSpeaker;
        private ComboBox comboRecordArctis;
    }
}