using AIF.UVT.DatabaseLayer;
using AIF.UVT.UVTService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIF.UVT
{
    public partial class PastorizasyonProsesTakip_1 : Form
    {
        //font start
        public int initialWidth;

        public int initialHeight;
        public float initialFontSize;
        //font end

        public PastorizasyonProsesTakip_1(string _type, string _kullaniciid, string _UretimFisNo, string _PartiNo, string _UrunTanimi, string _istasyon, int _row, string _tarih1)
        {
            InitializeComponent();
            //font start
            AutoScaleMode = AutoScaleMode.None;

            initialWidth = Width;
            initialHeight = Height;

            initialFontSize = label1.Font.Size;
            label1.Resize += Form_Resize;
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

            button2.Font = new Font(button2.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button2.Font.Style);

            button3.Font = new Font(button3.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button3.Font.Style);

            button4.Font = new Font(button4.Font.FontFamily, initialFontSize *
              (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
              button4.Font.Style);

            button5.Font = new Font(button5.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button5.Font.Style);

            btnOzetEkranaDon.Font = new Font(btnOzetEkranaDon.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               btnOzetEkranaDon.Font.Style);

            btnOnayla.Font = new Font(btnOnayla.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               btnOnayla.Font.Style);
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

        private void PastorizasyonProsesTakip_1_Load(object sender, EventArgs e)
        {
            dtgProsesOzellikleri1Load();
            dtgProsesOzellikleri2Load();
            dtgProsesOzellikleri3Load();
            dtgGunlukOzet1Load();
            dtgGunlukOzet2Load();
        }

        private string UretimFisNo = "";
        private string partiNo = "";
        private string istasyon = "";
        private string UrunTanimi = "";
        private string type = "";
        private string kullaniciid = "";
        private int row = 0;
        private SqlCommand cmd = null;
        private string tarih1 = "";

        private void dtgProsesOzellikleri1Load()
        {
            string sql = "SELECT T1.\"U_SutunAlinanTankAdi\" as \"Sütün Alındığı Tank Adı\",T1.\"U_AlinanSutunPartiNo\" as \"Alınan Sütün Parti Numarası\",T1.\"U_YagCekilecekSutMik\" as \"Yağ Çekilecek Süt Miktarı\",T1.\"U_SutYagOrani\" as \"Süt Yağ Oranı\",T1.\"U_SutunPh\" as \"Süt PH Değeri\", T1.\"U_KremaYogunlugu\" as \"Krema Yoğunluğu\",T1.\"U_KremaYagOrani\" as \"Krema Yağ Oranı\",T1.\"U_KremaPh\" as \"Krema PH Değeri\",T1.\"U_CekilenKremaMikKG\" as \"Çekilen Krema Miktarı KG\",T1.\"U_CekilenKremaMikLT\" as \"Çekilen Krema Miktarı LT\",T1.\"U_KalanSutMik\" as \"Krema Çekildikten Sonra Kalan Süt Miktarı\",T1.\"U_YagAlnmsSutYagOr\" as \"Yağı Alınmış Sütün Yağ Oranı\",T1.\"U_YagAlnmsSutPH\" as \"Yağı Alınmış Sütün PH Değeri\",T1.\"U_SutunGondTankAdi\" as \"Sütün Gönderildiği Tank Adı\"  FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);

            //Commit
            dtgProsesOzellikleri1.DataSource = dt;

            dtgProsesOzellikleri1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgProsesOzellikleri1.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgProsesOzellikleri1.EnableHeadersVisualStyles = false;
            dtgProsesOzellikleri1.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            if (dt.Rows.Count == 0)
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                DataRow dr = dt.NewRow();
                dr["Yağ Çekilecek Süt Miktarı"] = Convert.ToString("0", cultureTR);
                dr["Süt Yağ Oranı"] = Convert.ToString("0", cultureTR);
                dr["Süt PH Değeri"] = Convert.ToString("0", cultureTR);
                dr["Krema Yoğunluğu"] = Convert.ToString("0", cultureTR);
                dr["Krema Yağ Oranı"] = Convert.ToString("0", cultureTR);
                dr["Krema PH Değeri"] = Convert.ToString("0", cultureTR);
                dr["Çekilen Krema Miktarı KG"] = Convert.ToString("0", cultureTR);
                dr["Çekilen Krema Miktarı LT"] = Convert.ToString("0", cultureTR);
                dr["Krema Çekildikten Sonra Kalan Süt Miktarı"] = Convert.ToString("0", cultureTR);
                dr["Yağı Alınmış Sütün Yağ Oranı"] = Convert.ToString("0", cultureTR);
                dr["Yağı Alınmış Sütün PH Değeri"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);
            }

            dtgProsesOzellikleri1.Columns["Yağ Çekilecek Süt Miktarı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Yağ Çekilecek Süt Miktarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Süt Yağ Oranı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Süt Yağ Oranı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Süt PH Değeri"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Süt PH Değeri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Krema Yoğunluğu"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Krema Yoğunluğu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Krema Yağ Oranı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Krema Yağ Oranı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Krema PH Değeri"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Krema PH Değeri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Çekilen Krema Miktarı KG"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Çekilen Krema Miktarı KG"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Çekilen Krema Miktarı LT"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Çekilen Krema Miktarı LT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Krema Çekildikten Sonra Kalan Süt Miktarı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Krema Çekildikten Sonra Kalan Süt Miktarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Yağı Alınmış Sütün Yağ Oranı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Yağı Alınmış Sütün Yağ Oranı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri1.Columns["Yağı Alınmış Sütün PH Değeri"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri1.Columns["Yağı Alınmış Sütün PH Değeri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dtgProsesOzellikleri1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgProsesOzellikleri1.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgProsesOzellikleri1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //dtgProsesOzellikleri1.Rows[0].Height = dtgProsesOzellikleri1.Height - dtgProsesOzellikleri1.ColumnHeadersHeight;
            //dtgProsesOzellikleri1.AutoResizeColumns();
        }

        private void dtgProsesOzellikleri2Load()
        {
            string sql = "SELECT T1.\"U_UretilenKremaParti\" as \"Üretilen Krema Parti Numarası\",T1.\"U_KremaPastBasSaat\" as \"Krema Pastörizasyon Baş.Saat\",T1.\"U_KremaPastSicakligi\" as \"Krema Past.Sıcaklığı\",T1.\"U_KremaPastBitSaat\" as \"Krema Pastörizasyon Bit.Saat\",T1.\"U_KremaMayalamaSaati\" as \"Krema Mayalama Saati\", T1.\"U_KremaMayalamaSicak\" as \"Krema Mayalama Sıcaklığı\",T1.\"U_KremaMayalamaPh\" as \"Krema Mayalama PH\",T1.\"U_MayalamaKazanFiltTem\" as \"Mayalama Kazanı Filtre Temizliği\",T1.\"U_KremaDolumBasSaat\" as \"Krema Dolum Baş.Saat\",T1.\"U_KremaDolumBitSaat\" as \"Krema Dolum Bit.Saat\",T1.\"U_KremaDolumYapan\" as \"Krema Dolumu Yapan Personel\",T1.\"U_DolumSicakligi\" as \"Dolum Sıcaklığı\",T1.\"U_UretimYapan\" as \"Üretim Yapan Operatör\",T1.\"U_KontrolEdenMuh\" as \"Kontrol Eden Mühendis\" FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ2\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);

            //Commit
            dtgProsesOzellikleri2.DataSource = dt;

            dtgProsesOzellikleri2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgProsesOzellikleri2.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgProsesOzellikleri2.EnableHeadersVisualStyles = false;
            dtgProsesOzellikleri2.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            if (dt.Rows.Count == 0)
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                DataRow dr = dt.NewRow();
                dr["Krema Past.Sıcaklığı"] = Convert.ToString("0", cultureTR);
                dr["Krema Mayalama Sıcaklığı"] = Convert.ToString("0", cultureTR);
                dr["Krema Mayalama PH"] = Convert.ToString("0", cultureTR);
                dr["Dolum Sıcaklığı"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);
            }

            dtgProsesOzellikleri2.Columns["Krema Past.Sıcaklığı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri2.Columns["Krema Past.Sıcaklığı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri2.Columns["Krema Mayalama Sıcaklığı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri2.Columns["Krema Mayalama Sıcaklığı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri2.Columns["Dolum Sıcaklığı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri2.Columns["Dolum Sıcaklığı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri2.Columns["Krema Mayalama PH"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri2.Columns["Krema Mayalama PH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dtgProsesOzellikleri2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgProsesOzellikleri2.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgProsesOzellikleri2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //dtgProsesOzellikleri1.Rows[0].Height = dtgProsesOzellikleri1.Height - dtgProsesOzellikleri1.ColumnHeadersHeight;
            //dtgProsesOzellikleri1.AutoResizeColumns();

            dtgProsesOzellikleri2.Columns["Krema Pastörizasyon Baş.Saat"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Krema Pastörizasyon Bit.Saat"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Krema Mayalama Saati"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Krema Mayalama Sıcaklığı"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Krema Mayalama PH"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Mayalama Kazanı Filtre Temizliği"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Krema Dolum Baş.Saat"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Krema Dolum Bit.Saat"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Krema Dolumu Yapan Personel"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Dolum Sıcaklığı"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Üretim Yapan Operatör"].ReadOnly = true;
            dtgProsesOzellikleri2.Columns["Kontrol Eden Mühendis"].ReadOnly = true;
        }

        private void dtgProsesOzellikleri3Load()
        {
            string sql = "SELECT T1.\"U_UretilenKremaParti\" as \"Üretilen Krema Parti Numarası\",T1.\"U_KulKulturAdiVeKodu\" as \"Kullanılan Kültür Adı Ve Kodu\",T1.\"U_KulturMiktari\" as \"Kullanılan Kültür Miktarı\",T1.\"U_UretilenKremaMik\" as \"Üretilen Krema Miktarı\",T1.\"U_KremaYagOrani\" as \"Kremanın Yağ Oranı\",T1.\"U_DolumYapilanAmbalaj\" as \"Dolum Yapılan Ambalaj\",T1.\"U_KulAmbalajMik\" as \"Kullanılan Ambalaj Miktarı\",T1.\"U_BirAmbOrtMiktar\" as \"1 Ambalajla Doldurulan Ortalama Krema Miktarı\",T1.\"U_KremaDepoPh\" as \"Krema Depoya Atıldığında PH\",T1.\"U_KremaninDepoSicakligi\" as \"Kremanın Atıldığı Depo Sıcaklığı\",T1.\"U_UretimYapan\" as \"Üretim Yapan Operatör\",T1.\"U_KontrolEdenMuh\" as \"Kontrol Eden Mühendis\" FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ3\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);

            //Commit
            dtgProsesOzellikleri3.DataSource = dt;

            dtgProsesOzellikleri3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgProsesOzellikleri3.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgProsesOzellikleri3.EnableHeadersVisualStyles = false;
            dtgProsesOzellikleri3.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            if (dt.Rows.Count == 0)
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                DataRow dr = dt.NewRow();
                dr["Üretilen Krema Miktarı"] = Convert.ToString("0", cultureTR);
                dr["Kremanın Yağ Oranı"] = Convert.ToString("0", cultureTR);
                dr["Kullanılan Ambalaj Miktarı"] = Convert.ToString("0", cultureTR);
                dr["Kullanılan Kültür Miktarı"] = Convert.ToString("0", cultureTR);
                dr["1 Ambalajla Doldurulan Ortalama Krema Miktarı"] = Convert.ToString("0", cultureTR);
                dr["Krema Depoya Atıldığında PH"] = Convert.ToString("0", cultureTR);
                dr["Kremanın Atıldığı Depo Sıcaklığı"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);
            }

            dtgProsesOzellikleri3.Columns["Üretilen Krema Miktarı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri3.Columns["Üretilen Krema Miktarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri3.Columns["Kremanın Yağ Oranı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri3.Columns["Kremanın Yağ Oranı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri3.Columns["Kullanılan Ambalaj Miktarı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri3.Columns["Kullanılan Ambalaj Miktarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri3.Columns["Kullanılan Kültür Miktarı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri3.Columns["Kullanılan Kültür Miktarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri3.Columns["1 Ambalajla Doldurulan Ortalama Krema Miktarı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri3.Columns["1 Ambalajla Doldurulan Ortalama Krema Miktarı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri3.Columns["Krema Depoya Atıldığında PH"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri3.Columns["Krema Depoya Atıldığında PH"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgProsesOzellikleri3.Columns["Kremanın Atıldığı Depo Sıcaklığı"].DefaultCellStyle.Format = "N2";
            dtgProsesOzellikleri3.Columns["Kremanın Atıldığı Depo Sıcaklığı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dtgProsesOzellikleri3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgProsesOzellikleri3.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgProsesOzellikleri3.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //dtgProsesOzellikleri1.Rows[0].Height = dtgProsesOzellikleri1.Height - dtgProsesOzellikleri1.ColumnHeadersHeight;
            //dtgProsesOzellikleri1.AutoResizeColumns();

            dtgProsesOzellikleri3.Columns["Kullanılan Kültür Adı Ve Kodu"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Kullanılan Kültür Miktarı"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Üretilen Krema Miktarı"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Kremanın Yağ Oranı"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Dolum Yapılan Ambalaj"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Kullanılan Ambalaj Miktarı"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["1 Ambalajla Doldurulan Ortalama Krema Miktarı"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Krema Depoya Atıldığında PH"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Kremanın Atıldığı Depo Sıcaklığı"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Üretim Yapan Operatör"].ReadOnly = true;
            dtgProsesOzellikleri3.Columns["Kontrol Eden Mühendis"].ReadOnly = true;
        }

        private void dtgGunlukOzet1Load()
        {
            string sql = "SELECT T1.\"U_VeriAdi\" as \"Veri Adı\",T1.\"U_Kova\" as \"Kova\",T1.\"U_Teneke\" as \"Teneke\",T1.\"U_ToplamveyaOrt\" as \"Toplam Veya Ortalama\" FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ4\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);

            //Commit
            dtgGunlukOzet1.DataSource = dt;

            dtgGunlukOzet1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgGunlukOzet1.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgGunlukOzet1.EnableHeadersVisualStyles = false;
            dtgGunlukOzet1.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            if (dt.Rows.Count == 0)
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                DataRow dr = dt.NewRow();
                dr["Veri Adı"] = "Çekilen Krema Miktarı KG";
                dr["Kova"] = Convert.ToString("0", cultureTR);
                dr["Teneke"] = Convert.ToString("0", cultureTR);
                dr["Toplam Veya Ortalama"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "Kullanılan Ambalaj Miktarı(Adet)";
                dr["Kova"] = Convert.ToString("0", cultureTR);
                dr["Teneke"] = Convert.ToString("0", cultureTR);
                dr["Toplam Veya Ortalama"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "1 Ambalajla Doldurulan Ortalama";
                dr["Kova"] = Convert.ToString("0", cultureTR);
                dr["Teneke"] = Convert.ToString("0", cultureTR);
                dr["Toplam Veya Ortalama"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "Krema Ortalama Yağ Oranı";
                dr["Kova"] = Convert.ToString("0", cultureTR);
                dr["Teneke"] = Convert.ToString("0", cultureTR);
                dr["Toplam Veya Ortalama"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "Krema Ortalama PH Değeri";
                dr["Kova"] = Convert.ToString("0", cultureTR);
                dr["Teneke"] = Convert.ToString("0", cultureTR);
                dr["Toplam Veya Ortalama"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);
            }

            dtgGunlukOzet1.Columns["Kova"].DefaultCellStyle.Format = "N2";
            dtgGunlukOzet1.Columns["Kova"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgGunlukOzet1.Columns["Teneke"].DefaultCellStyle.Format = "N2";
            dtgGunlukOzet1.Columns["Teneke"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgGunlukOzet1.Columns["Toplam Veya Ortalama"].DefaultCellStyle.Format = "N2";
            dtgGunlukOzet1.Columns["Toplam Veya Ortalama"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dtgGunlukOzet1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgGunlukOzet1.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgGunlukOzet1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //dtgProsesOzellikleri1.Rows[0].Height = dtgProsesOzellikleri1.Height - dtgProsesOzellikleri1.ColumnHeadersHeight;
            //dtgProsesOzellikleri1.AutoResizeColumns();
        }

        private void dtgGunlukOzet2Load()
        {
            string sql = "SELECT T1.\"U_VeriAdi\" as \"Veri Adı\",T1.\"U_Deger\" as \"Değer\" FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ5\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";
            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            sda.Fill(dt);

            //Commit
            dtgGunlukOzet2.DataSource = dt;

            dtgGunlukOzet2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgGunlukOzet2.ColumnHeadersDefaultCellStyle.BackColor = Color.IndianRed;
            dtgGunlukOzet2.EnableHeadersVisualStyles = false;
            dtgGunlukOzet2.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            if (dt.Rows.Count == 0)
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");

                DataRow dr = dt.NewRow();
                dr["Veri Adı"] = "Çekilmesi Gereken Toplam Krema Miktarı KG";
                dr["Değer"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "Çekilen Krema Miktarı KG";
                dr["Değer"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "Eksik Çekilen Krema Miktarı KG";
                dr["Değer"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "Üretime Verilen Krema Miktarı KG";
                dr["Değer"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Veri Adı"] = "Depoya Giren Net Krema Miktarı KG";
                dr["Değer"] = Convert.ToString("0", cultureTR);

                dt.Rows.Add(dr);
            }

            dtgGunlukOzet2.Columns["Değer"].DefaultCellStyle.Format = "N2";
            dtgGunlukOzet2.Columns["Değer"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dtgGunlukOzet2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgGunlukOzet2.AutoResizeRows();
            //dtgProsesOzellikleri1.AutoResizeColumns();

            //dtgProsesOzellikleri1.Columns["Görevli Operatör"].Visible = false;

            foreach (DataGridViewColumn column in dtgGunlukOzet2.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //dtgProsesOzellikleri1.Rows[0].Height = dtgProsesOzellikleri1.Height - dtgProsesOzellikleri1.ColumnHeadersHeight;
            //dtgProsesOzellikleri1.AutoResizeColumns();
        }

        private void dtgProsesOzellikleri1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql = "SELECT T1.\"U_SutunAlinanTankAdi\" as \"Sütün Alındığı Tank Adı\",T1.\"U_AlinanSutunPartiNo\" as \"Alınan Sütün Parti Numarası\",,T1.\"U_SutunGondTankAdi\" as \"Sütün Gönderildiği Tank Adı\"  FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";

            //if (dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Pas Gönderim Başlangıç Saati" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Pas Gönderim Bitiş Saati" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Pasın 80 Dereceye Gelme Saati" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Pasın 88 Dereceye Gelme Saati" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Pasın Tanktan Boşaltılma Başlangıç Saati" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Pasın Tanktan Boşaltılma Bitiş Saati" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Baskı Başlangıç Saati" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Baskı Bitiş Saati")
            //{
            //    //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, true);
            //    //n.ShowDialog();

            //    SaatTarihGirisi n = new SaatTarihGirisi(dtgProsesOzellikleri1);
            //    n.ShowDialog();

            //}
            if (dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Yağ Çekilecek Süt Miktarı" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Süt Yağ Oranı" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Süt PH Değeri" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Krema Yoğunluğu" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Krema Yağ Oranı" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Krema PH Değeri" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Çekilen Krema Miktarı KG" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Çekilen Krema Miktarı LT" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Krema Çekildikten Sonra Kalan Süt Miktarı" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Yağı Alınmış Sütün Yağ Oranı" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Yağı Alınmış Sütün PH Değeri")
            {
                //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, false);
                SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, false);
                n.ShowDialog();
            }
            else if (dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Sütün Alındığı Tank Adı" || dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Sütün Gönderildiği Tank Adı")
            {
                string sql1 = "Select '' as \"Kod\",'' as \"Süt Depoları\" ";
                sql1 += " UNION ALL ";
                sql1 += "Select \"WhsCode\" as \"Kod\",\"WhsName\" as \"Süt Depoları\" from OWHS where \"U_DepoTipi\" = '01' ";

                SelectList selectList = new SelectList(sql1, dtgProsesOzellikleri1, -1, e.ColumnIndex, _autoresizerow: false);
                selectList.ShowDialog();
            }
            //else if (dtgProsesOzellikleri1.Columns[e.ColumnIndex].Name == "Lor Türü")
            //{
            //    string sql1 = "Select '0' as \"Kod\",'Yağlı' as \"Aciklama\" ";
            //    sql1 += " UNION ALL ";
            //    sql1 += "Select '1' as \"Kod\",'Yağsız' as \"Aciklama\" ";

            //    SelectList selectList = new SelectList(sql1, dtgProsesOzellikleri1, -1, 1, _autoresizerow: false);
            //    selectList.ShowDialog();

            //}
        }

        private void dtgProsesOzellikleri2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Past.Sıcaklığı" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Mayalama Sıcaklığı" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Mayalama PH" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Dolum Sıcaklığı")
            {
                //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, false);
                SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri2, false);
                n.ShowDialog();
            }
            else if (dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Pastörizasyon Baş.Saat" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Pastörizasyon Bit.Saat" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Mayalama Saati" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Dolum Bit.Saat" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Dolum Baş.Saat")
            {
                //    //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, true);
                //    //n.ShowDialog();

                SaatTarihGirisi n = new SaatTarihGirisi(dtgProsesOzellikleri2);
                n.ShowDialog();
            }
            else if (dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Mayalama Kazanı Filtre Temizliği")
            {
                string sql1 = "Select '0' as \"Kod\",'Uygun Değil' as \"Aciklama\" ";
                sql1 += " UNION ALL ";
                sql1 += "Select '1' as \"Kod\",'Uygun' as \"Aciklama\" ";

                SelectList selectList = new SelectList(sql1, dtgProsesOzellikleri2, -1, e.ColumnIndex, _autoresizerow: false);
                selectList.ShowDialog();
            }
            else if (dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Üretim Yapan Operatör" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Kontrol Eden Mühendis" || dtgProsesOzellikleri2.Columns[e.ColumnIndex].Name == "Krema Dolumu Yapan Personel")
            {
                if (istasyon.StartsWith("IST"))
                {
                    DateTime dtTarih = new DateTime(Convert.ToInt32(tarih1.Substring(0, 4)), Convert.ToInt32(tarih1.Substring(4, 2)), Convert.ToInt32(tarih1.Substring(6, 2)));
                    string gunfield = "U_Gun" + dtTarih.Day;
                    string sql = "";

                    #region Günlük Personel Planlama 2 ekranı

                    //sql = "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = '" + type + "' or T1.\"U_Bolum2\" = '" + type + "' or T1.\"U_Bolum3\" = '" + type + "') and " + gunfield + " = 'X' ";

                    //if (AtanmisIsler.Joker)
                    //{
                    //    sql += " UNION ALL ";

                    //    sql += "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = 'JOKER' or T1.\"U_Bolum2\" = 'JOKER' or T1.\"U_Bolum3\" = 'JOKER') ";
                    //}

                    #endregion Günlük Personel Planlama 2 ekranı

                    #region Günlük Personel Planlama 3 ekranı

                    //sql = "SELECT \"U_PersonelNo\" AS \"Personel No\",\"U_PersonelAdi\" AS \"Personel Adı\" FROM \"@AIF_GUNLUKPLAN\" AS T0 INNER JOIN \"@AIF_GUNLUKPLAN1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" ";
                    //sql += "WHERE Convert(varchar,T0.U_Tarih,112) = '" + dtTarih.ToString("yyyyMMdd") + "' AND (T1.\"U_Bolum1\" = '" + type + "' or T1.\"U_Bolum2\" = '" + type + "' or T1.\"U_Bolum3\" = '" + type + "') and \"U_Durum\" = 'X'";

                    //if (AtanmisIsler.Joker)
                    //{
                    //    sql += " UNION ALL ";

                    //    sql = "SELECT \"U_PersonelNo\" AS \"Personel No\",\"U_PersonelAdi\" AS \"Personel Adı\" FROM \"@AIF_GUNLUKPLAN\" AS T0 INNER JOIN \"@AIF_GUNLUKPLAN1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" ";
                    //    sql += "WHERE Convert(varchar,T0.U_Tarih,112) = '" + dtTarih.ToString("yyyyMMdd") + "' AND (T1.\"U_Bolum1\" = 'JOKER' or T1.\"U_Bolum2\" = 'JOKER' or T1.\"U_Bolum3\" = 'JOKER') and \"U_Durum\" = 'X'";
                    //}

                    #endregion Günlük Personel Planlama 3 ekranı

                    #region Günlük Personel Planlama 4 ekranı
                    sql = "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = '" + type + "' or T1.\"U_Bolum2\" = '" + type + "' or T1.\"U_Bolum3\" = '" + type + "') and " + gunfield + " = 'X' ";

                    if (AtanmisIsler.Joker)
                    {
                        sql += " UNION ALL ";

                        sql += "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = 'JOKER' or T1.\"U_Bolum2\" = 'JOKER' or T1.\"U_Bolum3\" = 'JOKER') ";
                    }
                    #endregion Günlük Personel Planlama 4 ekranı
                    SelectList selectList = new SelectList(sql, dtgProsesOzellikleri2, -1, e.ColumnIndex, _autoresizerow: false);
                    selectList.ShowDialog();
                }
            }
        }

        private void dtgProsesOzellikleri3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kullanılan Kültür Miktarı" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Üretilen Krema Miktarı" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kremanın Yağ Oranı" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kullanılan Ambalaj Miktarı" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Krema Depoya Atıldığında PH" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kremanın Atıldığı Depo Sıcaklığı" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kremanın Atıldığı Depo Sıcaklığı")
            {
                //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, false);
                SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri3, false);
                n.ShowDialog();

                if (dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Üretilen Krema Miktarı" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kullanılan Ambalaj Miktarı")
                {
                    var UretilenKremaMiktari = dtgProsesOzellikleri3.Rows[e.RowIndex].Cells["Üretilen Krema Miktarı"].Value.ToString() == "" ? 0 : Convert.ToDouble(dtgProsesOzellikleri3.Rows[e.RowIndex].Cells["Üretilen Krema Miktarı"].Value);
                    var KullanilanAmbalajMiktari = dtgProsesOzellikleri3.Rows[e.RowIndex].Cells["Kullanılan Ambalaj Miktarı"].Value.ToString() == "" ? 0 : Convert.ToDouble(dtgProsesOzellikleri3.Rows[e.RowIndex].Cells["Kullanılan Ambalaj Miktarı"].Value);

                    if (UretilenKremaMiktari > 0 && KullanilanAmbalajMiktari > 0)
                    {
                        var sonuc = UretilenKremaMiktari / KullanilanAmbalajMiktari;
                        dtgProsesOzellikleri3.Rows[e.RowIndex].Cells["1 Ambalajla Doldurulan Ortalama Krema Miktarı"].Value = sonuc.ToString();
                    }
                    else
                    {
                        dtgProsesOzellikleri3.Rows[e.RowIndex].Cells["1 Ambalajla Doldurulan Ortalama Krema Miktarı"].Value = "0";
                    }
                }
            }
            else if (dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Krema Pastörizasyon Baş.Saat")
            {
                //    //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, true);
                //    //n.ShowDialog();

                SaatTarihGirisi n = new SaatTarihGirisi(dtgProsesOzellikleri3);
                n.ShowDialog();
            }
            else if (dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Üretim Yapan Operatör" || dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kontrol Eden Mühendis")
            {
                if (istasyon.StartsWith("IST"))
                {
                    //string sql = "Select \"empID\" as \"Kullanıcı Kodu\", (\"firstName\" + ' ' + \"lastName\") as 'Ad Soyad' from OHEM where \"Active\" = 'Y'";

                    string field = "U_" + istasyon;

                    DateTime dtTarih = new DateTime(Convert.ToInt32(tarih1.Substring(0, 4)), Convert.ToInt32(tarih1.Substring(4, 2)), Convert.ToInt32(tarih1.Substring(6, 2)));
                    string gunfield = "U_Gun" + dtTarih.Day;
                    string sql = "";

                    #region Günlük Personel Planlama 2 ekranı

                    //sql = "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = '" + type + "' or T1.\"U_Bolum2\" = '" + type + "' or T1.\"U_Bolum3\" = '" + type + "') and " + gunfield + " = 'X' ";

                    //if (AtanmisIsler.Joker)
                    //{
                    //    sql += " UNION ALL ";

                    //    sql += "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = 'JOKER' or T1.\"U_Bolum2\" = 'JOKER' or T1.\"U_Bolum3\" = 'JOKER') ";
                    //}

                    #endregion Günlük Personel Planlama 2 ekranı

                    #region Günlük Personel Planlama 3 ekranı

                    //sql = "SELECT \"U_PersonelNo\" AS \"Personel No\",\"U_PersonelAdi\" AS \"Personel Adı\" FROM \"@AIF_GUNLUKPLAN\" AS T0 INNER JOIN \"@AIF_GUNLUKPLAN1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" ";
                    //sql += "WHERE Convert(varchar,T0.U_Tarih,112) = '" + dtTarih.ToString("yyyyMMdd") + "' AND (T1.\"U_Bolum1\" = '" + type + "' or T1.\"U_Bolum2\" = '" + type + "' or T1.\"U_Bolum3\" = '" + type + "') and \"U_Durum\" = 'X'";

                    //if (AtanmisIsler.Joker)
                    //{
                    //    sql += " UNION ALL ";

                    //    sql = "SELECT \"U_PersonelNo\" AS \"Personel No\",\"U_PersonelAdi\" AS \"Personel Adı\" FROM \"@AIF_GUNLUKPLAN\" AS T0 INNER JOIN \"@AIF_GUNLUKPLAN1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" ";
                    //    sql += "WHERE Convert(varchar,T0.U_Tarih,112) = '" + dtTarih.ToString("yyyyMMdd") + "' AND (T1.\"U_Bolum1\" = 'JOKER' or T1.\"U_Bolum2\" = 'JOKER' or T1.\"U_Bolum3\" = 'JOKER') and \"U_Durum\" = 'X'";
                    //}

                    #endregion Günlük Personel Planlama 3 ekranı

                    #region Günlük Personel Planlama 4 ekranı
                    sql = "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = '" + type + "' or T1.\"U_Bolum2\" = '" + type + "' or T1.\"U_Bolum3\" = '" + type + "') and " + gunfield + " = 'X' ";

                    if (AtanmisIsler.Joker)
                    {
                        sql += " UNION ALL ";

                        sql += "Select \"U_PersonelNo\" as \"Personel No\", \"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNLUKPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Aylar\" = '" + dtTarih.Month.ToString().PadLeft(2, '0') + "' and T0.\"U_Yil\" = '" + dtTarih.Year.ToString() + "' and (T1.\"U_Bolum1\" = 'JOKER' or T1.\"U_Bolum2\" = 'JOKER' or T1.\"U_Bolum3\" = 'JOKER') ";
                    }
                    #endregion Günlük Personel Planlama 4 ekranı

                    //string sql = "Select \"U_PersonelNo\" as \"Personel No\",\"U_PersonelAdi\" as \"Personel Adı\" from \"@AIF_GUNPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where \"U_Tarih\" = '" + DateTime.Now.ToString("yyyyMMdd") + "' and \"" + field + "\" = 'Y'";

                    SelectList selectList = new SelectList(sql, dtgProsesOzellikleri3, -1, e.ColumnIndex, _autoresizerow: false);
                    selectList.ShowDialog();

                    //dtgProsesOzellikleri1.AutoResizeRows();
                }
            }
            else if (dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Kullanılan Kültür Adı Ve Kodu")
            {
                string sql1 = "Select '0' as \"Kod\",'M01' as \"Kod\" ";
                sql1 += " UNION ALL ";
                sql1 += "Select '1' as \"Kod\",'M03' as \"Kod\" ";

                SelectList selectList = new SelectList(sql1, dtgProsesOzellikleri3, -1, e.ColumnIndex, _autoresizerow: false);
                selectList.ShowDialog();
            }
            else if (dtgProsesOzellikleri3.Columns[e.ColumnIndex].Name == "Dolum Yapılan Ambalaj")
            {
                string sql1 = "Select '0' as \"Kod\",'Teneke' as \"Tür\" ";
                sql1 += " UNION ALL ";
                sql1 += "Select '1' as \"Kod\",'Kova' as \"Tür\" ";

                SelectList selectList = new SelectList(sql1, dtgProsesOzellikleri3, -1, e.ColumnIndex, _autoresizerow: false);
                selectList.ShowDialog();
            }
        }

        private void dtgGunlukOzet1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgGunlukOzet1.Columns[e.ColumnIndex].Name == "Kova" || dtgGunlukOzet1.Columns[e.ColumnIndex].Name == "Teneke" || dtgGunlukOzet1.Columns[e.ColumnIndex].Name == "Toplam Veya Ortalama")
            {
                //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, false);
                SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgGunlukOzet1, false);
                n.ShowDialog();
            }
        }

        private void dtgGunlukOzet2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgGunlukOzet2.Columns[e.ColumnIndex].Name == "Değer")
            {
                //SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgProsesOzellikleri1, false);
                SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgGunlukOzet2, false);
                n.ShowDialog();
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            UVTServiceSoapClient client = new UVTServiceSoapClient();
            PastorizasyonProsesTakipAnaliz nesne = new PastorizasyonProsesTakipAnaliz();
            PastorizasyonProsesOzellikleri1 pastorizasyonProsesOzellikleri1 = new PastorizasyonProsesOzellikleri1();
            List<PastorizasyonProsesOzellikleri1> pastorizasyonProsesOzellikleri1s = new List<PastorizasyonProsesOzellikleri1>();
            PastorizasyonProsesOzellikleri2 pastorizasyonProsesOzellikleri2 = new PastorizasyonProsesOzellikleri2();
            List<PastorizasyonProsesOzellikleri2> pastorizasyonProsesOzellikleri2s = new List<PastorizasyonProsesOzellikleri2>();
            PastorizasyonProsesOzellikleri3 pastorizasyonProsesOzellikleri3 = new PastorizasyonProsesOzellikleri3();
            List<PastorizasyonProsesOzellikleri3> pastorizasyonProsesOzellikleri3s = new List<PastorizasyonProsesOzellikleri3>();
            PastorizasyonGunluk pastorizasyonGunluk1 = new PastorizasyonGunluk();
            List<PastorizasyonGunluk> pastorizasyonGunluk1s = new List<PastorizasyonGunluk>();
            PastorizasyonGunluk2 pastorizasyonGunluk2 = new PastorizasyonGunluk2();
            List<PastorizasyonGunluk2> pastorizasyonGunluk2s = new List<PastorizasyonGunluk2>();

            nesne.PartiNo = txtPartyNo.Text;
            nesne.UretimSiparisNo = txtUretimSiparisNo.Text;
            nesne.UretilenUrunTanimi = txtUrunTanimi.Text;

            foreach (DataGridViewRow dr in dtgProsesOzellikleri1.Rows)
            {
                pastorizasyonProsesOzellikleri1 = new PastorizasyonProsesOzellikleri1();
                pastorizasyonProsesOzellikleri1.SutunAlinanTankAdi = dr.Cells["Sütün Alındığı Tank Adı"].Value == DBNull.Value ? "" : dr.Cells["Sütün Alındığı Tank Adı"].Value.ToString();
                pastorizasyonProsesOzellikleri1.AlinanSutunPartiNo = dr.Cells["Alınan Sütün Parti Numarası"].Value == DBNull.Value ? "" : dr.Cells["Alınan Sütün Parti Numarası"].Value.ToString();
                pastorizasyonProsesOzellikleri1.YagCekilecekSutMik = dr.Cells["Yağ Çekilecek Süt Miktarı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Yağ Çekilecek Süt Miktarı"].Value);
                pastorizasyonProsesOzellikleri1.SutYagOrani = dr.Cells["Süt Yağ Oranı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Süt Yağ Oranı"].Value);
                pastorizasyonProsesOzellikleri1.SutunPh = dr.Cells["Süt PH Değeri"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Süt PH Değeri"].Value);
                pastorizasyonProsesOzellikleri1.KremaYogunlugu = dr.Cells["Krema Yoğunluğu"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema Yoğunluğu"].Value);
                pastorizasyonProsesOzellikleri1.KremaYagOrani = dr.Cells["Krema Yağ Oranı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema Yağ Oranı"].Value);
                pastorizasyonProsesOzellikleri1.KremaPh = dr.Cells["Krema PH Değeri"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema PH Değeri"].Value);
                pastorizasyonProsesOzellikleri1.CekilenKremaMikKG = dr.Cells["Çekilen Krema Miktarı KG"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Çekilen Krema Miktarı KG"].Value);
                pastorizasyonProsesOzellikleri1.CekilenKremaMikLT = dr.Cells["Çekilen Krema Miktarı LT"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Çekilen Krema Miktarı LT"].Value);
                pastorizasyonProsesOzellikleri1.KalanSutMik = dr.Cells["Krema Çekildikten Sonra Kalan Süt Miktarı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema Çekildikten Sonra Kalan Süt Miktarı"].Value);
                pastorizasyonProsesOzellikleri1.YagAlnmsSutYagOr = dr.Cells["Yağı Alınmış Sütün Yağ Oranı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Yağı Alınmış Sütün Yağ Oranı"].Value);
                pastorizasyonProsesOzellikleri1.YagAlnmsSutPH = dr.Cells["Yağı Alınmış Sütün PH Değeri"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Yağı Alınmış Sütün PH Değeri"].Value);
                pastorizasyonProsesOzellikleri1.SutunGondTankAdi = dr.Cells["Sütün Gönderildiği Tank Adı"].Value == DBNull.Value ? "" : dr.Cells["Sütün Gönderildiği Tank Adı"].Value.ToString();

                pastorizasyonProsesOzellikleri1s.Add(pastorizasyonProsesOzellikleri1);
            }

            nesne.PastorizasyonProsesOzellikleri1s = pastorizasyonProsesOzellikleri1s.ToArray();

            foreach (DataGridViewRow dr in dtgProsesOzellikleri2.Rows)
            {
                pastorizasyonProsesOzellikleri2 = new PastorizasyonProsesOzellikleri2();
                pastorizasyonProsesOzellikleri2.UretilenKremaParti = dr.Cells["Üretilen Krema Parti Numarası"].Value == DBNull.Value ? "" : dr.Cells["Üretilen Krema Parti Numarası"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KremaPastBasSaat = dr.Cells["Krema Pastörizasyon Baş.Saat"].Value == DBNull.Value ? "" : dr.Cells["Krema Pastörizasyon Baş.Saat"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KremaPastSicakligi = dr.Cells["Krema Past.Sıcaklığı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema Past.Sıcaklığı"].Value);
                pastorizasyonProsesOzellikleri2.KremaPastBitSaat = dr.Cells["Krema Pastörizasyon Bit.Saat"].Value == DBNull.Value ? "" : dr.Cells["Krema Pastörizasyon Bit.Saat"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KremaMayalamaSaati = dr.Cells["Krema Mayalama Saati"].Value == DBNull.Value ? "" : dr.Cells["Krema Mayalama Saati"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KremaMayalamaSicak = dr.Cells["Krema Mayalama Sıcaklığı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema Mayalama Sıcaklığı"].Value);
                pastorizasyonProsesOzellikleri2.KremaMayalamaPh = dr.Cells["Krema Mayalama PH"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema Mayalama PH"].Value);
                pastorizasyonProsesOzellikleri2.MayalamaKazanFiltTem = dr.Cells["Mayalama Kazanı Filtre Temizliği"].Value == DBNull.Value ? "" : dr.Cells["Mayalama Kazanı Filtre Temizliği"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KremaDolumBasSaat = dr.Cells["Krema Dolum Baş.Saat"].Value == DBNull.Value ? "" : dr.Cells["Krema Dolum Baş.Saat"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KremaDolumBitSaat = dr.Cells["Krema Dolum Bit.Saat"].Value == DBNull.Value ? "" : dr.Cells["Krema Dolum Bit.Saat"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KremaDolumYapan = dr.Cells["Krema Dolumu Yapan Personel"].Value == DBNull.Value ? "" : dr.Cells["Krema Dolumu Yapan Personel"].Value.ToString();
                pastorizasyonProsesOzellikleri2.DolumSicakligi = dr.Cells["Dolum Sıcaklığı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Dolum Sıcaklığı"].Value);
                pastorizasyonProsesOzellikleri2.UretimYapan = dr.Cells["Üretim Yapan Operatör"].Value == DBNull.Value ? "" : dr.Cells["Üretim Yapan Operatör"].Value.ToString();
                pastorizasyonProsesOzellikleri2.KontrolEdenMuh = dr.Cells["Kontrol Eden Mühendis"].Value == DBNull.Value ? "" : dr.Cells["Kontrol Eden Mühendis"].Value.ToString();

                pastorizasyonProsesOzellikleri2s.Add(pastorizasyonProsesOzellikleri2);
            }

            nesne.PastorizasyonProsesOzellikleri2s = pastorizasyonProsesOzellikleri2s.ToArray();

            foreach (DataGridViewRow dr in dtgProsesOzellikleri3.Rows)
            {
                pastorizasyonProsesOzellikleri3 = new PastorizasyonProsesOzellikleri3();

                pastorizasyonProsesOzellikleri3.UretilenKremaParti = dr.Cells["Üretilen Krema Parti Numarası"].Value == DBNull.Value ? "" : dr.Cells["Üretilen Krema Parti Numarası"].Value.ToString();
                pastorizasyonProsesOzellikleri3.KullanilanKulturVeKodu = dr.Cells["Kullanılan Kültür Adı Ve Kodu"].Value == DBNull.Value ? "" : dr.Cells["Kullanılan Kültür Adı Ve Kodu"].Value.ToString();
                pastorizasyonProsesOzellikleri3.KulturMiktari = dr.Cells["Kullanılan Kültür Miktarı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Kullanılan Kültür Miktarı"].Value);
                pastorizasyonProsesOzellikleri3.UretilenKremaMiktari = dr.Cells["Üretilen Krema Miktarı"].Value == DBNull.Value ? "" : dr.Cells["Üretilen Krema Miktarı"].Value.ToString();
                pastorizasyonProsesOzellikleri3.KremaYagOrani = dr.Cells["Kremanın Yağ Oranı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Kremanın Yağ Oranı"].Value);
                pastorizasyonProsesOzellikleri3.DolumYapilanAmbalaj = dr.Cells["Dolum Yapılan Ambalaj"].Value == DBNull.Value ? "" : dr.Cells["Dolum Yapılan Ambalaj"].Value.ToString();
                pastorizasyonProsesOzellikleri3.KullanimYapilanAmbalajMiktari = dr.Cells["Kullanılan Ambalaj Miktarı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Kullanılan Ambalaj Miktarı"].Value);
                pastorizasyonProsesOzellikleri3.BirAmbOrtMiktar = dr.Cells["1 Ambalajla Doldurulan Ortalama Krema Miktarı"].Value == DBNull.Value ? "" : dr.Cells["1 Ambalajla Doldurulan Ortalama Krema Miktarı"].Value.ToString();
                pastorizasyonProsesOzellikleri3.KremaDepoPh = dr.Cells["Krema Depoya Atıldığında PH"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Krema Depoya Atıldığında PH"].Value);
                pastorizasyonProsesOzellikleri3.KremaninDepoSicakligi = dr.Cells["Kremanın Atıldığı Depo Sıcaklığı"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Kremanın Atıldığı Depo Sıcaklığı"].Value);
                pastorizasyonProsesOzellikleri3.UretimYapanOperator = dr.Cells["Üretim Yapan Operatör"].Value == DBNull.Value ? "" : dr.Cells["Üretim Yapan Operatör"].Value.ToString();
                pastorizasyonProsesOzellikleri3.KontrolEdenMuhendis = dr.Cells["Kontrol Eden Mühendis"].Value == DBNull.Value ? "" : dr.Cells["Kontrol Eden Mühendis"].Value.ToString();

                pastorizasyonProsesOzellikleri3s.Add(pastorizasyonProsesOzellikleri3);
            }

            nesne.PastorizasyonProsesOzellikleri3s = pastorizasyonProsesOzellikleri3s.ToArray();

            foreach (DataGridViewRow dr in dtgGunlukOzet1.Rows)
            {
                pastorizasyonGunluk1 = new PastorizasyonGunluk();

                string sql = "SELECT T1.\"U_VeriAdi\" as \"Veri Adı\",T1.\"U_Kova\" as \"Kova\",T1.\"U_Teneke\" as \"Teneke\",T1.\"U_ToplamveyaOrt\" as \"Toplam Veya Ortalama\" FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ4\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";

                pastorizasyonGunluk1.VeriAdi = dr.Cells["Veri Adı"].Value == DBNull.Value ? "" : dr.Cells["Veri Adı"].Value.ToString();

                pastorizasyonGunluk1.Kova = dr.Cells["Kova"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Kova"].Value);

                pastorizasyonGunluk1.Teneke = dr.Cells["Teneke"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Teneke"].Value);

                pastorizasyonGunluk1.ToplamveyaOrt = dr.Cells["Toplam Veya Ortalama"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Toplam Veya Ortalama"].Value);

                pastorizasyonGunluk1s.Add(pastorizasyonGunluk1);
            }

            nesne.PastorizasyonGunluk1s = pastorizasyonGunluk1s.ToArray();

            foreach (DataGridViewRow dr in dtgGunlukOzet2.Rows)
            {
                pastorizasyonGunluk2 = new PastorizasyonGunluk2();

                string sql = "SELECT T1.\"U_VeriAdi\" as \"Veri Adı\",T1.\"U_Deger\" as \"Değer\" FROM \"@AIF_PASPRSS_ANLZ\" AS T0 INNER JOIN \"@AIF_PASPRSS_ANLZ5\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_PartiNo\" = '" + partiNo + "'";

                pastorizasyonGunluk2.VeriAdi = dr.Cells["Veri Adı"].Value == DBNull.Value ? "" : dr.Cells["Veri Adı"].Value.ToString();

                pastorizasyonGunluk2.Deger = dr.Cells["Değer"].Value == DBNull.Value ? 0 : Convert.ToDouble(dr.Cells["Değer"].Value);

                pastorizasyonGunluk2s.Add(pastorizasyonGunluk2);
            }

            nesne.PastorizasyonGunluk2s = pastorizasyonGunluk2s.ToArray();

            var resp = client.AddOrUpdatePastorizasyonProsesTakipAnaliz(nesne, Giris.dbName, Giris.mKodValue);

            CustomMsgBtn.Show(resp.Description, "UYARI", "TAMAM");

            if (resp.Value == 0)
            {
                btnOzetEkranaDon.PerformClick();
            }
        }

        private void btnOzetEkranaDon_Click(object sender, EventArgs e)
        {
            BanaAitİsler banaAitİsler = new BanaAitİsler(type, kullaniciid, row, initialWidth, initialHeight, tarih1);
            banaAitİsler.Show();
            Close();
        }
    }
}