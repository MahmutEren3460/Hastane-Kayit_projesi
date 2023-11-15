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
    public partial class FormBilgiGuncelle : Form
    {
        public FormBilgiGuncelle()
        {
            InitializeComponent();
        }
        public string TCno;

        sqlbaglanti bgl = new sqlbaglanti();
        private void FormBilgiGuncelle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            SqlCommand cmd = new SqlCommand("select * from tbl_hastalar where HastaTC=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",mskTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTelefon.Text = dr[4].ToString();
                cmbCinsiyet.Text = dr[5].ToString();
                txtSifre.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("update tbl_hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaCinsiyet=@p4,HastaSifre=@p5 where HastaTC=@p6",bgl.baglanti());
            cmd2.Parameters.AddWithValue("@p1",txtAd.Text);
            cmd2.Parameters.AddWithValue("@p2",txtSoyad.Text);
            cmd2.Parameters.AddWithValue("@p3",mskTelefon.Text);
            cmd2.Parameters.AddWithValue("@p4", cmbCinsiyet.Text);
            cmd2.Parameters.AddWithValue("@p5", txtSifre.Text);
            cmd2.Parameters.AddWithValue("@p6", mskTC.Text);
            cmd2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
