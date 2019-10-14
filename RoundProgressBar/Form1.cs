using System;
using System.Windows.Forms;

namespace RoundProgressBar
{
    public partial class Form1 : Form
    {
        private CircularProgressBar cbar = new CircularProgressBar();
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Ini()
        {
            pictureBox1.Controls.Add(cbar);
            cbar.RunProgress();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ini();
        }
    }
}
