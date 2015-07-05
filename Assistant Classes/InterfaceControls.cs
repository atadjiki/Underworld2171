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
    public static class InterfaceControls
    {
        public static void DisableControls(GUI gui)
        {
            List<Control> controlList = gui.Controls.OfType<TextBox>().Cast<Control>().ToList();
            controlList.Remove(gui.infoBox);
            controlList.Remove(gui.creditsBox);
            controlList.Remove(gui.statusStrip);

            foreach (Control c in controlList)
            {
                c.Enabled = false;
            }

        }

        public static void EnableControls(GUI gui)
        {
            foreach (Control c in gui.Controls)
            {
                c.Enabled = true;
            }
        }
    }
}
