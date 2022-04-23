using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace steganography
{
    public partial class Form1 : Form
    {
        public Button but = new Button();
        public Form1()
        {
            InitializeComponent();
            but.Text = "My Button";
            but.Size = new Size(200, 20);
            but.Location = new Point(10, 10);
            this.Controls.Add(but);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
