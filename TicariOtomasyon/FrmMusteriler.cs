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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglanti = new sqlbaglantisi();
        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_MUSTERILER", baglanti.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        public void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_ILLER", baglanti.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[0]);
            }
            baglanti.baglanti().Close();
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ID from TBL_ILLER Where SEHIR = @Sehir", baglanti.baglanti());
            komut.Parameters.AddWithValue("@Sehir", CmbIl.SelectedItem);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                SqlCommand komut2 = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR = @Sehir", baglanti.baglanti());
                komut2.Parameters.AddWithValue("@Sehir", dr[0]);
                SqlDataReader dr2 = komut2.ExecuteReader(0);
                while (dr2.Read())
                {
                    CmbIlce.Properties.Items.Add(dr2[0]);
                }
            }
            baglanti.baglanti().Close();


        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select ID from TBL_ILCELER Where ILCE = @ilce", baglanti.baglanti());
            komut.Parameters.AddWithValue("@ilce", CmbIlce.SelectedItem);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                SqlCommand ekle = new SqlCommand("Insert  into TBL_MUSTERILER values (@ad,@soyad,@tel1,@tel2,@tc,@mail,@ilce,@adres,@vdaire)", baglanti.baglanti());
                ekle.Parameters.AddWithValue("@ad", TxtAd.Text);
                ekle.Parameters.AddWithValue("@soyad", TxtSoyad.Text);
                ekle.Parameters.AddWithValue("@tel1", MskTel1.Text);
                ekle.Parameters.AddWithValue("@tel2", MskTel2.Text);
                ekle.Parameters.AddWithValue("@tc", MskTc.Text);
                ekle.Parameters.AddWithValue("@mail", TxtMail.Text);
                ekle.Parameters.AddWithValue("@ilce", dr[0]);
                ekle.Parameters.AddWithValue("@adres", RchAdres.Text);
                ekle.Parameters.AddWithValue("@vdaire", TxtVergiD.Text);
                ekle.ExecuteNonQuery();
            }
            listele();
            baglanti.baglanti().Close();


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                SqlCommand komut = new SqlCommand("Select ILCE,(Select SEHIR from TBL_ILLER Where ID = t.SEHIR ) from TBL_ILCELER t Where ID =@id",baglanti.baglanti());
                komut.Parameters.AddWithValue("@id",dr[7]);
                SqlDataReader dr1 = komut.ExecuteReader();
                if(dr1.Read())
                {
                    TxtId.Text = dr[0].ToString();
                    TxtAd.Text = dr[1].ToString();
                    TxtSoyad.Text = dr[2].ToString();
                    MskTel1.Text = dr[3].ToString();
                    MskTel2.Text = dr[4].ToString();
                    MskTc.Text = dr[5].ToString();
                    TxtMail.Text = dr[6].ToString();
                    CmbIl.Text = dr1[1].ToString();
                    CmbIlce.Text = dr1[0].ToString();
                    RchAdres.Text = dr[8].ToString();
                    TxtVergiD.Text = dr[9].ToString();
                }
                
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult x = MessageBox.Show("Silmek İstediğinizden Emin Misiniz?", "Sil Mesajı", MessageBoxButtons.YesNo);
            if (x == DialogResult.Yes)
            {
                SqlCommand sil = new SqlCommand("Delete from TBL_MUSTERILER Where ID=@id", baglanti.baglanti());
                sil.Parameters.AddWithValue("@id", TxtId.Text);
                sil.ExecuteNonQuery();
                baglanti.baglanti().Close();
                listele();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select ID from TBL_ILCELER Where ILCE = @ilce", baglanti.baglanti());
            komut.Parameters.AddWithValue("@ilce", CmbIlce.SelectedItem);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                SqlCommand guncelle = new SqlCommand("update TBL_MUSTERILER Set AD=@ad,SOYAD=@soyad,TELEFON=@tel1,TELEFON2=@tel2,TC=@tc,MAIL=@mail,ILCE=@ilce,ADRES=@adres,VERGIDAIRE=@vdaire Where ID = @id", baglanti.baglanti());
                guncelle.Parameters.AddWithValue("@ad", TxtAd.Text);
                guncelle.Parameters.AddWithValue("@soyad", TxtSoyad.Text);
                guncelle.Parameters.AddWithValue("@tel1", MskTel1.Text);
                guncelle.Parameters.AddWithValue("@tel2", MskTel2.Text);
                guncelle.Parameters.AddWithValue("@tc", MskTc.Text);
                guncelle.Parameters.AddWithValue("@mail", TxtMail.Text);
                guncelle.Parameters.AddWithValue("@ilce", dr[0]);
                guncelle.Parameters.AddWithValue("@adres", RchAdres.Text);
                guncelle.Parameters.AddWithValue("@vdaire", TxtVergiD.Text);
                guncelle.Parameters.AddWithValue("@id", TxtId.Text);
                guncelle.ExecuteNonQuery();
                baglanti.baglanti().Close();
                listele();
            }
        }
    }
}
