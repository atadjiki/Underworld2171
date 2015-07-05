/*
 * Author: Arash Tadjiki
 * Last Updated: 7/3/2015
 * Version: 1.2
 * atadjiki@yahoo.com 
 * */
using Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CityOfRemnantsGUI.Assistant_Classes
{
    public static class ButtonControls
    {
        public static bool cursorsFlag = false;
     

        public static void setCursorsForNextTurn(GUI gui)
        {
            reActivateButtons(gui.buttonList);

            //get buttons to be kept active (i.e everything except for neighbors of player tiles)
            List<String> toKeep = PlayerResources.gameState.getAllNeighbors(PlayerResources.gameState.selectCurrentPlayer);

            //deactivate all invalid buttons
            deActivateButtonsExceptFor(gui, toKeep);
        }
        /*
        * Changes the color of the selected button to the 
        * chosen color of the current player.
        * */
        public static void setButtonLight(Button button, int player)
        {
            if (player == 1)
            {
                button.BackColor = PlayerResources.player1Color;
            }
            else if (player == 2)
            {
                button.BackColor = PlayerResources.player2Color;
            }
            else if (player == 3)
            {
                button.BackColor = PlayerResources.player3Color;
            }
            else if (player == 4)
            {
                button.BackColor = PlayerResources.player4Color;
            }
            else if (player == 0)
            {
                button.BackColor = System.Drawing.Color.Transparent;
                button.Text = "";
            }

        }

        /*
      * reactivates all buttons which have been turned off during the turn
      **/
        public static void reActivateButtons(List<Button> buttonList)
        {
            if (cursorsFlag == false)
            {
                foreach (Button b in buttonList)
                {

                    b.Cursor = Cursors.Hand;
                    b.Enabled = true;
                }
            }


        }

        /*
        * dactivates all buttons which have been turned on during the turn
        **/
        public static void deactivateAllButtons(List<Button> buttonList)
        {
            foreach (Button b in buttonList)
            {

                b.Enabled = false;
            }

        }

        /*
         * Deactivates all buttons which are members of the incoming List of button names
         **/
        public static void deActivateButtonsExceptFor(GUI gui, List<String> toKeep)
        {
            if (cursorsFlag == false)
            {
                foreach (Button b in gui.buttonList)
                {
                    //if the button is not a player neighbor, deactivate it
                    if (!(toKeep.Contains(b.Name)))
                    {
                        b.Cursor = Cursors.No;
                    }
                }
            }

        }

        public static void setButtonToolTips(GUI gui)
        {
            int index = 0;

            foreach (Button b in gui.buttonList)
            {
                string tierText = PlayerResources.gameState.gameBoard.getBoard().ElementAt(index).getTier();
                index++;

                gui.toolTipControl.SetToolTip(b, tierText);
            }
        }

        /*
       * Adds all button objects (which correspond to game squares)
       * to a list. This list can be used to bridge the game data
       * to the GUI
       * */
        public static List<Button> populateButtons(GUI gui)
        {

            List<Button> result = gui.Controls.OfType<Button>().Concat(gui.gamePanel.Controls.OfType<Button>()).ToList();

            return result;

        }

        public static void genericMouseHover(object sender, EventArgs e, GUI gui)
        {
            var btn = sender as Button;
            int index; //the address of this button in the context of the game board
            string name = btn.Name; //the name of the button object

            //extract the integer number from the button name that corresponds to the game grid
            index = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(name, "[^\\d]")));

            gui.toolTipControl.SetToolTip(btn, "Yield: " + PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).yield.ToString());

            gui.toolTipControl.Show(gui.toolTipControl.GetToolTip(btn), btn);
        }
    }
}
