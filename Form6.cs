using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _437project
{

    public partial class Form6 : Form
    {
        public Form6(string curr)
        {
            InitializeComponent();
            textBox1.Text = curr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Please enter a string");
            } else
            {
                this.ReturnValue1 = textBox1.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
