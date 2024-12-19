using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListClassHastane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Randevu randevu;
        BindingList<Randevu> list = new BindingList<Randevu>();

        private void Form1_Load(object sender, EventArgs e)
        {

            // ComboBox'a polikliniklerin eklenmesi
            string[] poliklinikler = {
                "Kardiyoloji", "Dahiliye", "Ortopedi", "Kulak Burun Boğaz", "Göz Hastalıkları",
                "Cildiye", "Nöroloji", "Üroloji", "Gastroenteroloji", "Radyoloji",
                "Psikiyatri", "Plastik Cerrahi", "Fizik Tedavi", "Onkoloji", "Endokrinoloji",
                "Hematoloji", "Kadın Hastalıkları ve Doğum", "Enfeksiyon Hastalıkları",
                "Üreme Endokrinolojisi ve İnfertilite", "Genel Cerrahi"
            };

            cmbPoliklinik.Items.AddRange(poliklinikler);


            //dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12); // Varsayılan hücre fontu ayarı
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Sütun başlığı fontu ayarı


            list.Add(new Randevu(1, "Ahmet Aydın", "52154521", new DateTime(2023,11,23), true, "Göz"));
            list.Add(new Randevu(2, "Ayşe Uysal", "54511555", new DateTime(2023,11,24), true, "Kulak-Burun-Boğaz"));
            list.Add(new Randevu(3, "Hakan Demir", "52154521", new DateTime(2023,11,25), true, "Cilt"));
            list.Add(new Randevu(4, "Kemal Saygın", "52154521", new DateTime(2023,11,27), false, "Göz"));
            list.Add(new Randevu(5, "Mehmet Yılmaz", "55443322", new DateTime(2023, 11, 28), true, "Ortopedi"));
            list.Add(new Randevu(6, "Fatma Kaya", "52111223", new DateTime(2023, 11, 29), true, "Dahiliye"));
            list.Add(new Randevu(7, "Mustafa Arı", "53322114", new DateTime(2023, 11, 30), true, "Nöroloji"));
            list.Add(new Randevu(8, "Zeynep Yıldız", "52199887", new DateTime(2023, 12, 1), true, "Kardiyoloji"));
            list.Add(new Randevu(9, "Emre Çelik", "54321098", new DateTime(2023, 12, 2), false, "Gastroenteroloji"));
            list.Add(new Randevu(10, "Sevil Yılmaz", "55556789", new DateTime(2023, 12, 3), true, "Göğüs Hastalıkları"));
            list.Add(new Randevu(11, "Seda Akgün", "52223344", new DateTime(2023, 12, 4), true, "Dermatoloji"));
            list.Add(new Randevu(12, "Kadir Toprak", "53334455", new DateTime(2023, 12, 5), false, "Üroloji"));
            list.Add(new Randevu(13, "Şeyma Karadeniz", "54445566", new DateTime(2023, 12, 6), true, "Psikiyatri"));
            list.Add(new Randevu(14, "Cemal Yıldırım", "55556677", new DateTime(2023, 12, 7), true, "Plastik Cerrahi"));



            dataGridView1.DataSource = list.ToList();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtId.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["ad"].Value.ToString();
                txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
                cmbPoliklinik.Text = dataGridView1.CurrentRow.Cells["poliklinik"].Value.ToString();
                chkSigorta.Checked = (Boolean)dataGridView1.CurrentRow.Cells["sigorta"].Value;
                dtpDogumTarih.Value = (DateTime)dataGridView1.CurrentRow.Cells["tarih"].Value;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            int id=Convert.ToInt32(txtId.Text);
            string ad = txtAdSoyad.Text;
            string telefon=txtTelefon.Text;
            bool sigorta = chkSigorta.Checked;
            DateTime tarih = dtpDogumTarih.Value;
            string poliklinik = cmbPoliklinik.Text;
            Randevu yeniRandevu = new Randevu(id, ad,telefon,tarih,sigorta,poliklinik);

            list.Add(yeniRandevu);

            dataGridView1.DataSource = list.ToList();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Randevu secilenRandevu = selectedRow.DataBoundItem as Randevu;

                if (secilenRandevu != null)
                {
                    DialogResult result = MessageBox.Show("Seçili randevuyu silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (list.Contains(secilenRandevu))
                        {
                            list.Remove(secilenRandevu);
                            MessageBox.Show("Seçili randevu başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Seçili randevu listede bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seçili satır bir randevu öğesi içermiyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            dataGridView1.DataSource = list.ToList();


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satır varsa, bu satırdan ilgili veriyi alabiliriz
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // DataGridView'de sakladığınız nesne türüne (Randevu) dönüştürme işlemi yapabiliriz
                Randevu secilenRandevu = selectedRow.DataBoundItem as Randevu;

                if (secilenRandevu != null)
                {
                    // Yeni değerleri kullanarak seçilen öğenin özelliklerini güncelleme
                    int id = Convert.ToInt32(txtId.Text);
                    string ad = txtAdSoyad.Text;
                    string telefon = txtTelefon.Text;
                    bool sigorta = chkSigorta.Checked;
                    DateTime tarih = dtpDogumTarih.Value;
                    string poliklinik = cmbPoliklinik.Text;

                    secilenRandevu.Ad = ad;
                    secilenRandevu.Telefon = telefon;
                    secilenRandevu.Sigorta = sigorta;
                    secilenRandevu.Tarih = tarih;
                    secilenRandevu.Poliklinik= poliklinik;

                    // DataGridView'i güncelleme
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = list;
                }
            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string arananMetin = txtAra.Text.ToLower(); // Arama metnini küçük harfe çevirerek tutuyoruz.

            // Eşleşen verileri tutmak için yeni bir BindingList oluşturuyoruz.
            BindingList<Randevu> filtrelenmisListe = new BindingList<Randevu>(
                list.Where(randevu =>
                    randevu.Ad.ToLower().Contains(arananMetin) || // Randevu adı içeriyorsa
                    randevu.Telefon.ToLower().Contains(arananMetin) || // Telefon numarası içeriyorsa
                    randevu.Poliklinik.ToLower().Contains(arananMetin) // Poliklinik içeriyorsa
                ).ToList()
            );

            dataGridView1.DataSource = filtrelenmisListe;
        }
    }
}
