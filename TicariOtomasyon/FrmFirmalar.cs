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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        private void groupControl4_Paint(object sender, PaintEventArgs e)
        {

        }
        sqlbaglantisi baglanti = new sqlbaglantisi();
         void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR from TBL_ILLER", baglanti.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[0]);
            }
            baglanti.baglanti().Close();
        }

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FIRMALAR",baglanti.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();
            sehirlistesi();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
             DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                SqlCommand komut = new SqlCommand("Select ILCE,(Select SEHIR from TBL_ILLER Where ID = t.SEHIR ) from TBL_ILCELER t Where ID =@id",baglanti.baglanti());
                komut.Parameters.AddWithValue("@id",dr[11]);
                SqlDataReader dr1 = komut.ExecuteReader();
                if(dr1.Read())
                {
                    TxtId.Text = dr[0].ToString();
                    TxtAd.Text = dr[1].ToString();
                    TxtYGorev.Text = dr[2].ToString();
                    MskYTc.Text = dr[3].ToString();
                    TxtYAd.Text = dr[4].ToString();
                    TxtYSoyad.Text = dr[5].ToString();
                    MskTel1.Text = dr[6].ToString();
                    MskTel2.Text = dr[7].ToString();
                    MskTel3.Text = dr[8].ToString();
                    TxtMail.Text = dr[9].ToString();
                    MskFaks.Text = dr[10].ToString();
                    TxtVergiD.Text = dr[12].ToString();
                    RchAdres.Text = dr[13].ToString();
                    TxtKod1.Text = dr[14].ToString();
                    TxtKod2.Text = dr[15].ToString();
                    TxtKod3.Text = dr[16].ToString();
                    TxtSektor.Text = dr[17].ToString();
                    CmbIl.Text = dr1[1].ToString();
                    CmbIlce.Text = dr1[0].ToString();
                   
                }
                
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select ID from TBL_ILCELER t Where ILCE =@id", baglanti.baglanti());
            komut.Parameters.AddWithValue("@id", CmbIlce.SelectedItem);
            SqlDataReader dr1 = komut.ExecuteReader();
            if (dr1.Read())
            {
                SqlCommand ekle = new SqlCommand("Insert into TBL_FIRMALAR Values (@ad,@gorev,@tc,@yad,@ysoyad,@tel1,@tel2,@tel3,@mail,@fax," +
                "@ilce,@vdaire,@adres,@okod1,@okod2,@okod3,@sektor)", baglanti.baglanti());
                ekle.Parameters.AddWithValue("@ad", TxtAd.Text);
                ekle.Parameters.AddWithValue("@gorev", TxtYGorev.Text);
                ekle.Parameters.AddWithValue("@tc", MskYTc.Text);
                ekle.Parameters.AddWithValue("@yad", TxtYAd.Text);
                ekle.Parameters.AddWithValue("@ysoyad", TxtYSoyad.Text);
                ekle.Parameters.AddWithValue("@tel1", MskTel1.Text);
                ekle.Parameters.AddWithValue("@tel2", MskTel2.Text);
                ekle.Parameters.AddWithValue("@tel3", MskTel3.Text);
                ekle.Parameters.AddWithValue("@mail", TxtMail.Text);
                ekle.Parameters.AddWithValue("@fax", MskFaks.Text);
                ekle.Parameters.AddWithValue("@ilce", TxtId.Text);
                ekle.Parameters.AddWithValue("@vdaire", TxtVergiD.Text);
                ekle.Parameters.AddWithValue("@adres", RchAdres.Text);
                ekle.Parameters.AddWithValue("@okod1", TxtKod1.Text);
                ekle.Parameters.AddWithValue("@okod2", TxtKod2.Text);
                ekle.Parameters.AddWithValue("@okod3", TxtKod3.Text);
                ekle.Parameters.AddWithValue("@sektor", TxtSektor.Text);
                ekle.ExecuteNonQuery();
                baglanti.baglanti().Close();
                firmalistesi();
            }
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
    }
}
