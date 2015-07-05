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
    public static class InformationDisplays
    {
        public static void noMoreActionsText(RichTextBox infoBox)
        {
            infoBox.ForeColor = System.Drawing.Color.Red;
            infoBox.Text = "No more actions can be taken this turn.";

            SoundEffectPlayer.playSoundEffect(Properties.Resources.processing3);
        }

        public static void insufficientFundsText(RichTextBox infoBox)
        {
            infoBox.ForeColor = System.Drawing.Color.Red;
            infoBox.Text = "Insufficient funds for this action!";

            SoundEffectPlayer.playSoundEffect(Properties.Resources.processing3);
        }

        public static void transactionCompleteText(RichTextBox infoBox)
        {
            infoBox.ForeColor = System.Drawing.Color.Green;
            infoBox.Text = "Transaction Complete.";

            SoundEffectPlayer.playSoundEffect(Properties.Resources.transaction_completed);
        }




    }
}
