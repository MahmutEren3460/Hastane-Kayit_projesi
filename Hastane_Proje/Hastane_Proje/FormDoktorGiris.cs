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
    public partial class FormDoktorGiris : Form
    {
        public FormDoktorGiris()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_doktorlar where DoktorTC=@p1 and DoktorSifre=@p2", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",mskTCKimlik.Text);
            cmd .Parameters.AddWithValue("@p2",txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                FormDoktorDetay frd = new FormDoktorDetay();
                frd.TC = mskTCKimlik.Text;
                frd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girdiğiniz TC & Şifre Hatalı","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bgl.baglanti().Close();
        }
    }
}
