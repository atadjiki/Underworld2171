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
    public static class PlayerResources
    {
        internal static System.Drawing.Color player1Color = System.Drawing.Color.FromArgb(90, 233, 38, 38);
        internal static System.Drawing.Color player2Color = System.Drawing.Color.FromArgb(90, 85, 206, 71);
        internal static System.Drawing.Color player3Color = System.Drawing.Color.FromArgb(90, 75, 87, 166);
        internal static System.Drawing.Color player4Color = System.Drawing.Color.FromArgb(90, 232, 143, 27);

        internal static Random rndAction = new Random();
        internal static Random rndTile = new Random();
        internal static Random rndProperty = new Random();

        internal static GameState gameState; //will be later contained in the GUIstate
    }
}
