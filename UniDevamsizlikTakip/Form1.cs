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

            // Açýlan menüyü doldur
            foreach (var ders in devamsizliklar.Keys)
            {
                comboBox1.Items.Add(ders);
            }

            // Ýlk dersi seçme
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

            // Seçilen dersin devamsýzlýk saatini güncelle
            devamsizliklar[secilenDers] += eklenecekSaat;

            // Ekrani anýnda yenile
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