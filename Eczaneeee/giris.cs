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
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=eczane.accdb");
        private void button1_Click(object sender, EventArgs e)
        {
            IlacBilgileri ilac = new IlacBilgileri();
            ilac.Show(); 
            this.Hide();
        }

        private void Giriş_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            hastakayıt hastakayıt = new hastakayıt();
            hastakayıt.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            personelkayıt personelkayıt = new personelkayıt();
            personelkayıt.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stoktakibi stoktakibi = new stoktakibi();
            stoktakibi.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            satis satis = new satis();
            satis.Show();
            this.Hide();
        }
    }
}
