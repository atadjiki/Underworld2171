/*
 * Author: Arash Tadjiki
 * Last Updated: 7/3/2015
 * Version: 1.2
 * atadjiki@yahoo.com 
 * */
using Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CityOfRemnantsGUI.Assistant_Classes
{
    public static class LoadSave
    {
        public static bool loadFlag = false; //use when starting a game from a loading context

        public static T loadGame<T>(string FileSpec)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));

            using (FileStream aFile = new FileStream(FileSpec, FileMode.Open))
            {
                byte[] buffer = new byte[aFile.Length];
                aFile.Read(buffer, 0, (int)aFile.Length);

                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    return (T)formatter.Deserialize(stream);
                }
            }
        }

        public static void saveGame<T>(T ToSerialize, string FileSpec)
        {
            Directory.CreateDirectory(FileSpec.Substring(0, FileSpec.LastIndexOf('\\')));
            FileStream outFile = File.Create(FileSpec);
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            formatter.Serialize(outFile, ToSerialize);
        }

        public static void saveButtonClick(GUI gui)
        {
            DialogResult saveResult = MessageBox.Show("Save Game?", "Save", MessageBoxButtons.OKCancel);
            if (saveResult == DialogResult.OK)
            {
                using (var dialog = new System.Windows.Forms.SaveFileDialog())
                {
                    dialog.DefaultExt = "*.ser";
                    dialog.Filter = "City of Remnants Save Games (*.ser)|*.ser|All files (*.*)|*.*";
                    dialog.FileName = "saveGame.ser";
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string fileName = dialog.FileName;
                        LoadSave.saveGame<GameState>(PlayerResources.gameState, fileName);
                    }
                }
            }
            else
            {
                return;
            }
        }

        internal static void loadButtonClick(GUI gui)
        {
            DialogResult loadResult = MessageBox.Show("Load Game?", "Load", MessageBoxButtons.OKCancel);
            if (loadResult == DialogResult.OK)
            {
                using (var dialog = new System.Windows.Forms.OpenFileDialog())
                {
                    dialog.DefaultExt = "*.ser";
                    dialog.Filter = "City of Remnants Save Games (*.ser)|*.ser|All files (*.*)|*.*";
                    dialog.FileName = "saveGame.ser";
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string fileName = dialog.FileName;
                        string filePath = Path.GetFullPath(fileName);

                        PlayerResources.gameState = LoadSave.loadGame<GameState>(filePath);
                        loadFlag = true;
                        gui.startGameButton_Click(gui, new EventArgs());
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
