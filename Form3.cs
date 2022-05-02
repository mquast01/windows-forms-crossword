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
using Newtonsoft.Json;

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

                MessageBox.Show(data.size.rows.ToString());
                for (int c = 0; c < data.size.rows; c++)
                {
                    MessageBox.Show(c.ToString());
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
            if(this.ViewMode == CurrView.LetterView)
            {
                String number = "";
                //if (e.Value != null && e.Value.ToString() != "." && data.gridnums[e.ColumnIndex * data.size.cols + e.RowIndex] != 0)
                //MessageBox.Show(e.ColumnIndex.ToString());
                //MessageBox.Show(e.RowIndex.ToString());
                if (e.Value != null && e.Value.ToString() != "." && data.gridnums[e.ColumnIndex * data.size.cols + e.RowIndex] != 0)
                {
                    number = data.gridnums[e.RowIndex * data.size.cols + e.ColumnIndex].ToString();
                    Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                    e.Graphics.FillRectangle(Brushes.White, r);
                    Font f = new Font(e.CellStyle.Font.FontFamily, 6);
                    e.Graphics.DrawString(number, f, Brushes.Black, r);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
            } 
            
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
            /*
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
            */
            var dc = dataGridView1[e.ColumnIndex, e.RowIndex];
            dataGridView1[e.ColumnIndex, e.RowIndex].Value = dc.Value.ToString().ToUpper();
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
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
            /*
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
            */

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            
        }

        //saves the current crossword
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if currently in answers save just answers to grid
            if(ViewMode == CurrView.LetterView)
            {
                data.grid = data.grid_save;
                for(int i = 0; i < data.grid_save.Count; i++)
                {
                    data.grid_save[i] = "";
                }
                /*
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        data.grid[i * dataGridView1.Rows.Count + j] = row.Cells[j].Value.ToString();
                        //grid save should be empty
                        data.grid_save[i * dataGridView1.Rows.Count + j] = "";
                    }
                }
                */
            } else
            {
                //save current nums
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        data.gridnums[i * dataGridView1.Rows.Count + j] = Int32.Parse(row.Cells[j].Value.ToString());
                    }
                }
            }

            //add answers to data obj
            data.clues = new CrossWord.clues();
            data.clues.across = new List<string>();
            for (int i = 0; i < listView1.Items.Count; i++)
            {  
                data.clues.across.Add(listView1.Items[i].Text);
            }
            data.clues.down = new List<string>();
            for (int i = 0; i < listView2.Items.Count; i++)
            {   
                data.clues.down.Add(listView2.Items[i].Text);
            }

            string output = JsonConvert.SerializeObject(data);
            output = ROT13(output);
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            SaveFileDialog dlg = new SaveFileDialog();

            saveFileDialog1.Filter = "Json Files (*.json)|*.json";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                writer.WriteLine(output);

                writer.Dispose();

                writer.Close();
            }
        }

        private void dataGridView1_CellValidating(object sender,
        DataGridViewCellValidatingEventArgs e)
        {
            if(this.ViewMode == CurrView.NumView)
            {
                int i;
                if (!int.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    MessageBox.Show("please enter numeric value");
                }
                else
                {
                    // the input is numeric 
                }
            }
            
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6("");
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                string val = form.ReturnValue1;            //values preserved after close
                var listViewItem = new ListViewItem(val);
                listView1.Items.Add(listViewItem);
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                {
                    Form6 form = new Form6(listView1.SelectedItems[i].Text);
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string val = form.ReturnValue1;            //values preserved after close
                        listView1.SelectedItems[i].Text = val;
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(this.ViewMode == CurrView.LetterView)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        data.grid_save[i * dataGridView1.Rows.Count + j] = row.Cells[j].Value.ToString();
                        row.Cells[j].Value = data.gridnums[i * dataGridView1.Rows.Count + j];
                    }
                }
                toolStripButton2.Text = "Swap to grid letters";
                this.ViewMode = CurrView.NumView;
                this.dataGridView1.CurrentCell.Value = 0;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[j]).MaxInputLength = 3;
                }

            } else if (this.ViewMode == CurrView.NumView)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        data.gridnums[i * dataGridView1.Rows.Count + j] = Int32.Parse(row.Cells[j].Value.ToString());
                        row.Cells[j].Value = data.grid_save[i * dataGridView1.Rows.Count + j];
                        /*Style.BackColor = Color.Black;
                    dc.Style.ForeColor = Color.Black;
                    dc.Style.SelectionBackColor = Color.Black;
                    dc.Style.SelectionForeColor = Color.Black;
                        */
                    }
                }
                toolStripButton2.Text = "Swap to grid nums";
                this.ViewMode = CurrView.LetterView;
                this.dataGridView1.CurrentCell.Value = "";
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[j]).MaxInputLength = 1;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6("");
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                string val = form.ReturnValue1;            //values preserved after close
                var listViewItem = new ListViewItem(val);
                listView2.Items.Add(listViewItem);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                for (int i = listView2.SelectedItems.Count - 1; i >= 0; i--)
                {
                    Form6 form = new Form6(listView2.SelectedItems[i].Text);
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string val = form.ReturnValue1;            //values preserved after close
                        listView2.SelectedItems[i].Text = val;
                    }
                }
            }
        }

        static string ROT13(string input)
        {
            return !string.IsNullOrEmpty(input) ? new string(input.ToCharArray().Select(s => { return (char)((s >= 97 && s <= 122) ? ((s + 13 > 122) ? s - 13 : s + 13) : (s >= 65 && s <= 90 ? (s + 13 > 90 ? s - 13 : s + 13) : s)); }).ToArray()) : input;
        }
    }

}
