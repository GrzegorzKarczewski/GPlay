﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPlay
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string currentTrack;
        WMPLib.WindowsMediaPlayer mp3player = new WMPLib.WindowsMediaPlayer();
        double currentTrackPosition = 0;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            mp3player.controls.currentPosition = currentTrackPosition;
            if (currentTrackPosition == 0)
            {
                mp3player.URL = currentTrack;

                mp3player.controls.play();
                MessageBox.Show("current track position is always zero");
            }
            else
            {
                mp3player.controls.currentPosition = currentTrackPosition;
                mp3player.controls.play();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            // Sets position in current track globally so when resumed
            // program knows from where to play
            mp3player.controls.pause();
            currentTrackPosition = mp3player.controls.currentPosition;
            MessageBox.Show(currentTrackPosition.ToString());
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            mp3player.controls.stop();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openMusicFile = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Music Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".mp3",
                Filter = "mp3 files (*.mp3)|*.mp3",
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true

            };

            if (openMusicFile.ShowDialog() == DialogResult.OK)
            {

                tB_currentTrack.Text = openMusicFile.FileName;
                currentTrack = openMusicFile.FileName;
                //TODO : display name of the track only
                //          now it displays whole path
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GPlay is a open source music player." +
                "Made by Grzegorz Karczewski");
        }
    }
}
