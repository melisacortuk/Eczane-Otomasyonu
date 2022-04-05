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
    public partial class satis : Form
    {
        public satis()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=eczane.accdb");
         private void SilButton_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Dikkat Tüm Verileriniz Silinebilir.", MessageBoxButtons.YesNo);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE  FROM  satis WHERE tc_kimlik='" + textBox6.Text + "'", baglanti);
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
            giris giriş = new giris();
            giriş.Show();
            this.Close();
        }
        public void yenile()
        {
            DataTable dt = new DataTable();
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT tc_kimlik,adi_soyadi,d_tarihi,telefonu,sosyal_guvencesi,ilacin_adi,ilacin_fiyati,miktar,toplam_fiyat,islem_tarihi from satis", baglanti);
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
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            MiktarNUD.Text = "";
            textBox5.Text = "";
            maskedTextBox3.Text = "";
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void Satış_Load(object sender, EventArgs e)
        {
            yenile();
            comboBox1.Items.Add("Var");
            comboBox1.Items.Add("Yok");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void DuzenleButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand db = new OleDbCommand("UPDATE  satis  Set tc_kimlik = '" + textBox1.Text + "',adi_soyadi= '" + textBox2.Text + "',d_tarihi= '" + maskedTextBox2.Text + "', telefonu= '" + maskedTextBox2.Text + "' ,sosyal_guvencesi= '" + comboBox1.Text + "',ilacin_adi= '" + textBox3.Text + "',ilacin_fiyati= '" + textBox4.Text + "',miktar= '" + MiktarNUD.Text + "',toplam_fiyat= '" + textBox5.Text + "',islem_tarihi= '" + maskedTextBox3.Text + "' WHERE tc_kimlik='" + textBox6.Text + "'", baglanti);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox6.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[getir].Cells[3].Value.ToString();
            maskedTextBox2.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[getir].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[getir].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[getir].Cells[6].Value.ToString();
            MiktarNUD.Text = dataGridView1.Rows[getir].Cells[7].Value.ToString();
            textBox5.Text = dataGridView1.Rows[getir].Cells[8].Value.ToString();
            maskedTextBox3.Text = dataGridView1.Rows[getir].Cells[9].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                kontrol();
                if (x == true)
                {
                    OleDbCommand ekle = new OleDbCommand("insert into satis(tc_kimlik,adi_soyadi,d_tarihi,telefonu,sosyal_guvencesi,ilacin_adi,ilacin_fiyati,miktar,toplam_fiyat,islem_tarihi)" +
                    "values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + maskedTextBox1.Text + "', '" + maskedTextBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + MiktarNUD.Text + "','" + textBox5.Text + "','" + maskedTextBox3.Text + "')", baglanti);

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

        private void button3_Click(object sender, EventArgs e)
        {
            int miktar = 0; //Integer oluşturduk ve değerini 0 yaptık.        
            int ver = dataGridView1.SelectedCells[0].RowIndex; //Değer oluşturduk ve datagridwiev'ın sütunundan veriyi aldık.
            label10.Text = dataGridView1.Rows[ver].Cells[7].Value.ToString();  //Bu veriyi string yapıp label1'e aktardık.
            miktar = Convert.ToInt32(label10.Text); //label1'deki değeri convert yaptık.
            miktar -= 1; //Bu değeri 1 azalttık.


            OleDbCommand güncelle = new OleDbCommand("UPDATE satis set miktar='" + miktar + "' WHERE tc_kimlik='" + textBox1.Text + "'", baglanti);
            baglanti.Open();
            güncelle.ExecuteNonQuery();
            baglanti.Close();
            yenile();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
