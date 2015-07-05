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

namespace Internals
{
    [Serializable ]
    public class Player
    {

        public List<Square> playerOwnedTiles;

        public string name;

        public int goldCount;

        public Player()
        {
            name = "player";
            goldCount = 0;
            playerOwnedTiles = new List<Square>();
        }

        public Player(string _name)
        {
            name = _name;
            goldCount = 0;
            playerOwnedTiles = new List<Square>();
        }

        public void addSquare(Square input)
        {
            playerOwnedTiles.Add(input);
        }
        public void removeSquare(Square input)
        {
            playerOwnedTiles.Remove(input);
        }

        public List<Square> getPlayerTiles()
        {
            return playerOwnedTiles;
        }

        public string getName()
        {
            return name;
        }
        
        public int calculateIncome()
        {
            //get the yields of all squares
            int totalYieldPerTurn = 0;

            foreach (Square s in playerOwnedTiles)
            {
                totalYieldPerTurn += s.yield;
            }

            return totalYieldPerTurn;
        }
    
    }
}
