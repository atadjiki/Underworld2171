/*
 * Author: Arash Tadjiki
 * Last Updated: 7/3/2015
 * Version: 1.2
 * atadjiki@yahoo.com 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CityOfRemnantsGUI.Assistant_Classes
{
    public static class PanelControls
    {

        public static bool setupFlag = true;
        public static bool menuFlag = true;

        /*
          * Updates the info panel texts of all players.
          * Score is based on the yield of the tiles currently under a player's control
          * */
        public static void updateScore(GUI gui)
        {
            statusStripPlayerScores(gui);
        }

        public static void menuSetup(GUI gui)
        {
            gui.menuPanel.Visible = true;
            gui.statusStrip.Visible = false;
            gui.gamePanel.Visible = false;
            gui.gamePanel.SendToBack();
            gui.menuPanel.BringToFront();
            gui.statusStrip.SendToBack();
            gui.sidePanel.SendToBack();
        }
        /*
        * Bring the menu screen into focus
        * */
        public static void toggleMenuPanel(GUI gui)
        {
            SoundEffectPlayer.playSoundEffect(Properties.Resources.start_game);
     
            //set conditions for game? (menu screen)?
            gui.startGameButton.Text = "BACK";
            menuSetup(gui);

        }
        public static void toggleGamePanel(GUI gui)
        {

            SoundEffectPlayer.playSoundEffect(Properties.Resources.game_start);

            gui.menuPanel.Visible = false;
            gui.statusStrip.Visible = true;
            gui.gamePanel.Visible = true;
            gui.gamePanel.BringToFront();
            gui.menuPanel.SendToBack();
            gui.statusStrip.BringToFront();
            gui.sidePanel.BringToFront();
        }

        /*
        * Detect the "m" and "s"
        * keys in order to toggle the
        * menu screen and sound controls.
        **/
        public static void genericKeyPress(object sender, KeyPressEventArgs e, GUI gui)
        {
            if (setupFlag == true)
                return;

            if (e.KeyChar.ToString() == Keys.M.ToString().ToLower())
            {
                //toggle menu screen
                if (menuFlag == true)
                {
                    //show the menu panel, and hide the game panel
                    toggleMenuPanel(gui);
                    menuFlag = false;

                }
                else
                {
                    //show the game panel, and show the menu panel
                    toggleGamePanel(gui);
                    menuFlag = true;
                }


            }
            else if (e.KeyChar.ToString() == Keys.S.ToString().ToLower())
            {
                //toggle sound
                if (SoundEffectPlayer.soundFlag == true)
                {
                    SoundEffectPlayer.backgroundMusicPlayer.Pause();
                    SoundEffectPlayer.soundFlag = false;
                }
                else
                {
                    SoundEffectPlayer.backgroundMusicPlayer.Play();
                    SoundEffectPlayer.soundFlag = true;
                }

            }
        }

        public static void endTurnButtonClick(GUI gui, object sender, EventArgs e)
        {
            SoundEffectPlayer.playSoundEffect(Properties.Resources.end_turn);
            GamePlayerFunctions.processGameLoop(gui); //will compute turns for players 2-4, then return to the player
        }

        internal static void statusStripPlayerScores(GUI gui)
        {
            gui.player1Score.Text = PlayerResources.gameState.player1.getName() + " :" + PlayerResources.gameState.player1.calculateIncome();
            gui.player2Score.Text = PlayerResources.gameState.player2.getName() + " :" + PlayerResources.gameState.player2.calculateIncome();
            gui.player3Score.Text = PlayerResources.gameState.player3.getName() + " :" + PlayerResources.gameState.player3.calculateIncome();
            gui.player4Score.Text = PlayerResources.gameState.player4.getName() + " :" + PlayerResources.gameState.player4.calculateIncome();
        }
    }
}
