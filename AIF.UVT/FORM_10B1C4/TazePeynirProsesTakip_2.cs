using AIF.UVT.DatabaseLayer;
using AIF.UVT.UVTService;
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

namespace AIF.UVT
{
    public partial class TazePeynirProsesTakip_2 : Form
    {
        //font start.tasarım
        public int initialWidth;

        public int initialHeight;
        public float initialFontSize;

        //font end
        private string tarih1 = "";

        public TazePeynirProsesTakip_2(string _type, string _kullaniciid, string _UretimFisNo, string _PartiNo, string _UrunTanimi, string _istasyon, int _row, int _width, int _height, string _tarih1)
        {
            InitializeComponent();

            //font start
            AutoScaleMode = AutoScaleMode.None;

            initialWidth = Width;
            initialHeight = Height;

            initialFontSize = label1.Font.Size;
            label1.Resize += Form_Resize;

            initialFontSize = label2.Font.Size;
            label2.Resize += Form_Resize;

            initialFontSize = label3.Font.Size;
            label3.Resize += Form_Resize;

            initialFontSize = label4.Font.Size;
            label4.Resize += Form_Resize;

            initialFontSize = label5.Font.Size;
            label5.Resize += Form_Resize;

            initialFontSize = txtUretimTarihi.Font.Size;
            txtUretimTarihi.Resize += Form_Resize;

            initialFontSize = txtPaketlemeTarihi.Font.Size;
            txtPaketlemeTarihi.Resize += Form_Resize;

            initialFontSize = txtUretimSiparisNo.Font.Size;
            txtUretimSiparisNo.Resize += Form_Resize;

            initialFontSize = txtPartyNo.Font.Size;
            txtPartyNo.Resize += Form_Resize;

            initialFontSize = txtUrunTanimi.Font.Size;
            txtUrunTanimi.Resize += Form_Resize;

            initialFontSize = button1.Font.Size;
            button1.Resize += Form_Resize;

            initialFontSize = button3.Font.Size;
            button3.Resize += Form_Resize;

            initialFontSize = button4.Font.Size;
            button4.Resize += Form_Resize;

            initialFontSize = button5.Font.Size;
            button5.Resize += Form_Resize;

            initialFontSize = button6.Font.Size;
            button6.Resize += Form_Resize;

            initialFontSize = button8.Font.Size;
            button8.Resize += Form_Resize;

            initialFontSize = btnOzetEkraniDon.Font.Size;
            btnOzetEkraniDon.Resize += Form_Resize;
            //font end

            UretimFisNo = _UretimFisNo;
            partiNo = _PartiNo;
            UrunTanimi = _UrunTanimi;
            type = _type;
            kullaniciid = _kullaniciid;
            row = _row;
            istasyon = _istasyon;
            tarih1 = _tarih1;

            txtUretimSiparisNo.Text = UretimFisNo;
            txtUrunTanimi.Text = UrunTanimi;
            txtPartyNo.Text = partiNo;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            //font start
            SuspendLayout();
            float proportionalNewWidth = (float)Width / initialWidth;
            float proportionalNewHeight = (float)Height / initialHeight;

            label1.Font = new Font(label1.Font.FontFamily, initialFontSize *
              (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
              label1.Font.Style);

            label2.Font = new Font(label2.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               label2.Font.Style);

            label3.Font = new Font(label3.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               label3.Font.Style);

            label4.Font = new Font(label4.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               label4.Font.Style);

            label5.Font = new Font(label5.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                label5.Font.Style);

            txtUretimTarihi.Font = new Font(txtUretimTarihi.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtUretimTarihi.Font.Style);

            txtPaketlemeTarihi.Font = new Font(txtPaketlemeTarihi.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtPaketlemeTarihi.Font.Style);

            txtUretimSiparisNo.Font = new Font(txtUretimSiparisNo.Font.FontFamily, initialFontSize *
              (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
              txtUretimSiparisNo.Font.Style);

            txtPartyNo.Font = new Font(txtPartyNo.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtPartyNo.Font.Style);

            txtUrunTanimi.Font = new Font(txtUrunTanimi.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtUrunTanimi.Font.Style);

            button1.Font = new Font(button1.Font.FontFamily, initialFontSize *
              (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
              button1.Font.Style);

            button3.Font = new Font(button3.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button3.Font.Style);

            button4.Font = new Font(button4.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button4.Font.Style);

            button5.Font = new Font(button5.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button5.Font.Style);

            button6.Font = new Font(button6.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button6.Font.Style);

            button8.Font = new Font(button8.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button8.Font.Style);

            btnOzetEkraniDon.Font = new Font(btnOzetEkraniDon.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               btnOzetEkraniDon.Font.Style);
            ResumeLayout();
            //font end
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ClassStyle |= 0x20000;

                cp.ExStyle |= 0x02000000;

                return cp;
            }
        }

        private string UretimFisNo = "";
        private string partiNo = "";
        private string istasyon = "";
        private string UrunTanimi = "";
        private string type = "";
        private string kullaniciid = "";
        private int row = 0;
        private SqlCommand cmd = null;

        private void TazePeynirProsesTakip_2_Load(object sender, EventArgs e)
        {
            txtUretimTarihi.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtPaketlemeTarihi.Text = DateTime.Now.ToString("dd/MM/yyyy");

            string sql = "SELECT T0.\"U_Aciklama\" as \"Açıklama\" FROM \"@AIF_TZPRSS2_ANLZ\" AS T0 where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtAciklama.Text = dt.Rows[0].ItemArray[0].ToString();
            }

            dtgMamulOzellikleri();
            //dtgDinlenmeVePaketleme();
            dtgDinlenmeVePaketleme();
            dtgSarfMalzemeKullanim();
            //dtgDedektordenGecirme();
            dtgGramajKontrol();

            DataGridViewColumn dataGridViewColumn = dtMamulOz.Columns["Kuru Madde(%)"];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.RoyalBlue;

            dataGridViewColumn = dtMamulOz.Columns["Yağ Oranı (%)"];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.RoyalBlue;

            dataGridViewColumn = dtMamulOz.Columns["PH Değeri"];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.RoyalBlue;

            dataGridViewColumn = dtMamulOz.Columns["SH Değeri"];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.RoyalBlue;

            dataGridViewColumn = dtMamulOz.Columns["Tuz Oranı(%)"];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.RoyalBlue;
        }

        private void btnOzetEkraniDon_Click(object sender, EventArgs e)
        {
            BanaAitİsler banaAitİsler = new BanaAitİsler(type, kullaniciid, row, initialWidth, initialHeight, tarih1);
            banaAitİsler.Show();
            Close();
        }

        private DataTable dtMamulOzellikleri = new DataTable();
        private DataTable dtGramaj = new DataTable();

        private void dtgMamulOzellikleri()
        {
            string sql = "SELECT T1.\"U_UretilenUrunler\" as \"Üretilen Ürünler\",T1.\"U_PaketlemeOncesiSicakik\" as \"Paketleme Öncesi Ürün Sıcaklığı\", T1.\"U_UretimMiktari\" as \"Paketlenen Ürün Miktarı (Adet)\", T1.\"U_FireUrunMiktari\" as \"Fire Ürün Miktarı (Adet)\", T1.\"U_NumuneUrunMiktari\" as \"Numune Ürün Miktarı (Adet)\", T1.\"U_DepoyaGirenUrunMik\" as \"Depoya Giren Ürün Miktarı\", T1.\"U_KuruMadde\" as \"Kuru Madde(%)\", T1.\"U_YagOrani\" as \"Yağ Oranı (%)\", T1.\"U_PH\" as \"PH Değeri\", T1.\"U_SH\" as \"SH Değeri\", T1.\"U_TuzOrani\" as \"Tuz Oranı(%)\" FROM \"@AIF_TZPRSS2_ANLZ\" AS T0 INNER JOIN \"@AIF_TZPRSS2_ANLZ1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);
            dtMamulOzellikleri = dt;

            if (dt.Rows.Count == 0)
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");
                DataRow dr = dtMamulOzellikleri.NewRow();
                dr["Üretilen Ürünler"] = "";
                dr["Paketleme Öncesi Ürün Sıcaklığı"] = 0;
                dr["Paketlenen Ürün Miktarı (Adet)"] = 0;
                dr["Fire Ürün Miktarı (Adet)"] = 0;
                dr["Numune Ürün Miktarı (Adet)"] = 0;
                dr["Depoya Giren Ürün Miktarı"] = 0;
                dr["Kuru Madde(%)"] = 0;
                dr["Yağ Oranı (%)"] = 0;
                dr["PH Değeri"] = 0;
                dr["SH Değeri"] = 0;
                dr["Tuz Oranı(%)"] = 0;

                dtMamulOzellikleri.Rows.Add(dr);
            }

            //Commit
            dtMamulOz.DataSource = dt;

            dtMamulOz.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtMamulOz.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtMamulOz.EnableHeadersVisualStyles = false;
            dtMamulOz.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            dtMamulOz.Columns["Paketleme Öncesi Ürün Sıcaklığı"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Paketleme Öncesi Ürün Sıcaklığı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["Paketlenen Ürün Miktarı (Adet)"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Paketlenen Ürün Miktarı (Adet)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["Numune Ürün Miktarı (Adet)"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Numune Ürün Miktarı (Adet)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["Fire Ürün Miktarı (Adet)"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Fire Ürün Miktarı (Adet)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["Depoya Giren Ürün Miktarı"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Depoya Giren Ürün Miktarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["Kuru Madde(%)"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Kuru Madde(%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["Yağ Oranı (%)"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Yağ Oranı (%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["PH Değeri"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["PH Değeri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["SH Değeri"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["SH Değeri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.Columns["Tuz Oranı(%)"].DefaultCellStyle.Format = "N2";
            dtMamulOz.Columns["Tuz Oranı(%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtMamulOz.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dtMamulOz.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtMamulOz.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dtMamulOz.Columns["Üretilen Ürünler"].Width = dtMamulOz.Columns["Üretilen Ürünler"].Width + 100;
        }

        private void dtgSarfMalzemeKullanim()
        {
            string sql = "SELECT T1.\"U_MalzemeAdi\" as \"Malzeme Adı\",T1.\"U_MalMarkaTedarikci\" as \"Malzeme Marka ve Tedarikçi\",T1.\"U_SarfMalzemePartiNo\" as \"Sarf Malzemesi Parti No\",Convert(float,T1.\"U_Miktar\") as \"Miktar\",T1.\"U_Birim\" as \"Birim\" FROM \"@AIF_TZPRSS2_ANLZ\" AS T0 INNER JOIN \"@AIF_TZPRSS2_ANLZ2\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_UretimTarihi\" = '" + tarih1 + "'";

            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);

            //Commit
            dtgSarfMalzeme.DataSource = dt;

            dtgSarfMalzeme.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgSarfMalzeme.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgSarfMalzeme.EnableHeadersVisualStyles = false;
            dtgSarfMalzeme.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            if (dt.Rows.Count == 0)
            {
                //sql = "select T0.ItemName as \"Malzeme Adı\",CardName as \"Malzemenin Markası ve Tedarikçisi\",BatchNum as \"Sarf Malzemesi Parti No\",Quantity as \"Miktar\",T1.InvntryUom as \"Birim\" from IBT1 T0 inner join OITM T1 ON T0.ItemCode = T1.ItemCode where BaseType = 60 and BaseEntry in (select DocEntry from OIGE where U_BatchNumber = '" + partiNo + "')";

                sql = "SELECT T1.dscription as \"Malzeme Adı\" ,(select TOP 1 CardName from IBT1 where BatchNum = t3.BatchNum and Direction = '0' order by DocDate desc) as \"Malzemenin Markası ve Tedarikçisi\",t3.BatchNum as \"Sarf Malzemesi Parti No\", sum(t3.Quantity) as \"Miktar\", T4.CntUnitMsr as \"Birim\"  FROM OWOR T0 ";
                sql += "INNER JOIN IGE1 T1 ON T0.DocEntry = T1.BaseEntry ";
                sql += "INNER JOIN IBT1 T3 ON T1.Docentry = T3.BaseEntry and T3.BaseLinNum = T1.LineNum ";
                sql += "INNER JOIN OITM T4 ON T1.ItemCode = T4.ItemCode ";
                sql += "WHERE T0.[U_ISTASYON] = 'IST005' and T0.[PostDate] = '" + tarih1 + "' AND T1.BASETYPE = '202' and t3.BaseType = '60' and T4.QryGroup1 = 'Y' group by T1.dscription ,t3.BatchNum,  T4.CntUnitMsr";

                cmd = new SqlCommand(sql, Connection.sql);

                if (Connection.sql.State != ConnectionState.Open)
                    Connection.sql.Open();

                sda = new SqlDataAdapter(cmd);
                dttemp = new DataTable();
                sda.Fill(dttemp);

                foreach (DataRow dr1 in dttemp.Rows)
                {
                    System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                    DataRow dr = dt.NewRow();
                    dr["Malzeme Adı"] = dr1["Malzeme Adı"].ToString();
                    dr["Malzeme Marka ve Tedarikçi"] = dr1["Malzemenin Markası ve Tedarikçisi"].ToString();
                    dr["Sarf Malzemesi Parti No"] = dr1["Sarf Malzemesi Parti No"].ToString();
                    dr["Miktar"] = Convert.ToDouble(dr1["Miktar"].ToString());
                    dr["Birim"] = dr1["Birim"].ToString();

                    dt.Rows.Add(dr);
                }

                //DataRow dr = dt.NewRow();
                //dr["Miktar"] = Convert.ToString("0", cultureTR);

                //dt.Rows.Add(dr);
            }

            dtgSarfMalzeme.Columns["Miktar"].DefaultCellStyle.Format = "N2";
            dtgSarfMalzeme.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            //dtgSarfMalzeme.Columns["Yağ Oranı (%)"].DefaultCellStyle.Format = "N2";
            //dtgSarfMalzeme.Columns["Yağ Oranı (%)"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            //dtgSarfMalzeme.Columns["PH Değeri"].DefaultCellStyle.Format = "N2";
            //dtgSarfMalzeme.Columns["PH Değeri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            //dtgSarfMalzeme.Columns["SH Değeri"].DefaultCellStyle.Format = "N2";
            //dtgSarfMalzeme.Columns["SH Değeri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            //dtgSarfMalzeme.Columns["Miktar"].DefaultCellStyle.Format = "N2";
            //dtgSarfMalzeme.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgSarfMalzeme.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgSarfMalzeme.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgSarfMalzeme.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dtgGramajKontrol()
        {
            string sql = "SELECT T1.\"U_UrunCesidi\" as \"Ürün Çeşidi\", T1.\"U_PartiNo\" as \"Parti No\", T1.\"U_BirinciOrnek\" as \"1.Örnek\", T1.\"U_IkinciOrnek\" as \"2.Örnek\", T1.\"U_UcuncuOrnek\" as \"3.Örnek\", T1.\"U_DorduncuOrnek\" as \"4.Örnek\", T1.\"U_BesinciOrnek\" as \"5.Örnek\", T1.\"U_AltinciOrnek\" as \"6.Örnek\", T1.\"U_YedinciOrnek\" as \"7.Örnek\"  FROM \"@AIF_TZPRSS2_ANLZ\" AS T0 INNER JOIN \"@AIF_TZPRSS2_ANLZ3\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);
            dtGramaj = dt;

            if (dt.Rows.Count == 0)
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                DataRow dr = dtGramaj.NewRow();
                dr["Ürün Çeşidi"] = "";
                dr["Parti No"] = "";
                dr["1.Örnek"] = 0;
                dr["2.Örnek"] = 0;
                dr["3.Örnek"] = 0;
                dr["4.Örnek"] = 0;
                dr["5.Örnek"] = 0;
                dr["6.Örnek"] = 0;
                dr["7.Örnek"] = 0;

                dtGramaj.Rows.Add(dr);
            }

            //Commit
            dtgGramaj.DataSource = dt;

            dtgGramaj.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgGramaj.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgGramaj.EnableHeadersVisualStyles = false;
            dtgGramaj.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            dtgGramaj.Columns["1.Örnek"].DefaultCellStyle.Format = "N2";
            dtgGramaj.Columns["1.Örnek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgGramaj.Columns["2.Örnek"].DefaultCellStyle.Format = "N2";
            dtgGramaj.Columns["2.Örnek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgGramaj.Columns["3.Örnek"].DefaultCellStyle.Format = "N2";
            dtgGramaj.Columns["3.Örnek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgGramaj.Columns["4.Örnek"].DefaultCellStyle.Format = "N2";
            dtgGramaj.Columns["4.Örnek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgGramaj.Columns["5.Örnek"].DefaultCellStyle.Format = "N2";
            dtgGramaj.Columns["5.Örnek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgGramaj.Columns["6.Örnek"].DefaultCellStyle.Format = "N2";
            dtgGramaj.Columns["6.Örnek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgGramaj.Columns["7.Örnek"].DefaultCellStyle.Format = "N2";
            dtgGramaj.Columns["7.Örnek"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgGramaj.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dtgGramaj.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgGramaj.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dtgGramaj.Columns["Ürün Çeşidi"].Width = dtgGramaj.Columns["Ürün Çeşidi"].Width + 100;
        }

        private void dtgDinlenmeVePaketleme()
        {
            DateTime dtTarih = new DateTime(Convert.ToInt32(tarih1.Substring(0, 4)), Convert.ToInt32(tarih1.Substring(4, 2)), Convert.ToInt32(tarih1.Substring(6, 2)));
            string sql = "SELECT T1.\"U_AlanAdi\" as \"Alan Adı\",T1.\"U_SifirSekizSicaklik\" as \"08:00 Sıcaklık\", T1.\"U_SifirSekizNem\" as \"08:00 Nem\", T1.\"U_OnikiSicaklik\" as \"12:00 Sıcaklık\", T1.\"U_OnikiNem\" as \"12:00 Nem\", T1.\"U_OnBesSicaklik\" as \"15:00 Sıcaklık\", T1.\"U_OnBesNem\" as \"15:00 Nem\",T1.\"U_OnSekizSicaklik\" as \"18:00 Sıcaklık\", T1.\"U_OnSekizNem\" as \"18:00 Nem\" FROM \"@AIF_TZPDNPKT\" AS T1 where T1.\"U_UretimTarihi\" = '" + dtTarih.ToString("yyyyMMdd") + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);

            //Commit
            dtgDinlendirmeVePaket.DataSource = dt;

            dtgDinlendirmeVePaket.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgDinlendirmeVePaket.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgDinlendirmeVePaket.EnableHeadersVisualStyles = false;
            dtgDinlendirmeVePaket.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            if (dt.Rows.Count == 0)
            {
                //System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                DataRow dr = dt.NewRow();
                dr["Alan Adı"] = "Dinlendirme Odası";

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Alan Adı"] = "Paketleme Odası";

                dt.Rows.Add(dr);
            }

            dtgDinlendirmeVePaket.Columns["08:00 Sıcaklık"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["08:00 Sıcaklık"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dtgDinlendirmeVePaket.Columns["08:00 Nem"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["08:00 Nem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dtgDinlendirmeVePaket.Columns["12:00 Sıcaklık"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["12:00 Sıcaklık"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dtgDinlendirmeVePaket.Columns["12:00 Nem"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["12:00 Nem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dtgDinlendirmeVePaket.Columns["15:00 Sıcaklık"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["15:00 Sıcaklık"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dtgDinlendirmeVePaket.Columns["15:00 Nem"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["15:00 Nem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dtgDinlendirmeVePaket.Columns["18:00 Sıcaklık"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["18:00 Sıcaklık"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            dtgDinlendirmeVePaket.Columns["18:00 Nem"].DefaultCellStyle.Format = "N2";
            dtgDinlendirmeVePaket.Columns["18:00 Nem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;

            dtgDinlendirmeVePaket.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgDinlendirmeVePaket.AutoResizeRows();
            //dtgDinlendirmeVePaket.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgDinlendirmeVePaket.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dtMamulOz_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtMamulOz.Columns[e.ColumnIndex].Name == "Kuru Madde(%)" || dtMamulOz.Columns[e.ColumnIndex].Name == "Yağ Oranı (%)" || dtMamulOz.Columns[e.ColumnIndex].Name == "PH Değeri" || dtMamulOz.Columns[e.ColumnIndex].Name == "SH Değeri" || dtMamulOz.Columns[e.ColumnIndex].Name == "Paketleme Öncesi Ürün Sıcaklığı" || dtMamulOz.Columns[e.ColumnIndex].Name == "Paketlenen Ürün Miktarı (Adet)" || dtMamulOz.Columns[e.ColumnIndex].Name == "Fire Ürün Miktarı (Adet)" || dtMamulOz.Columns[e.ColumnIndex].Name == "Numune Ürün Miktarı (Adet)" || dtMamulOz.Columns[e.ColumnIndex].Name == "Depoya Giren Ürün Miktarı" || dtMamulOz.Columns[e.ColumnIndex].Name == "Tuz Oranı(%)")
                {
                    SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtMamulOz);
                    n.ShowDialog();

                    #region Süt Gönderim Saat Kontrolleri

                    //var baslangicSaati = dtgProsesOzellikleri1.Rows[e.RowIndex].Cells["Süt Gönderim Başlangıç Saati"].Value.ToString();
                    //var bitisSaati = dtgProsesOzellikleri1.Rows[e.RowIndex].Cells["Süt Gönderim Bitiş Saati"].Value.ToString();

                    //if (baslangicSaati.ToString() != "" && bitisSaati.ToString() != "")
                    //{
                    //    TimeSpan girisCikisFarki = DateTime.Parse(bitisSaati).Subtract(DateTime.Parse(baslangicSaati));
                    //    dtgProsesOzellikleri2_1.Rows[0].Cells["Süt Gönderim Süresi (DK)"].Value = girisCikisFarki.TotalMinutes.ToString();
                    //}
                    //else
                    //{
                    //    dtgProsesOzellikleri2_1.Rows[0].Cells["Süt Gönderim Süresi (DK)"].Value = "0";
                    //}

                    //ProsesOzellikleri1Topla();

                    #endregion Süt Gönderim Saat Kontrolleri
                }
                else if (dtMamulOz.Columns[e.ColumnIndex].Name == "Üretilen Ürünler")
                {
                    string sql_AnalizParam = "Select \"U_Deger1\",\"U_Deger2\" from \"@AIF_GNLKANLZPRM\" where \"U_Kod\" ='4'";
                    cmd = new SqlCommand(sql_AnalizParam, Connection.sql);

                    if (Connection.sql.State != ConnectionState.Open)
                        Connection.sql.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt_Sorgu = new DataTable();
                    sda.Fill(dt_Sorgu);

                    //dtgSecim.DataSource = dt;
                    //dtSecim = dt;

                    Connection.sql.Close();

                    if (dt_Sorgu.Rows.Count > 0)
                    {
                        string sql1 = "Select TOP 1 '' as \"Kalem Kodu\",'' as \"Kalem Adı\" FROM OITM where \"U_ItemGroup2\" = '" + dt_Sorgu.Rows[0][0].ToString() + "' and \"ItmsGrpCod\" = '" + dt_Sorgu.Rows[0][1].ToString() + "' ";
                        sql1 += " UNION ALL ";
                        sql1 += "Select ItemCode as \"Kalem Kodu\",ItemName as \"Kalem Adı\" FROM OITM where \"U_ItemGroup2\" = '" + dt_Sorgu.Rows[0][0].ToString() + "' and \"ItmsGrpCod\" = '" + dt_Sorgu.Rows[0][1].ToString() + "'";

                        SelectList selectList = new SelectList(sql1, dtMamulOz, -1, 0, _autoresizerow: false);
                        selectList.ShowDialog();

                        var sonSatir = dtMamulOz.Rows[dtMamulOz.RowCount - 1].Cells[e.ColumnIndex].Value.ToString();

                        if (sonSatir != "")
                        {
                            System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                            DataRow dr = dtMamulOzellikleri.NewRow();
                            dr["Üretilen Ürünler"] = "";
                            dr["Paketleme Öncesi Ürün Sıcaklığı"] = 0;
                            dr["Paketlenen Ürün Miktarı (Adet)"] = 0;
                            dr["Fire Ürün Miktarı (Adet)"] = 0;
                            dr["Numune Ürün Miktarı (Adet)"] = 0;
                            dr["Depoya Giren Ürün Miktarı"] = 0;
                            dr["Kuru Madde(%)"] = 0;
                            dr["Yağ Oranı (%)"] = 0;
                            dr["PH Değeri"] = 0;
                            dr["SH Değeri"] = 0;
                            dr["Tuz Oranı(%)"] = 0;

                            dtMamulOzellikleri.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dtgDinlendirmeVePaket_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "08:00 Sıcaklık" || dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "08:00 Nem" || dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "12:00 Sıcaklık" || dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "12:00 Nem" || dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "15:00 Sıcaklık" || dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "15:00 Nem" || dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "18:00 Sıcaklık" || dtgDinlendirmeVePaket.Columns[e.ColumnIndex].Name == "18:00 Nem")
                {
                    SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgDinlendirmeVePaket);
                    n.ShowDialog();

                    #region Süt Gönderim Saat Kontrolleri

                    //var baslangicSaati = dtgProsesOzellikleri1.Rows[e.RowIndex].Cells["Süt Gönderim Başlangıç Saati"].Value.ToString();
                    //var bitisSaati = dtgProsesOzellikleri1.Rows[e.RowIndex].Cells["Süt Gönderim Bitiş Saati"].Value.ToString();

                    //if (baslangicSaati.ToString() != "" && bitisSaati.ToString() != "")
                    //{
                    //    TimeSpan girisCikisFarki = DateTime.Parse(bitisSaati).Subtract(DateTime.Parse(baslangicSaati));
                    //    dtgProsesOzellikleri2_1.Rows[0].Cells["Süt Gönderim Süresi (DK)"].Value = girisCikisFarki.TotalMinutes.ToString();
                    //}
                    //else
                    //{
                    //    dtgProsesOzellikleri2_1.Rows[0].Cells["Süt Gönderim Süresi (DK)"].Value = "0";
                    //}

                    //ProsesOzellikleri1Topla();

                    #endregion Süt Gönderim Saat Kontrolleri
                }
            }
            catch (Exception)
            {
            }
        }

        private void dtgGramaj_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgGramaj.Columns[e.ColumnIndex].Name == "1.Örnek" || dtgGramaj.Columns[e.ColumnIndex].Name == "2.Örnek" || dtgGramaj.Columns[e.ColumnIndex].Name == "3.Örnek" || dtgGramaj.Columns[e.ColumnIndex].Name == "4.Örnek" || dtgGramaj.Columns[e.ColumnIndex].Name == "5.Örnek" || dtgGramaj.Columns[e.ColumnIndex].Name == "6.Örnek" || dtgGramaj.Columns[e.ColumnIndex].Name == "7.Örnek")
                {
                    SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgGramaj);
                    n.ShowDialog();

                    #region Süt Gönderim Saat Kontrolleri

                    //var baslangicSaati = dtgProsesOzellikleri1.Rows[e.RowIndex].Cells["Süt Gönderim Başlangıç Saati"].Value.ToString();
                    //var bitisSaati = dtgProsesOzellikleri1.Rows[e.RowIndex].Cells["Süt Gönderim Bitiş Saati"].Value.ToString();

                    //if (baslangicSaati.ToString() != "" && bitisSaati.ToString() != "")
                    //{
                    //    TimeSpan girisCikisFarki = DateTime.Parse(bitisSaati).Subtract(DateTime.Parse(baslangicSaati));
                    //    dtgProsesOzellikleri2_1.Rows[0].Cells["Süt Gönderim Süresi (DK)"].Value = girisCikisFarki.TotalMinutes.ToString();
                    //}
                    //else
                    //{
                    //    dtgProsesOzellikleri2_1.Rows[0].Cells["Süt Gönderim Süresi (DK)"].Value = "0";
                    //}

                    //ProsesOzellikleri1Topla();

                    #endregion Süt Gönderim Saat Kontrolleri
                }
                else if (dtgGramaj.Columns[e.ColumnIndex].Name == "Ürün Çeşidi")
                {
                    string sql_AnalizParam = "Select \"U_Deger1\",\"U_Deger2\" from \"@AIF_GNLKANLZPRM\" where \"U_Kod\" ='4'";
                    cmd = new SqlCommand(sql_AnalizParam, Connection.sql);

                    if (Connection.sql.State != ConnectionState.Open)
                        Connection.sql.Open();

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt_Sorgu = new DataTable();
                    sda.Fill(dt_Sorgu);

                    //dtgSecim.DataSource = dt;
                    //dtSecim = dt;

                    Connection.sql.Close();

                    if (dt_Sorgu.Rows.Count > 0)
                    {
                        string sql1 = "Select TOP 1 '' as \"Kalem Kodu\",'' as \"Kalem Adı\" FROM OITM where \"U_ItemGroup2\" = '" + dt_Sorgu.Rows[0][0].ToString() + "' and \"ItmsGrpCod\" = '" + dt_Sorgu.Rows[0][1].ToString() + "' ";
                        sql1 += " UNION ALL ";
                        sql1 += "Select ItemCode as \"Kalem Kodu\",ItemName as \"Kalem Adı\" FROM OITM where \"U_ItemGroup2\" = '" + dt_Sorgu.Rows[0][0].ToString() + "' and \"ItmsGrpCod\" = '" + dt_Sorgu.Rows[0][1].ToString() + "'";

                        SelectList selectList = new SelectList(sql1, dtgGramaj, -1, 0, _autoresizerow: false);
                        selectList.ShowDialog();

                        var sonSatir = dtgGramaj.Rows[dtgGramaj.RowCount - 1].Cells[e.ColumnIndex].Value.ToString();

                        if (sonSatir != "")
                        {
                            System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                            DataRow dr = dtGramaj.NewRow();
                            dr["Ürün Çeşidi"] = "";
                            dr["Parti No"] = "";
                            dr["1.Örnek"] = 0;
                            dr["2.Örnek"] = 0;
                            dr["3.Örnek"] = 0;
                            dr["4.Örnek"] = 0;
                            dr["5.Örnek"] = 0;
                            dr["6.Örnek"] = 0;
                            dr["7.Örnek"] = 0;

                            dtGramaj.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                UVTServiceSoapClient client = new UVTServiceSoapClient();
                TazePeynirTakipAnaliz2 nesne = new TazePeynirTakipAnaliz2();

                TazePeynir2MamulOzellikleri1 tazePeynir2MamulOzellikleri1 = new TazePeynir2MamulOzellikleri1();
                List<TazePeynir2MamulOzellikleri1> tazePeynir2MamulOzellikleri1s = new List<TazePeynir2MamulOzellikleri1>();

                TazePeynir2SarfMalzemeKullanim tazePeynir2SarfMalzemeKullanim = new TazePeynir2SarfMalzemeKullanim();
                List<TazePeynir2SarfMalzemeKullanim> tazePeynir2SarfMalzemeKullanims = new List<TazePeynir2SarfMalzemeKullanim>();

                TazePeynir2GramajKontrol tazePeynir2GramajKontrol = new TazePeynir2GramajKontrol();
                List<TazePeynir2GramajKontrol> tazePeynir2GramajKontrols = new List<TazePeynir2GramajKontrol>();

                TazePeynir2DinlendirmeVePaketleme tazePeynir2DinlendirmeVePaketleme = new TazePeynir2DinlendirmeVePaketleme();
                List<TazePeynir2DinlendirmeVePaketleme> tazePeynir2DinlendirmeVePaketlemes = new List<TazePeynir2DinlendirmeVePaketleme>();

                nesne.PartiNo = txtPartyNo.Text;
                nesne.Aciklama = txtAciklama.Text;
                nesne.KalemKodu = "";
                nesne.KalemTanimi = txtUrunTanimi.Text;

                nesne.UretimTarihi = txtUretimTarihi.Text.Substring(6, 4) + txtUretimTarihi.Text.Substring(3, 2) + txtUretimTarihi.Text.Substring(0, 2);
                nesne.PaketlemeTarihi = txtUretimTarihi.Text.Substring(6, 4) + txtUretimTarihi.Text.Substring(3, 2) + txtUretimTarihi.Text.Substring(0, 2);

                foreach (DataGridViewRow dr in dtMamulOz.Rows)
                {
                    tazePeynir2MamulOzellikleri1 = new TazePeynir2MamulOzellikleri1();

                    tazePeynir2MamulOzellikleri1.UretilenUrun = dr.Cells["Üretilen Ürünler"].Value == DBNull.Value ? "" : dr.Cells["Üretilen Ürünler"].Value.ToString();
                    tazePeynir2MamulOzellikleri1.PaketlemeOncesiSicaklik = dr.Cells["Paketleme Öncesi Ürün Sıcaklığı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Paketleme Öncesi Ürün Sıcaklığı"].Value);
                    tazePeynir2MamulOzellikleri1.PaketlenenUrunMiktari = dr.Cells["Paketlenen Ürün Miktarı (Adet)"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Paketlenen Ürün Miktarı (Adet)"].Value);
                    tazePeynir2MamulOzellikleri1.FireUrunMiktari = dr.Cells["Fire Ürün Miktarı (Adet)"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Fire Ürün Miktarı (Adet)"].Value);
                    tazePeynir2MamulOzellikleri1.NumuneUrunMiktari = dr.Cells["Numune Ürün Miktarı (Adet)"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Numune Ürün Miktarı (Adet)"].Value);
                    tazePeynir2MamulOzellikleri1.DepoyaGirenUrunMiktari = dr.Cells["Depoya Giren Ürün Miktarı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Depoya Giren Ürün Miktarı"].Value);
                    tazePeynir2MamulOzellikleri1.KuruMadde = dr.Cells["Kuru Madde(%)"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Kuru Madde(%)"].Value);
                    tazePeynir2MamulOzellikleri1.YagOrani = dr.Cells["Yağ Oranı (%)"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Yağ Oranı (%)"].Value);
                    tazePeynir2MamulOzellikleri1.PH = dr.Cells["PH Değeri"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["PH Değeri"].Value);
                    tazePeynir2MamulOzellikleri1.SH = dr.Cells["SH Değeri"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["SH Değeri"].Value);
                    tazePeynir2MamulOzellikleri1.TuzOrani = dr.Cells["Tuz Oranı(%)"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Tuz Oranı(%)"].Value);

                    tazePeynir2MamulOzellikleri1s.Add(tazePeynir2MamulOzellikleri1);
                }

                nesne.tazePeynirMamulOzellikleri1s = tazePeynir2MamulOzellikleri1s.ToArray();

                foreach (DataGridViewRow dr in dtgSarfMalzeme.Rows)
                {
                    tazePeynir2SarfMalzemeKullanim = new TazePeynir2SarfMalzemeKullanim();

                    tazePeynir2SarfMalzemeKullanim.MalzemeAdi = dr.Cells["Malzeme Adı"].Value == DBNull.Value ? "" : dr.Cells["Malzeme Adı"].Value.ToString();
                    tazePeynir2SarfMalzemeKullanim.MalzemeMarkaTedarikcisi = dr.Cells["Malzeme Marka ve Tedarikçi"].Value == DBNull.Value ? "" : dr.Cells["Malzeme Marka ve Tedarikçi"].Value.ToString();
                    tazePeynir2SarfMalzemeKullanim.SarfMalzemePartiNo = dr.Cells["Sarf Malzemesi Parti No"].Value == DBNull.Value ? "" : dr.Cells["Sarf Malzemesi Parti No"].Value.ToString();
                    tazePeynir2SarfMalzemeKullanim.Miktar = dr.Cells["Miktar"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Miktar"].Value);
                    tazePeynir2SarfMalzemeKullanim.Birim = dr.Cells["Birim"].Value == DBNull.Value ? "" : dr.Cells["Birim"].Value.ToString();

                    tazePeynir2SarfMalzemeKullanims.Add(tazePeynir2SarfMalzemeKullanim);
                }

                nesne.tazePeynir2SarfMalzemeKullanims = tazePeynir2SarfMalzemeKullanims.ToArray();

                foreach (DataGridViewRow dr in dtgGramaj.Rows)
                {
                    tazePeynir2GramajKontrol = new TazePeynir2GramajKontrol();
                    tazePeynir2GramajKontrol.UrunCesidi = dr.Cells["Ürün Çeşidi"].Value == DBNull.Value ? "" : dr.Cells["Ürün Çeşidi"].Value.ToString();
                    tazePeynir2GramajKontrol.PartiNo = dr.Cells["Parti No"].Value == DBNull.Value ? "" : dr.Cells["Parti No"].Value.ToString();
                    tazePeynir2GramajKontrol.BirinciOrnek = dr.Cells["1.Örnek"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["1.Örnek"].Value);
                    tazePeynir2GramajKontrol.IkinciOrnek = dr.Cells["2.Örnek"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["2.Örnek"].Value);
                    tazePeynir2GramajKontrol.UcuncuOrnek = dr.Cells["3.Örnek"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["3.Örnek"].Value);
                    tazePeynir2GramajKontrol.DorduncuOrnek = dr.Cells["4.Örnek"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["4.Örnek"].Value);
                    tazePeynir2GramajKontrol.BesinciOrnek = dr.Cells["5.Örnek"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["5.Örnek"].Value);
                    tazePeynir2GramajKontrol.AltinciOrnek = dr.Cells["6.Örnek"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["6.Örnek"].Value);
                    tazePeynir2GramajKontrol.YedinciOrnek = dr.Cells["7.Örnek"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["7.Örnek"].Value);

                    tazePeynir2GramajKontrols.Add(tazePeynir2GramajKontrol);
                }

                nesne.tazePeynir2GramajKontrols = tazePeynir2GramajKontrols.ToArray();

                var resp = client.AddOrUpdateTazePeynirProsesAnalizTakip2(nesne, Giris.dbName, Giris.mKodValue);

                string mesaj = resp.Description;

                nesne = new TazePeynirTakipAnaliz2();

                foreach (DataGridViewRow dr in dtgDinlendirmeVePaket.Rows)
                {
                    tazePeynir2DinlendirmeVePaketleme = new TazePeynir2DinlendirmeVePaketleme();

                    tazePeynir2DinlendirmeVePaketleme.UretimTarihi = DateTime.Now.ToString("yyyyMMdd");
                    tazePeynir2DinlendirmeVePaketleme.AlanAdi = dr.Cells["Alan Adı"].Value == DBNull.Value ? "" : dr.Cells["Alan Adı"].Value.ToString();
                    tazePeynir2DinlendirmeVePaketleme.SifirSekizSicaklik = dr.Cells["08:00 Sıcaklık"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["08:00 Sıcaklık"].Value);
                    tazePeynir2DinlendirmeVePaketleme.SifirSekizNem = dr.Cells["08:00 Nem"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["08:00 Nem"].Value);
                    tazePeynir2DinlendirmeVePaketleme.OnikiSicaklik = dr.Cells["12:00 Sıcaklık"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["12:00 Sıcaklık"].Value);
                    tazePeynir2DinlendirmeVePaketleme.OnikiNem = dr.Cells["12:00 Nem"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["12:00 Nem"].Value);
                    tazePeynir2DinlendirmeVePaketleme.OnBesSicaklik = dr.Cells["15:00 Sıcaklık"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["15:00 Sıcaklık"].Value);
                    tazePeynir2DinlendirmeVePaketleme.OnBesNem = dr.Cells["15:00 Nem"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["15:00 Nem"].Value);
                    tazePeynir2DinlendirmeVePaketleme.OnSekizSicaklik = dr.Cells["18:00 Sıcaklık"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["18:00 Sıcaklık"].Value);
                    tazePeynir2DinlendirmeVePaketleme.OnSekizNem = dr.Cells["18:00 Nem"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["18:00 Nem"].Value);

                    tazePeynir2DinlendirmeVePaketlemes.Add(tazePeynir2DinlendirmeVePaketleme);
                }

                resp = client.AddOrUpdateTazePeynirKurutmaVePaketlemeOdasi(tazePeynir2DinlendirmeVePaketlemes.ToArray(), Giris.dbName, Giris.mKodValue);

                mesaj += Environment.NewLine;
                mesaj += "Dinlenme ve Paketleme Odası Sıcaklık ve Nem Takip" + resp.Description;

                CustomMsgBtn.Show(resp.Description, "UYARI", "TAMAM");
                if (resp.Value == 0)
                {
                    btnOzetEkraniDon.PerformClick();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AciklamaGirisi aciklama = new AciklamaGirisi(txtAciklama, txtAciklama.Text, initialWidth, initialHeight);
            aciklama.ShowDialog();
        }
    }
}