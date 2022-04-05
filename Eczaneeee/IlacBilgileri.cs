using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Eczaneeee
{
    public partial class IlacBilgileri : Form
    {
        public IlacBilgileri()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=eczane.accdb");
        private void button3_Click(object sender, EventArgs e)
        {
            giris giriş = new giris();
            giriş.Show();
            this.Close();

        }
        public void yenile()
        {
            DataTable dt = new DataTable();
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT barkod_no,ilacin_adi,uretici_firma,kutu_sayisi,fiyati,kullanim_amaci,yan_etkileri from ilac", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        bool x;
        void kontrol()
        {
            baglanti.Open();
            OleDbCommand kullan = new OleDbCommand("SELECT * FROM kullanici WHERE kullanici_adi=@P1", baglanti);
            kullan.Parameters.AddWithValue("@p1", textBox1.Text);
            OleDbDataReader okut = kullan.ExecuteReader();

            if (okut.Read())
            {
                x = false;
            }
            else
                x = true;
            baglanti.Close();
        }
        public void yoket()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            MiktarNUD.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        private void IlacBilgileri_Load(object sender, EventArgs e)
        {
            yenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kontrol();
                if (x == true)
                {
                    OleDbCommand ekle = new OleDbCommand("insert into ilac(barkod_no,ilacin_adi,uretici_firma,kutu_sayisi,fiyati,kullanim_amaci,yan_etkileri)" +
                    "values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + MiktarNUD.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')", baglanti);

                    baglanti.Open();
                    ekle.ExecuteNonQuery();
                    baglanti.Close();


                    yenile();
                    MessageBox.Show("Veriler Eklenmiştir.");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                    yoket();
                }
                else
                    MessageBox.Show("Bu Veri Zaten Kayıtlı.");
            }

            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                baglanti.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Dikkat Tüm Verileriniz Silinebilir.", MessageBoxButtons.YesNo);
            
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE  FROM  ilac WHERE barkod_no='" + textBox8.Text + "'", baglanti);
                    baglanti.Open();
                    sil.ExecuteNonQuery();
                    baglanti.Close();
                    yenile();
                    MessageBox.Show("Kaydınız Silinmiştir.");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
            }
            else if (cevap == DialogResult.No)
            {
                MessageBox.Show("İşleminiz İptal Edildi.");
            }
            else
                MessageBox.Show("İşlemi Sonlandırdınız.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand db = new OleDbCommand("UPDATE  ilac  Set barkod_no = '" + textBox1.Text + "',ilacin_adi= '" + textBox2.Text + "',uretici_firma= '" + textBox3.Text + "', kutu_sayisi= '" + MiktarNUD.Text + "' ,fiyati= '" + textBox5.Text + "',kullanim_amaci= '" + textBox6.Text + "',yan_etkileri= '" + textBox7.Text + "' WHERE barkod_no='" + textBox8.Text + "'", baglanti);
                baglanti.Open();
                db.ExecuteNonQuery();
                baglanti.Close();
                yenile();
                MessageBox.Show("Güncellenmiştir.");

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
                yoket();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                baglanti.Close();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox8.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
            MiktarNUD.Text = dataGridView1.Rows[getir].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[getir].Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.Rows[getir].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.Rows[getir].Cells[6].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
