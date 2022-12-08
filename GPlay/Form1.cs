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

        private bool isPlaying = false;
        private bool isPaused = false;
        private bool isStopped = false;
        private bool isLoaded = false;
        private bool isNextTrack = false;
        private bool isDefaultPlaylistLoaded = false;
        List<string> atrributes;
        private bool isOtherPlaylistLoaded = false;
        System.Windows.Forms.ListBox playlistBoxTwo;
        System.Windows.Forms.ListBox ActivePlaylistbox;

        public Form1()
        {
            InitializeComponent();
            GSettings.ReadAllSettings();
            LoadDefaultPlaylist();
            l_mediatype.Text = string.Empty;
            l_currentPosition.Text = string.Empty;
            l_trackLength.Text = string.Empty;
            //tabControl1 = new TabControl(); // this line of code has me spent few hours looking for a problem why second tab wouldnt show.
            // When i saw this and realize i'm creating a new instance of tabcontrol1
            // instead of previous one i understood that i'm refering to different object then intended 
            // leaving this comment as a reminder
            ActivePlaylistbox = new ListBox();
            tabPage1.Controls.Add(playlistBox);

            playlistBox.Dock = DockStyle.Fill;
            tabPage1.Text = defaultPlaylist;

           

        }

        private void LoadDefaultPlaylist()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(defaultPlaylist);
                foreach (string line in lines)
                {
                    playlistBox.Items.Add(line);
                }
                isDefaultPlaylistLoaded = true;
            }
            catch { }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 100;
            trackBar2.Value = 0;
            trackBar2.LargeChange = 1;
            trackBar2.SmallChange = 1;
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
            isPaused = false;
            isStopped = false;

            mp3player.controls.currentPosition = currentTrackPosition;
            if (currentTrackPosition == 0)
            {
                trackBar2.Value = 0;

                // This code ensures that when selected with mouse
                // we can still unpause it with play button
                //currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                // mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                playlistBox.SelectedIndex = 0;
                playlistBox.SelectedItem = playlistBox.SelectedIndex;
                currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                mp3player.settings.volume = 100;
                playFileAndSetOtherStuff();
                isPlaying = true;
                new Thread(delegate ()
                {
                    mp3player.PlayStateChange += Mp3player_LoadInfo;
                }).Start();
                //.Text = currentTrack.Remove(currentTrack.Length - 4);
                mp3player_PlayStateChange();
                //Text = mp3player.currentMedia.getItemInfo("title");

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

                // Setting active playlistbox


                
                if (!isPaused && !isStopped)
                {
                    // code below should be executed every Interval (1 sec)
                    //l_trackLength.Text = TimeSpan.FromSeconds((mp3player.currentMedia.duration/60)).ToString();

                    // trackBar2.Maximum = ((int)mp3player.currentMedia.duration + 1);
                    //trackBar2.TickFrequency = 100/(int)mp3player.currentMedia.duration;
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



                    // This part of code is responsible for changing to next track on the list
                    if (trackBar2.Value == trackBar2.Maximum - 1)
                    {
                        int playlistLength = ActivePlaylistbox.Items.Count - 1;

                        // Conditions needed to check if its last track on the list
                        // if it is, skip to first
                        // We can put this into function later for a user to choose this if they want
                        if (ActivePlaylistbox.SelectedIndex == playlistLength)
                        {
                            ActivePlaylistbox.SelectedIndex = 0;
                            ActivePlaylistbox.SelectedItem = ActivePlaylistbox.SelectedIndex;
                            currentTrack = ActivePlaylistbox.GetItemText(ActivePlaylistbox.SelectedItem);
                            mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                            playFileAndSetOtherStuff();
                            isPlaying = true;
                            isNextTrack = true;
                        }
                        else
                        {
                            ActivePlaylistbox.SelectedIndex = ActivePlaylistbox.SelectedIndex + 1;
                            ActivePlaylistbox.SelectedItem = ActivePlaylistbox.SelectedIndex;
                            currentTrack = ActivePlaylistbox.GetItemText(ActivePlaylistbox.SelectedItem);
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

                //tB_currentTrack.Text = openMusicFile.FileName;
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
            // zrobic zeby druga playlista korzystala z tego samego eventu
            //ref ListBox activePlaylistBox =
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
            {
                if (ActivePlaylistbox != null)
                    ActivePlaylistbox = playlistBox;
            }

            if (ActivePlaylistbox.Items.Count > 0)
            {

                int index = ActivePlaylistbox.IndexFromPoint(e.Location);

                if (index != System.Windows.Forms.ListBox.NoMatches)
                {
                    currentTrack = ActivePlaylistbox.GetItemText(ActivePlaylistbox.SelectedItem);
                    MessageBox.Show(currentTrack);
                    mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                    playFileAndSetOtherStuff();
                    isPlaying = true;

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
                if (isOtherPlaylistLoaded)
                {
                    if (ActivePlaylistbox.Items.Count > 0)
                    {
                        ActivePlaylistbox.Items.Clear();
                    }
                    string[] lines = System.IO.File.ReadAllLines(openPlaylistFile.FileName);
                    foreach (string line in lines)
                    {
                        ActivePlaylistbox.Items.Add(line);
                    }

                    // show current playlist name
                    //l_currentPlaylist.Text = openPlaylistFile.FileName;
                    //saving opened playlist as default so we have the same on reopening aplication
                    GSettings.AddUpdateAppSettings("DefaultPlaylist", openPlaylistFile.FileName);
                }
                else if (!isOtherPlaylistLoaded)
                {
                    if (isDefaultPlaylistLoaded)
                    {

                        // Creating seperate playlistbox and TABPage
                       
                        playlistBoxTwo = new System.Windows.Forms.ListBox();
                        playlistBoxTwo.Dock = DockStyle.Fill;
                        tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                |       System.Windows.Forms.AnchorStyles.Right)));
                        playlistBoxTwo.FormattingEnabled = true;
                        playlistBoxTwo.Location = new System.Drawing.Point(0, 65);
                        playlistBoxTwo.Name = "playlistBoxTwo";
                        playlistBoxTwo.Size = new System.Drawing.Size(1184, 667);
                        playlistBoxTwo.TabIndex = 6;
                        playlistBoxTwo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.playlistBox_MouseDoubleClick);




                        TabPage tabSecondPlaylist = new TabPage("tabSecondPlaylist");
                       // tabControl1.Controls.Add(tabSecondPlaylist);
                        //this.Controls.Add(tabControl1);

                        tabSecondPlaylist.Location = new System.Drawing.Point(8, 44); // it was 4,22
                        //tabSecondPlaylist.Name = "tabSecondPlaylist";
                        tabSecondPlaylist.Padding = new System.Windows.Forms.Padding(3);
                        tabSecondPlaylist.Size = new System.Drawing.Size(1176, 665);
                        tabSecondPlaylist.TabIndex = 1;
                        tabSecondPlaylist.Text = "";
                        tabSecondPlaylist.UseVisualStyleBackColor = true;
                        tabSecondPlaylist.ForeColor = System.Drawing.Color.Azure;
                        //
                        tabSecondPlaylist.Controls.Add(playlistBoxTwo);
                        tabControl1.TabPages.Add(tabSecondPlaylist);
                        // tabControl1.Controls.Add(tabSecondPlaylist);

                        //tabSecondPlaylist.BringToFront();


                        // populating playlist
                        if (playlistBoxTwo.Items.Count > 0)
                        {
                            playlistBoxTwo.Items.Clear();
                        }
                        string[] lines = System.IO.File.ReadAllLines(openPlaylistFile.FileName);
                        foreach (string line in lines)
                        {
                            playlistBoxTwo.Items.Add(line);
                        }
                        tabSecondPlaylist.Text = openPlaylistFile.FileName;
                        tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(tabSecondPlaylist_Selected);

                        MessageBox.Show(openPlaylistFile.FileName);
                        

                        // show current playlist name
                        //l_currentPlaylist.Text = openPlaylistFile.FileName;
                        //saving opened playlist as default so we have the same on reopening aplication
                        //GSettings.AddUpdateAppSettings("DefaultPlaylist", openPlaylistFile.FileName);

                    }
                }

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
            seconds = 0;
            trackBar2.Value = 0;

            mp3player_PlayStateChange();

            // Starting a seperate thread and wait for 1000ms for track to load
            // This was needed when tracks change when they end
            new Thread(delegate ()
            {
                mp3player.PlayStateChange += Mp3player_LoadInfo;
            }).Start();



        }

        private void Mp3player_LoadInfo(int NewState)
        {

            if (NewState == 3)
            {
                isLoaded = true;


                if (isLoaded) // 
                {
                    try
                    {

                        l_trackLength.Text = TimeSpan.FromSeconds((mp3player.currentMedia.duration)).ToString();

                        trackBar2.Maximum = ((int)mp3player.currentMedia.duration + 1);
                        trackBar2.TickFrequency = 100 / (int)mp3player.currentMedia.duration;

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
                        Text = mp3player.currentMedia.getItemInfo("title");



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

        private void l_currentPlaylist_Click(object sender, EventArgs e)
        {

        }
        private void b_prev_Click(object sender, EventArgs e)
        {
            if (playlistBox.Items.Count > 0)
            {

                playlistBox.SelectedIndex = playlistBox.SelectedIndex - 1;
                playlistBox.SelectedItem = playlistBox.SelectedIndex;
                currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                playFileAndSetOtherStuff();
                isPlaying = true;
            }
        }
        private void b_next_Click(object sender, EventArgs e)
        {
            if (playlistBox.Items.Count > 0)
            {

                playlistBox.SelectedIndex = playlistBox.SelectedIndex + 1;
                playlistBox.SelectedItem = playlistBox.SelectedIndex;
                currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                playFileAndSetOtherStuff();
                isPlaying = true;
            }

        }


        private void tabSecondPlaylist_Selected(object sender, TabControlEventArgs e)
        {
            // Change active playlistbox if selected second playlist
            if (ActivePlaylistbox != null && playlistBoxTwo != null)
                ActivePlaylistbox = playlistBoxTwo;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            // for now its empty as doubleclick as setter for active playlist
        }
    }
}
