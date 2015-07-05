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
using Internals;

namespace InternalTests
{
    /*
     * A short test application to prove that from any given 
     * square board, the number of neighbors for any square can be determined by its address
     * */

    class NeighborDetection
    {
        static void Main(string[] args)
        {

            int width = 8; 

            GameBoard boardIndex = new GameBoard(width); //initialize a game board of a given width
            boardIndex.populateBoard(); //populate the board with square objects

            //query the board for square inforation 
            Console.WriteLine();
            Console.WriteLine("Board of width: " + width);
            Console.WriteLine(boardIndex.printNeighborInformation());

            Console.WriteLine();
            Console.WriteLine("Board Information");
            Console.WriteLine();
            Console.WriteLine(boardIndex.printBoard()); //print the board to the consle 

            Console.Read();
         


        }
    }
}
