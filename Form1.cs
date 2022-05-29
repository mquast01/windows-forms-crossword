using Newtonsoft.Json;

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
        //First definition for the data class, contains everything needed
        public class Data
        {
            public Data(int _cols, int _rows, string _author, string _publisher, string _title)
            {
                size = new size();
                size.cols = _cols;
                //MessageBox.Show(size.cols.ToString());
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
            pictureBox1.Load("https://static.thenounproject.com/png/2094362-200.png");
            //LoadJson(@"C:\Users\mquas\source\repos\437project\28.json");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //open a crossword file locally
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
            if (filePath != null && filePath != "")
            {
                LoadJson(filePath);
                if (data != null)
                {
                    Form2 form = new Form2(data);
                    this.RemoveOwnedForm(form);
                    
                    form.ShowDialog();
                    this.Close();

                }
            }


        }

        //API call to github for crossword
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

        //in conjunction with GetDataAsync
        public async void LoadJson(string path)
        {
            //string text = File.ReadAllText(path);
            //text = ROT13(text);
            //MessageBox.Show(text);
            //Data deserializedProduct = JsonConvert.DeserializeObject<Data>(text);
            //return deserializedProduct;

            
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
        {   
            Form5 form = new Form5();
            form.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        static string ROT13(string input)
        {
            return !string.IsNullOrEmpty(input) ? new string(input.ToCharArray().Select(s => { return (char)((s >= 97 && s <= 122) ? ((s + 13 > 122) ? s - 13 : s + 13) : (s >= 65 && s <= 90 ? (s + 13 > 90 ? s - 13 : s + 13) : s)); }).ToArray()) : input;
        }
    }
}