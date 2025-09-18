namespace UniDevamsizlikTakip
{
    public partial class Form1 : Form

    {
        Dictionary<string, int> devamsizliklar;
        Dictionary<string, int> maxDevamsizlik;
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Dersleri tanýmla
            devamsizliklar = new Dictionary<string, int>()
            {
                {"GÖRSEL PROGRAMLAMA I", 0},
                {"ÝLERÝ NESNE TABANLI PROGRAMLAMA", 0},
                {"ÝNTERNET PROGRAMCILIÐI I", 0},
                {"PYTHON PROGRAMLAMA", 0},
                {"SÝSTEM ANALÝZ VE TASARIMI", 0}
            };

            // Devamsýzlýk Limitlerini tanýmla
            maxDevamsizlik = new Dictionary<string, int>()
            {
                {"GÖRSEL PROGRAMLAMA I", 13},
                {"ÝLERÝ NESNE TABANLI PROGRAMLAMA", 13},
                {"ÝNTERNET PROGRAMCILIÐI I", 13},
                {"PYTHON PROGRAMLAMA", 9},
                {"SÝSTEM ANALÝZ VE TASARIMI", 9}
            };



        }
    }
}
