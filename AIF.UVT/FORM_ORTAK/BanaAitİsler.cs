using AIF.UVT.DatabaseLayer;
using AIF.UVT.UVTService;
using SAPbobsCOM;
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
using System.Windows.Forms.VisualStyles;

namespace AIF.UVT
{
    public partial class BanaAitİsler : Form
    {
        //font start
        public int initialWidth;

        public int initialHeight;
        public float initialFontSize;
        //font end

        private string tarih1 = "";

        public BanaAitİsler(string type, string _kullaniciid, int _rowid = 0, int _width = 0, int _height = 0, string _tarih1 = "")
        {
            _type = type;
            kullanciid = _kullaniciid;
            rowid = _rowid;
            tarih1 = _tarih1;

            InitializeComponent();

            //font start
            AutoScaleMode = AutoScaleMode.None;

            initialWidth = Width;
            initialHeight = Height;

            initialFontSize = button1.Font.Size;
            button1.Resize += Form_Resize;

            initialFontSize = button2.Font.Size;
            button2.Resize += Form_Resize;

            initialFontSize = button3.Font.Size;
            button3.Resize += Form_Resize;

            initialFontSize = button4.Font.Size;
            button4.Resize += Form_Resize;

            initialFontSize = dataGridView1.Font.Size;
            dataGridView1.Resize += Form_Resize;

            initialFontSize = dataGridView3.Font.Size;
            dataGridView3.Resize += Form_Resize;
            //font end
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            //font start
            SuspendLayout();

            float proportionalNewWidth = (float)Width / initialWidth;
            float proportionalNewHeight = (float)Height / initialHeight;

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

            btnGunlukTemizlik.Font = new Font(btnGunlukTemizlik.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                btnGunlukTemizlik.Font.Style);

            btnGunlukAnaliz.Font = new Font(btnGunlukAnaliz.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                btnGunlukAnaliz.Font.Style);

            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               dataGridView1.Font.Style);

            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               FontStyle.Bold);

            dataGridView3.Font = new Font(dataGridView3.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                dataGridView3.Font.Style);

            dataGridView3.Font = new Font(dataGridView3.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                FontStyle.Bold);

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

        private void Center_Text()
        {
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            Double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            String tmp = " ";
            Double tmpWidth = 0;
            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }
            this.Text = tmp + this.Text.Trim();
        }

        private DataTable dtAnalysisData = new DataTable();
        private string _type = "";
        private string kullanciid = "";
        private int rowid = 0;
        private List<string> emptyrow = new List<string>();
        private List<Tuple<string, string>> parametresbuttonlist = new List<Tuple<string, string>>();
        private SqlCommand cmd = null;
        private string istasyon = "";
        private void BanaAitİsler_Load(object sender, EventArgs e)
        {
            #region MKOD İle Background Değişimi

            var lastOpenedForm = Application.OpenForms.Cast<Form>().Last();

            if (Giris.mKodValue == "10B1C4")
            {
                lastOpenedForm.BackgroundImage = Properties.Resources.OTAT_UVT_ArkaPlanV3;
            }
            else if (Giris.mKodValue == "20R5DB")
            {
                lastOpenedForm.BackgroundImage = Properties.Resources.YORUK_UVT_ArkaPlanv2;
            }

            #endregion MKOD İle Background Değişimi
            try
            {
                string val = _type;

                if (Giris.mKodValue == "10B1C4")
                {
                    if (val != "IST001" && val != "IST004" && val != "IST002" && val != "IST007" && val != "IST005")
                    {
                        btnGunlukAnaliz.Visible = false;
                    }
                }
                else if (Giris.mKodValue == "20R5DB")
                {

                }

                //Center_Text();

                filtrelemeDurumlariOlustur();
                string sql = "";

                if (Giris.UretimPartilendirmeSekli == "")
                {
                    CustomMsgBtn.Show("Lütfen Üretim Partilendirme Şekli seçimi yapınız.", "UYARI", "TAMAM");
                    return;
                }

                if (Giris.UretimPartilendirmeSekli == "1")
                {
                    sql = "SELECT DISTINCT T3.DocEntry as [Üretim Fiş No],T3.ItemCode as [Ürün Kodu],T3.ProdName as [Ürün Tanımı],T3.PlannedQty as [Miktar],T3.\"CmpltQty\" as [Gerçekleşen Miktar],convert(varchar, T3.StartDate , 104) as Tarih,T3.[U_Istasyon] as \"Istasyon\",T5.\"CodeBars\" as \"Barkod\",T5.\"U_UVTVarsayilanDepo\" FROM OWOR as T3 INNER JOIN WOR4 T4 ON T3.DocEntry = T4.DocEntry INNER JOIN OITM as T5 ON T5.ItemCode = T3.ItemCode WHERE T3.DueDate = '" + tarih1 + "' and T3.Status = 'R' ";
                    if (_type != "")
                    {
                        sql += " and T3.[U_Istasyon]= '" + _type + "'";
                    }

                    sql += "order by T3.DocEntry";
                }
                else if (Giris.UretimPartilendirmeSekli == "2")
                {
                    sql = "SELECT T3.ItemCode AS[Ürün Kodu],T3.ProdName AS[Ürün Tanımı],SUM(T3.PlannedQty) AS[Miktar],sum(T3.\"CmpltQty\") AS[Gerçekleşen Miktar],convert(varchar, T3.StartDate, 104) AS Tarih, T3.[U_Istasyon] AS \"Istasyon\",T5.\"CodeBars\" AS \"Barkod\",T5.\"U_UVTVarsayilanDepo\" FROM OWOR AS T3 INNER JOIN WOR4 T4 ON T3.DocEntry = T4.DocEntry INNER JOIN OITM AS T5 ON T5.ItemCode = T3.ItemCode WHERE T3.DueDate = '" + tarih1 + "' AND (T3.Status = 'R' OR T3.Status = 'L') ";

                    if (_type != "")
                    {
                        sql += "  AND T3.[U_Istasyon]= '" + _type + "' ";

                    }

                    sql += " GROUP BY T3.ItemCode,T3.ProdName,convert(varchar, T3.StartDate, 104), T3.[U_Istasyon], T5.\"CodeBars\",T5.\"U_UVTVarsayilanDepo\"  ";
                }
                cmd = new SqlCommand(sql, Connection.sql);

                if (Connection.sql.State != ConnectionState.Open)
                    Connection.sql.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                DataTable dttemp = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Miktar"].DefaultCellStyle.Format = "N2";
                dataGridView1.Columns["Gerçekleşen Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Gerçekleşen Miktar"].DefaultCellStyle.Format = "N2";
                //dataGridView1.Columns["Tarih"].Visible = false;
                dataGridView1.Columns["Istasyon"].Visible = false;
                dataGridView1.Columns["Barkod"].Visible = false;
                dataGridView1.Columns["U_UVTVarsayilanDepo"].Visible = false;
                dataGridView3.AutoResizeRows();
                for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
                {
                    dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                DataGridViewButtonColumn Select = new DataGridViewButtonColumn();
                Select.Name = "Sec";
                Select.Text = "Seç";
                Select.UseColumnTextForButtonValue = true;
                if (dataGridView1.Columns["Sec"] == null)
                {
                    dataGridView1.Columns.Insert(dataGridView1.Columns.Count, Select);
                }

                foreach (DataGridViewColumn col in dataGridView3.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Font = new Font("Bahnschrift", 11F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                setFormatGrid(dataGridView1, 15);

                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dttemp = new DataTable();
                sda.Fill(dt);

                sql = "Select \"U_StationCode\" as IstasyonKodu,\"U_RotaCode\" as RotaKodu,\"U_AnalysisCode\" as AnalizKodu from \"@AIF_ANALYSISPARAM\" where \"U_Active\" = 'Y'";
                cmd = new SqlCommand(sql, Connection.sql);
                sda = new SqlDataAdapter();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dtAnalysisData);

                var arg = new DataGridViewCellEventArgs(dataGridView1.Rows.Count, rowid);
                dataGridView1_CellClick(dataGridView1, arg);
                if (rowid != 0)
                {
                    dataGridView1.Rows[0].Cells[0].Selected = false;
                }

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[rowid].Cells[0].Selected = true;
                }
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView3.Rows[0].Cells[0].Selected = false;
                }

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewColumn column in dataGridView3.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                vScrollBar2.Maximum = dataGridView1.RowCount + 5;

                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGoldenrodYellow;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

                SatirRenkle(rowid, dataGridView1);
            }
            catch (Exception ex)
            {
            }
            try
            {
                dataGridView1.Columns["Miktar"].HeaderText = "Planlanan Miktar";
                dataGridView1.Columns["U_UVTVarsayilanDepo"].Visible = false;

            }
            catch (Exception)
            {
            }
        }

        private void setFormatGrid(DataGridView dtg, int value)
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();

            foreach (DataGridViewColumn col in dtg.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Bahnschrift", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            //for (int i = 0; i <= dtg.Rows.Count - 1; i++)
            //{
            //    //var aa = dtg.RowTemplate.Height;
            //    //dtg.Rows[i].Height = aa + value;
            //    if (i % 2 == 0)
            //        dtg.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
            //    else
            //        dtg.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;

            //    dtg.Rows[i].DefaultCellStyle.ForeColor = Color.White;
            //}
        }

        private void addButton(DataGridView dt)
        {
            parametresbuttonlist = new List<Tuple<string, string>>();
            parametresbuttonlist.Add(Tuple.Create("Baslat", "Başlat"));
            parametresbuttonlist.Add(Tuple.Create("Duraklat", "Duraklat"));
            //parametresbuttonlist.Add(Tuple.Create("DevamEt", "DevamEt"));
            parametresbuttonlist.Add(Tuple.Create("Tamamla", "Tamamla"));
            parametresbuttonlist.Add(Tuple.Create("Analiz", "Analiz"));
            parametresbuttonlist.Add(Tuple.Create("SarfEt", "SarfEt"));

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();

            foreach (var item in parametresbuttonlist)
            {
                btn = new DataGridViewButtonColumn();
                dt.Columns.Add(btn);
                btn.HeaderText = "";
                btn.Text = item.Item2;
                btn.Name = "btn" + item.Item1;
                if (item.Item1 != "Tamamla")
                {
                    btn.UseColumnTextForButtonValue = true;
                }
            }

            dt.RowHeadersVisible = false;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].Width = 90;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var BtnCell = (DataGridViewButtonCell)dataGridView3.Rows[i].Cells["btnTamamla"];
                BtnCell.Value = "Tamamla";
            }

            //dt.ScrollBars = ScrollBars.None;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Hide();
            AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type, kullanciid, dataGridView1.CurrentCell.RowIndex, initialWidth, initialHeight);
            n.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AtanmisIsler atanmisIsler = new AtanmisIsler("", null, kullanciid, Width, Height);
            atanmisIsler.Show();
            Close();
        }

        private void dataGridView3_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            return;
            if (emptyrow.Contains(e.RowIndex.ToString()))
            {
                e.PaintBackground(e.ClipBounds, true);

                Rectangle r = e.CellBounds;

                Rectangle r1 = this.dataGridView3.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                r.Width += r1.Width - 90;

                r.Height -= 1;

                using (SolidBrush brBk = new SolidBrush(e.CellStyle.BackColor))

                using (SolidBrush brFr = new SolidBrush(e.CellStyle.ForeColor))

                {
                    SolidBrush s1 = new SolidBrush(Color.Orange);
                    e.Graphics.FillRectangle(s1, r);

                    StringFormat sf = new StringFormat();

                    sf.Alignment = StringAlignment.Center;

                    sf.LineAlignment = StringAlignment.Center;
                    //e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //e.CellStyle.WrapMode = DataGridViewTriState.True;

                    if (e.ColumnIndex == 5)
                    {
                        FontFamily fontFamily = new FontFamily("Arial");
                        Font font = new Font(
                           fontFamily,
                           14,
                           FontStyle.Bold,
                           GraphicsUnit.Pixel);

                        SolidBrush s = new SolidBrush(Color.White);
                        string tur = dataGridView3.Rows[e.RowIndex + 1].Cells[0].Value.ToString();

                        e.Graphics.DrawString(tur, font, s, r, sf);
                        dataGridView3.Rows[e.RowIndex].Height = dataGridView3.Rows[e.RowIndex + 1].Height;
                        dataGridView3.Rows[e.RowIndex].ReadOnly = true;
                    }

                    e.Handled = true;
                }
            }

            return;
        }

        private string tarih = "";
        System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var Barkod = dataGridView3.CurrentCell.Value;

                if (Barkod.ToString() == "Barkod")
                {
                    string UretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    string PartiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string UrunTanimi = dataGridView3.Rows[e.RowIndex].Cells["Ürün Tanımı"].Value.ToString();
                    string BarkodNumarasi = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Barkod"].Value.ToString();
                    string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                    double GerceklesenMik = Convert.ToDouble(dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells["GerceklesenMiktar"].Value);
                    string gerceklesen = Convert.ToString(GerceklesenMik.ToString("N2"), cultureTR);

                    if (Giris.mKodValue == "10B1C4")
                    {
                        Barkod n = new Barkod(UretimFisNo, PartiNo, UrunTanimi, tarih, initialWidth, initialHeight, BarkodNumarasi, UrunKodu, "", _type);
                        n.Show();
                    }
                    if (Giris.mKodValue == "20R5DB")
                    {
                        Barkod n = new Barkod(UretimFisNo, PartiNo, UrunTanimi, tarih, initialWidth, initialHeight, BarkodNumarasi, UrunKodu, gerceklesen, _type);
                        n.Show();
                    }

                    return;
                }

                if (senderGrid.Columns[e.ColumnIndex].Name == "btnAnaliz")
                {
                    string val = dataGridView3.Rows[e.RowIndex].Cells["Istasyon"].Value.ToString();
                    string PartiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string RotaKodu = dataGridView3.Rows[e.RowIndex].Cells["RotaKodu"].Value.ToString();
                    string UretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    string UrunTanimi = dataGridView3.Rows[e.RowIndex].Cells["Ürün Tanımı"].Value.ToString();
                    string rotaAdi = dataGridView3.Rows[e.RowIndex].Cells["Rota"].Value.ToString();

                    string Durum = dataGridView3.Rows[e.RowIndex].Cells["Durum"].Value.ToString();

                    if (Durum == "Başlanmadı")
                    {
                        CustomMsgBtn.Show("Başlanmamış rota üzerinde analiz işlemi gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    //if (satirDurumlaris.Where(x => x.PartiNo == PartiNo && x.rotaKodu == RotaKodu).Select(y => y.Durum).FirstOrDefault() == "3")
                    //{
                    //    MessageBox.Show("Tamamlanmış rota üzerinde işlem gerçekleştiremezsiniz.");
                    //    return;
                    //}

                    //if (!butonAktif(PartiNo, RotaKodu))
                    //{
                    //    string RotaAdi = satirDurumlaris.Where(x => x.PartiNo == PartiNo && x.aktif == true).Select(y => y.rotaAdi).FirstOrDefault();
                    //    MessageBox.Show(string.Format("{0} parti numaralı {1} aşaması bitirilmeden {2} aşamasına geçilemez.", PartiNo, RotaAdi, rotaAdi));
                    //    return;
                    //}

                    //DataRow[] dtrw = dtAnalysisData.Select("IstasyonKodu = '" + val + "' and RotaKodu='" + rota + "'");
                    DataRow[] dtrw = dtAnalysisData.Select("IstasyonKodu = '" + val + "' and RotaKodu = '" + RotaKodu + "'");
                    string analizEkranid = dtrw.Count() > 0 ? dtrw[0]["AnalizKodu"].ToString() : "-1";

                    if (analizEkranid == "-1")
                    {
                        CustomMsgBtn.Show("Analiz ekranı yapılandırması için yönetici ile irtibata geçiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    if (Giris.mKodValue == "10B1C4")
                    {
                        if (analizEkranid == "1")
                        {
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            AyranProsesTakip_1 n = new AyranProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunGrubu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "2")
                        {
                            TazePeynirProsesTakip_1 n = new TazePeynirProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, Width, Height, tarih1, urunKodu);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "3")
                        {
                            TazePeynirProsesTakip_2 n = new TazePeynirProsesTakip_2(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, Width, Height, dataGridView1.CurrentCell.RowIndex, tarih1);
                            n.Show();
                            Close();

                            //string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                            //string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                            //TelemeAnalizGiris n = new TelemeAnalizGiris(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, Width, Height, urunKodu, val, dataGridView1.CurrentCell.RowIndex);
                            //n.Show();
                            //TelemeProsesTakip n = new TelemeProsesTakip(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1);
                            //n.Show();
                            //Close();
                        }
                        else if (analizEkranid == "4")
                        {
                            TelemeAnalizGiris n = new TelemeAnalizGiris(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, urunKodu, val, dataGridView1.CurrentCell.RowIndex, Width, Height, tarih1);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "5")
                        {
                            TelemeProsesTakip n = new TelemeProsesTakip(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, Width, Height, tarih1);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "6")
                        {
                            //string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                            //string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                            TereyagProsesTakip_1 n = new TereyagProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, Width, Height, tarih1, urunKodu);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "7")
                        {
                            TereyagProsesTakip_2 n = new TereyagProsesTakip_2(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, Width, Height, tarih1);
                            n.Show();
                            Close();

                            //string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                            //string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();

                            //TereyagProsesTakip_1 n = new TereyagProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, Width, Height, dataGridView1.CurrentCell.RowIndex);

                            //TostPeynirProsesTakip_1 n = new TostPeynirProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, Width, Height, dataGridView1.CurrentCell.RowIndex);
                            //n.Show();
                            //Close();

                            //TereyagProsesTakip_2 n = new TereyagProsesTakip_2(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, Width, Height, dataGridView1.CurrentCell.RowIndex);
                            //n.Show();
                            //Close();

                            //TostPeynirProsesTakip_2 n = new TostPeynirProsesTakip_2(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, Width, Height, dataGridView1.CurrentCell.RowIndex);
                            //n.Show();
                            //Close();

                            //TazePeynirProsesTakip_2 n = new TazePeynirProsesTakip_2(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, initialWidth, initialHeight, tarih1);
                            //n.Show();
                            //Close();

                            //TereyagAnalizGiris n = new TereyagAnalizGiris(_type, kullanciid, dataGridView1.CurrentCell.RowIndex);
                            //n.Show();
                            //Close();
                        }
                        else if (analizEkranid == "8")
                        {
                            //Close();
                            //TereyagAnalizGiris n = new TereyagAnalizGiris(_type);
                            //n.Show();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();

                            AyranProsesTakip_1 n = new AyranProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunGrubu);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "9")
                        {
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type, kullanciid, dataGridView1.CurrentCell.RowIndex, initialWidth, initialHeight);
                            //n.Show();
                            //Close();

                            TostPeynirProsesTakip_1 n = new TostPeynirProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, Width, Height, tarih1, urunKodu);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "10")
                        {
                            TostPeynirProsesTakip_2 n = new TostPeynirProsesTakip_2(_type, kullanciid, uretimFisNo, partiNo, UrunTanimi, val, dataGridView1.CurrentCell.RowIndex, Width, Height, tarih1);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "11")
                        {
                            TelemeProsesTakip_2 n = new TelemeProsesTakip_2(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "12")
                        {
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            YogurtProsesTakip_1 n = new YogurtProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunGrubu);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "13")
                        {
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            LorProsesTakip n = new LorProsesTakip(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "14")
                        {
                            PastorizasyonProsesTakip_1 n = new PastorizasyonProsesTakip_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1);
                            n.Show();
                            Close();
                        }
                    }
                    else if (Giris.mKodValue == "20R5DB")
                    {
                        if (analizEkranid == "1") //Ayran Sütü Hazırlık
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_007_1 n = new _20_FSAPKY_007_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "2") //Ayran Pastörizasyon Hazırlık 1 
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_007_2_1 n = new _20_FSAPKY_007_2_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "3") //Homojen Yoğurt Sütü
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_007_3 n = new _20_FSAPKY_007_3(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "4") //Pastörizasyon Analiz
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_001 n = new _20_FSAPKY_001(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "5") //Kaşar Teleme
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_002_1 n = new _20_FSAPKY_002_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "6") //Kaşar Haşlama
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_002_2 n = new _20_FSAPKY_002_2(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "7") //Kaşar Paketleme
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_002_3 n = new _20_FSAPKY_002_3(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "8") //Konsantre Ürün UF Analiz - 1 //Konsantre Ürün UF Analiz - 2
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_004_1_1 n = new _20_FSAPKY_004_1_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "9") //UF Proses Ür.
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_004_2 n = new _20_FSAPKY_004_2(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "10") //Lor Pisirim
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_008_1 n = new _20_FSAPKY_008_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "11") //Tereyagi Ürün Analizi
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_010_1 n = new _20_FSAPKY_010_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "12") //Kaymak Ürün Analiz
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_011_1 n = new _20_FSAPKY_011_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "13") //Krem Peynir Üretim Analizi
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_009_1 n = new _20_FSAPKY_009_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "14") //Krema Ürün Analizi
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_012_1 n = new _20_FSAPKY_012_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "15") //Beyaz Peynir Paketleme 
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_003_2 n = new _20_FSAPKY_003_2(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "16") //Beyaz Teleme --
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_003_1 n = new _20_FSAPKY_003_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);;
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "17") //Homojen Yoğurt Sütü --
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_005_2 n = new _20_FSAPKY_005_2(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "18") //Homojenize Yoğurt Dolum İnkübasyon 1 --
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_005_3_1 n = new _20_FSAPKY_005_3_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "19") //Stand.Kaymaklı Yoğurt Analiz 1 --
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_006_1_1 n = new _20_FSAPKY_006_1_1(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                        else if (analizEkranid == "20") //Krema Ürün Paketleme --
                        {
                            string UrunKodu = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                            string UrunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                            _20_FSAPKY_012_2 n = new _20_FSAPKY_012_2(_type, kullanciid, uretimFisNo, partiNo, urunTanimi, val, dataGridView1.CurrentCell.RowIndex, tarih1, UrunKodu);
                            //AyranPaketlemeAnalizi n = new AyranPaketlemeAnalizi(_type);
                            n.Show();
                            Close();
                        }
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "btnSarfEt")
                {
                    string PartiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string RotaKodu = dataGridView3.Rows[e.RowIndex].Cells["RotaKodu"].Value.ToString();
                    string rotaAdi = dataGridView3.Rows[e.RowIndex].Cells["Rota"].Value.ToString();
                    string Durum = dataGridView3.Rows[e.RowIndex].Cells["Durum"].Value.ToString();

                    if (Durum == "Başlanmadı")
                    {
                        CustomMsgBtn.Show("Başlanmamış rota üzerinde sarfiyat işlemi gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    if (satirDurumlaris.Where(x => x.PartiNo == PartiNo && x.rotaKodu == RotaKodu).Select(y => y.Durum).FirstOrDefault() == "3")
                    {
                        CustomMsgBtn.Show("Tamamlanmış rota üzerinde işlem gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    //if (!butonAktif(PartiNo, RotaKodu))
                    //{
                    //    string RotaAdi = satirDurumlaris.Where(x => x.PartiNo == PartiNo && x.aktif == true).Select(y => y.rotaAdi).FirstOrDefault();
                    //    MessageBox.Show(string.Format("{0} parti numaralı {1} aşaması bitirilmeden {2} aşamasına geçilemez.", PartiNo, RotaAdi, rotaAdi));
                    //    return;
                    //}

                    string val = _type;
                    string stageid = dataGridView3.Rows[e.RowIndex].Cells["StageID"].Value.ToString();
                    string DocNum = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    double partiKatSayi = Convert.ToDouble(dataGridView3.Rows[e.RowIndex].Cells["KatSayi"].Value);
                    string urunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                    string urunTanimi = dataGridView3.Rows[e.RowIndex].Cells["Ürün Tanımı"].Value.ToString();
                    if (DocNum != "")
                    {
                        UretimSarfiyat_2 n = new UretimSarfiyat_2(stageid, DocNum, val, kullanciid, partiKatSayi, PartiNo, RotaKodu, urunGrubu, urunTanimi, dataGridView1.CurrentCell.RowIndex, initialWidth, initialHeight, tarih1);
                        //UretimSarfiyat n = new UretimSarfiyat(stageid, DocNum, val, kullanciid, partiKatSayi, PartiNo, RotaKodu, urunGrubu, urunTanimi, dataGridView1.CurrentCell.RowIndex);
                        n.ShowDialog();
                        //Close();
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "btnBaslat")
                {
                    string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string rota = dataGridView3.Rows[e.RowIndex].Cells["RotaKodu"].Value.ToString();
                    string rotaAdi = dataGridView3.Rows[e.RowIndex].Cells["Rota"].Value.ToString();

                    if (satirDurumlaris.Where(x => x.PartiNo == partiNo && x.rotaKodu == rota).Select(y => y.Durum).FirstOrDefault() == "3")
                    {
                        CustomMsgBtn.Show("Tamamlanmış rota üzerinde işlem gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    if (!butonAktif(partiNo, rota))
                    {
                        string RotaAdi = satirDurumlaris.Where(x => x.PartiNo == partiNo && x.aktif == true).Select(y => y.rotaAdi).FirstOrDefault();
                        CustomMsgBtn.Show(string.Format("{0} parti numaralı {1} aşaması bitirilmeden {2} aşamasına geçilemez.", partiNo, RotaAdi, rotaAdi), "UYARI", "TAMAM");
                        return;
                    }

                    if (satirDurumlaris.Where(x => x.PartiNo == partiNo).Select(y => y.Durum).FirstOrDefault() == "3")
                    {
                        CustomMsgBtn.Show("Tamamlanmış rota üzerinde işlem gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    if (!butonAktif_SatirKarsilastirma(dataGridView3.CurrentCell.RowIndex))
                    {
                        DialogResult answer = CustomMsgBox.Show("Daha önce başlanmamış partiler mevcuttur. Devam etmek istiyor musunuz?", "Uyarı", "Evet", "Hayır");

                        if (!CustomMsgBox.Value)
                        {
                            return;
                        }

                        //string RotaAdi = satirDurumlaris.Where(x => x.PartiNo == partiNo && x.aktif == true).Select(y => y.rotaAdi).FirstOrDefault();
                        //MessageBox.Show(string.Format("{0} parti numaralı {1} aşaması bitirilmeden {2} aşamasına geçilemez.", partiNo, RotaAdi, rotaAdi));
                        //return;
                    }

                    string stageid = dataGridView3.Rows[e.RowIndex].Cells["StageID"].Value.ToString();
                    string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    double planlananMiktar = Convert.ToDouble(dataGridView3.Rows[e.RowIndex].Cells["Miktar"].Value);
                    //AkviteIslemleri n = new AkviteIslemleri(_type, kullanciid, rota, stageid, uretimFisNo, "1", partiNo, urunKodu, "", Width, Height, dataGridView1.CurrentCell.RowIndex);
                    dataGridView1.Refresh();
                    AktiviteIslemleri_2 n = new AktiviteIslemleri_2(_type, kullanciid, rota, stageid, uretimFisNo, "1", partiNo, urunKodu, "", dataGridView1.CurrentCell.RowIndex, planlananMiktar, initialWidth, initialHeight, tarih1, "");
                    n.Show();
                    Close();
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "btnDuraklat")
                {
                    string rota = dataGridView3.Rows[e.RowIndex].Cells["RotaKodu"].Value.ToString();
                    string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string rotaAdi = dataGridView3.Rows[e.RowIndex].Cells["Rota"].Value.ToString();
                    string Durum = dataGridView3.Rows[e.RowIndex].Cells["Durum"].Value.ToString();

                    if (Durum == "Başlanmadı")
                    {
                        CustomMsgBtn.Show("Başlanmamış rota üzerinde duraklatma işlemi gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    if (satirDurumlaris.Where(x => x.PartiNo == partiNo && x.rotaKodu == rota).Select(y => y.Durum).FirstOrDefault() == "3")
                    {
                        CustomMsgBtn.Show("Tamamlanmış rota üzerinde işlem gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    if (!butonAktif(partiNo, rota))
                    {
                        string RotaAdi = satirDurumlaris.Where(x => x.PartiNo == partiNo && x.aktif == true).Select(y => y.rotaAdi).FirstOrDefault();
                        CustomMsgBtn.Show(string.Format("{0} parti numaralı {1} aşaması bitirilmeden {2} aşamasına geçilemez.", partiNo, RotaAdi, rotaAdi), "UYARI", "TAMAM");
                        return;
                    }

                    string stageid = dataGridView3.Rows[e.RowIndex].Cells["StageID"].Value.ToString();
                    string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    double planlananMiktar = Convert.ToDouble(dataGridView3.Rows[e.RowIndex].Cells["Miktar"].Value);
                    //AkviteIslemleri n = new AkviteIslemleri(_type, kullanciid, rota, stageid, uretimFisNo, "2", partiNo, urunKodu, "", Width, Height, dataGridView1.CurrentCell.RowIndex);
                    AktiviteIslemleri_2 n = new AktiviteIslemleri_2(_type, kullanciid, rota, stageid, uretimFisNo, "2", partiNo, urunKodu, "", dataGridView1.CurrentCell.RowIndex, planlananMiktar, initialWidth, initialHeight, tarih1, "");
                    n.Show();
                    Close();
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "btnDevamEt")
                {
                    string rota = dataGridView3.Rows[e.RowIndex].Cells["RotaKodu"].Value.ToString();
                    string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string rotaAdi = dataGridView3.Rows[e.RowIndex].Cells["Rota"].Value.ToString();

                    if (satirDurumlaris.Where(x => x.PartiNo == partiNo && x.rotaKodu == rota).Select(y => y.Durum).FirstOrDefault() == "3")
                    {
                        CustomMsgBtn.Show("Tamamlanmış rota üzerinde işlem gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    if (!butonAktif(partiNo, rota))
                    {
                        string RotaAdi = satirDurumlaris.Where(x => x.PartiNo == partiNo && x.aktif == true).Select(y => y.rotaAdi).FirstOrDefault();
                        CustomMsgBtn.Show(string.Format("{0} parti numaralı {1} aşaması bitirilmeden {2} aşamasına geçilemez.", partiNo, RotaAdi, rotaAdi), "UYARI", "TAMAM");
                        return;
                    }

                    string stageid = dataGridView3.Rows[e.RowIndex].Cells["StageID"].Value.ToString();
                    string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    double planlananMiktar = Convert.ToDouble(dataGridView3.Rows[e.RowIndex].Cells["Miktar"].Value);

                    //AkviteIslemleri n = new AkviteIslemleri(_type, kullanciid, rota, stageid, uretimFisNo, "3", partiNo, urunKodu, "", dataGridView1.CurrentCell.RowIndex, initialWidth, initialHeight);
                    AktiviteIslemleri_2 n = new AktiviteIslemleri_2(_type, kullanciid, rota, stageid, uretimFisNo, "3", partiNo, urunKodu, "", dataGridView1.CurrentCell.RowIndex, planlananMiktar, initialWidth, initialHeight, tarih1, "");
                    n.Show();
                    Close();
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "btnTamamla")
                {
                    string rota = dataGridView3.Rows[e.RowIndex].Cells["RotaKodu"].Value.ToString();
                    string partiNo = dataGridView3.Rows[e.RowIndex].Cells["Parti No"].Value.ToString();
                    string rotaAdi = dataGridView3.Rows[e.RowIndex].Cells["Rota"].Value.ToString();
                    string varsayilanDepo = dataGridView3.Rows[e.RowIndex].Cells["U_UVTVarsayilanDepo"].Value.ToString();

                    string Durum = dataGridView3.Rows[e.RowIndex].Cells["Durum"].Value.ToString();

                    if (Durum == "Başlanmadı")
                    {
                        CustomMsgBtn.Show("Başlanmamış rota üzerinde tamamlama işlemi gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    if (satirDurumlaris.Where(x => x.PartiNo == partiNo && x.rotaKodu == rota).Select(y => y.Durum).FirstOrDefault() == "3")
                    {
                        CustomMsgBtn.Show("Tamamlanmış rota üzerinde işlem gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                        return;
                    }

                    #region sarf edilmeden tmaamla butonuna basılmaması içn kontrol eklendi. 1 ise belge eklensin.değilse eklenmesin.

                    //SqlDataAdapter sda_SarfEdilen = new SqlDataAdapter(cmd);
                    //DataTable dt_SarfEdilen = new DataTable();
                    //string sarfEdilenSql = "";

                    ////sarfEdilenSql = "SELECT * from OIGE T0 where T0.\"U_BatchNumber\"= '" + partiNo + "' ";

                    //sarfEdilenSql = "SELECT ISNULL(CASE when T2.TreeType ='P' and COUNT(T0.DocEntry)>0 then 1 when T2.TreeType<>'P' then 1 else 0 end,0) as \"Geç 1\" from OIGE T0 inner join IGE1 T1 on T1.DocEntry = T0.DocEntry inner join OITM T2 on T2.ItemCode = T1.ItemCode ";

                    //sarfEdilenSql += " where T0.U_BatchNumber = '" + partiNo + "' group by T2.TreeType";

                    //cmd = new SqlCommand(sarfEdilenSql, Connection.sql);

                    //if (Connection.sql.State != ConnectionState.Open)
                    //{
                    //    Connection.sql.Open();
                    //}
                    //sda_SarfEdilen = new SqlDataAdapter(cmd);
                    //sda_SarfEdilen.Fill(dt_SarfEdilen);

                    //if (dt_SarfEdilen.Rows.Count == 0)
                    //{
                    //    CustomMsgBtn.Show("Sarf edilme işlemi yapılmadan tamamlama işlemi gerçekleştiremezsiniz.", "UYARI", "TAMAM");
                    //    return;
                    //}

                    #endregion

                    if (!butonAktif(partiNo, rota))
                    {
                        string RotaAdi = satirDurumlaris.Where(x => x.PartiNo == partiNo && x.aktif == true).Select(y => y.rotaAdi).FirstOrDefault();
                        CustomMsgBtn.Show(string.Format("{0} parti numaralı {1} aşaması bitirilmeden {2} aşamasına geçilemez.", partiNo, RotaAdi, rotaAdi), "UYARI", "TAMAM");
                        return;
                    }

                    string stageid = dataGridView3.Rows[e.RowIndex].Cells["StageID"].Value.ToString();
                    string uretimFisNo = dataGridView3.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value.ToString();
                    double planlananMiktar = Convert.ToDouble(dataGridView3.Rows[e.RowIndex].Cells["Miktar"].Value);
                    string uretimdengirisyap = "";

                    if (stageid == maxStageId.ToString())
                    {
                        uretimdengirisyap = "Y";
                    }

                    #region NE OLURSA OLSUN TAMAMLARKEN SARF EKRANINA GİTMESİ İÇİN KOYULDU
                    string val = _type;
                    double partiKatSayi = Convert.ToDouble(dataGridView3.Rows[e.RowIndex].Cells["KatSayi"].Value);
                    string urunGrubu = dataGridView3.Rows[e.RowIndex].Cells["Ürün Grubu"].Value.ToString();
                    string urunTanimi = dataGridView3.Rows[e.RowIndex].Cells["Ürün Tanımı"].Value.ToString();
                    var aktifCell = dataGridView1.CurrentCell;
                    if (uretimFisNo != "")
                    {
                        UretimSarfiyat_2 n2 = new UretimSarfiyat_2(stageid, uretimFisNo, val, kullanciid, partiKatSayi, partiNo, rota, urunGrubu, urunTanimi, aktifCell.RowIndex, initialWidth, initialHeight, tarih1);
                        //UretimSarfiyat n = new UretimSarfiyat(stageid, DocNum, val, kullanciid, partiKatSayi, PartiNo, RotaKodu, urunGrubu, urunTanimi, dataGridView1.CurrentCell.RowIndex);
                        n2.ShowDialog();
                        //Close();
                    }
                    #endregion

                    //if (!sarfagoturuldu)
                    //{

                    if (UretimSarfiyat_2.aktiviteEkraninaGit == "Ok")
                    {
                        AktiviteIslemleri_2 n = new AktiviteIslemleri_2(_type, kullanciid, rota, stageid, uretimFisNo, "4", partiNo, urunKodu, uretimdengirisyap, aktifCell.RowIndex, planlananMiktar, initialWidth, initialHeight, tarih1, varsayilanDepo);
                        //AkviteIslemleri n = new AkviteIslemleri(_type, kullanciid, rota, stageid, uretimFisNo, "4", partiNo, urunKodu, uretimdengirisyap, Width, Height, dataGridView1.CurrentCell.RowIndex);
                        n.ShowDialog();
                        Close();
                    }
                    //UretimSarfiyat_2.aktiviteEkraninaGit = "";
                    //}
                }
            }
        }

        private bool butonAktif(string partiNo, string rota)
        {
            //var edit = satirDurumlaris.Where(x => x.PartiNo == partiNo && x.rotaKodu == rota).FirstOrDefault();
            var edit = satirDurumlaris.Where(x => x.PartiNo == partiNo).FirstOrDefault();

            return edit.aktif;
        }

        private bool butonAktif_SatirKarsilastirma(int row)
        {
            var edit = satirDurumlaris.Where(x => x.Row < row && x.Durum == "1").ToList();

            if (edit.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int maxStageId = 0;
        private string urunKodu = "";
        private string urunTanimi = "";
        private int PartiNoKolonWidth = 0;

        //type = 1 --> Tümünü Listele
        private void Listele(int row, string type)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[0];
            tarih = dataGridView1.Rows[row].Cells["Tarih"].Value.ToString();
            var docentry = "";

            if (Giris.UretimPartilendirmeSekli == "1")
            {
                docentry = dataGridView1.Rows[row].Cells["Üretim Fiş No"].Value.ToString();
            }
            urunKodu = dataGridView1.Rows[row].Cells["Ürün Kodu"].Value.ToString();
            urunTanimi = dataGridView1.Rows[row].Cells["Ürün Tanımı"].Value.ToString();

            string sql = "";

            if (Giris.UretimPartilendirmeSekli == "1")
            {
                sql = " Select T1.\"U_PartiNo\",\"U_Miktar\",\"U_PartiKatSayi\" from  \"@AIF_URT_PART\" as T0 INNER JOIN \"@AIF_URT_PART1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_UretimSipNo\" = '" + docentry + "' order by \"LineId\"";
            }
            //else if (Giris.UretimPartilendirmeSekli == "2")
            //{
            //    sql = "SELECT T1.\"U_PartiNo\",\"U_Miktar\",\"U_PartiKatSayi\",\"U_UretimSipNo\" FROM \"@AIF_URT_PART\" AS T0 INNER JOIN \"@AIF_URT_PART1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" LEFT JOIN OWOR as T2 on T0.U_UretimSipNo = t2.DocEntry WHERE T2.U_GrupSipNo = '" + docentry + "' ORDER BY \"LineId\" ";
            //}

            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
                Connection.sql.Open();

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataTable dttemp = new DataTable();
            if (Giris.UretimPartilendirmeSekli == "1")
            {
                sda.Fill(dt);
            }

            if (Giris.UretimPartilendirmeSekli == "1")
            {
                sql = "SELECT DISTINCT (select T1.Descr from CUFD as T0 INNER JOIN UFD1 as T1 ON T0.FieldID = T1.FieldID where T0.TableID = 'OWOR' and T1.TableID = 'OWOR' and T1.FldValue = T3.[U_Istasyon]) as  [Ürün Grubu], T4.Name as Rota, T3.DocEntry as [Üretim Fiş No],' ' as [Parti No],' ' as KatSayi, T3.ItemCode as [Ürün Kodu],T3.ProdName as [Ürün Tanımı],T3.PlannedQty as [Miktar],T3.PlannedQty as [GerceklesenMiktar], '' as Durum,'' as DurumKodu, T3.[U_Istasyon] as Istasyon,T4.SeqNum as StageID, (Select \"Code\" from ORST where AbsEntry = T4.StgEntry) as RotaKodu,T6.\"U_UVTVarsayilanDepo\" FROM OWOR as T3 INNER JOIN WOR1 as T5 ON T3.DocEntry = T5.DocEntry INNER JOIN WOR4 T4 ON T5.DocEntry = T4.DocEntry and T5.StageId = T4.StageId INNER JOIN OITM as T6 ON T6.ItemCode = T3.ItemCode WHERE T5.DocEntry = '" + docentry + "'";
            }
            else if (Giris.UretimPartilendirmeSekli == "2")
            {
                #region ÜRETİM SİPARİŞİ GRUPLANDIRILMIŞ PARTİ 
                if (Giris.mKodValue == "10B1C4")
                {
                    sql = "SELECT DISTINCT (select T1.Descr from CUFD as T0 INNER JOIN UFD1 as T1 ON T0.FieldID = T1.FieldID where T0.TableID = 'OWOR' and T1.TableID = 'OWOR' and T1.FldValue = T3.[U_Istasyon]) as  [Ürün Grubu], T4.Name as Rota, T3.DocEntry as [Üretim Fiş No],T3.\"U_GrupPartiNo\" as [Parti No],' ' as KatSayi, T3.ItemCode as [Ürün Kodu],T3.ProdName as [Ürün Tanımı],T3.PlannedQty as [Miktar],T3.PlannedQty as [GerceklesenMiktar], '' as Durum,'' as DurumKodu, T3.[U_Istasyon] as Istasyon,T4.SeqNum as StageID, (Select \"Code\" from ORST where AbsEntry = T4.StgEntry) as RotaKodu,T6.\"U_UVTVarsayilanDepo\" FROM OWOR as T3 INNER JOIN WOR1 as T5 ON T3.DocEntry = T5.DocEntry INNER JOIN WOR4 T4 ON T5.DocEntry = T4.DocEntry and T5.StageId = T4.StageId INNER JOIN OITM as T6 ON T6.ItemCode = T3.ItemCode WHERE  T3.StartDate = '" + tarih1 + "' and T3.U_ISTASYON = '" + _type + "' AND T3.ItemCode = '" + urunKodu + "' AND T3.\"Status\" != 'C'";
                } 
                #endregion

                #region üretim sparişi tek parti oldugu sorgu.parti gruplandırma öncesi
                if (Giris.mKodValue == "20R5DB")
                {
                    sql = "SELECT DISTINCT (select T1.Descr from CUFD as T0 INNER JOIN UFD1 as T1 ON T0.FieldID = T1.FieldID where T0.TableID = 'OWOR' and T1.TableID = 'OWOR' and T1.FldValue = T3.[U_Istasyon]) as  [Ürün Grubu], T4.Name as Rota, T3.DocEntry as [Üretim Fiş No],' ' as [Parti No],' ' as KatSayi, T3.ItemCode as [Ürün Kodu],T3.ProdName as [Ürün Tanımı],T3.PlannedQty as [Miktar],T3.PlannedQty as [GerceklesenMiktar], '' as Durum,'' as DurumKodu, T3.[U_Istasyon] as Istasyon,T4.SeqNum as StageID, (Select \"Code\" from ORST where AbsEntry = T4.StgEntry) as RotaKodu,T6.\"U_UVTVarsayilanDepo\" FROM OWOR as T3 INNER JOIN WOR1 as T5 ON T3.DocEntry = T5.DocEntry INNER JOIN WOR4 T4 ON T5.DocEntry = T4.DocEntry and T5.StageId = T4.StageId INNER JOIN OITM as T6 ON T6.ItemCode = T3.ItemCode WHERE  T3.StartDate = '" + tarih1 + "' and T3.U_ISTASYON = '" + _type + "' AND T3.ItemCode = '" + urunKodu + "' AND T3.\"Status\" != 'C'";
                }
                #endregion 
            }

            cmd = new SqlCommand(sql, Connection.sql);

            sda = new SqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dttemp = new DataTable();
            sda.Fill(dt2);

            if (Giris.UretimPartilendirmeSekli == "1")
            {
                dttemp = dt2.Copy();
                dttemp.Rows.Clear();
                foreach (DataRow dr in dt2.Rows)
                {
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        dr["Parti No"] = dr2["U_PartiNo"];
                        dr["Miktar"] = dr2["U_Miktar"];
                        dr["KatSayi"] = dr2["U_PartiKatSayi"];
                        dttemp.Rows.Add(dr.ItemArray);
                    }
                }
            }
            else if (Giris.UretimPartilendirmeSekli == "2")
            {
                //foreach (DataRow dr in dt2.Rows)
                //{
                //    //if (birSatirDonsun > 0)
                //    //{
                //    //    break;
                //    //}
                //    foreach (DataRow dr2 in dt.AsEnumerable().Where(x => x.Field<string>("U_UretimSipNo") == dr["Üretim Fiş No"].ToString()))
                //    {
                //        dr["Parti No"] = dr2["U_PartiNo"];
                //        dr["Miktar"] = dr2["U_Miktar"];
                //        dr["KatSayi"] = dr2["U_PartiKatSayi"];
                //        dttemp.Rows.Add(dr.ItemArray);

                //        //birSatirDonsun++;
                //    }
                //}

                //foreach (DataRow dr in dt2.Rows)
                //{
                //    dr["Parti No"] = dr2["U_PartiNo"];
                //    dr["Miktar"] = dr2["U_Miktar"];
                //    dr["KatSayi"] = dr2["U_PartiKatSayi"];
                //    dttemp.Rows.Add(dr.ItemArray);
                //}
            }

            sda = new SqlDataAdapter(cmd);
            string query2 = "";
            if (Giris.UretimPartilendirmeSekli == "1")
            {
                foreach (DataRow item in dttemp.Rows)
                {
                    dt3 = new DataTable();
                    //query2 = "SELECT T0.\"Quantity\" FROM IBT1 T0 WHERE T0.[BaseType] = '59' and  T0.[BatchNum] = '" + item["Parti No"] + "' and T0.\"ItemCode\" = '" + urunKodu + "'";

                    query2 = "select TOP 1 T1.Quantity from OIGN as T0 INNER JOIN IGN1 AS T1 ON T0.DocEntry=T1.DocEntry where U_BatchNumber = '" + item["Parti No"] + "' and T1.\"ItemCode\" = '" + urunKodu + "'";

                    cmd = new SqlCommand(query2, Connection.sql);
                    sda = new SqlDataAdapter(cmd);

                    sda.Fill(dt3);

                    if (dt3.Rows.Count > 0)
                    {
                        item["GerceklesenMiktar"] = dt3.Rows[0][0].ToString();
                    }
                    else
                    {
                        item["GerceklesenMiktar"] = "0";

                    }
                }
            }
            else if (Giris.UretimPartilendirmeSekli == "2")
            {
                foreach (DataRow item in dt2.Rows)
                {
                    if (Giris.mKodValue == "20R5DB")
                    {
                        item["Parti No"] = tarih1 + "-" + item["Üretim Fiş No"] + "-1"; //parti no hep sonu 1 olması için eklendi.gruplama partilendirmeden önce çalışıyordu 
                    }
                    if (Giris.mKodValue == "10B1C4")
                    {
                        item["Parti No"] = item["Parti No"].ToString();  //gruplandırılmış partileri getirir
                    }
                    item["KatSayi"] = "1";
                    dt3 = new DataTable();
                    //query2 = "SELECT T0.\"Quantity\" FROM IBT1 T0 WHERE T0.[BaseType] = '59' and  T0.[BatchNum] = '" + item["Parti No"] + "' and T0.\"ItemCode\" = '" + urunKodu + "'";

                    query2 = "select TOP 1 T1.Quantity from OIGN as T0 INNER JOIN IGN1 AS T1 ON T0.DocEntry=T1.DocEntry where U_BatchNumber = '" + item["Parti No"] + "' and T1.\"ItemCode\" = '" + urunKodu + "'";

                    cmd = new SqlCommand(query2, Connection.sql);
                    sda = new SqlDataAdapter(cmd);

                    sda.Fill(dt3);

                    if (dt3.Rows.Count > 0)
                    {
                        item["GerceklesenMiktar"] = dt3.Rows[0][0].ToString();
                    }
                    else
                    {
                        item["GerceklesenMiktar"] = "0";
                    }


                }
                dttemp = dt2.Copy();
            }

            DataTable newDataTable = dttemp.Copy();
            //newDataTable.Rows.Clear();

            //if (dttemp.Rows.Count > 0)
            //{
            //    //DataView dv = new DataView(dttemp);
            //    //dv.Sort = "Parti No, StageId ASC";
            //    //dttemp = dt.DefaultView.ToTable();

            //    newDataTable = dttemp.AsEnumerable()
            //           .OrderBy(r => r.Field<string>("Parti No"))
            //           .ThenBy(r => r.Field<int>("StageId"))
            //           .CopyToDataTable();
            //}
            satirDurumlaris = new List<SatirDurumlari>();
            int ix = 0;
            foreach (DataRow itm in newDataTable.Rows)
            {
                string partiNo = itm["Parti No"].ToString();
                string uretimFisNo = itm["Üretim Fiş No"].ToString();
                string rotaKodu = itm["RotaKodu"].ToString();
                string rotaAdiDg = itm["Rota"].ToString();

                #region ÜRETİM GİRİŞ-ÇIKIŞ YAPILMADAN ÜRETİM SİPARİŞİ TAMAMLANDI YAPILDI İSE SATIR DURUMU TAMAMLANDI OLARAK ALINIR
                dt = new DataTable();
                sql = "SELECT * FROM OWOR AS T88 WHERE T88.\"DocEntry\" = '" + uretimFisNo + "' AND T88.\"Status\" = 'L' AND ISNULL(T88.\"CmpltQty\",0) = 0 ";
                cmd = new SqlCommand(sql, Connection.sql);

                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dt = new DataTable();
                    sql = "Select \"U_DurunAciklama\",\"U_DurumKodu\" from \"@AIF_ROTA_DURUM\" where \"U_UretimFisNo\" = '" + uretimFisNo + "' and \"U_RotaKodu\" = '" + rotaKodu + "' and \"U_PartiNo\" = '" + partiNo + "'";

                    cmd = new SqlCommand(sql, Connection.sql);

                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0][0] = "Tamamlandı";
                        dt.Rows[0][1] = "3";
                        itm["Durum"] = dt.Rows[0][0].ToString();
                        itm["DurumKodu"] = dt.Rows[0][1].ToString();
                        if (dt.Rows[0][1].ToString() == "3")
                            satirDurumlaris.Add(new SatirDurumlari { PartiNo = partiNo, Row = ix, Durum = dt.Rows[0][1].ToString(), aktif = false, rotaKodu = rotaKodu, rotaAdi = rotaAdiDg });
                        else
                            satirDurumlaris.Add(new SatirDurumlari { PartiNo = partiNo, Row = ix, Durum = dt.Rows[0][1].ToString(), aktif = true, rotaKodu = rotaKodu, rotaAdi = rotaAdiDg });

                        if (filterelemeDurumlaris.Where(x => x.Tip == "3").First().Durum == "1"
                            && dt.Rows[0][1].ToString() == "3")
                        {
                            satirDurumlaris[satirDurumlaris.Count - 1].SatiriSil = "Y";
                        }
                        else if (filterelemeDurumlaris.Where(x => x.Tip == "2").First().Durum == "1"
                            && dt.Rows[0][1].ToString() == "2")
                        {
                            satirDurumlaris[satirDurumlaris.Count - 1].SatiriSil = "Y";
                        }

                        ix++;
                    }
                }
                else
                {
                    #region ÜRETİM GİRİŞ-ÇIKIŞ YAPILMADAN ÜRETİM SİPARİŞİ BU ŞEKİLDE LİSTELENİYORDU.YUKARIDA ÖNCELİK GETİRİLDİ.O ŞARTA UYMADIĞINDA NORMAL İŞLEMLER DEVAM EDECEK
                    dt = new DataTable();
                    sql = "Select \"U_DurunAciklama\",\"U_DurumKodu\" from \"@AIF_ROTA_DURUM\" where \"U_UretimFisNo\" = '" + uretimFisNo + "' and \"U_RotaKodu\" = '" + rotaKodu + "' and \"U_PartiNo\" = '" + partiNo + "'";

                    cmd = new SqlCommand(sql, Connection.sql);

                    sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        itm["Durum"] = dt.Rows[0][0].ToString();
                        itm["DurumKodu"] = dt.Rows[0][1].ToString();
                        if (dt.Rows[0][1].ToString() == "3")
                            satirDurumlaris.Add(new SatirDurumlari { PartiNo = partiNo, Row = ix, Durum = dt.Rows[0][1].ToString(), aktif = false, rotaKodu = rotaKodu, rotaAdi = rotaAdiDg });
                        else
                            satirDurumlaris.Add(new SatirDurumlari { PartiNo = partiNo, Row = ix, Durum = dt.Rows[0][1].ToString(), aktif = true, rotaKodu = rotaKodu, rotaAdi = rotaAdiDg });

                        if (filterelemeDurumlaris.Where(x => x.Tip == "3").First().Durum == "1"
                            && dt.Rows[0][1].ToString() == "3")
                        {
                            satirDurumlaris[satirDurumlaris.Count - 1].SatiriSil = "Y";
                        }
                        else if (filterelemeDurumlaris.Where(x => x.Tip == "2").First().Durum == "1"
                            && dt.Rows[0][1].ToString() == "2")
                        {
                            satirDurumlaris[satirDurumlaris.Count - 1].SatiriSil = "Y";
                        }

                        ix++;
                    }
                    else
                    {
                        itm["Durum"] = "Başlanmadı";
                        itm["DurumKodu"] = "1";
                        if (satirDurumlaris.Where(x => x.PartiNo == partiNo && x.Durum != "3").Count() > 0)
                            satirDurumlaris.Add(new SatirDurumlari { PartiNo = partiNo, Row = ix, Durum = "1", aktif = false, rotaKodu = rotaKodu, rotaAdi = rotaAdiDg });
                        else
                            satirDurumlaris.Add(new SatirDurumlari { PartiNo = partiNo, Row = ix, Durum = "1", aktif = true, rotaKodu = rotaKodu, rotaAdi = rotaAdiDg });

                        if (filterelemeDurumlaris.Where(x => x.Tip == "1").First().Durum == "1")
                        {
                            satirDurumlaris[satirDurumlaris.Count - 1].SatiriSil = "Y";
                        }

                        ix++;
                    }
                    #endregion
                }
                #endregion

                
            }

            dataGridView1.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            bool sirala = false;
            if (filterelemeDurumlaris.Where(x => x.Tip == "3").First().Durum == "1")
            {
                var query = newDataTable.AsEnumerable().Where(r => r.Field<string>("DurumKodu") == "3");

                foreach (var row1 in query.ToList())
                    row1.Delete();

                newDataTable.AcceptChanges();

                satirDurumlaris.RemoveAll(x => x.SatiriSil == "Y" && x.Durum == "3");
            }

            if (filterelemeDurumlaris.Where(x => x.Tip == "2").First().Durum == "1")
            {
                var query = newDataTable.AsEnumerable().Where(r => r.Field<string>("DurumKodu") == "2");

                foreach (var row1 in query.ToList())
                    row1.Delete();

                newDataTable.AcceptChanges();

                //satirDurumlaris.RemoveAll(x => x.SatiriSil == "Y" && x.Durum == "2");
            }

            if (filterelemeDurumlaris.Where(x => x.Tip == "1").First().Durum == "1")
            {
                var query = newDataTable.AsEnumerable().Where(r => r.Field<string>("DurumKodu") == "1");

                foreach (var row1 in query.ToList())
                    row1.Delete();

                newDataTable.AcceptChanges();

                satirDurumlaris.RemoveAll(x => x.SatiriSil == "Y" && x.Durum == "1");
            }

            if (!sirala)
            {
                int z = 0;

                foreach (var item in satirDurumlaris.Where(x => x.SatiriSil != "Y"))
                {
                    item.Row = z;
                    z++;
                }
            }

            dataGridView3.DataSource = newDataTable;

            dataGridView3.Columns["Ürün Kodu"].Visible = false;
            dataGridView3.Columns["Istasyon"].Visible = false;
            dataGridView3.Columns["StageID"].Visible = false;
            dataGridView3.Columns["KatSayi"].Visible = false;
            dataGridView3.Columns["RotaKodu"].Visible = false;
            dataGridView3.Columns["DurumKodu"].Visible = false;
            dataGridView3.Columns["U_UVTVarsayilanDepo"].Visible = false;

            if (dataGridView3.Columns.Contains("btnBaslat") != true)
            {
                addButton(dataGridView3);
            }

            dataGridView3.AutoResizeRows();
            for (int i = 0; i <= dataGridView3.Columns.Count - 1; i++)
            {
                dataGridView3.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.AutoResizeRows();
            //foreach (DataGridViewColumn col in dataGridView3.Columns)
            //{
            //    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    col.HeaderCell.Style.Font = new Font("Bahnschrift", 11F, FontStyle.Bold, GraphicsUnit.Pixel);
            //}

            //dataGridView3.Columns["Parti No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dataGridView3.Columns["Miktar"].DefaultCellStyle.Format = "N2";
            dataGridView3.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.Columns["GerceklesenMiktar"].DefaultCellStyle.Format = "N2";
            dataGridView3.Columns["GerceklesenMiktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.Columns["KatSayi"].DefaultCellStyle.Format = "N2";
            dataGridView3.Columns["KatSayi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
            setFormatGrid(dataGridView3, 35);

            if (newDataTable.Rows.Count > 0)
            {
                List<int> stageids = newDataTable.AsEnumerable().Select(al => al.Field<int>("StageId")).Distinct().ToList();
                int max = stageids.Max();
                maxStageId = max;

                foreach (var item in satirDurumlaris.Where(x => x.SatiriSil != "Y"))
                {
                    if (item.Durum == "2")
                    {
                        dataGridView3.Rows[item.Row].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        dataGridView3.Rows[item.Row].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else if (item.Durum == "1")
                    {
                        dataGridView3.Rows[item.Row].DefaultCellStyle.BackColor = Color.IndianRed;
                    }
                    else if (item.Durum == "3")
                    {
                        dataGridView3.Rows[item.Row].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    }
                }

                // vScrollBar1.Maximum = dataGridView3.Rows.Count + 1;
                vScrollBar1.Maximum = dataGridView3.RowCount + 5;

                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    var BtnCell = (DataGridViewButtonCell)dataGridView3.Rows[i].Cells["btnTamamla"];
                    BtnCell.Value = "Tamamla";
                    if (dataGridView3.Rows[i].Height < 60)
                    {
                        dataGridView3.Rows[i].Height = 60;
                    }
                }

                var aaa11 = satirDurumlaris.GroupBy(c => new
                {
                    c.PartiNo,
                    c.Durum
                });

                foreach (var item in aaa11)
                {
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        string parti = dataGridView3.Rows[i].Cells["Parti No"].Value.ToString();

                        if (aaa11.Where(x => x.Key.PartiNo == parti).Count() == 1 && aaa11.Where(x => x.Key.PartiNo == parti).First().Key.Durum == "3")
                        {
                            var BtnCell = (DataGridViewButtonCell)dataGridView3.Rows[i].Cells["btnTamamla"];
                            BtnCell.Value = "Barkod";
                        }
                    }
                }
            }

            if (PartiNoKolonWidth == 0)
            {
                PartiNoKolonWidth = dataGridView3.Columns["Parti No"].Width;
            }

            //dataGridView3.Columns["Parti No"].Width = PartiNoKolonWidth + 20;
            dataGridView3.Columns["GerceklesenMiktar"].HeaderText = "Gerçek Mik";
            dataGridView3.Columns["Parti No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView3.Columns["Üretim Fiş No"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_type == "")
                {
                    _type = dataGridView1.Rows[e.RowIndex].Cells["Istasyon"].Value.ToString();
                }

                if (dataGridView1.Rows.Count > 0)
                {
                    SatirRenkle(dataGridView1.CurrentCell.RowIndex, dataGridView1);
                }
                Listele(e.RowIndex, "1");
            }
            catch (Exception ex)
            {
            }
        }

        private void SatirRenkle(int index, DataGridView dtg)
        {
            try
            {
                for (int i = 0; i < dtg.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        dtg.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        dtg.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        continue;
                    }

                    if (i % 2 == 0)
                        dtg.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                    else
                        dtg.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;

                    dtg.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
            }
            catch (Exception)
            {
            }
        }

        private List<SatirDurumlari> satirDurumlaris = new List<SatirDurumlari>();

        public class SatirDurumlari
        {
            public string PartiNo { get; set; }

            public int Row { get; set; }

            public string Durum { get; set; }

            public bool aktif { get; set; }

            public string rotaKodu { get; set; }

            public string rotaAdi { get; set; }

            public string SatiriSil { get; set; }
        }

        private List<FilterelemeDurumlari> filterelemeDurumlaris = new List<FilterelemeDurumlari>();

        public class FilterelemeDurumlari
        {
            public string Tip { get; set; }

            public string Durum { get; set; }

            public string TipAdi { get; set; }
        }

        private void filtrelemeDurumlariOlustur()
        {
            filterelemeDurumlaris.Add(new FilterelemeDurumlari { Tip = "1", Durum = "0", TipAdi = "BAŞLANMAYAN İŞLERİ GİZLE" });
            filterelemeDurumlaris.Add(new FilterelemeDurumlari { Tip = "2", Durum = "0", TipAdi = "DEVAM EDENLERİ GİZLE" });
            filterelemeDurumlaris.Add(new FilterelemeDurumlari { Tip = "3", Durum = "0", TipAdi = "TAMAMLANANLARI GİZLE" });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "TAMAMLANANLARI GİZLE")
            {
                filterelemeDurumlaris.Where(x => x.Tip == "3").First().Durum = "1";
                button4.Text = "TAMAMLANANLARI GÖSTER";
                Listele(dataGridView1.CurrentCell.RowIndex, "3");
            }
            else
            {
                filterelemeDurumlaris.Where(x => x.Tip == "3").First().Durum = "0";
                button4.Text = "TAMAMLANANLARI GİZLE";
                Listele(dataGridView1.CurrentCell.RowIndex, "4");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "BAŞLANMAYAN İŞLERİ GİZLE")
            {
                filterelemeDurumlaris.Where(x => x.Tip == "1").First().Durum = "1";
                button3.Text = "BAŞLANMAYAN İŞLERİ GÖSTER";
                Listele(dataGridView1.CurrentCell.RowIndex, "2");
            }
            else
            {
                filterelemeDurumlaris.Where(x => x.Tip == "1").First().Durum = "0";
                button3.Text = "BAŞLANMAYAN İŞLERİ GİZLE";
                Listele(dataGridView1.CurrentCell.RowIndex, "1");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "DEVAM EDENLERİ GİZLE")
            {
                filterelemeDurumlaris.Where(x => x.Tip == "2").First().Durum = "1";
                button2.Text = "DEVAM EDENLERİ GÖSTER";
                Listele(dataGridView1.CurrentCell.RowIndex, "2");
            }
            else
            {
                filterelemeDurumlaris.Where(x => x.Tip == "2").First().Durum = "0";
                button2.Text = "DEVAM EDENLERİ GİZLE";
                Listele(dataGridView1.CurrentCell.RowIndex, "2");
            }
        }

        //SCROLLBAR START
        private void dataGridView3_Scroll(object sender, ScrollEventArgs e)
        {
            vScrollBar1.Value = e.NewValue;
        }

        //SCROLLBAR END
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dataGridView3.FirstDisplayedScrollingRowIndex = e.NewValue;
            }
            catch (Exception ex)
            {
            }
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = e.NewValue;
            }
            catch (Exception ex)
            {
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            vScrollBar2.Value = e.NewValue;
        }

        private void btnGunlukTemizlik_Click(object sender, EventArgs e)
        {
            //YogurtProsesTakip_1 n2 = new YogurtProsesTakip_1(_type, kullanciid, "", "", urunTanimi, "", 0, tarih1, "");
            //n2.Show();
            //Close();

            //LorProsesTakip n2 = new LorProsesTakip(_type, kullanciid, "", "", urunTanimi, "", 0, tarih1);
            //n2.Show();
            //Close();

            //PastorizasyonProsesTakip_1 n2 = new PastorizasyonProsesTakip_1(_type, kullanciid, "", "", urunTanimi, "", 0, tarih1);
            //n2.Show();

            //AyranProsesTakip_3 n2 = new AyranProsesTakip_3(_type, kullanciid, "", "", 0, tarih1);
            //n2.Show();

            //YogurtProsesTakip_2 n2 = new YogurtProsesTakip_2(_type, kullanciid, 0, tarih1);
            //n2.Show();

            int row = dataGridView1.CurrentCell == null ? 0 : dataGridView1.CurrentCell.RowIndex;
            TemizlikTakip n = new TemizlikTakip(_type, kullanciid, row, initialWidth, initialHeight, tarih1);
            n.Show();
            Close();
        }

        private void btnGunlukAnaliz_Click(object sender, EventArgs e)
        {
            string val = _type;

            int row = dataGridView1.CurrentCell == null ? 0 : dataGridView1.CurrentCell.RowIndex;

            if (val == "IST005")
            {
                TazePeynirProsesTakip_2 n = new TazePeynirProsesTakip_2(_type, kullanciid, "", "", "", val, row, Width, Height, tarih1);
                n.Show();
                Close();
            }
            else if (val == "IST007")
            {
                TereyagProsesTakip_2 n = new TereyagProsesTakip_2(_type, kullanciid, "", "", "", val, row, Width, Height, tarih1);
                n.Show();
                Close();
            }
            else if (val == "IST002")
            {
                YogurtProsesTakip_2 n = new YogurtProsesTakip_2(_type, kullanciid, row, tarih1);
                n.Show();
                Close();
            }
            else if (val == "IST004")
            {
                TostPeynirProsesTakip_2 n = new TostPeynirProsesTakip_2(_type, kullanciid, "", "", "", val, row, Width, Height, tarih1);
                n.Show();
                Close();
            }
            else if (val == "IST001")
            {
                AyranProsesTakip_3 n = new AyranProsesTakip_3(_type, kullanciid, "", "", row, tarih1);
                n.Show();
                Close();
            }
            //AyranGunlukOzet_1 n2 = new AyranGunlukOzet_1(_type, kullanciid, "", "", 0, tarih1);
            //n2.Show();
        }
    }
}