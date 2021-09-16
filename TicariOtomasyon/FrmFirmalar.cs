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
    }
}
