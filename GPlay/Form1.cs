using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GPlay
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string currentTrack;
        string selectedFolder;

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
                // This code ensures that when selected with mouse
                // we can still unpause it with play button
                currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                mp3player.URL = selectedFolder + "\\" + currentTrack;        
                mp3player.controls.play();
                tB_currentTrack.Text = currentTrack.Remove(currentTrack.Length - 4);
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
             
            }
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GPlay is a open source music player. " +
                "Made by Grzegorz Karczewski");
        }

        private void createPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog playlistFolder = new FolderBrowserDialog
            {
             // placeholder comment for visibility
            };
            if (playlistFolder.ShowDialog() == DialogResult.OK)
            {
         
                selectedFolder = playlistFolder.SelectedPath;
                foreach (var playlistItem in selectedFolder)
                {
                    var results = Directory.GetFiles(selectedFolder, "*.mp3")
                          .Select(file => Path.GetFileName(file)).ToArray();
                    playlistBox.Items.AddRange(results);
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playlistBox.Items.Count > 0)
            {
                if (playlistBox.SelectedItem != null)
                {
                    currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                    mp3player.URL = selectedFolder + "\\" + currentTrack;
                    mp3player.controls.play();
                    tB_currentTrack.Text = currentTrack.Remove(currentTrack.Length - 4);
                }
            }
        }

        private void savePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";      
            saveFileDialog1.Title = "Save Playlist";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {


                //using (FileStream fs = File.Create(saveFileDialog1.FileName))
                //{
                //    foreach (var item in playlistBox.Items)
                //    {
                //        var length = item.ToString().Length;
                //        Byte[] playlistContent = new UTF8Encoding(true).GetBytes(item.ToString());
                //        fs.Write(playlistContent, 0, length);


                //    }
                //}

                // another way
                try 
               {
                    using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        StreamWriter write = new StreamWriter(fs);
                        foreach (var item in playlistBox.Items)
                        {
                            write.BaseStream.Seek(0, SeekOrigin.End);
                            write.WriteLine(selectedFolder + "\\" + item.ToString());
                            //write.WriteLine(Environment.NewLine);
                            write.Flush();
                        }
                        fs.Close();
                    }
                }
              catch
                {
                    MessageBox.Show("ERROR Saving Playlist");
                }



            }
           
        }
    }
}
