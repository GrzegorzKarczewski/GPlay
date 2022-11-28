using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;


namespace GPlay
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ReadAllSettings();
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

        // Global variables used in app
        string defaultPlaylist = ConfigurationManager.AppSettings.Get("DefaultPlaylist");
        string currentTrack;
        string currentPlaylist;
        string selectedFolder;
        WMPLib.WindowsMediaPlayer mp3player = new WMPLib.WindowsMediaPlayer();
        double currentTrackPosition = 0;



        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                Console.WriteLine(result);

            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 100;
            //trackBar1.Value = mp3player.settings.volume;

        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            mp3player.controls.currentPosition = currentTrackPosition;
            if (currentTrackPosition == 0)
            {
                // This code ensures that when selected with mouse
                // we can still unpause it with play button
                currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);
                mp3player.settings.volume = 100;
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
                string[] results = Directory.GetFileSystemEntries(selectedFolder, "*.mp3", SearchOption.AllDirectories);
                playlistBox.Items.AddRange(results);


            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (playlistBox.Items.Count > 0)
            {
                if (playlistBox.SelectedItem != null)
                {
                    currentTrack = playlistBox.GetItemText(playlistBox.SelectedItem);
                    mp3player.URL = Path.Combine(selectedFolder + Path.DirectorySeparatorChar + currentTrack);

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
                    AddUpdateAppSettings("DefaultPlaylist", saveFileDialog1.FileName);
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
    }
}
