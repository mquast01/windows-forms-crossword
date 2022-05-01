using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _437project.CrossWord.Data;

namespace _437project
{
    public partial class Form3 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        _437project.CrossWord.Data data;

        
        public Form3(_437project.CrossWord.Data _data)
        {
            InitializeComponent();
            listView2.View = System.Windows.Forms.View.Tile;
            listView1.View = System.Windows.Forms.View.Tile;
            data = _data;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 50;

            dataGridView1.BackgroundColor = Color.Black;
            this.dataGridView1.ColumnCount = data.size.cols;
            for (int r = 0; r < data.size.cols; r++)
            {
                this.dataGridView1.Columns[r].Width = 40;
                ((DataGridViewTextBoxColumn)dataGridView1.Columns[r]).MaxInputLength = 1;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridView1);


                for (int c = 0; c < data.size.rows; c++)
                {
                    row.Cells[c].Value = "";


                }

                this.dataGridView1.Rows.Add(row);
            }
            
            dataGridView1.DataSource = gridSource;

            label4.Text = data.title;
            label5.Text = data.publisher;
            label2.Text = data.date.ToString();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            /*
            String number = "";
            if (e.Value != null && e.Value.ToString() != "." && data.gridnums[e.RowIndex * data.size.cols + e.ColumnIndex] != 0)
            {   
                number = data.gridnums[e.RowIndex * data.size.cols + e.ColumnIndex].ToString();
                Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                e.Graphics.FillRectangle(Brushes.White, r);
                Font f = new Font(e.CellStyle.Font.FontFamily, 6);
                e.Graphics.DrawString(number, f, Brushes.Black, r);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;

            }
            */
        }
        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellEventArgs e)
        {
            var dc = dataGridView1[e.ColumnIndex, e.RowIndex];
            if (dc.Value != null)
            {
                if(dc.Value.ToString() == ".")
                {
                    dc.Style.BackColor = Color.Black;
                    dc.Style.ForeColor = Color.Black;
                    dc.Style.SelectionBackColor = Color.White;
                    dc.Style.SelectionForeColor = Color.Black;
                } else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = dc.Value.ToString().ToUpper();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void enableCell(DataGridViewCell dc, bool enabled)
        {
            //toggle read-only state
            dc.ReadOnly = !enabled;
            if (enabled)
            {
                //restore cell style to the default value
                dc.Style.BackColor = dc.OwningColumn.DefaultCellStyle.BackColor;
                dc.Style.ForeColor = dc.OwningColumn.DefaultCellStyle.ForeColor;
            }
            else
            {
                //gray out the cell
                dc.Style.BackColor = Color.Black;
                dc.Style.ForeColor = Color.Black;
                dc.Style.SelectionBackColor = Color.Black;
                dc.Style.SelectionForeColor = Color.Black;
            }

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            /*
            whenever there isnt a period:
            start a new string

                use current index of words for the answers

                while not a period or end build string and compare

                if string = answer mark all values as green font color(go backwards length of string?)
            */
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            int k = 0;
            for (int i = 0; i < 1; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                for(int j = 0; j < row.Cells.Count; j++)
                {
                    string curr = "";
                    while(j < row.Cells.Count && row.Cells[j].Value.ToString() != ".")
                    {
                        curr += row.Cells[j].Value.ToString();
                        j++;
                    }
                    if(curr == data.answers.across[k].ToString())
                    {
                        int m = j - curr.Length;
                        while(m < j)
                        {
                            row.Cells[m].Style = new DataGridViewCellStyle { ForeColor = Color.DarkGreen };
                            m++;
                        }
                    }
                    k++;
                    //return;
                    /*
                    DataGridViewCell cell = row.Cells[j];
                    if (cell.Value.ToString() == data.grid[i * data.size.rows + j].ToString() && cell.Value.ToString() != ".")
                    {
                        //cell.Style 
                        //row.Cells[c].Value = data.grid[r * data.size.cols + c];
                        cell.Style = new DataGridViewCellStyle { ForeColor = Color.DarkGreen };
                        //cell.
                    }
                    */
                }
            }
            
            
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var listViewItem = new ListViewItem("sdfsdf");
            listView1.Items.Add(listViewItem);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete the selected hints?" +
                    "", "Delete Selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = listView1.SelectedItems[i];
                        listView1.Items[itm.Index].Remove();
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete the selected hints?" +
                    "", "Delete Selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = listView1.SelectedItems[i];
                        listView1.Items[itm.Index].Remove();
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Are you sure you want to delete the selected hints?" +
                    "", "Delete Selected", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    for (int i = listView2.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = listView2.SelectedItems[i];
                        listView2.Items[itm.Index].Remove();
                    }
                }
            }
        }
    }

}
