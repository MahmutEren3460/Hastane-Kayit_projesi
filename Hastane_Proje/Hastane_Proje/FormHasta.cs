using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Proje
{
    public partial class FormHasta : Form
    {
        public FormHasta()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        private void lkbUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormHastaKayıt hastaKayıt = new FormHastaKayıt();
            hastaKayıt.Show();

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_hastalar where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",mskTCKimlik.Text);
            cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                FormHastaDetay HastaDetay = new FormHastaDetay();
                HastaDetay.tc = mskTCKimlik.Text;
                HastaDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }
            bgl.baglanti().Close();
        }
    }
}
