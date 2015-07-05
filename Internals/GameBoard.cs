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
    [Serializable]
    public class GameBoard
    {

        public int width;
        public List<Square> boardIndex;

        public GameBoard(int _width)
        {
            width = _width;
            boardIndex = new List<Square>();
        }

        public GameBoard()
        {
            width = 8;
            boardIndex = new List<Square>();
        }

         public void populateBoard()
        {
            //for every index, create a square object and add master list
            for (int index = 1; index <= (width * width); index++)
            {

                int neighborsCount; // determine the amount of neighbors for this square
                Square newSquare = null;
                int realIndex = index ;

                //ideally, a square would have four neighbors (N,S,E,W), but edge cases must be accounted for:

                //case 1: index is on the first row (1-width)
                if (index <= width)
                {
                    //case 2: index is on the first row AND the first column (1)         
                    if (index == 1)
                    {
                        neighborsCount = 2;
                        newSquare = new Square(realIndex, neighborsCount, 0, false, true, true, false, 1);
                    }
                    //case 3: index is on the first row AND the last column (width)
                    else if(index == width)
                    {
                        neighborsCount = 2;
                        newSquare = new Square(realIndex, neighborsCount, 0, false, true, false, true, 1);
                    }

                    //else, square has 3 neighbors
                    else
                    {
                        neighborsCount = 3;
                        newSquare = new Square(realIndex, neighborsCount, 0, false, true, true, true, 1);
                    }
                }
                //case 4: square is on the last row:
                else if (index > (width * width - width) && index <= (width * width))
                {
                    //case 5: index is on the last row AND the first column (1)                
                    if (index == (width * width))
                    {
                        neighborsCount = 2;
                        newSquare = new Square(realIndex, neighborsCount, 0, true, false, false, true, 1);
                    }
                    //case 6: index is on the last row AND the last column (width)
                    else if(index == (width * width - (width - 1)))
                    {
                        neighborsCount = 2;
                        newSquare = new Square(realIndex, neighborsCount, 0, true, false, true, false, 1);
                    }

                    //else, square has 3 neighbors
                    else
                    {
                        neighborsCount = 3;
                        newSquare = new Square(realIndex, neighborsCount, 0, true, false, true, true, 1);
                    }
                }
                //case 7: square is on the first column (index -1 is an integer factor of width)
                else if ((index - 1) % width == 0)
                {
                    neighborsCount = 3;
                    newSquare = new Square(realIndex, neighborsCount, 0, true, true, true, false, 1);
                }
                //case 8: square is on the last column (index is an integer factor of width)
                else if ((index) % width == 0)
                {
                    neighborsCount = 3;
                    newSquare = new Square(realIndex, neighborsCount, 0, true, true, false, true, 1);
                }
                else
                {
                    neighborsCount = 4;
                    newSquare = new Square(realIndex, neighborsCount, 0, true, true, true, true, 2);
                }
                //finally, add the new square to the map

                boardIndex.Add(newSquare);

            }
        }

        public string printBoard()
        {
            //now print the board as a grid, printing the number of neighbors in each square
            int rowCount = 1;
            int indexCount = 0;
            string result = "";

            while (rowCount <= width)
            {
                string row = " | ";
                for (int i = 0; i < width; i++)
                {
                    row += boardIndex.ElementAt(indexCount).getNeighborCount() + " | ";
                    indexCount++;
                }

                result += row + "\n" + "\n";
                
                rowCount++;
            }

            return result;
        }

      
        public string printNeighborInformation()
        {
            string result = "";

            foreach(Square s in boardIndex)
            {

                Dictionary<string, bool> neighbor_info = s.getNeighborInformation();
                int index = s.getIndex();
                int neighborCount = s.getNeighborCount();
                Dictionary<string, int> neighborIndexes = calculateIndexOfNeighbors(s);

                string neighbor_string = "Neighbors: ";
                if(s.getNeighborInformation()["north"] == true)
                {
                    neighbor_string += "north: ";
                    neighbor_string += neighborIndexes["n"] + " ";
                }
                if (s.getNeighborInformation()["south"] == true)
                {
                    neighbor_string += "south: ";
                    neighbor_string += neighborIndexes["s"] + " ";
                }
                if (s.getNeighborInformation()["east"] == true)
                {
                    neighbor_string += "east: ";
                    neighbor_string += neighborIndexes["e"] + " ";
                }
                if (s.getNeighborInformation()["west"] == true)
                {
                    neighbor_string += "west: ";
                    neighbor_string += neighborIndexes["w"] + " "; 
                }

                result += "Square: " + index + " , " + neighbor_string +"\n";
            }

            return result;
        }

        //nsew
        public Dictionary<string, int> calculateIndexOfNeighbors(Square square)
        {
            int index = square.getIndex();
            Dictionary<string, int> neighborIndexes = new Dictionary<string,int>();
            Dictionary<string, bool> neighboorBools = square.getNeighborInformation();

            if(neighboorBools["north"] == true)
            {
                neighborIndexes.Add("n", index - width);
            }
            if (neighboorBools["south"] == true)
            {
                neighborIndexes.Add("s", index + width);
            }
            if (neighboorBools["east"] == true)
            {
                neighborIndexes.Add("e", index + 1);
            }
            if (neighboorBools["west"] == true)
            {
                neighborIndexes.Add("w", index - 1);
            }

            return neighborIndexes;
        }

        public int calculateIndexOfNeighbor(Square square, String orientation)
        {
            int index = square.getIndex();
           
            Dictionary<string, bool> neighboorBools = square.getNeighborInformation();

            if (orientation == "north")
            {
                return (index - width);
            }
            else if (orientation == "south")
            {
                return (index + width);
            }
            else if (orientation == "east")
            {
                return (index + 1);
            }
            else if (orientation == "west")
            {
                return (index - 1);
            }

            return index;
        }

        public List<Square> getBoard()
        {
            return boardIndex;
        }

    }


    }


