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
    public partial class Form4 : Form
    {
        _437project.CrossWord.Data data;
        public Form4()
        { 
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateNum();
        }
        private bool ValidateNum()
        {
            bool bStatus = true;
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "Enter a positive number");
                bStatus = false;
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
                try
                {
                    int temp = int.Parse(textBox1.Text);
                    errorProvider1.SetError(textBox1, "");
                    if (temp <= 0)
                    {
                        errorProvider1.SetError(textBox1, "Enter a positive number");
                        bStatus = false;
                    }
                }
                catch
                {
                    errorProvider1.SetError(textBox1, "Enter a positive number");
                    bStatus = false;
                }
            }
            return bStatus;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox1.Text);
            
            var cols = int.Parse(textBox1.Text);
            var rows = int.Parse(textBox5.Text);

            var author = textBox2.Text.ToString();
            var title = textBox3.Text.ToString();
            var publisher = textBox4.Text.ToString();
            var data = new _437project.CrossWord.Data(cols, rows, author, publisher, title);
            data.gridnums = new int[cols * rows];
            data.grid_save = new List<string>(new string[cols * rows]);
            for (int i = 0; i < data.gridnums.Count; i++)
            {
                data.gridnums[i] = 0;
                data.grid_save[i] = "";
            }
            
            var form3 = new Form3(data);
            //MessageBox.Show(data.author.ToString());
            form3.ShowDialog();
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
