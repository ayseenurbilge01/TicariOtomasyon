using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class FrmAnaSayfa : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        FrmUrunler urun;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(urun ==null)
            {
                urun = new FrmUrunler();
                urun.MdiParent = this;
                urun.Show();
            }
            

        }
        FrmMusteriler musteri;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(musteri == null)
            {
                musteri = new FrmMusteriler();
                musteri.MdiParent = this;
                musteri.Show();
            }
        }
    }
}
