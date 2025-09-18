using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UniDevamsizlikTakip
{
    public partial class Form1 : Form
    {
        Dictionary<string, int>? devamsizliklar;
        Dictionary<string, int>? maxDevamsizlik;

        // Kayýt dosyasý
        private readonly string dosyaYolu = "devamsizlik.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Maksimum devamsýzlýk haklarý 
            maxDevamsizlik = new Dictionary<string, int>()
            {
                {"GÖRSEL PROGRAMLAMA I", 13},
                {"ÝLERÝ NESNE TABANLI PROGRAMLAMA", 13},
                {"ÝNTERNET PROGRAMCILIÐI I", 13},
                {"PYTHON PROGRAMLAMA", 9},
                {"SÝSTEM ANALÝZ VE TASARIMI", 9}
            };

            VerileriYukle();

            // Açýlan menüyü doldur
            if (devamsizliklar != null)
            {
                foreach (var ders in devamsizliklar.Keys)
                {
                    comboBox1.Items.Add(ders);
                }
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
            // Eðer kayýt dosyasý varsa, içindeki verileri oku
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
            else // Eðer kayýt dosyasý yoksa dersleri sýfýrdan oluþtur
            {
                devamsizliklar = new Dictionary<string, int>()
                {
                    {"GÖRSEL PROGRAMLAMA I", 0},
                    {"ÝLERÝ NESNE TABANLI PROGRAMLAMA", 0},
                    {"ÝNTERNET PROGRAMCILIÐI I", 0},
                    {"PYTHON PROGRAMLAMA", 0},
                    {"SÝSTEM ANALÝZ VE TASARIMI", 0}
                };
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Listeden seçili olan satýrý al
            string? seciliSatir = listBox1.SelectedItem?.ToString();

            // Eðer listeden hiçbir þey seçilmemiþse uyarý mesajý göster 
            if (string.IsNullOrEmpty(seciliSatir))
            {
                MessageBox.Show("Listeden bir ders seç.", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçili satýrdan dersin adýný ayýkla
            string dersAdi = seciliSatir.Substring(0, seciliSatir.IndexOf('[')).Trim();

            // Seçili dersin devamsýzlýðýný sýfýrla
            if (devamsizliklar != null && devamsizliklar.ContainsKey(dersAdi))
            {
                devamsizliklar[dersAdi] = 0;
            }

            // Ekraný ve dosyayý en güncel haliyle yenile
            GuncelleListBox();
            Kaydet();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Çizilecek satýrýn metnini alma
            string text = listBox1.Items[e.Index].ToString();

            // Varsayýlan metin rengi
            Color textColor = Color.Black;

            try
            {
                string sayilarBolumu = text.Substring(text.IndexOf('[') + 1, text.IndexOf(']') - text.IndexOf('[') - 1);
                var parcalar = sayilarBolumu.Split('/');
                int mevcutDevamsizlik = int.Parse(parcalar[0].Trim());
                int maxDevamsizlikHakki = int.Parse(parcalar[1].Trim());

                if (mevcutDevamsizlik > maxDevamsizlikHakki)
                {
                    textColor = Color.Red; // Sýnýrý geçtiyse KIRMIZI
                }
                else if (mevcutDevamsizlik >= maxDevamsizlikHakki * 0.8)
                {
                    textColor = Color.DarkOrange; // Sýnýr %80'ine ulaþtýysa TURUNCU renk
                }
            }
            catch
            {
                // Hata olursa varsayýlan siyah renk olsun
            }

            e.DrawBackground();

            using (Brush brush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Listede seçilen dersi açýlýr menüde de seçili yap
            comboBox1.SelectedItem = listBox1.SelectedItem?.ToString().Substring(0, listBox1.SelectedItem.ToString().IndexOf('[')).Trim();
        }
    }
}