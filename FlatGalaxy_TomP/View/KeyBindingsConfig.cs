using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatGalaxy_TomP.View
{
    public partial class KeyBindingsConfig : Form
    {
        public Dictionary<string, Keys> KeyBinds { get; set; }
        private string toBeChangedLabel;

        public KeyBindingsConfig(Dictionary<string, Keys> keyBinds)
        {
            InitializeComponent();
            this.KeyBinds = keyBinds;
            this.KeyPreview = true;
            FasterKey.Text      = keyBinds["faster"].ToString();
            SlowerKey.Text      = keyBinds["slower"].ToString();
            PauseKey.Text       = keyBinds["pause"].ToString();
            Back5Label.Text     = keyBinds["back"].ToString();
            this.KeyDown += new KeyEventHandler(Config_KeyDown);
        }

        private void FasterKey_Click(object sender, EventArgs e)
        {
            FasterKey.Text = "Press any key to change the " + KeyBinds["faster"].ToString() + " keybinding...";
            toBeChangedLabel = "faster";
        }

        private void SlowerKey_Click(object sender, EventArgs e)
        {
            SlowerKey.Text = "Press any key to change the " + KeyBinds["slower"].ToString() + " keybinding...";
            toBeChangedLabel = "slower";
        }

        private void PauseKey_Click(object sender, EventArgs e)
        {
            PauseKey.Text = "Press any key to change the " + KeyBinds["pause"].ToString() + " keybinding...";
            toBeChangedLabel = "pause";
        }

        private void GoBack5Key_Click(object sender, EventArgs e)
        {
            GoBack5Key.Text = "Press any key to change the " + KeyBinds["back"].ToString() + " keybinding...";
            toBeChangedLabel = "back";
        }

        private void Config_KeyDown(object sender, KeyEventArgs e)
        {
            switch (toBeChangedLabel)
            {
                case "faster":
                    KeyBinds["faster"] = e.KeyData;
                    toBeChangedLabel = "";
                    FasterKey.Text = KeyBinds["faster"].ToString();
                    break;
                case "slower":
                    KeyBinds["slower"] = e.KeyData;
                    toBeChangedLabel = "";
                    SlowerKey.Text = KeyBinds["slower"].ToString();
                    break;
                case "pause":
                    KeyBinds["pause"] = e.KeyData;
                    toBeChangedLabel = "";
                    PauseKey.Text = KeyBinds["pause"].ToString();
                    break;
                case "back":
                    KeyBinds["back"] = e.KeyData;
                    toBeChangedLabel = "";
                    GoBack5Key.Text = KeyBinds["back"].ToString();
                    break;
                default:
                    toBeChangedLabel = "";
                    break;
            }
        }
    }
}
