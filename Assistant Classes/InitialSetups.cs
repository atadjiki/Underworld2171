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

namespace CityOfRemnantsGUI.Assistant_Classes
{
    public static class InitialSetups
    {
        public static void placePlayersInCorners(GUI gui)
        {
            GamePlayerFunctions.generalButtonClick(gui, gui.button1, 1); //replace with actions not gen button clicks
            GamePlayerFunctions.advanceTurn(gui);
            GamePlayerFunctions.generalButtonClick(gui, gui.button8, 8);
            GamePlayerFunctions.advanceTurn(gui);
            GamePlayerFunctions.generalButtonClick(gui, gui.button57, 57);
            GamePlayerFunctions.advanceTurn(gui);
            GamePlayerFunctions.generalButtonClick(gui, gui.button64, 64);
            GamePlayerFunctions.advanceTurn(gui);
        }

        public static void preliminarySetup(GUI gui)
        {

            PanelControls.setupFlag = true;
            PlayerResources.gameState = new GameState(); //create brand new game

            //populate button list
            gui.buttonList = ButtonControls.populateButtons(gui);

            //set status bar
            gui.turnStatus.Text = "Turn: Player " + PlayerResources.gameState.currentPlayer;

            PlayerResources.gameState.currentPlayer = 1;

            //place players in corners
            InitialSetups.placePlayersInCorners(gui);
            ButtonControls.setCursorsForNextTurn(gui);
            PanelControls.setupFlag = false; //declare end of setup
            LoadSave.loadFlag = false; //declare end of game load

            GamePlayerFunctions.availableAction = true;
            //update player credits

            //give all players an initial income of 2000 credits
            PlayerResources.gameState.initialIncome(2000);
            gui.creditsBox.Text = "" + PlayerResources.gameState.selectCurrentPlayer().goldCount;
            gui.infoBox.Text = " Welcome. " + PlayerResources.gameState.player1.getName() + "'s \n turn.";
        }
    }
}
