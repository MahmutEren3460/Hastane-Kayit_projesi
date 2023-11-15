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
    public partial class FormSekreterDetay : Form
    {
        public FormSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCNo;

        sqlbaglanti bgl = new sqlbaglanti();
        private void FormSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCNo;

            // ad soyad çekme
            SqlCommand cmd = new SqlCommand("select SekreterAdSoyad from tbl_sekreter where SekreterTC=@p1",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read()) 
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            // Branş datagride aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // Doktor datagride aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter dad = new SqlDataAdapter("select (DoktorAd+' '+DoktorSoyad)as'Doktorlar',DoktorBrans from tbl_doktorlar", bgl.baglanti());
            dad.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // Brans combobox aktarma
            SqlCommand cmd2 = new SqlCommand("select BransAd from tbl_branslar", bgl.baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }
        
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmdkaydet = new SqlCommand("insert into tbl_randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            cmdkaydet.Parameters.AddWithValue("@r1",mskTarih.Text);
            cmdkaydet.Parameters.AddWithValue("@r2",mskSaat.Text);
            cmdkaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            cmdkaydet.Parameters.AddWithValue("@r4",cmbDoktor.Text);
            cmdkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand cmddoktor = new SqlCommand("select DoktorAd,DoktorSoyad from tbl_doktorlar where DoktorBrans=@p1", bgl.baglanti());
            cmddoktor.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader drdoktor = cmddoktor.ExecuteReader();
            while(drdoktor.Read())
            {
                cmbDoktor.Items.Add(drdoktor[0] + " " + drdoktor[1]);
            }
            bgl.baglanti().Close();
        }

        private void bntOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand cmdolustur = new SqlCommand("insert into tbl_duyurular (Duyuru) values (@d1)", bgl.baglanti());
            cmdolustur.Parameters.AddWithValue("@d1",rchDuyuru.Text);
            cmdolustur.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu","Duyuru",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FormSekreterDoktor SekreterDoktor= new FormSekreterDoktor();
            SekreterDoktor.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FormSekreterBrans SekreterBrans = new FormSekreterBrans();
            SekreterBrans.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            FormSekreterRandevu fsr=new FormSekreterRandevu();
            fsr.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FormDuyurular fsd=new FormDuyurular();
            fsd.Show();
        }
    }
}
