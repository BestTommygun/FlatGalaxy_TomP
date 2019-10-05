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
    public partial class WebFileLoader : Form
    {
        public string webAdress { get; set; }
        public WebFileLoader()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webAdress = textBox1.Text;
            Close();
        }
    }
}
