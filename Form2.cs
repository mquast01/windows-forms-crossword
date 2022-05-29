using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace _437project
{
    public partial class Form2 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        CrossWord.Data data;

        
        public Form2(_437project.CrossWord.Data _data)
        {
            InitializeComponent();
            data = _data;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //initalize the board
        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 50;

            dataGridView1.BackgroundColor = Color.Black;
            this.dataGridView1.ColumnCount = data.size.cols;
            for (int r = 0; r < data.size.cols; r++)
            {
                this.dataGridView1.Columns[r].Width = 50;
                //this.dataGridView1.Columns[r].MinimumWidth = 50;
                ((DataGridViewTextBoxColumn)dataGridView1.Columns[r]).MaxInputLength = 1;
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridView1);


                for (int c = 0; c < data.size.rows; c++)
                {
                    if(data.grid[r * data.size.cols + c] == ".")
                    {
                        enableCell(row.Cells[c], false);
                        row.Cells[c].Value = data.grid[r * data.size.cols + c];
                    } else
                    {
                        if(data.grid_save != null)
                        {
                            row.Cells[c].Value = data.grid_save[r * data.size.cols + c];
                        } else
                        {
                            row.Cells[c].Value = "";
                        }
                        //row.Cells[c].Value = data.grid[r * data.size.cols + c];
                        
                    }
                    

                }

                this.dataGridView1.Rows.Add(row);
            }
            
            dataGridView1.DataSource = gridSource;
            
            //initialize clues in list views
            if (data.clues != null && data.clues.across != null)
            {

            }
            for (int i = 0; i < data.clues.across.Count(); i++)
            {
                listView1.Items.Add(data.clues.across[i], i);
            }

            listView1.View = System.Windows.Forms.View.Tile;
            listView1.Show();

            for (int i = 0; i < data.clues.down.Count(); i++)
            {
                listView2.Items.Add(data.clues.down[i], i);

            }
            listView2.View = System.Windows.Forms.View.Tile;
            listView2.Show();

            label2.Text = data.title + "\n"  + data.publisher + "\n" + data.date;


        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        //paint the superscript numbers in the top corner
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            String number = "";
            //if the value is not black square tile and a number exists paint the superscript
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
        }

        //auto format to uppercase
        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
        }

        //auto format to uppercase
        private void dataGridView1_CellValidating(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
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
            //check if any number matches the answer key at data.grid
            int k = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                for(int j = 0; j < row.Cells.Count; j++)
                {

                    var curr = data.grid[i * data.size.rows + j].ToString();
                    //MessageBox.Show(curr);
                    if(curr != ".")
                    {
                        if (curr == row.Cells[j].Value.ToString())
                        {
                            row.Cells[j].Style = new DataGridViewCellStyle { ForeColor = Color.DarkGreen };
                        } else
                        {
                            row.Cells[j].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                        }
                    }

                }
            }
            
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            data.grid_save = new List<string>();
            string curr = "";
            int k = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    //data.grid_save = new List<string> { curr };
                    data.grid_save.Add(row.Cells[j].Value.ToString());
                    //MessageBox.Show(data.grid_save.ToString());

                }
            }

            //https://stackoverflow.com/questions/18739091/is-it-possible-to-write-a-rot13-in-one-line
            string output = JsonConvert.SerializeObject(data);
            //output = ROT13(output);
            //MessageBox.Show(ROT13(output));
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            SaveFileDialog dlg = new SaveFileDialog();

            saveFileDialog1.Filter = "Json Files (*.json|*.json";
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

        static string ROT13(string input)
        {
            return !string.IsNullOrEmpty(input) ? new string(input.ToCharArray().Select(s => { return (char)((s >= 97 && s <= 122) ? ((s + 13 > 122) ? s - 13 : s + 13) : (s >= 65 && s <= 90 ? (s + 13 > 90 ? s - 13 : s + 13) : s)); }).ToArray()) : input;
        }
    }

}
