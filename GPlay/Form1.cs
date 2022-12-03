using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security;
using System.Threading;
using System.Timers;
using System.Transactions;
using System.Windows.Forms;



namespace GPlay
{

    
    public partial class Form1 : Form
    {
        // Global variables used in app
        string defaultPlaylist = ConfigurationManager.AppSettings.Get("DefaultPlaylist");
        string currentTrack;
        string currentPlaylist;
        string selectedFolder;
        WMPLib.WindowsMediaPlayer mp3player = new WMPLib.WindowsMediaPlayer();
        double currentTrackPosition = 0;
        System.Timers.Timer myTimer;
        int seconds = 0;
        int currentDuration;
        TimeSpan time;
        private bool isPlaying = false;
        private bool isPaused = false;
        private bool isStopped = false;
        private bool isLoaded = false;
        private bool isNextTrack = false;
        List<string> atrributes;


        public Form1()
        {
            InitializeComponent();
            GSettings.ReadAllSettings();
            LoadDefaultPlaylist();
        
        }

        private void LoadDefaultPlaylist()
        {
            string[] lines = System.IO.File.ReadAllLines(defaultPlaylist);
            foreach (string line in lines)
            {
                playlistBox.Items.Add(line);
            }
            l_currentPlaylist.Text = defaultPlaylist;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 100;
            trackBar2.Value = 0;
            trackBar2.LargeChange = 1;
            trackBar2.SmallChange= 1;
            trackBar2.TickFrequency = 1;
            myTimer = new System.Timers.Timer();
            myTimer.Interval = 1000;
            myTimer.Elapsed += timer2_Tick;


        }

        // function for handling playstatechange for current track
        private void mp3player_PlayStateChange()
        {

            if (isPlaying)
            {
              
                myTimer.Start();
                Text = mp3player.currentMedia.getItemInfo("title");

            }
            if (isPaused)
            {
                myTimer.Stop();

            }
            if (isStopped)
            {
                int seconds = 0;
                l_currentPosition.Text = TimeSpan.FromSeconds(seconds).ToString();
                myTimer.Stop();
                trackBar2.Value = 0;
               // l_currentPosition.Text= string.Empty;
                mp3player.controls.currentPosition = 0;
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            isPaused= false;
            isStopped = false;
           
            mp3player.controls.currentPosition = currentTrackPosition;
            if (currentTrackPosition == 0)
            {
                // This code ensures that when selected with mouse
                // we can still unpause it with play button
                currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                mp3player.settings.volume = 100;
                mp3player.controls.play();
                isPlaying= true;

                tB_currentTrack.Text = currentTrack.Remove(currentTrack.Length - 4);
                mp3player_PlayStateChange();
            }
            else
            {
                mp3player.controls.currentPosition = currentTrackPosition;
                isPlaying = true;
                playFileAndSetOtherStuff();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            // Sets position in current track globally so when resumed
            // program knows from where to play
            mp3player.controls.pause();
            isPaused = true;
            mp3player_PlayStateChange();
            currentTrackPosition = mp3player.controls.currentPosition;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            mp3player.controls.stop();
            isStopped = true;
            mp3player.controls.currentPosition = 0;
            mp3player_PlayStateChange();
        }


        private void timer2_Tick(object sender, ElapsedEventArgs e)
        {

            //timer 1 tick event handler 
            // timer was used to track media current playtime and set trackbar
            // caused problems with media file being played with artifacts 
            Invoke(new Action(() =>
            {

                if (!isPaused && !isStopped)
                {
                    // code below should be executed every Interval (1 sec)
                    l_trackLength.Text = TimeSpan.FromSeconds((mp3player.currentMedia.duration/60)).ToString();
                    
                    trackBar2.Maximum = ((int)mp3player.currentMedia.duration + 1);
                    trackBar2.TickFrequency = 100/(int)mp3player.currentMedia.duration;
                    //trackBar2.TickFrequency = (int)mp3player.currentMedia.duration;


                    seconds += 1;
                    if (isNextTrack)
                    {
                        trackBar2.Value = 0;
                        // We are entering here only once to reset the trackbar 
                        isNextTrack = false;
                    }

                    trackBar2.Value = trackBar2.Value + 1;
                    
                    //trackBar2.ValueChanged += moveInTrack;
                    //l_currentPosition.Text += TimeSpan.FromSeconds(seconds).ToString();
                    l_currentPosition.Text = (TimeSpan.FromSeconds((int)mp3player.controls.currentPosition)).ToString();

                    // TODO : 1)
                    // 2)
                    // play next track if currentDuration reached trackbarmaximum

                    currentDuration = (int)mp3player.currentMedia.duration;
                    // This part of code is responsible for changing to next track on the list
                    if (trackBar2.Value == trackBar2.Maximum - 1)
                    {
                        int playlistLength = playlistBox.Items.Count -1;
                     
                        // Conditions needed to check if its last track on the list
                        // if it is, skip to first
                        if (playlistBox.SelectedIndex == playlistLength )
                        {
                            playlistBox.SelectedIndex = 0;
                            playlistBox.SelectedItem = playlistBox.SelectedIndex;
                            currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                            mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                            playFileAndSetOtherStuff();
                            isPlaying = true;
                            isNextTrack = true;
                        }
                        else
                        {
                            playlistBox.SelectedIndex = playlistBox.SelectedIndex + 1;
                            playlistBox.SelectedItem = playlistBox.SelectedIndex;
                            currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                            mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                            playFileAndSetOtherStuff();
                            isPlaying = true;
                            isNextTrack = true;
                        }
                    }

                }
                
            })); 

        }

        private void moveInTrack(object sender, EventArgs e)
        {
            mp3player.controls.currentPosition = trackBar2.Value;
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
                string[] results = Directory.GetFileSystemEntries(selectedFolder, "*.mp3", SearchOption.AllDirectories);
                playlistBox.Items.AddRange(results);


            }

        }

       // private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
         private void playlistBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (playlistBox.Items.Count > 0)
            {   
                int index = this.playlistBox.IndexFromPoint(e.Location);
              
                if (index != System.Windows.Forms.ListBox.NoMatches)
                {
                    currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                    mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                    playFileAndSetOtherStuff();
                    isPlaying = true;
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
                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        StreamWriter write = new StreamWriter(fs);
                        if (playlistBox.Items.Count != 0)
                        {
                            foreach (var item in playlistBox.Items)
                            {
                                write.BaseStream.Seek(0, SeekOrigin.End);
                                write.WriteLine(item.ToString());
                                // must flush to save entire list
                                // without flush it won't save everything
                                write.Flush();
                            }
                            fs.Close();
                        }
                        else
                        {
                            MessageBox.Show("ERROR: Playlist Empty");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("ERROR: Saving Playlist");
                }
                // Saving playlist name to configfile
                try
                {
                    GSettings.AddUpdateAppSettings("DefaultPlaylist", saveFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("ERROR Trying to update configuration with playlist");
                }


            }

        }

        private void openPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {


            OpenFileDialog openPlaylistFile = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Playlist Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".txt",
                Filter = "txt files (*.txt)|*.txt",
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true

            };

            if (openPlaylistFile.ShowDialog() == DialogResult.OK)
            {

                if (playlistBox.Items.Count > 0)
                {
                    playlistBox.Items.Clear();
                }
                string[] lines = System.IO.File.ReadAllLines(openPlaylistFile.FileName);
                foreach (string line in lines)
                {
                    playlistBox.Items.Add(line);
                }

                // show current playlist name
                l_currentPlaylist.Text = openPlaylistFile.FileName;
                //saving opened playlist as default so we have the same on reopening aplication
                GSettings.AddUpdateAppSettings("DefaultPlaylist", openPlaylistFile.FileName);
            }
        }




        private void clearCurrentPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playlistBox.Items.Count > 0)
            {
                playlistBox.Items.Clear();
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            mp3player.settings.volume = trackBar1.Value;
        }

        private void playFileAndSetOtherStuff()
        {
            
            mp3player.controls.play();
            isPlaying = true;
            mp3player_PlayStateChange();
            
            // Starting a seperate thread and wait for 1000ms for track to load
            // This was needed when tracks change when they end
            new Thread(delegate () {
                mp3player.PlayStateChange += Mp3player_LoadInfo;
            }).Start();
            

        }

        private void Mp3player_LoadInfo(int NewState)
        {

            if (NewState == 3)
            {
                Thread.Sleep(1000);
                isLoaded = true;


                if (isLoaded) // 
                {
                    try
                    {
                        int i = mp3player.currentMedia.attributeCount - 1;
                        l_mediatype.Text = mp3player.currentMedia.attributeCount.ToString();
                        atrributes = new List<string>();
                        while (i > 0)
                        {
                            atrributes.Add(mp3player.currentMedia.getAttributeName(i).ToString());
                            i--;
                        }
                        string mediaType = mp3player.currentMedia.getItemInfo("FileType");
                        string BitRate = mp3player.currentMedia.getItemInfo("BitRate");
                        BitRate = BitRate.Remove(BitRate.Length - 3) + " kbps";
                        string TrackInfo = mediaType.ToUpper() + " | " + BitRate + " | " + mp3player.currentMedia.getItemInfo("CurrentBitrate");// +                                                                                                     
                        l_mediatype.Text = TrackInfo;


                        foreach (var attrs in atrributes)
                        {
                            // MessageBox.Show(mp3player.currentMedia.getItemInfo(attrs).ToString());
                        }
                    }
                    catch
                    {

                    }
                }
            }

        }

        private void Mp3player_PlayStateChange(int NewState)
        {
            throw new NotImplementedException();
        }

        private void l_trackLength_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            //mp3player.controls.currentPosition = trackBar2.Value;
           // l_currentPosition.Text = mp3player.controls.currentPosition.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            mp3player.controls.stop();
            myTimer.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }


}
