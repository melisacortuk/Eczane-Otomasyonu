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
    public partial class hastakayıt : Form
    {
        public hastakayıt()
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
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT tc_kimlik,adi_soyadi,d_tarihi,sosyal_guvencesi,adresi,telefonu,kullanilan_ilac,kullanim_sekli,kullanim_zamani from hasta", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hastakayıt_Load(object sender, EventArgs e)
        {
            yenile();
            comboBox1.Items.Add("Var");
            comboBox1.Items.Add("Yok");
            comboBox2.Items.Add("Aç");
            comboBox2.Items.Add("Tok");
            comboBox3.Items.Add("Sabah");
            comboBox3.Items.Add("Öğle");
            comboBox3.Items.Add("Akşam");
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
            maskedTextBox2.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
            maskedTextBox1.Text = "";
            textBox5.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                kontrol();
                if (x == true)
                {
                    OleDbCommand ekle = new OleDbCommand("insert into hasta(tc_kimlik,adi_soyadi,d_tarihi,sosyal_guvencesi,adresi,telefonu,kullanilan_ilac,kullanim_sekli,kullanim_zamani)" +
                        "values('" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox2.Text + "','" + comboBox1.Text + "', '" + textBox3.Text + "','" + maskedTextBox1.Text + "','" + textBox5.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "')", baglanti);

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand db = new OleDbCommand("UPDATE  hasta  Set tc_kimlik = '" + textBox1.Text + "',adi_soyadi= '" + textBox2.Text + "',d_tarihi= '" + maskedTextBox2.Text + "', sosyal_guvencesi= '" + comboBox1.Text + "' ,adresi= '" + textBox3.Text + "',telefonu= '" + maskedTextBox1.Text + "',kullanilan_ilac= '" + textBox5.Text + "',kullanim_sekli= '" + comboBox2.Text + "',kullanim_zamani= '" + comboBox3.Text + "' WHERE tc_kimlik='" + textBox4.Text + "'", baglanti);
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Dikkat Tüm Verileriniz Silinebilir.", MessageBoxButtons.YesNo);

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE  FROM  hasta WHERE tc_kimlik='" + textBox4.Text + "'", baglanti);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox4.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
            maskedTextBox2.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[getir].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[getir].Cells[4].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[getir].Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.Rows[getir].Cells[6].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[getir].Cells[7].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[getir].Cells[8].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
