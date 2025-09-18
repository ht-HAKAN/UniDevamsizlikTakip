using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UniDevamsizlikTakip
{
    public partial class Form1 : Form
    {
        Dictionary<string, int>? devamsizliklar;
        Dictionary<string, int>? maxDevamsizlik;

        // Kay�t dosyas�
        private readonly string dosyaYolu = "devamsizlik.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Maksimum devams�zl�k haklar� 
            maxDevamsizlik = new Dictionary<string, int>()
            {
                {"G�RSEL PROGRAMLAMA I", 13},
                {"�LER� NESNE TABANLI PROGRAMLAMA", 13},
                {"�NTERNET PROGRAMCILI�I I", 13},
                {"PYTHON PROGRAMLAMA", 9},
                {"S�STEM ANAL�Z VE TASARIMI", 9}
            };

            VerileriYukle();

            // A��lan men�y� doldur
            if (devamsizliklar != null)
            {
                foreach (var ders in devamsizliklar.Keys)
                {
                    comboBox1.Items.Add(ders);
                }
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

            devamsizliklar[secilenDers] += eklenecekSaat;

            GuncelleListBox();
            Kaydet();
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

        void Kaydet()
        {
            var satirlar = new List<string>();
            if (devamsizliklar != null)
            {
                foreach (var ders in devamsizliklar)
                {
                    satirlar.Add($"{ders.Key}:{ders.Value}");
                }
            }
            File.WriteAllLines(dosyaYolu, satirlar);
        }

        void VerileriYukle()
        {
            // E�er kay�t dosyas� varsa, i�indeki verileri oku
            if (File.Exists(dosyaYolu))
            {
                devamsizliklar = new Dictionary<string, int>();
                var satirlar = File.ReadAllLines(dosyaYolu);
                foreach (var satir in satirlar)
                {
                    var parcalar = satir.Split(':');
                    if (parcalar.Length == 2)
                    {
                        string dersAdi = parcalar[0];
                        int.TryParse(parcalar[1], out int devamsizlikSayisi);
                        devamsizliklar[dersAdi] = devamsizlikSayisi;
                    }
                }
            }
            else // E�er kay�t dosyas� yoksa dersleri s�f�rdan olu�tur
            {
                devamsizliklar = new Dictionary<string, int>()
                {
                    {"G�RSEL PROGRAMLAMA I", 0},
                    {"�LER� NESNE TABANLI PROGRAMLAMA", 0},
                    {"�NTERNET PROGRAMCILI�I I", 0},
                    {"PYTHON PROGRAMLAMA", 0},
                    {"S�STEM ANAL�Z VE TASARIMI", 0}
                };
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Listeden se�ili olan sat�r� al
            string? seciliSatir = listBox1.SelectedItem?.ToString();

            // E�er listeden hi�bir �ey se�ilmemi�se uyar� mesaj� g�ster 
            if (string.IsNullOrEmpty(seciliSatir))
            {
                MessageBox.Show("Listeden bir ders se�.", "Uyar�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Se�ili sat�rdan dersin ad�n� ay�kla
            string dersAdi = seciliSatir.Substring(0, seciliSatir.IndexOf('[')).Trim();

            // Se�ili dersin devams�zl���n� s�f�rla
            if (devamsizliklar != null && devamsizliklar.ContainsKey(dersAdi))
            {
                devamsizliklar[dersAdi] = 0;
            }

            // Ekran� ve dosyay� en g�ncel haliyle yenile
            GuncelleListBox();
            Kaydet();
        }
    }
}