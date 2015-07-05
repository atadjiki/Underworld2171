/*
 * Author: Arash Tadjiki
 * Last Updated: 7/3/2015
 * Version: 1.2
 * atadjiki@yahoo.com 
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Internals;
using System.IO;
using System.Windows.Media;
using System.Xml.Serialization;
using CityOfRemnantsGUI.Assistant_Classes;


namespace CityOfRemnantsGUI
{
    public partial class GUI : Form
    {
        internal List<Button> buttonList; //keeps track of buttons in the interface. should be added to GUIstate

        public GUI()
        {
            InitializeComponent();
            SoundEffectPlayer.soundSetup();
            PanelControls.menuSetup(this); //initialize menu
        }

        internal void startGameButton_Click(object sender, EventArgs e)
        {
            if (PanelControls.setupFlag == true)
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                InitialSetups.preliminarySetup(this);
            }
                
            PanelControls.toggleGamePanel(this);
        }
        internal void endTurnButton_Click(object sender, EventArgs e)
        {
            PanelControls.endTurnButtonClick(this, sender, e);
        }
        internal void OnGenericButtonClick(object sender, EventArgs e)
        {

            GamePlayerFunctions.genericButtonClickEvent(this, sender, e);

        }
        internal void onGenericMouseHover(object sender, EventArgs e)
        {
            ButtonControls.genericMouseHover(sender, e, this);
        }
        internal void loadButton_Click(object sender, EventArgs e)
        {
            LoadSave.loadButtonClick(this);
        }
        internal void saveButton_Click(object sender, EventArgs e)
        {

            LoadSave.saveButtonClick(this);
        }
        internal void KeyPressCases(object sender, KeyPressEventArgs e)
        {

            PanelControls.genericKeyPress(sender, e, this);
        }

    }

}
