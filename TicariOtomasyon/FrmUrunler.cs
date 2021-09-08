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

namespace TicariOtomasyon
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglanti = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_URUNLER", baglanti.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand kaydet = new SqlCommand("Insert into TBL_URUNLER Values (@Ad,@Marka,@Model,@Yıl,@Adet,@Afiyat,@Sfiyat,@Detay)",baglanti.baglanti());
            kaydet.Parameters.AddWithValue("@Ad", TxtAd.Text);
            kaydet.Parameters.AddWithValue("@Marka", TxtMarka.Text);
            kaydet.Parameters.AddWithValue("@Model", TxtModel.Text);
            kaydet.Parameters.AddWithValue("@Yıl", MskYil.Text);
            kaydet.Parameters.AddWithValue("@Adet", int.Parse((NudAdet.Value).ToString()));
            kaydet.Parameters.AddWithValue("@Afiyat", decimal.Parse( TxtAlis.Text));
            kaydet.Parameters.AddWithValue("@Sfiyat", decimal.Parse( TxtSatis.Text));
            kaydet.Parameters.AddWithValue("@Detay", RchDetay.Text);
            kaydet.ExecuteNonQuery();
            baglanti.baglanti().Close();
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("Delete from TBL_URUNLER where ID = @Id",baglanti.baglanti());
            sil.Parameters.AddWithValue("@Id",TxtId.Text);
            sil.ExecuteNonQuery();
            baglanti.baglanti().Close();
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            TxtId.Text = dr[0].ToString();
            TxtAd.Text = dr[1].ToString();
            TxtMarka.Text = dr[2].ToString();
            TxtModel.Text = dr[3].ToString();
            MskYil.Text = dr[4].ToString();
            NudAdet.Value = int.Parse( dr[5].ToString());
            TxtAlis.Text = dr[6].ToString();
            TxtSatis.Text = dr[7].ToString();
            RchDetay.Text = dr[8].ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("Update TBL_URUNLER Set AD=@Ad,MARKA=@Marka,MODEL=@Model,YIL=@Yıl," +
                "ADET=@Adet,ALISFIYAT=@Afiyat,SATISFIYAT=@Sfiyat,DETAY=@Detay where ID=@Id",baglanti.baglanti());
            guncelle.Parameters.AddWithValue("@Ad", TxtAd.Text);
            guncelle.Parameters.AddWithValue("@Marka", TxtMarka.Text);
            guncelle.Parameters.AddWithValue("@Model", TxtModel.Text);
            guncelle.Parameters.AddWithValue("@Yıl", MskYil.Text);
            guncelle.Parameters.AddWithValue("@Adet", int.Parse((NudAdet.Value).ToString()));
            guncelle.Parameters.AddWithValue("@Afiyat", decimal.Parse(TxtAlis.Text));
            guncelle.Parameters.AddWithValue("@Sfiyat", decimal.Parse(TxtSatis.Text));
            guncelle.Parameters.AddWithValue("@Detay", RchDetay.Text);
            guncelle.Parameters.AddWithValue("@Id", TxtId.Text);
            guncelle.ExecuteNonQuery();
            baglanti.baglanti().Close();
            listele();
        }
    }
}
