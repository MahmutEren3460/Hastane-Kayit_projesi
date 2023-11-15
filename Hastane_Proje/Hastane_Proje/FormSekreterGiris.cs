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
    public partial class FormSekreterGiris : Form
    {
        public FormSekreterGiris()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from tbl_sekreter where SekreterTC=@p1 and SekreterSifre=@p2",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", mskTCKimlik.Text);
            cmd.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                FormSekreterDetay fr = new FormSekreterDetay();
                fr.TCNo = mskTCKimlik.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girilen TC & Şifre hatalı","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
