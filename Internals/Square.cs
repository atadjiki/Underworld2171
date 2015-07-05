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
    /*
     * A class to represent the tiles of the game board. 
     * Currently, only the index and number of neighbors are recorded. 
     * Future changes will include the "state" of the tile (claimed, unclaimed), and 
     * if claimed, which player has claimed them. 
     * Tiles also produce a yield or income to players who claimed them, determined by the number of neighbors. 
     **/
    [Serializable]
    public class Square
    {

        public int neighborCount; //how many neighbors this particular square has (up to four)
        public int indexNumber; //index of square ranging from 1 to (width^2)
        public int current_occupier; //0 if nobody (1-4) for players
        public int tier; //1 to 3
        public int yield;

        public bool north_neighbor;
        public bool south_neighbor;
        public bool west_neighbor;
        public bool east_neighbor;


        public Square()
        {
            neighborCount = 0;
            indexNumber = 0;
            current_occupier = 0;
            tier = 0;
            yield = 0;
            north_neighbor = false;
            south_neighbor = false;
            west_neighbor = false;
            east_neighbor = false;

        }
        public Square(int index, int neighbors, int occupied, bool north, bool south, bool east, bool west, int _tier)
        {
            indexNumber = index;

            if (neighbors < 2 || neighbors > 4)
                throw new ArgumentOutOfRangeException();
            neighborCount = neighbors;

            if (occupied < 0 || occupied > 4)
                throw new ArgumentOutOfRangeException();

            if (_tier < 1 || _tier > 3)
                throw new ArgumentOutOfRangeException();
            tier = _tier;


            current_occupier = occupied;
            yield = 0;

            north_neighbor = north;
            south_neighbor = south;
            west_neighbor = west;
            east_neighbor = east;

        }

        public int getNeighborCount()
        {
            return neighborCount;
        }

        public int getIndex()
        {
            return indexNumber;
        }
        
        public int getOccupier()
        {
            return current_occupier;
        }
        public void setOccupier(int newOccupier)
        {
            current_occupier = newOccupier;
        }

        public string getTier()
        {
            if (tier == 1)
                return "Poor";
            else if (tier == 2)
                return "Medium";
            else
            {
                return "Rich";
            }
        }

        public Dictionary<string, bool> getNeighborInformation()
        {
            Dictionary<string, bool> neighborInfo = new Dictionary<string, bool>();

            neighborInfo.Add("north", north_neighbor);
            neighborInfo.Add("south", south_neighbor);
            neighborInfo.Add("east", east_neighbor);
            neighborInfo.Add("west", west_neighbor);

            return neighborInfo;
        }

    }
}
