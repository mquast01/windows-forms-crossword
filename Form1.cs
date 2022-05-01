using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace _437project
{
    public partial class CrossWord : Form
    {
        public class answers
        {
            public List<string>? across { get; set; }
            public List<string>? down { get; set; }
        }
        public class clues
        {
            public List<string>? across { get; set; }
            public List<string>? down { get; set; }
        }
        public class size
        {
            public int cols { get; set; }
            public int rows { get; set; }
        }
        public class Data
        {
            public Data(int _cols, int _rows, string _author, string _publisher, string _title)
            {
                size = new size();
                size.cols = _cols;
                size.rows = _rows;
                author = _author;
                publisher = _publisher;
                title = _title;
            }
            public string? acrossmap { get; set; }
            public bool admin { get; set; }
            public answers? answers { get; set; }
            public string? author { get; set; }
            public bool? autowrap { get; set; }
            public bool? bbars { get; set; }
            public bool? circles { get; set; }

            public clues? clues;
            public bool? code { get; set; }
            public string? copyright { get; set; }
            public DateTime date { get; set; }
            public string? dow { get; set; }
            public bool? downmap { get; set; }
            public string? editor { get; set; }
            public List<string>? grid { get; set; }
            public List<string>? grid_save { get; set; }
            public IList<int>? gridnums { get; set; }
            public string? publisher { get; set; }
            public size size { get; set; }
            
            public string? title { get; set; }
        }
        private string? json { get; set; }

        Data data;

        static HttpClient client = new HttpClient();

        public CrossWord()
        {
            

            InitializeComponent();
            //LoadJson(@"C:\Users\mquas\source\repos\437project\28.json");

        }

        


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            //MessageBox.Show(filePath);
            if(filePath != null && filePath != "")
            {
                LoadJson(filePath);
                if (data != null)
                {
                    Form2 form = new Form2(data);
                    form.ShowDialog();
                }
            } 
            
            
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
                    MessageBox.Show(str);
                }
                return str;
            }  
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return str;
        }
      
        public async void LoadJson(string path)
        {
            //string str = await GetDataAsync("https://raw.githubusercontent.com/doshea/nyt_crosswords/master/1983/06/06.json");
            //MessageBox.Show(str);

            // read file into a string and deserialize JSON to a type
            //Data? data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(@"28.json"));

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            {   
                JsonSerializer serializer = new JsonSerializer();
                data = (Data)serializer.Deserialize(file, typeof(Data));
                //MessageBox.Show(data.clues.across[0]);
                //return data;
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {   /*
            string str = await GetDataAsync("https://raw.githubusercontent.com/doshea/nyt_crosswords/master/1983/06/06.json");
            MessageBox.Show(str);
            */
            Form5 form = new Form5();
            form.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}