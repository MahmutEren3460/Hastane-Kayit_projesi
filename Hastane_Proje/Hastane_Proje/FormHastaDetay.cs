using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class FormHastaDetay : Form
    {
        public FormHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;

        sqlbaglanti bgl = new sqlbaglanti();
        private void FormHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;

            // ad soyad çekme
            SqlCommand cmd = new SqlCommand("select HastaAd,HastaSoyad from tbl_hastalar where HastaTC=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //randevu geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where HastaTC="+tc,bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            // branş çekme
            SqlCommand cmd2 = new SqlCommand("select BransAd from tbl_branslar",bgl.baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand cmd3 = new SqlCommand("select DoktorAd,DoktorSoyad from tbl_doktorlar where DoktorBrans=@p1", bgl.baglanti());
            cmd3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0]+ " " + dr3[1]);

            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where RandevuBrans='"+cmbBrans.Text+"'"+"and RandevuDoktor='"+cmbDoktor.Text+"'and RandevuDurum=0",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkBilgiGuncelle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormBilgiGuncelle frm = new FormBilgiGuncelle();
            frm.TCno=lblTC.Text;
            frm.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update tbl_randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where RandevuId=@p3", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",lblTC.Text);
            cmd.Parameters.AddWithValue("@p2",rchSikayet.Text);
            cmd.Parameters.AddWithValue("@p3",txtid.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
