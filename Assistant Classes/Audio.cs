/*
 * Author: Arash Tadjiki
 * Last Updated: 7/3/2015
 * Version: 1.2
 * atadjiki@yahoo.com 
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityOfRemnantsGUI
{
    public static class SoundEffectPlayer
    {
        public static bool soundFlag;
        public static System.Media.SoundPlayer sp;
        public static string backgroundMusicFilePath;
        public static System.Windows.Media.MediaPlayer backgroundMusicPlayer;

        /********************************************************************************************
         * ************************** Sound related methods: ***************************************
         * *****************************************************************************************/

        static SoundEffectPlayer()
        {
            soundFlag = true; //use when toggling the background music
            backgroundMusicFilePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Resources\\guiSoundtrack.wav");
            backgroundMusicPlayer = new System.Windows.Media.MediaPlayer(); //for background music
        }

        public static void soundSetup()
        {
            PlaybackMusic();
        }
        /*
         * Plays background music at path specified in the GUI class
         * */
        public static void PlaybackMusic()
        {
            if (backgroundMusicFilePath != null)
            {
                backgroundMusicPlayer.Open(new Uri(backgroundMusicFilePath));
                backgroundMusicPlayer.MediaEnded += new EventHandler(Media_Ended);
                backgroundMusicPlayer.Play();

                return;
            }
        }
        /*
         * If the song stops, reload it and play it again.
         * */
        public static void Media_Ended(object sender, EventArgs e)
        {
            backgroundMusicPlayer.Open(new Uri(backgroundMusicFilePath));
            backgroundMusicPlayer.Play();
        }

        public static void playSoundEffect(UnmanagedMemoryStream soundFile)
        {
            if (soundFlag == true)
            {
                Stream stream = soundFile;
                sp = new System.Media.SoundPlayer(stream);
                sp.Play();
            }
        }
    }
}
