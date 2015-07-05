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
    public static class GamePlayerFunctions
    {

        internal static bool aiFlag = false; //set to true during processGameLoop
        internal static bool availableAction = true; //if false, the player will have to end their turn

        public static void genericButtonClickEvent(GUI gui, object sender, EventArgs e)
        {
            SoundEffectPlayer.playSoundEffect(Properties.Resources.button_press);

            //retrieve the button object that has been interacted with
            var btn = sender as Button;
            int index; //the address of this button in the context of the game board
            string name = btn.Name; //the name of the button object

            //extract the integer number from the button name that corresponds to the game grid
            index = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(name, "[^\\d]")));

            ButtonControls.setCursorsForNextTurn(gui);

            generalButtonClick(gui, btn, index); //generate button click based on the extracted index number
        }


        /*
         * Simulates the "transaction" of a player taking a square on the board. 
         * A choice can be made between the type of development. 
         * Also supports an automated track for the computer AI
         * */
        public static void generalButtonClick(GUI gui, Button button, int index)
        {

            if (availableAction == false && PanelControls.setupFlag == false)
            {
                InformationDisplays.noMoreActionsText(gui.infoBox);
                return;
            }


            //if the player already owns this tile
            //they should not waste their turn re-taking it ;)
            if (PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).getOccupier() == PlayerResources.gameState.currentPlayer && LoadSave.loadFlag == false)
            {
                return;
            }

            //if the selection is an illegal move, prevent the move
            if (PlayerResources.gameState.verifyNeighbors(PlayerResources.gameState.selectCurrentPlayer(), index) == false && LoadSave.loadFlag == false)
            {
                return;
            }

            //if the selection is not unoccupied and belongs to another player, it cannot be taken
            if (PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).getOccupier() != 0)
            {
                return;
            }

            if (PanelControls.setupFlag == false && LoadSave.loadFlag == false && aiFlag == false)
            {
                //verify that player wants to take a tile for their turn
                DialogResult dialogResult = MessageBox.Show("Buy Selected Tile?", "City of Remnants", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    SoundEffectPlayer.playSoundEffect(Properties.Resources.cancel_option);
                    return;
                }
            }

            SoundEffectPlayer.playSoundEffect(Properties.Resources.ok_option);


            if (PanelControls.setupFlag == false && aiFlag == false)
            {
                //choose cost of develop to determine yield of square
                MessageBoxManager.OK = "Racket";
                MessageBoxManager.Cancel = "Front";
                MessageBoxManager.Register();

                //verify that player wants to take a tile for their turn
                DialogResult dialogResult2 = MessageBox.Show("Choose Development Type: \nRacket (2000 cred/250 per turn), \nFront (1000 cred/60 per turn) ", "City of Remnants", MessageBoxButtons.OKCancel);

                if (dialogResult2 == DialogResult.OK)
                {
                    SoundEffectPlayer.playSoundEffect(Properties.Resources.ok_option2);


                    //check if player has the right balance
                    if (PlayerResources.gameState.selectCurrentPlayer().goldCount >= 2000)
                    {
                        //set yield of square to 250
                        PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).yield = 250;
                        PlayerResources.gameState.selectCurrentPlayer().goldCount += -2000;
                    }
                    else
                    {
                        MessageBoxManager.Unregister();

                        SoundEffectPlayer.playSoundEffect(Properties.Resources.cancel_option2);

                        return;
                    }
                }
                else if (dialogResult2 == DialogResult.Cancel)
                {

                    SoundEffectPlayer.playSoundEffect(Properties.Resources.ok_option);


                    //check if player has the right balance
                    if (PlayerResources.gameState.selectCurrentPlayer().goldCount >= 1000)
                    {
                        //set yield of square to 60
                        PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).yield = 60;
                        PlayerResources.gameState.selectCurrentPlayer().goldCount += -1000;
                    }
                    else
                    {
                        MessageBoxManager.Unregister();
                        InformationDisplays.insufficientFundsText(gui.infoBox);
                        return;
                    }

                }
                else
                {
                    MessageBoxManager.Unregister();
                    return;
                }

                MessageBoxManager.Unregister();
            }
            else if (PanelControls.setupFlag == true && aiFlag == false)
            {
                //headquarters all have a yield of 100
                PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).yield = 100;

            }
            else if (aiFlag == true)
            {
                //figure out what prices the player can afford 
                List<string> aiChoices = new List<string>();
                aiChoices.Add("FRONT");
                if (PlayerResources.gameState.selectCurrentPlayer().goldCount >= 2000)
                    aiChoices.Add("RACKET");

                //now pick one and buy it 
                int r = PlayerResources.rndProperty.Next(aiChoices.Count);
                string randomAction = (string)aiChoices[r];

                if (randomAction == "FRONT")
                {
                    //buy front
                    //set yield of square to 60
                    PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).yield = 60;
                    PlayerResources.gameState.selectCurrentPlayer().goldCount += -1000;
                }
                else
                {
                    //buy racket
                    //set yield of square to 250
                    PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).yield = 250;
                    PlayerResources.gameState.selectCurrentPlayer().goldCount += -2000;
                }
            }


            if (aiFlag == false)
            {
                InformationDisplays.transactionCompleteText(gui.infoBox);

                //update player credits
                gui.creditsBox.Text = "" + PlayerResources.gameState.selectCurrentPlayer().goldCount;
            }

            //set button tooltips 
            ButtonControls.setButtonToolTips(gui);

            //toggle button light
            ButtonControls.setButtonLight(button, PlayerResources.gameState.currentPlayer);

            //change occupier of selected square
            PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).setOccupier(PlayerResources.gameState.currentPlayer);

            //update player information
            PlayerResources.gameState.updatePlayerStatistics(PlayerResources.gameState.currentPlayer, PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1));

            //update the stats bar
            PanelControls.updateScore(gui);

            //set cursors 
            ButtonControls.setCursorsForNextTurn(gui);


            availableAction = false; // no more actions

            //refresh button text
            if (PanelControls.setupFlag == true)
                button.Text = PlayerResources.gameState.currentPlayer.ToString();

        }

        public static void processGameLoop(GUI gui)
        {
            InterfaceControls.DisableControls(gui);
            ButtonControls.cursorsFlag = true;
            gui.Cursor = Cursors.WaitCursor;
            aiFlag = true;
            advanceTurn(gui); //set current turn to player turn

            while (PlayerResources.gameState.currentPlayer != 1)
            {


                //for each player, determine the possible actions they can take, 
                //randomly do one of the actions, and then advance to the next turn 
                List<string> possibleActions;

                //retrieve possible actions that the player can do
                possibleActions = PlayerResources.gameState.returnPossibleActions(PlayerResources.gameState.selectCurrentPlayer());

                //if there are no possible actions, omit the step (the player is broke)
                if (possibleActions.Count != 0)
                {

                    while (availableAction == true)
                    {
                        //from possible actions, pick one and do it
                        int r = PlayerResources.rndAction.Next(possibleActions.Count);
                        string randomAction = (string)possibleActions[r];

                        //if the choice was to buy a development, do the appropriate action and then advance turn 

                        if (randomAction == "BUY")
                        {
                            List<string> possibleNeighbors = PlayerResources.gameState.getAllNeighbors(PlayerResources.gameState.selectCurrentPlayer);
                            int randomIndex = PlayerResources.rndTile.Next(possibleNeighbors.Count);
                            string randomButton = (string)possibleNeighbors[randomIndex];
                            int index = Convert.ToInt32(string.Join(null, System.Text.RegularExpressions.Regex.Split(randomButton, "[^\\d]")));

                            if (PlayerResources.gameState.gameBoard.getBoard().ElementAt(index - 1).getOccupier() == 0)
                            {
                                //generate a button click with the returned button
                                Button button = gui.buttonList.Find(item => item.Name == randomButton);
                                GamePlayerFunctions.generalButtonClick(gui,button, index);
                            }
                            else
                            {
                                availableAction = false;
                            }
                        }

                    }
                }

                //now that the player has done their action, advance the turn 
                advanceTurn(gui);
            }

            aiFlag = false;
            ButtonControls.cursorsFlag = false;

            PlayerResources.gameState.addIncomesForAllPlayers();
            gui.creditsBox.Text = "" + PlayerResources.gameState.selectCurrentPlayer().goldCount;
            InterfaceControls.EnableControls(gui);
            gui.Cursor = Cursors.Default;
        }

        public static void advanceTurn(GUI gui)
        {

            //update scores
            PanelControls.updateScore(gui);

            //set the turn for the next player's move
            PlayerResources.gameState.toggleTurn();

            //set status bar information
            gui.turnStatus.Text = "Turn: Player " + PlayerResources.gameState.currentPlayer;

            ButtonControls.setCursorsForNextTurn(gui);

            if (PlayerResources.gameState.currentPlayer == 1 && GamePlayerFunctions.aiFlag == false)
            {

                gui.creditsBox.Text = PlayerResources.gameState.player1.goldCount.ToString();
            }

            GamePlayerFunctions.availableAction = true;

            gui.infoBox.ForeColor = System.Drawing.Color.Green;
            gui.infoBox.Text = "Turn: " + PlayerResources.gameState.selectCurrentPlayer().getName();

        }
    } 
}
