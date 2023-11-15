using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class FormGirisler : Form
    {
        public FormGirisler()
        {
            InitializeComponent();
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            FormHasta HastaGiris = new FormHasta();
            HastaGiris.Show();
            this.Hide();
        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            FormDoktorGiris DoktorGiris = new FormDoktorGiris();
            DoktorGiris.Show();
            this.Hide();
        }

        private void btnSekreter_Click(object sender, EventArgs e)
        {
            FormSekreterGiris SekreterGiris = new FormSekreterGiris();
            SekreterGiris.Show();
            this.Hide();
        }
    }
}
