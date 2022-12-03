﻿using System;
using System.Windows.Forms;

namespace GPlay
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonPlay = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.playlistBox = new System.Windows.Forms.ListBox();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCurrentPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.l_playlist = new System.Windows.Forms.Label();
            this.l_currentPlaylist = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.l_mediatype = new System.Windows.Forms.Label();
            this.l_currentPosition = new System.Windows.Forms.Label();
            this.l_trackLength = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(217, 0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(298, 0);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 3;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(379, 0);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // playlistBox
            // 
            this.playlistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playlistBox.FormattingEnabled = true;
            this.playlistBox.Location = new System.Drawing.Point(-12, 65);
            this.playlistBox.Name = "playlistBox";
            this.playlistBox.Size = new System.Drawing.Size(1184, 667);
            this.playlistBox.TabIndex = 6;
            this.playlistBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.playlistBox_MouseDoubleClick);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTrackToolStripMenuItem,
            this.openPlaylistToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openTrackToolStripMenuItem
            // 
            this.openTrackToolStripMenuItem.Name = "openTrackToolStripMenuItem";
            this.openTrackToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openTrackToolStripMenuItem.Text = "Open track";
            this.openTrackToolStripMenuItem.Click += new System.EventHandler(this.openTrackToolStripMenuItem_Click);
            // 
            // openPlaylistToolStripMenuItem
            // 
            this.openPlaylistToolStripMenuItem.Name = "openPlaylistToolStripMenuItem";
            this.openPlaylistToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openPlaylistToolStripMenuItem.Text = "Open playlist";
            this.openPlaylistToolStripMenuItem.Click += new System.EventHandler(this.openPlaylistToolStripMenuItem_Click);
            // 
            // playlistToolStripMenuItem
            // 
            this.playlistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createPlaylistToolStripMenuItem,
            this.savePlaylistToolStripMenuItem,
            this.clearCurrentPlaylistToolStripMenuItem});
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(56, 19);
            this.playlistToolStripMenuItem.Text = "Playlist";
            // 
            // createPlaylistToolStripMenuItem
            // 
            this.createPlaylistToolStripMenuItem.Name = "createPlaylistToolStripMenuItem";
            this.createPlaylistToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.createPlaylistToolStripMenuItem.Text = "Create Playlist";
            this.createPlaylistToolStripMenuItem.Click += new System.EventHandler(this.createPlaylistToolStripMenuItem_Click);
            // 
            // savePlaylistToolStripMenuItem
            // 
            this.savePlaylistToolStripMenuItem.Name = "savePlaylistToolStripMenuItem";
            this.savePlaylistToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.savePlaylistToolStripMenuItem.Text = "Save Playlist";
            this.savePlaylistToolStripMenuItem.Click += new System.EventHandler(this.savePlaylistToolStripMenuItem_Click);
            // 
            // clearCurrentPlaylistToolStripMenuItem
            // 
            this.clearCurrentPlaylistToolStripMenuItem.Name = "clearCurrentPlaylistToolStripMenuItem";
            this.clearCurrentPlaylistToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.clearCurrentPlaylistToolStripMenuItem.Text = "Clear current Playlist";
            this.clearCurrentPlaylistToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentPlaylistToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.aboutToolStripMenuItem.Text = "Help";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 38);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // l_playlist
            // 
            this.l_playlist.AutoSize = true;
            this.l_playlist.Location = new System.Drawing.Point(-3, 49);
            this.l_playlist.Name = "l_playlist";
            this.l_playlist.Size = new System.Drawing.Size(81, 13);
            this.l_playlist.TabIndex = 7;
            this.l_playlist.Text = "Current playlist: ";
            // 
            // l_currentPlaylist
            // 
            this.l_currentPlaylist.AutoSize = true;
            this.l_currentPlaylist.Location = new System.Drawing.Point(70, 49);
            this.l_currentPlaylist.Name = "l_currentPlaylist";
            this.l_currentPlaylist.Size = new System.Drawing.Size(35, 13);
            this.l_currentPlaylist.TabIndex = 8;
            this.l_currentPlaylist.Text = "label2";
            this.l_currentPlaylist.Click += new System.EventHandler(this.l_currentPlaylist_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBar1.Location = new System.Drawing.Point(604, 0);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(81, 45);
            this.trackBar1.TabIndex = 9;
            this.trackBar1.TickFrequency = 5;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // trackBar2
            // 
            this.trackBar2.LargeChange = 1;
            this.trackBar2.Location = new System.Drawing.Point(703, 0);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(469, 45);
            this.trackBar2.TabIndex = 1;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar2_MouseDown);
            // 
            // l_mediatype
            // 
            this.l_mediatype.AutoSize = true;
            this.l_mediatype.Location = new System.Drawing.Point(-3, 739);
            this.l_mediatype.Margin = new System.Windows.Forms.Padding(0);
            this.l_mediatype.Name = "l_mediatype";
            this.l_mediatype.Size = new System.Drawing.Size(30, 13);
            this.l_mediatype.TabIndex = 13;
            this.l_mediatype.Text = "MTP";
            this.l_mediatype.Click += new System.EventHandler(this.label1_Click);
            // 
            // l_currentPosition
            // 
            this.l_currentPosition.AutoSize = true;
            this.l_currentPosition.Location = new System.Drawing.Point(89, 739);
            this.l_currentPosition.Name = "l_currentPosition";
            this.l_currentPosition.Size = new System.Drawing.Size(35, 13);
            this.l_currentPosition.TabIndex = 14;
            this.l_currentPosition.Text = "label1";
            // 
            // l_trackLength
            // 
            this.l_trackLength.AutoSize = true;
            this.l_trackLength.Location = new System.Drawing.Point(151, 739);
            this.l_trackLength.Name = "l_trackLength";
            this.l_trackLength.Size = new System.Drawing.Size(35, 13);
            this.l_trackLength.TabIndex = 15;
            this.l_trackLength.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.l_trackLength);
            this.Controls.Add(this.l_currentPosition);
            this.Controls.Add(this.l_mediatype);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.l_currentPlaylist);
            this.Controls.Add(this.l_playlist);
            this.Controls.Add(this.playlistBox);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "GPlay";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ListBox playlistBox;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem savePlaylistToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem clearCurrentPlaylistToolStripMenuItem;
        private System.Windows.Forms.Label l_playlist;
        private System.Windows.Forms.Label l_currentPlaylist;
        private System.Windows.Forms.TrackBar trackBar1;
       // private Timer timer2;
      
        public TrackBar trackBar2;
        private Label l_mediatype;
        private Label l_currentPosition;
        private Label l_trackLength;
    }
}

