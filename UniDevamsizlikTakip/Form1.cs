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
            // Dersleri tan�mla
            devamsizliklar = new Dictionary<string, int>()
            {
                {"G�RSEL PROGRAMLAMA I", 0},
                {"�LER� NESNE TABANLI PROGRAMLAMA", 0},
                {"�NTERNET PROGRAMCILI�I I", 0},
                {"PYTHON PROGRAMLAMA", 0},
                {"S�STEM ANAL�Z VE TASARIMI", 0}
            };

            // Devams�zl�k Limitlerini tan�mla
            maxDevamsizlik = new Dictionary<string, int>()
            {
                {"G�RSEL PROGRAMLAMA I", 13},
                {"�LER� NESNE TABANLI PROGRAMLAMA", 13},
                {"�NTERNET PROGRAMCILI�I I", 13},
                {"PYTHON PROGRAMLAMA", 9},
                {"S�STEM ANAL�Z VE TASARIMI", 9}
            };



        }
    }
}
