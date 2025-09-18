namespace UniDevamsizlikTakip
{
    public partial class Form1 : Form
    {
        Dictionary<string, int>? devamsizliklar;
        Dictionary<string, int>? maxDevamsizlik;

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

            // A��lan men�y� doldur
            foreach (var ders in devamsizliklar.Keys)
            {
                comboBox1.Items.Add(ders);
            }

            // �lk dersi se�me
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            GuncelleListBox();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string? secilenDers = comboBox1.SelectedItem?.ToString();
            int eklenecekSaat = (int)numericUpDown1.Value;

            if (string.IsNullOrEmpty(secilenDers) || devamsizliklar == null)
            {
                return;
            }

            // Se�ilen dersin devams�zl�k saatini g�ncelle
            devamsizliklar[secilenDers] += eklenecekSaat;

            // Ekrani an�nda yenile
            GuncelleListBox();
        }
        void GuncelleListBox()
        {
            listBox1.Items.Clear();
            if (devamsizliklar != null)
            {
                foreach (var ders in devamsizliklar)
                {
                    string dersAdi = ders.Key;
                    int mevcutDevamsizlik = ders.Value;
                    if (maxDevamsizlik != null)
                    {
                        int maxDevamsizlikHakki = maxDevamsizlik[dersAdi];
                        listBox1.Items.Add($"{dersAdi} [{mevcutDevamsizlik} / {maxDevamsizlikHakki}] Saat");
                    }
                }
            }
        }
    }
}