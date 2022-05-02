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
    public partial class Form5 : Form
    {
        static HttpClient client = new HttpClient();

        public Form5()
        { 
            InitializeComponent();
            SetMyCustomFormat();
        }

        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy dd MM";
        }
        static async Task<string> GetDataAsync(string path)
        {
            string str = null;
            try
            {

                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    str = await response.Content.ReadAsStringAsync();
                    //MessageBox.Show(str);
                }
                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return str;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        //get the specified json
        private async void button1_Click(object sender, EventArgs e)
        {
            string theDate = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            string url = "https://raw.githubusercontent.com/doshea/nyt_crosswords/master/" + theDate + ".json";
            //MessageBox.Show(url);
            string str = await GetDataAsync(url);
            if(str != null)
            {
                 JsonSerializer serializer = new JsonSerializer();
                var result = JsonConvert.DeserializeObject<CrossWord.Data>(str);
                if (result == null)
                {
                    MessageBox.Show("error?");
                }
                Form2 form = new Form2(result);
                form.ShowDialog();
            } else
            {
                MessageBox.Show("Sorry, there was an issue getting this crossword. Please try another");
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
