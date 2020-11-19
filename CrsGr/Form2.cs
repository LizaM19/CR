using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CrsGr
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void setTextToRich(String text)
        {
            richTextBox1.Text = text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText("file.txt", string.Empty);
        }
    }
}
