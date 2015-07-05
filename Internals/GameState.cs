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
using Internals;

using System.Threading.Tasks;
using System.Media;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Xml.Serialization;


namespace Internals
{
    [Serializable]
    public class GameState
    {

        public GameBoard gameBoard;
        public Player player1;
        public Player player2;
        public Player player3;
        public Player player4;
        public int currentPlayer;
        public int width;
        public List<int> gameMoves;

        /*
         * Default state for an entirely new game.
         * Takes no arguments.
         * Can be saved as a serialized object for future 
         * loading (using the loading constructor)
         * */
        public GameState()
        {
            width = 8;

            //generate a game board and populate it 
            gameBoard = new GameBoard(width);
            gameBoard.populateBoard();
            gameMoves = new List<int>();

            //generate players
            player1 = new Player("Player 1");
            player2 = new Player("Player 2");
            player3 = new Player("Player 3");
            player4 = new Player("Player 4");

            //set initial player
            currentPlayer = 1;

        }

        public GameState(string filePath)
        {
            GameState loadGame;

            //first de-serialize the game state
            using (FileStream fs = new FileStream(filePath, FileMode.Open)) //double check that...
            {
                XmlSerializer _xSer = new XmlSerializer(typeof(GameState));

                try
                {
                    loadGame = (GameState)_xSer.Deserialize(fs);
                }
                catch(SerializationException)
                {
                    throw new SerializationException("Save game is invalid.");
                }
                
            }

            width = loadGame.width;

            //generate a game board and populate it 
            gameBoard = loadGame.gameBoard;
            gameBoard.populateBoard();

            gameMoves = loadGame.gameMoves;

            //generate players
            player1 = loadGame.player1;
            player2 = loadGame.player2;
            player3 = loadGame.player3;
            player4 = loadGame.player4;

            //set initial player
            currentPlayer = 1;

        }


        public List<String> getAllNeighbors(Func<Player> selectCurrentPlayer)
        {
            List<Square> playerTiles = selectCurrentPlayer().getPlayerTiles();

            List<String> neighborIndexes = new List<String>();


            //for each player tile, determine the neighbors, and add their indexes to the resulting list
            foreach (Square s in playerTiles)
            {
                //booleans for neighbor directions
                Dictionary<string, bool> neighborInfo = s.getNeighborInformation();

                if (neighborInfo["north"] == true)
                    neighborIndexes.Add("button" + gameBoard.calculateIndexOfNeighbor(s, "north").ToString());
                if (neighborInfo["south"] == true)
                    neighborIndexes.Add("button" + gameBoard.calculateIndexOfNeighbor(s, "south").ToString());
                if (neighborInfo["east"] == true)
                    neighborIndexes.Add("button" + gameBoard.calculateIndexOfNeighbor(s, "east").ToString());
                if (neighborInfo["west"] == true)
                    neighborIndexes.Add("button" + gameBoard.calculateIndexOfNeighbor(s, "west").ToString());
            }

            return neighborIndexes;
        }

        /*
        * Returns true if the specified index is a neighbor
        * of the player's current territory
        * */
        public bool verifyNeighbors(Player player, int index)
        {
            //first retrieve all tiles belonging to the player
            List<Square> playerTiles = player.getPlayerTiles();

            //if the player doesn't have any tiles, return
            if (playerTiles.Count == 0)
            {
                return true;
            }

            //to contain the addresses of all available neighbors (allowed selections)
            List<int> neighborIndexes = new List<int>();

            //for all player tiles, get neighbors and add indexes to list

            foreach (Square s in playerTiles)
            {
                //first get neighbor information to know which ones exist
                Dictionary<string, bool> neighborInfo = s.getNeighborInformation();

                if (neighborInfo["north"] == true)
                    neighborIndexes.Add(gameBoard.calculateIndexOfNeighbor(s, "north"));
                if (neighborInfo["south"] == true)
                    neighborIndexes.Add(gameBoard.calculateIndexOfNeighbor(s, "south"));
                if (neighborInfo["east"] == true)
                    neighborIndexes.Add(gameBoard.calculateIndexOfNeighbor(s, "east"));
                if (neighborInfo["west"] == true)
                    neighborIndexes.Add(gameBoard.calculateIndexOfNeighbor(s, "west"));
            }

            //make sure the index of the button selected is a valid neighbor of the player's territory
            if (!(neighborIndexes.Contains(index)))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public void updatePlayerStatistics(int player, Square square)
        {

            //add square to corresponding player who seized it
            //and remove from everyone else's collection
            if (currentPlayer == 1)
            {
                player1.addSquare(square);
                player2.removeSquare(square);
                player3.removeSquare(square);
                player4.removeSquare(square);
            }
            else if (currentPlayer == 2)
            {
                player2.addSquare(square);
                player1.removeSquare(square);
                player3.removeSquare(square);
                player4.removeSquare(square);
            }
            else if (currentPlayer == 3)
            {
                player3.addSquare(square);
                player1.removeSquare(square);
                player2.removeSquare(square);
                player4.removeSquare(square);
            }
            else if (currentPlayer == 4)
            {
                player4.addSquare(square);
                player1.removeSquare(square);
                player2.removeSquare(square);
                player3.removeSquare(square);
            }

        }

        /*
       * Returns the player object
       * for the player who is currently
       * playing their turn
       * */
        public Player selectCurrentPlayer()
        {
            if (currentPlayer == 1)
            {
                return player1;
            }
            else if (currentPlayer == 2)
            {
                return player2;
            }
            else if (currentPlayer == 3)
            {
                return player3;
            }
            else
            {
                return player4;

            }
        }

        /*
        * Advances the turn one place.
        * After player 4's turn, 
        * the turn will be reset to player 1
         * */
        public void toggleTurn()
        {
            //reset turn back to player 1
            if (currentPlayer == 4)
                currentPlayer = 1;
            else
                currentPlayer++;
        }

        public void initialIncome(int amount)
        {
            player1.goldCount = amount;
            player2.goldCount = amount;
            player3.goldCount = amount; 
            player4.goldCount = amount;
        }

        public void addIncomesForAllPlayers()
        {
            //for each player, go through their tiles and add the yields to their gold counts
            foreach(Square s in player1.getPlayerTiles())
            {
                player1.goldCount += s.yield;
            }
            foreach (Square s in player2.getPlayerTiles())
            {
                player2.goldCount += s.yield;
            }
            foreach (Square s in player3.getPlayerTiles())
            {
                player3.goldCount += s.yield;
            }
            foreach (Square s in player4.getPlayerTiles())
            {
                player4.goldCount += s.yield;
            }
        }

        public List<string> returnPossibleActions(Player player)
        {

            List<string> possibleActions = new List<string>();

            //if enough money to purchase a development (i.e, over $1000), add the BUY string to the list
            if (player.goldCount > 1000 && player.playerOwnedTiles.Count > 0)
                possibleActions.Add("BUY");

            return possibleActions;
        }

        /*
        * Serializes the object as a file that can be retrieved later;
        * */
        public void serializeGame(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(GameState)); //set up xml serializer 

                xSer.Serialize(fs, this); //save game state to file path
            }
        }


    }





}
