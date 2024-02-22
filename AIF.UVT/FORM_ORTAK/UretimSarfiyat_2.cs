﻿using AIF.UVT.DatabaseLayer;
using AIF.UVT.Models;
using AIF.UVT.UVTService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIF.UVT
{
    public partial class UretimSarfiyat_2 : Form
    {
        //font start.
        public int initialWidth;

        public int initialHeight;
        public float initialFontSize;
        //font end

        public UretimSarfiyat_2(string _stageid, string _uretimFisNo, string _type, string _kullaniciid, double _partiKatSayi, string _partiNo, string _rotaKodu, string _urunGrubu, string _urunTanimi, int _row, int _width, int _height, string _tarih1)
        {
            stageid = _stageid;
            UretimFisNo = _uretimFisNo;
            type = _type;
            kullaniciid = _kullaniciid;
            partikatsayi = _partiKatSayi;
            partiNo = _partiNo;
            rotaKodu = _rotaKodu;
            urunGrubu = _urunGrubu;
            urunTanimi = _urunTanimi;
            row = _row;
            tarih1 = _tarih1;

            InitializeComponent();

            //font start
            AutoScaleMode = AutoScaleMode.None;

            initialWidth = _width;
            initialHeight = _height;

            initialFontSize = label1.Font.Size;
            label1.Resize += Form_Resize;
            //font end

            DataTable stageidTable = new DataTable();
            string sql = "Select  \"StageId\" from WOR4 where \"DocEntry\" = '" + UretimFisNo + "' and \"SeqNum\" = '" + stageid + "'";

            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
            {
                Connection.sql.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(stageidTable);

            Connection.sql.Close();

            if (stageidTable.Rows[0].ItemArray[0].ToString() != "")
            {
                stageid = stageidTable.Rows[0].ItemArray[0].ToString();
            }
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

            textBox1.Font = new Font(textBox1.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               textBox1.Font.Style);

            textBox2.Font = new Font(textBox2.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               textBox2.Font.Style);

            textBox3.Font = new Font(textBox3.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               textBox3.Font.Style);

            txtPartiNo.Font = new Font(txtPartiNo.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtPartiNo.Font.Style);

            txtUretimEmri.Font = new Font(txtUretimEmri.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtUretimEmri.Font.Style);

            txtUrunGrubu.Font = new Font(txtUrunGrubu.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtUrunGrubu.Font.Style);

            txtUrunTanimi.Font = new Font(txtUrunTanimi.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtUrunTanimi.Font.Style);

            button1.Font = new Font(button1.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button1.Font.Style);

            button2.Font = new Font(button2.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               button2.Font.Style);
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

        private string stageid = "";
        private string UretimFisNo = "";
        private string type = "";
        private string kullaniciid = "";
        private double partikatsayi = 0;
        private string partiNo = "";
        private string rotaKodu = "";
        private string urunGrubu = "";
        private string urunTanimi = "";
        private int row = 0;
        private string tarih1 = "";
        private DataTable dtSarfEdiecekMalzemeler = new DataTable();
        private SqlCommand cmd = null;
        public static string aktiviteEkraninaGit = "";
        private void SarfEdilecekMalzemeleriGetir()
        {
            double partikatsayisi = Convert.ToDouble(partikatsayi);

            string sql = "";

            if (Giris.mKodValue == "10B1C4")
            {
                sql = "Select *,(tbl.\"Planlanan Miktar\" - tbl.\"Gerçekleşen Miktar\") as Fark from (Select ItemCode as [Ürün Kodu],(Select ItemName from OITM as T1 where T1.ItemCode = T0.ItemCode) as [Ürün Tanımı], " + Environment.NewLine;
                sql += " ROUND((T0.PlannedQty / " + partikatsayisi.ToString().Replace(",", ".") + "),6) as [Planlanan Miktar],case when ROUND((T0.PlannedQty / " + partikatsayisi.ToString().Replace(",", ".") + "),6) > 0 then ISNULL((Select SUM(T3.\"Quantity\") from OIGE as T2 INNER JOIN IGE1 as T3 ON T2.\"DocEntry\" = T3.\"DocEntry\" where T2.\"U_BatchNumber\" = '" + partiNo + "' and T3.\"ItemCode\" = T0.\"ItemCode\" group by T3.\"ItemCode\"),0) else ISNULL((Select SUM(T3.\"Quantity\") * -1 from OIGN as T2 INNER JOIN IGN1 as T3 ON T2.\"DocEntry\" = T3.\"DocEntry\" where T2.\"U_BatchNumber\" = '" + partiNo + "' and T3.\"ItemCode\" = T0.\"ItemCode\" group by T3.\"ItemCode\"),0) end as [Gerçekleşen Miktar],wareHouse as \"Depo\", " + Environment.NewLine;
                sql += "T0.\"DocEntry\", " + Environment.NewLine;
                sql += " T0.LineNum as SiraNo from WOR1 as T0 where T0.StageId='" + stageid + "' and ItemType = '4' and T0.DocEntry = '" + UretimFisNo + "') as tbl";
            }

            if (Giris.mKodValue == "20R5DB")
            {
                sql = "Select *,(tbl.\"Planlanan Miktar\" - tbl.\"Gerçekleşen Miktar\") as Fark from (Select ItemCode as [Ürün Kodu],(Select ItemName from OITM as T1 where T1.ItemCode = T0.ItemCode) as [Ürün Tanımı], " + Environment.NewLine;
                sql += " ROUND((T0.PlannedQty / " + partikatsayisi.ToString().Replace(",", ".") + "),6) as [Planlanan Miktar],case when ROUND((T0.PlannedQty / " + partikatsayisi.ToString().Replace(",", ".") + "),6) > 0 then ISNULL((Select SUM(T3.\"Quantity\") from OIGE as T2 INNER JOIN IGE1 as T3 ON T2.\"DocEntry\" = T3.\"DocEntry\" where T2.\"U_BatchNumber\" = '" + partiNo + "' and T3.\"ItemCode\" = T0.\"ItemCode\" group by T3.\"ItemCode\"),0) else ISNULL((Select SUM(T3.\"Quantity\") * -1 from OIGN as T2 INNER JOIN IGN1 as T3 ON T2.\"DocEntry\" = T3.\"DocEntry\" where T2.\"U_BatchNumber\" = '" + partiNo + "' and T3.\"ItemCode\" = T0.\"ItemCode\" group by T3.\"ItemCode\"),0) end as [Gerçekleşen Miktar],wareHouse as \"Depo\",(SELECT \"WhsName\" from OWHS where T0.\"WareHouse\" = \"WhsCode\") as \"Depo Adı\", " + Environment.NewLine;
                sql += "T0.\"DocEntry\", " + Environment.NewLine;
                sql += " T0.LineNum as SiraNo from WOR1 as T0 where T0.StageId='" + stageid + "' and ItemType = '4' and T0.DocEntry = '" + UretimFisNo + "') as tbl";
            }

            cmd = new SqlCommand(sql, Connection.sql);

            if (Connection.sql.State != ConnectionState.Open)
            {
                Connection.sql.Open();
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dtSarfEdiecekMalzemeler);

            Connection.sql.Close();

            dtSarfEdiecekMalzemeler.Columns.Add("Miktar", typeof(double));

            dtgSarfMalzeme.DataSource = dtSarfEdiecekMalzemeler;

            #region sarf oran zorunlulugunda miktar null getirmek için eklendi
            //foreach (var row in dtSarfEdiecekMalzemeler.AsEnumerable().ToList())
            //{
            //row.SetField("Miktar", 0);
            //} 
            #endregion

            dtgSarfMalzeme.Columns["Planlanan Miktar"].DefaultCellStyle.Format = "N2";
            dtgSarfMalzeme.Columns["Planlanan Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgSarfMalzeme.Columns["Gerçekleşen Miktar"].DefaultCellStyle.Format = "N2";
            dtgSarfMalzeme.Columns["Gerçekleşen Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgSarfMalzeme.Columns["Miktar"].DefaultCellStyle.Format = "N2";
            dtgSarfMalzeme.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgSarfMalzeme.Columns["Fark"].DefaultCellStyle.Format = "N2";
            dtgSarfMalzeme.Columns["Fark"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgSarfMalzeme.Columns["Depo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dtgSarfMalzeme.Columns["DocEntry"].Visible = false;
            dtgSarfMalzeme.Columns["SiraNo"].Visible = false;
            dtgSarfMalzeme.Columns["Ürün Kodu"].ReadOnly = true;
            dtgSarfMalzeme.Columns["Ürün Tanımı"].ReadOnly = true;
            dtgSarfMalzeme.Columns["Planlanan Miktar"].ReadOnly = true;
            dtgSarfMalzeme.Columns["Gerçekleşen Miktar"].ReadOnly = true;
            dtgSarfMalzeme.Columns["Miktar"].ReadOnly = true;
            dtgSarfMalzeme.Columns["Depo"].ReadOnly = true;
            dtgSarfMalzeme.Columns["Fark"].ReadOnly = true;
            dtgSarfMalzeme.Columns["Ürün Kodu"].DisplayIndex = 0;
            dtgSarfMalzeme.Columns["Ürün Tanımı"].DisplayIndex = 1;
            dtgSarfMalzeme.Columns["Planlanan Miktar"].DisplayIndex = 2;
            dtgSarfMalzeme.Columns["Gerçekleşen Miktar"].DisplayIndex = 3;
            dtgSarfMalzeme.Columns["Miktar"].DisplayIndex = 4;
            dtgSarfMalzeme.Columns["Depo"].DisplayIndex = 5;
            if (Giris.mKodValue == "20R5DB")
            {
                dtgSarfMalzeme.Columns["Depo Adı"].DisplayIndex = 6;
            }
            dtgSarfMalzeme.Columns["Fark"].DisplayIndex = 7;

            if (Giris.mKodValue == "20R5DB")
            {
                dtgSarfMalzeme.Columns["Depo Adı"].Visible = true;
                dtgSarfMalzeme.Columns["Depo Adı"].ReadOnly = true;
                dtgSarfMalzeme.Columns["Depo Adı"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }

        private void UretimSarfiyat_2_Load(object sender, EventArgs e)
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
            SarfEdilecekMalzemeleriGetir();

            setFormatGrid(dtgSarfMalzeme);

            dtgSarfMalzeme.Columns["Miktar"].DefaultCellStyle.ForeColor = Color.LightGoldenrodYellow;

            for (int i = 0; i <= dtgSarfMalzeme.Columns.Count - 1; i++)
            {
                dtgSarfMalzeme.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dtgSarfMalzeme.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgSarfMalzeme.AutoResizeRows();
            if (Giris.mKodValue == "20R5DB")
            {
                dtgSarfMalzeme.Columns["Ürün Tanımı"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dtgSarfMalzeme.Columns["Ürün Tanımı"].Width = dtgSarfMalzeme.Width / 6;
            }

            for (int i = 0; i < dtgSarfMalzeme.Rows.Count; i++)
            {
                if (dtgSarfMalzeme.Rows[i].Height < 40)
                {
                    dtgSarfMalzeme.Rows[i].Height = 40;
                }
            }

            //setFormatGrid(dataGridView1);

            txtUrunGrubu.Text = urunGrubu;
            txtUrunTanimi.Text = urunTanimi;
            txtUretimEmri.Text = UretimFisNo;
            txtPartiNo.Text = partiNo;

            //dtgSarfMalzeme.Columns["Ürün Kodu"].DefaultCellStyle.ForeColor = Color.Red;
        }

        private void setFormatGrid(DataGridView dtg)
        {
            dtg.RowHeadersVisible = false;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dtg.Font = new System.Drawing.Font("Bahnschrift", 11.20F, FontStyle.Bold);

            int width = dtg.Width / dtg.Columns.Count;
            foreach (DataGridViewColumn col in dtg.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Bahnschrift", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
                col.Width = width - 8;
            }

            if (dtg.Name == "dtgSarfMalzeme")
            {
                for (int i = 0; i < dtg.Rows.Count; i++)
                {
                    double planlananMiktar = Convert.ToDouble(dtg.Rows[i].Cells["Planlanan Miktar"].Value);

                    if (planlananMiktar < 0)
                    {
                        dtg.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                    }
                    else
                    {
                        dtg.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    }
                }
            }
            else
            {
                for (int i = 0; i < dtg.Rows.Count; i++)
                {
                    double Miktar = Convert.ToDouble(dtg.Rows[i].Cells["Miktar"].Value);

                    if (Miktar == 0)
                    {
                        dtg.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                    }
                    else
                    {
                        dtg.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    }
                }
            }

            //for (int i = 0; i <= dtg.Rows.Count - 1; i++)
            //{
            //    if (i % 2 == 0)
            //        dtg.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
            //    else
            //        dtg.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;

            //    dtg.Rows[i].DefaultCellStyle.ForeColor = Color.White;
            //}
        }

        private DataTable dtParti = new DataTable();

        private void PartiGetir(string urunKodu, double TuketilecekMiktar, string depo)
        {
            DataTable dtPartiTemp = new DataTable();
            dtParti = new DataTable();
            dtParti.Columns.Add("UrunKodu", typeof(string));
            //dataTable.Columns.Add("PlanlananMiktar", typeof(double));
            //dataTable.Columns.Add("GerceklesenMiktar", typeof(double));
            dtParti.Columns.Add("Miktar", typeof(double));
            dtParti.Columns.Add("PartiNo", typeof(string));
            dtParti.Columns.Add("KulMiktar", typeof(double));

            DataRow drs = dtParti.NewRow();
            drs["UrunKodu"] = urunKodu;

            dtParti.Rows.Add(drs);

            dtPartiTemp = dtParti.Copy();
            dtPartiTemp.Rows.Clear();

            double tuketilenMiktar = 0;
            double kalanMiktar = TuketilecekMiktar;
            double sonuc = 0;

            foreach (DataRow item in dtParti.Rows)
            {
                string ItemCode = item["UrunKodu"].ToString();
                string sql = "";

                if (Giris.mKodValue == "10B1C4")
                {
                    sql = "Select * from(select BatchNum,MAX(DocDate) as \"DocDate\", ISNULL(sum(Quantity), 0) -ISNULL((select sum(Quantity) from IBT1 where ItemCode = '" + ItemCode + "' and Direction = '1' and WhsCode = '" + depo + "' and BatchNum = T0.\"BatchNum\" group by BatchNum),0)  as \"KulMiktar\" from IBT1 as T0 where ItemCode = '" + ItemCode + "' and Direction = '0' and WhsCode = '" + depo + "' group by BatchNum) as tbl where tbl.KulMiktar > 0 order by tbl.DocDate";
                }
                if (Giris.mKodValue == "20R5DB")
                {
                    #region 20221107 öncesi
                    //sql = "Select * from(select BatchNum, ISNULL(sum(Quantity), 0) -ISNULL((select sum(Quantity) from IBT1 where ItemCode = '" + ItemCode + "' and Direction = '1' and WhsCode = '" + depo + "' and BatchNum = T0.\"BatchNum\" group by BatchNum),0)  as \"KulMiktar\" from IBT1 as T0 where ItemCode = '" + ItemCode + "' and Direction = '0' and WhsCode = '" + depo + "' group by BatchNum) as tbl where tbl.KulMiktar > 0"; 
                    #endregion

                    sql = "select T0.\"DistNumber\" as \"BatchNum\", T1.\"Quantity\" AS \"KulMiktar\" from OBTN T0 inner join OBTQ T1 on T0.\"ItemCode\" = T1.\"ItemCode\" and T0.\"SysNumber\" = T1.\"SysNumber\" inner join OITM T2 on T0.\"ItemCode\" = T2.\"ItemCode\" where T1.\"Quantity\" > 0 and T0.\"ItemCode\" = '" + ItemCode + "' and T1.\"WhsCode\" = '" + depo + "' order by T1.\"WhsCode\", T0.\"DistNumber\" ";
                }

                SqlCommand cmd = null;
                cmd = new SqlCommand(sql, Connection.sql);

                if (Connection.sql.State != ConnectionState.Open)
                    Connection.sql.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow dr2 = dtPartiTemp.NewRow();
                    dr2["UrunKodu"] = item["UrunKodu"];

                    if (kalanMiktar > 0)
                    {
                        sonuc = kalanMiktar - Convert.ToDouble(dr["KulMiktar"]);

                        if (sonuc >= 0)
                        {
                            dr2["Miktar"] = Convert.ToDouble(dr["KulMiktar"]);
                            kalanMiktar = kalanMiktar - Convert.ToDouble(dr["KulMiktar"]);
                            tuketilenMiktar = tuketilenMiktar + Convert.ToDouble(dr2["Miktar"]);
                        }
                        else
                        {
                            dr2["Miktar"] = kalanMiktar;

                            tuketilenMiktar = tuketilenMiktar + Convert.ToDouble(dr2["Miktar"]);
                            kalanMiktar = 0;
                        }
                    }
                    else
                    {
                        dr2["Miktar"] = 0;
                    }
                    dr2["KulMiktar"] = dr["KulMiktar"];
                    dr2["PartiNo"] = dr["BatchNum"];

                    dtPartiTemp.Rows.Add(dr2);
                }

                dtParti = dtPartiTemp;

                dtgParti.DataSource = dtParti;

                dtgParti.Columns["UrunKodu"].HeaderText = "Ürün Kodu";
                dtgParti.Columns["KulMiktar"].DefaultCellStyle.Format = "N2";
                dtgParti.Columns["KulMiktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgParti.Columns["KulMiktar"].HeaderText = "Kullanılabilir Miktar";
                dtgParti.Columns["Miktar"].DefaultCellStyle.Format = "N2";
                dtgParti.Columns["Miktar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtgParti.Columns["PartiNo"].HeaderText = "Parti No";

                setFormatGrid(dtgParti);

                for (int i = 0; i <= dtgParti.Columns.Count - 1; i++)
                {
                    dtgParti.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                dtgParti.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dtgParti.AutoResizeRows();

                for (int i = 0; i < dtgParti.Rows.Count; i++)
                {
                    if (dtgParti.Rows[i].Height < 40)
                    {
                        dtgParti.Rows[i].Height = 40;
                    }
                }
            }
        }

        private void SatirRenkle(int index)
        {
            for (int i = 0; i < dtgSarfMalzeme.Rows.Count; i++)
            {
                if (i == index)
                {
                    dtgSarfMalzeme.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    dtgSarfMalzeme.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                    continue;
                }
                double planlananMiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[i].Cells["Planlanan Miktar"].Value);

                if (planlananMiktar < 0)
                {
                    dtgSarfMalzeme.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                }
                else
                {
                    dtgSarfMalzeme.Rows[i].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                }
            }
        }

        private void dtgSarfMalzeme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //var docentry = dtgSarfMalzeme.Rows[e.RowIndex].Cells["Üretim Fiş No"].Value;
            if (e.RowIndex != -1)
            {
                SatirRenkle(e.RowIndex);

                var senderGrid = (DataGridView)sender;

                #region depo adı için mkod ile ayrıldı -- otat
                if (Giris.mKodValue == "10B1C4")
                {
                    if (senderGrid.Columns[e.ColumnIndex].Name != "Depo")
                    {
                        System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");
                        var urunKodu = dtgSarfMalzeme.Rows[e.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                        var depo = dtgSarfMalzeme.Rows[e.RowIndex].Cells["Depo"].Value.ToString();
                        var miktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Planlanan Miktar"].Value);

                        if (miktar < 0) //Planlanan miktar 0 dan küçük ise 'Peynir altı suyu vb.'
                        {
                            dtgParti.DataSource = null;
                            SayiKlavyesiNew n1 = new SayiKlavyesiNew(null, dtgSarfMalzeme);
                            n1.ShowDialog();
                            if (SayiKlavyesiNew.GirisOk == "DEG")
                            {
                                dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = "-" + dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value;
                                var miktar1 = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value);
                                var fark2 = miktar - miktar1;

                                dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = fark2;



                                SayiKlavyesiNew.GirisOk = "";
                                return;
                            }
                            else
                            {
                                SayiKlavyesiNew.GirisOk = "";

                                return;
                            }
                        }

                        var Gerceklesenmiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Gerçekleşen Miktar"].Value);
                        double kullanilabilirMiktar = 0;
                        bool sifirmi = false;
                        miktar = miktar - Gerceklesenmiktar;

                        PartiGetir(urunKodu, miktar, depo);
                        #region sarf oran zorunluluğu yapıldığında yan griddeki partinin miktrını sayı klavyesine getirmesin diye kapatıldı
                        //if (dtgSarfMalzeme.Columns[e.ColumnIndex].Name == "Miktar")
                        //{
                        //if (dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value != DBNull.Value && Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value) == 0)
                        //{
                        //    sifirmi = true;
                        //    var partimiktar = dtParti.AsEnumerable().Sum(x => x.Field<double>("KulMiktar"));

                        //    if (miktar > 0)
                        //    {
                        //        if (miktar > partimiktar)
                        //            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(partimiktar, 2).ToString();
                        //        else
                        //            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(miktar, 2).ToString();
                        //    }
                        //    else
                        //    {
                        //        double sifir = 0;
                        //        dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(sifir, 2).ToString();
                        //    }

                        //    kullanilabilirMiktar = Math.Round(Convert.ToDouble(partimiktar), 2);
                        //}
                        //else
                        //{
                        //    var partimiktar = dtParti.AsEnumerable().Sum(x => x.Field<double>("KulMiktar"));

                        //    kullanilabilirMiktar = Math.Round(Convert.ToDouble(partimiktar), 2);
                        //} 
                        #endregion

                        var partimiktar = dtParti.AsEnumerable().Sum(x => x.Field<double>("KulMiktar"));

                        kullanilabilirMiktar = Math.Round(Convert.ToDouble(partimiktar), 2);

                        SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgSarfMalzeme);
                        n.ShowDialog();
                        if (SayiKlavyesiNew.GirisOk == "DEG")
                        {
                            if (dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value != DBNull.Value)
                            {

                                var GirilenMiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value);
                                miktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Planlanan Miktar"].Value);

                                //var gerceklesmisMiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Gerçekleşen Miktar"].Value);
                                //var girilenMiktarVeGerceklesmisMiktar = gerceklesmisMiktar + GirilenMiktar;

                                //double planlananYuzdeOnFazlasi = Math.Round(miktar + ((miktar * 10) / 100),2);

                                //if (girilenMiktarVeGerceklesmisMiktar > planlananYuzdeOnFazlasi)
                                //{
                                //    MessageBox.Show("Girilen miktar geçersizdir. Sapma payının dışındadır.");
                                //    GirilenMiktar = 0;
                                //    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(GirilenMiktar, 2).ToString();
                                //    dtgParti.DataSource = null;
                                //    return;
                                //}


                                if (GirilenMiktar > kullanilabilirMiktar)
                                {
                                    //MessageBox.Show(string.Format("Kullanılabilir miktar {0}'dır {1} değeri girilemez", Convert.ToString(kullanilabilirMiktar.ToString("N2"), cultureTR), Convert.ToString(GirilenMiktar.ToString("N2"), cultureTR)));

                                    CustomMsgBtn.Show(string.Format("Kullanılabilir miktar {0}'dır {1} değeri girilemez", Convert.ToString(kullanilabilirMiktar.ToString("N2"), cultureTR), Convert.ToString(GirilenMiktar.ToString("N2"), cultureTR)), "UYARI", "TAMAM");


                                    //dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(kullanilabilirMiktar.ToString(), cultureTR);
                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;
                                    var fark2 = miktar - kullanilabilirMiktar;

                                    HarcanacakMiktar = kullanilabilirMiktar;

                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = fark2;
                                    SayiKlavyesiNew.GirisOk = "";
                                    return;
                                }

                                HarcanacakMiktar = GirilenMiktar;

                                var fark = miktar - (GirilenMiktar);

                                dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = fark;

                                #region SARF ORANI HEASAPLAMA
                                //if (fark != 0)
                                //{
                                double sarfOran = 0;

                                string sql = "Select \"U_SarfOran\" FROM OITM where \"ItemCode\" = '" + urunKodu + "'";
                                cmd = new SqlCommand(sql, Connection.sql);

                                if (Connection.sql.State != ConnectionState.Open)
                                    Connection.sql.Open();

                                SqlDataAdapter sda2 = new SqlDataAdapter(cmd);
                                DataTable dt_Sorgu2 = new DataTable();
                                sda2.Fill(dt_Sorgu2);

                                Connection.sql.Close();
                                if (dt_Sorgu2.Rows[0][0] != null)
                                {
                                    sarfOran = dt_Sorgu2.Rows[0][0].ToString() == "" ? 0 : Convert.ToDouble(dt_Sorgu2.Rows[0][0]);

                                    if (sarfOran == 0)
                                    {
                                        if (miktar != GirilenMiktar)
                                        {
                                            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);


                                            var fark3 = miktar - (Gerceklesenmiktar);

                                            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);

                                            CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");

                                            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (sarfOran > 0 && sarfOran.ToString() != "")
                                        {
                                            if (Gerceklesenmiktar == 0)
                                            {
                                                double miktarinYuzdesi = (miktar * sarfOran) / 100;
                                                double toplamGirebilecekFazlaMiktar = miktar + miktarinYuzdesi;
                                                double toplamGirebilecekEksikMiktar = miktar - miktarinYuzdesi;

                                                if (GirilenMiktar > toplamGirebilecekFazlaMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;


                                                    return;
                                                }

                                                if (GirilenMiktar < toplamGirebilecekEksikMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                //Gerceklesenmiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Gerçekleşen Miktar"].Value);
                                                double miktarinYuzdesi = (miktar * sarfOran) / 100;
                                                double eklenenToplamMiktar = GirilenMiktar + Gerceklesenmiktar;

                                                double toplamGirebilecekFazlaMiktar = miktar + miktarinYuzdesi;
                                                double toplamGirebilecekEksikMiktar = miktar - miktarinYuzdesi;

                                                if (eklenenToplamMiktar > toplamGirebilecekFazlaMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);

                                                    if (eklenenToplamMiktar > 0)
                                                    {
                                                        var fark3 = miktar - (Gerceklesenmiktar);

                                                        dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);
                                                    }
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                                    return;
                                                }

                                                if (eklenenToplamMiktar < toplamGirebilecekEksikMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);

                                                    if (eklenenToplamMiktar > 0)
                                                    {
                                                        var fark3 = miktar - (Gerceklesenmiktar);

                                                        dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);
                                                    }
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                                    return;
                                                }

                                                if (eklenenToplamMiktar > 0)
                                                {
                                                    var fark3 = miktar - (eklenenToplamMiktar);

                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);
                                                }
                                            }
                                        }
                                    }

                                }

                                //}

                                #endregion SARF ORANI HEASAPLAMA

                                PartiGetir(urunKodu, GirilenMiktar, depo);

                                olustulacakPartilers.RemoveAll(x => x.KalemNo == urunKodu);
                                foreach (DataRow dr in dtParti.AsEnumerable().Where(x => x.Field<double>("Miktar") > 0))
                                {
                                    olustulacakPartilers.Add(new OlustulacakPartiler
                                    {
                                        KalemNo = dr["UrunKodu"].ToString(),
                                        Parti = dr["PartiNo"].ToString(),
                                        PartiMiktari = Convert.ToDouble(dr["Miktar"])
                                    });
                                }
                                SayiKlavyesiNew.GirisOk = "";
                            }
                            else
                            {
                                if (sifirmi)
                                {
                                    double sifir = 0;
                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(sifir, 2).ToString();
                                }
                            }

                            sifirmi = false;
                        }
                    }
                    else
                    {
                        var urunKodu = dtgSarfMalzeme.Rows[e.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                        var depo = dtgSarfMalzeme.Rows[dtgSarfMalzeme.CurrentCell.RowIndex].Cells["Depo"].Value.ToString();
                        string sql1 = "Select '' as \"Kod\",'' as \"Depolar\" ";
                        sql1 += " UNION ALL ";
                        sql1 += "Select T1.\"WhsCode\" as \"Kod\",T1.\"WhsName\" as \"Depolar\" from OITW as T0 INNER JOIN OWHS as T1 ON T0.\"WhsCode\" = T1.\"WhsCode\" where T0.\"ItemCode\" = '" + urunKodu + "' and \"U_DepoTipi\" = '01' ";

                        //01 süt deposu demektir. Yörüktede böyle olacaktır.

                        SelectList selectList = new SelectList(sql1, dtgSarfMalzeme, -1, e.ColumnIndex, _autoresizerow: false, _type: "DepoSec");
                        selectList.ShowDialog();
                        var depo2 = dtgSarfMalzeme.Rows[dtgSarfMalzeme.CurrentCell.RowIndex].Cells["Depo"].Value.ToString();

                        if (depo != depo2)
                        {
                            olustulacakPartilers.RemoveAll(x => x.KalemNo == urunKodu);
                            var arg = new DataGridViewCellEventArgs(3, e.RowIndex);
                            dtgSarfMalzeme_CellClick(dtgSarfMalzeme, arg);
                        }
                    }
                }
                #endregion

                #region depo adı için mkod ile ayrıldı -- yörük
                if (Giris.mKodValue == "20R5DB")
                {
                    if (senderGrid.Columns[e.ColumnIndex].Name != "Depo" && senderGrid.Columns[e.ColumnIndex].Name != "Depo Adı")
                    {
                        System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");
                        var urunKodu = dtgSarfMalzeme.Rows[e.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                        var depo = dtgSarfMalzeme.Rows[e.RowIndex].Cells["Depo"].Value.ToString();
                        var miktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Planlanan Miktar"].Value);

                        if (miktar < 0) //Planlanan miktar 0 dan küçük ise 'Peynir altı suyu vb.'
                        {
                            dtgParti.DataSource = null;
                            SayiKlavyesiNew n1 = new SayiKlavyesiNew(null, dtgSarfMalzeme);
                            n1.ShowDialog();
                            if (SayiKlavyesiNew.GirisOk == "DEG")
                            {
                                dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = "-" + dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value;
                                var miktar1 = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value);
                                var fark2 = miktar - miktar1;

                                dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = fark2;



                                SayiKlavyesiNew.GirisOk = "";
                                return;
                            }
                            else
                            {
                                SayiKlavyesiNew.GirisOk = "";

                                return;
                            }
                        }

                        var Gerceklesenmiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Gerçekleşen Miktar"].Value);
                        double kullanilabilirMiktar = 0;
                        bool sifirmi = false;
                        miktar = miktar - Gerceklesenmiktar;

                        PartiGetir(urunKodu, miktar, depo);
                        #region sarf oran zorunluluğu yapıldığında yan griddeki partinin miktrını sayı klavyesine getirmesin diye kapatıldı
                        //if (dtgSarfMalzeme.Columns[e.ColumnIndex].Name == "Miktar")
                        //{
                        //if (dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value != DBNull.Value && Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value) == 0)
                        //{
                        //    sifirmi = true;
                        //    var partimiktar = dtParti.AsEnumerable().Sum(x => x.Field<double>("KulMiktar"));

                        //    if (miktar > 0)
                        //    {
                        //        if (miktar > partimiktar)
                        //            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(partimiktar, 2).ToString();
                        //        else
                        //            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(miktar, 2).ToString();
                        //    }
                        //    else
                        //    {
                        //        double sifir = 0;
                        //        dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(sifir, 2).ToString();
                        //    }

                        //    kullanilabilirMiktar = Math.Round(Convert.ToDouble(partimiktar), 2);
                        //}
                        //else
                        //{
                        //    var partimiktar = dtParti.AsEnumerable().Sum(x => x.Field<double>("KulMiktar"));

                        //    kullanilabilirMiktar = Math.Round(Convert.ToDouble(partimiktar), 2);
                        //} 
                        #endregion

                        var partimiktar = dtParti.AsEnumerable().Sum(x => x.Field<double>("KulMiktar"));

                        kullanilabilirMiktar = Math.Round(Convert.ToDouble(partimiktar), 2);

                        SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgSarfMalzeme);
                        n.ShowDialog();
                        if (SayiKlavyesiNew.GirisOk == "DEG")
                        {
                            if (dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value != DBNull.Value)
                            {

                                var GirilenMiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value);
                                miktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Planlanan Miktar"].Value);

                                //var gerceklesmisMiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Gerçekleşen Miktar"].Value);
                                //var girilenMiktarVeGerceklesmisMiktar = gerceklesmisMiktar + GirilenMiktar;

                                //double planlananYuzdeOnFazlasi = Math.Round(miktar + ((miktar * 10) / 100),2);

                                //if (girilenMiktarVeGerceklesmisMiktar > planlananYuzdeOnFazlasi)
                                //{
                                //    MessageBox.Show("Girilen miktar geçersizdir. Sapma payının dışındadır.");
                                //    GirilenMiktar = 0;
                                //    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(GirilenMiktar, 2).ToString();
                                //    dtgParti.DataSource = null;
                                //    return;
                                //}


                                if (GirilenMiktar > kullanilabilirMiktar)
                                {
                                    //MessageBox.Show(string.Format("Kullanılabilir miktar {0}'dır {1} değeri girilemez", Convert.ToString(kullanilabilirMiktar.ToString("N2"), cultureTR), Convert.ToString(GirilenMiktar.ToString("N2"), cultureTR)));

                                    CustomMsgBtn.Show(string.Format("Kullanılabilir miktar {0}'dır {1} değeri girilemez", Convert.ToString(kullanilabilirMiktar.ToString("N2"), cultureTR), Convert.ToString(GirilenMiktar.ToString("N2"), cultureTR)), "UYARI", "TAMAM");


                                    //dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(kullanilabilirMiktar.ToString(), cultureTR);
                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;
                                    var fark2 = miktar - kullanilabilirMiktar;

                                    HarcanacakMiktar = kullanilabilirMiktar;

                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = fark2;
                                    SayiKlavyesiNew.GirisOk = "";
                                    return;
                                }

                                HarcanacakMiktar = GirilenMiktar;

                                var fark = miktar - (GirilenMiktar);

                                dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = fark;

                                #region SARF ORANI HEASAPLAMA
                                //if (fark != 0)
                                //{
                                double sarfOran = 0;

                                string sql = "Select \"U_SarfOran\" FROM OITM where \"ItemCode\" = '" + urunKodu + "'";
                                cmd = new SqlCommand(sql, Connection.sql);

                                if (Connection.sql.State != ConnectionState.Open)
                                    Connection.sql.Open();

                                SqlDataAdapter sda2 = new SqlDataAdapter(cmd);
                                DataTable dt_Sorgu2 = new DataTable();
                                sda2.Fill(dt_Sorgu2);

                                Connection.sql.Close();
                                if (dt_Sorgu2.Rows[0][0] != null)
                                {
                                    sarfOran = dt_Sorgu2.Rows[0][0].ToString() == "" ? 0 : Convert.ToDouble(dt_Sorgu2.Rows[0][0]);

                                    if (sarfOran == 0)
                                    {
                                        if (miktar != GirilenMiktar)
                                        {
                                            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);


                                            var fark3 = miktar - (Gerceklesenmiktar);

                                            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);

                                            CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");

                                            dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (sarfOran > 0 && sarfOran.ToString() != "")
                                        {
                                            if (Gerceklesenmiktar == 0)
                                            {
                                                double miktarinYuzdesi = (miktar * sarfOran) / 100;
                                                double toplamGirebilecekFazlaMiktar = miktar + miktarinYuzdesi;
                                                double toplamGirebilecekEksikMiktar = miktar - miktarinYuzdesi;

                                                if (GirilenMiktar > toplamGirebilecekFazlaMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;


                                                    return;
                                                }

                                                if (GirilenMiktar < toplamGirebilecekEksikMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                //Gerceklesenmiktar = Convert.ToDouble(dtgSarfMalzeme.Rows[e.RowIndex].Cells["Gerçekleşen Miktar"].Value);
                                                double miktarinYuzdesi = (miktar * sarfOran) / 100;
                                                double eklenenToplamMiktar = GirilenMiktar + Gerceklesenmiktar;

                                                double toplamGirebilecekFazlaMiktar = miktar + miktarinYuzdesi;
                                                double toplamGirebilecekEksikMiktar = miktar - miktarinYuzdesi;

                                                if (eklenenToplamMiktar > toplamGirebilecekFazlaMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);

                                                    if (eklenenToplamMiktar > 0)
                                                    {
                                                        var fark3 = miktar - (Gerceklesenmiktar);

                                                        dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);
                                                    }
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                                    return;
                                                }

                                                if (eklenenToplamMiktar < toplamGirebilecekEksikMiktar)
                                                {
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(0.ToString("N2"), cultureTR);

                                                    if (eklenenToplamMiktar > 0)
                                                    {
                                                        var fark3 = miktar - (Gerceklesenmiktar);

                                                        dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);
                                                    }
                                                    CustomMsgBtn.Show("Miktar girişi sarf için uyumsuzdur. Lütfen kontrol ediniz.", "UYARI", "TAMAM");
                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = DBNull.Value;

                                                    return;
                                                }

                                                if (eklenenToplamMiktar > 0)
                                                {
                                                    var fark3 = miktar - (eklenenToplamMiktar);

                                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Fark"].Value = Convert.ToString(fark3.ToString(), cultureTR);
                                                }
                                            }
                                        }
                                    }

                                }

                                //}

                                #endregion SARF ORANI HEASAPLAMA

                                PartiGetir(urunKodu, GirilenMiktar, depo);

                                olustulacakPartilers.RemoveAll(x => x.KalemNo == urunKodu);
                                foreach (DataRow dr in dtParti.AsEnumerable().Where(x => x.Field<double>("Miktar") > 0))
                                {
                                    olustulacakPartilers.Add(new OlustulacakPartiler
                                    {
                                        KalemNo = dr["UrunKodu"].ToString(),
                                        Parti = dr["PartiNo"].ToString(),
                                        PartiMiktari = Convert.ToDouble(dr["Miktar"])
                                    });
                                }
                                SayiKlavyesiNew.GirisOk = "";
                            }
                            else
                            {
                                if (sifirmi)
                                {
                                    double sifir = 0;
                                    dtgSarfMalzeme.Rows[e.RowIndex].Cells["Miktar"].Value = Math.Round(sifir, 2).ToString();
                                }
                            }

                            sifirmi = false;
                        }
                    }
                    else
                    {
                        #region depo seçimi yörük için kaldırılmıştır.20220418
                        //var urunKodu = dtgSarfMalzeme.Rows[e.RowIndex].Cells["Ürün Kodu"].Value.ToString();
                        //var depo = dtgSarfMalzeme.Rows[dtgSarfMalzeme.CurrentCell.RowIndex].Cells["Depo"].Value.ToString();
                        //string sql1 = "Select '' as \"Kod\",'' as \"Depolar\" ";
                        //sql1 += " UNION ALL ";
                        //sql1 += "Select T1.\"WhsCode\" as \"Kod\",T1.\"WhsName\" as \"Depolar\" from OITW as T0 INNER JOIN OWHS as T1 ON T0.\"WhsCode\" = T1.\"WhsCode\" where T0.\"ItemCode\" = '" + urunKodu + "' and \"U_DepoTipi\" = '01' ";

                        ////01 süt deposu demektir. Yörüktede böyle olacaktır.

                        //SelectList selectList = new SelectList(sql1, dtgSarfMalzeme, -1, e.ColumnIndex, _autoresizerow: false, _type: "DepoSec");
                        //selectList.ShowDialog();
                        //var depo2 = dtgSarfMalzeme.Rows[dtgSarfMalzeme.CurrentCell.RowIndex].Cells["Depo"].Value.ToString();

                        //if (depo != depo2)
                        //{
                        //    olustulacakPartilers.RemoveAll(x => x.KalemNo == urunKodu);
                        //    var arg = new DataGridViewCellEventArgs(3, e.RowIndex);
                        //    dtgSarfMalzeme_CellClick(dtgSarfMalzeme, arg);
                        //} 
                        #endregion
                    }
                }
                #endregion


            }
        }

        public static List<OlustulacakPartiler> olustulacakPartilers = new List<OlustulacakPartiler>();

        private void dtgSarfMalzeme_Sorted(object sender, EventArgs e)
        {
            setFormatGrid(dtgSarfMalzeme);

            for (int i = 0; i <= dtgSarfMalzeme.Columns.Count - 1; i++)
            {
                dtgSarfMalzeme.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dtgSarfMalzeme.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgSarfMalzeme.AutoResizeRows();

            for (int i = 0; i < dtgSarfMalzeme.Rows.Count; i++)
            {
                if (dtgSarfMalzeme.Rows[i].Height < 40)
                {
                    dtgSarfMalzeme.Rows[i].Height = 40;
                }
            }
        }

        private void dtgParti_Sorted(object sender, EventArgs e)
        {
            setFormatGrid(dtgParti);

            for (int i = 0; i <= dtgParti.Columns.Count - 1; i++)
            {
                dtgParti.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dtgParti.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dtgParti.AutoResizeRows();

            for (int i = 0; i < dtgParti.Rows.Count; i++)
            {
                if (dtgParti.Rows[i].Height < 40)
                {
                    dtgParti.Rows[i].Height = 40;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //BanaAitİsler n = new BanaAitİsler(type, kullaniciid, row, initialWidth, initialHeight, tarih1);
            //n.Show();
            type = "";
            aktiviteEkraninaGit = "";
            Close();
        }

        private double HarcanacakMiktar = 0;

        private void dtgParti_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                System.IFormatProvider cultureTR = new System.Globalization.CultureInfo("tr-TR");
                var grid = (sender as DataGridView);
                if (e.RowIndex != -1 && e.ColumnIndex == 1)
                {
                    string urunKodu = (grid.Rows[e.RowIndex]).Cells["UrunKodu"].Value.ToString() == "" ? "" : Convert.ToString((grid.Rows[e.RowIndex]).Cells["UrunKodu"].Value);

                    var miktar = (grid.Rows[e.RowIndex]).Cells["Miktar"].Value.ToString() == "" ? 0 : Convert.ToDouble((grid.Rows[e.RowIndex]).Cells["Miktar"].Value);
                    var kullanilanMiktar = (grid.Rows[e.RowIndex]).Cells["KulMiktar"].Value.ToString() == "" ? 0 : Convert.ToDouble((grid.Rows[e.RowIndex]).Cells["KulMiktar"].Value);

                    if (miktar > kullanilanMiktar)
                    {
                        CustomMsgBtn.Show("Kullanılabilir miktardan daha fazla miktar girilemez.", "UYARI", "TAMAM");
                        grid.Rows[e.RowIndex].Cells["Miktar"].Value = kullanilanMiktar;
                        return;
                    }

                    double partiMiktari = dtParti.AsEnumerable().Where(x => x.Field<double>("Miktar") > 0).Sum(y => y.Field<double>("Miktar"));

                    if (partiMiktari > HarcanacakMiktar)
                    {
                        double kalan = miktar - (partiMiktari - HarcanacakMiktar);

                        if (kalan == 0)
                        {
                            grid.Rows[e.RowIndex].Cells["Miktar"].Value = Convert.ToString(kalan.ToString("N2"), cultureTR);
                            CustomMsgBtn.Show("Parti tüketimi tamamlanmıştır. Daha fazla miktar girilemez.", "UYARI", "TAMAM");
                        }
                        else
                        {
                            grid.Rows[e.RowIndex].Cells["Miktar"].Value = miktar - (partiMiktari - HarcanacakMiktar);
                            //MessageBox.Show(string.Format("Harcanacak miktar {0}'dır. {1} değeri girilemez. Kalan Miktar {2}'dır Otomatik olarak getirildi.", Convert.ToString(HarcanacakMiktar.ToString("N2"), cultureTR), Convert.ToString(partiMiktari.ToString("N2"), cultureTR), Convert.ToString(kalan.ToString("N2"), cultureTR)));

                            CustomMsgBtn.Show(string.Format("Harcanacak miktar {0}'dır. {1} değeri girilemez. Kalan Miktar {2}'dır Otomatik olarak getirildi.", Convert.ToString(HarcanacakMiktar.ToString("N2"), cultureTR), Convert.ToString(partiMiktari.ToString("N2"), cultureTR), Convert.ToString(kalan.ToString("N2"), cultureTR)), "UYARI", "TAMAM");
                        }
                        return;
                    }

                    dtTemp = dtParti.AsEnumerable().Where(x => x.Field<string>("UrunKodu") == urunKodu).CopyToDataTable();
                    hesapla(miktar);

                    olustulacakPartilers.RemoveAll(x => x.KalemNo == urunKodu);
                    foreach (DataRow dr in dtParti.AsEnumerable().Where(x => x.Field<double>("Miktar") > 0))
                    {
                        olustulacakPartilers.Add(new OlustulacakPartiler
                        {
                            KalemNo = dr["UrunKodu"].ToString(),
                            Parti = dr["PartiNo"].ToString(),
                            PartiMiktari = Convert.ToDouble(dr["Miktar"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMsgBtn.Show("İşlem sırasında hata oluştu. Hata Kodu: PRT003 " + ex.Message, "UYARI", "TAMAM");
            }
        }

        private DataTable dtTemp = new DataTable();

        private bool hesapla(double _tuketilecekmiktar)
        {
            try
            {
                double sum = 0;
                double tuketilecekmiktar = _tuketilecekmiktar;
                double tuketilenMiktar = 0;
                double sonuc = 0;
                double kalan = 0;
                foreach (DataRow dr in dtTemp.Rows)
                {
                    sum += Convert.ToDouble(dr["Miktar"]);
                }

                if (sum > tuketilecekmiktar)
                {
                }
                else
                {
                    //sonuc = tuketilecekmiktar - sum;
                    //textBox2.Text = sum.ToString("N2");
                    //tuketilenMiktar = Convert.ToDouble(textBox2.Text);

                    kalan = tuketilecekmiktar - tuketilenMiktar;

                    //textBox3.Text = kalan.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                CustomMsgBtn.Show("Parti ekranında hesaplama yapılırken hata oluştu. Hata kodu: PRT004 " + ex.Message, "UYARI", "TAMAM");
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                #region satırlara bakarak sarf edilmemiş ürünü bularak uyarı verecek.aksi halde geri gidemeyecek
                //double satirdakiMiktar = 999999;
                int satirsayisiGerceklesen = 0;
                int satirsayisiMiktar = 0;

                #region kontrolden sonra ikinci kez sarf ekranını açmasın diye eklendi
                List<SatirVerisi> satirVerisis = new List<SatirVerisi>();
                int sira = 1;
                #endregion

                for (int i = 0; i < dtgSarfMalzeme.RowCount; i++)
                {
                    if (Convert.ToDouble(dtgSarfMalzeme.Rows[i].Cells["Gerçekleşen Miktar"].Value) == 0)
                    {
                        satirsayisiGerceklesen++;
                    }

                    if (dtgSarfMalzeme.Rows[i].Cells["Miktar"].Value == DBNull.Value)
                    {
                        satirsayisiMiktar++;
                    }

                    #region kontrolden sonra ikinci kez sarf ekranını açmasın diye eklendi
                    if (Convert.ToDouble(dtgSarfMalzeme.Rows[i].Cells["Gerçekleşen Miktar"].Value) == 0)
                    {
                        if (!string.IsNullOrEmpty(dtgSarfMalzeme.Rows[i].Cells["Gerçekleşen Miktar"].Value.ToString()))
                        {
                            #region SARF ORANI HEASAPLAMA - SATIRDAKİ GERÇEKLEŞEN MİKTAR 0 OLDUĞUNDA İŞLEMİ BİTİREBİLMESİ İÇİN SARF ORANI 100 VE ÜZERİ OLMASINA BAKILDI
                            double sarfOran = 0;

                            string sql = "Select \"U_SarfOran\" FROM OITM where \"ItemCode\" = '" + dtgSarfMalzeme.Rows[i].Cells["Ürün Kodu"].Value + "'";
                            cmd = new SqlCommand(sql, Connection.sql);

                            if (Connection.sql.State != ConnectionState.Open)
                                Connection.sql.Open();

                            SqlDataAdapter sda2 = new SqlDataAdapter(cmd);
                            DataTable dt_Sorgu2 = new DataTable();
                            sda2.Fill(dt_Sorgu2);

                            Connection.sql.Close();
                            if (dt_Sorgu2.Rows[0][0] != null)
                            {
                                sarfOran = dt_Sorgu2.Rows[0][0].ToString() == "" ? 0 : Convert.ToDouble(dt_Sorgu2.Rows[0][0]);

                                if (!string.IsNullOrEmpty(sarfOran.ToString()) && sarfOran < 100)
                                {
                                    if (dtgSarfMalzeme.Rows[i].Cells["Miktar"].Value == DBNull.Value || Convert.ToDouble(dtgSarfMalzeme.Rows[i].Cells["Miktar"].Value) == 0)
                                    { 
                                        satirVerisis.Add(new SatirVerisi
                                        {
                                            sira = sira++,
                                            urunkodu = dtgSarfMalzeme.Rows[i].Cells["Ürün Kodu"].Value.ToString(),
                                            sarforan = sarfOran,
                                        });
                                    } 
                                }
                                else //sarf oran 100 veya daha büyük ise, istediği gibi 0 miktar girişi yapabileceğinden listeye ekleme yapıldı.bu şekilde aktivite ekranına gdecek
                                {
                                    satirVerisis.Add(new SatirVerisi
                                    {
                                        sira = sira++,
                                        urunkodu = dtgSarfMalzeme.Rows[i].Cells["Ürün Kodu"].Value.ToString(),
                                        sarforan = sarfOran,
                                    });
                                }
                            }
                            #endregion SARF ORANI HEASAPLAMA - SATIRDAKİ GERÇEKLEŞEN MİKTAR 0 OLDUĞUNDA İŞLEMİ BİTİREBİLMESİ İÇİN SARF ORANI 100 VE ÜZERİ OLMASINA BAKILDI
                        }
                    }
                    #endregion kontrolden sonra ikinci kez sarf ekranını açmasın diye eklendi
                }

                #region kontrolden sonra ikinci kez sarf ekranını açmasın diye eklendi
                if (satirVerisis.Where(x => x.sarforan < 100).Count() > 0)
                {
                    aktiviteEkraninaGit = "";
                }
                else
                {
                    aktiviteEkraninaGit = "Ok";
                } 
                #endregion kontrolden sonra ikinci kez sarf ekranını açmasın diye eklendi


                if (dtgSarfMalzeme.Rows.Count == satirsayisiGerceklesen)
                {
                    if (satirsayisiMiktar > 0)
                    {
                        CustomMsgBtn.Show("Lütfen sarf miktarlarını kontrol ediniz.", "UYARI", "TAMAM");
                        return;
                    }
                }
                //foreach (DataGridViewRow row in dtgSarfMalzeme.Rows)
                //{
                //    if (row.Cells["Miktar"].Value.ToString() == "")
                //    {

                //        MessageBox.Show((row.Index + 1) + ". Satır girişi yapınız.");
                //        return;

                //    }
                //}

                #endregion

                if (dtgSarfMalzeme.Rows.Count == satirsayisiGerceklesen) //satırların hepsi sarf edildiğinde onaylanabilir
                {
                    UVTServiceSoapClient client = new UVTServiceSoapClient();
                    bool belgeOlusacakMi = false;
                    bool isOk = false;
                    List<InventoryGenExist> inventoryGenExist = new List<InventoryGenExist>();
                    List<Parti> parti = new List<Parti>();

                    foreach (DataRow dr in dtSarfEdiecekMalzemeler.AsEnumerable().Where(x => x.Field<double>("Miktar") > 0))
                    {
                        //if (dr["DocEntry"] != DBNull.Value) //Daah önce yaratılmış olan çıkışların satır numarası gelmediğinden bu kontrol yazıldı.
                        //{
                        //    continue;
                        //}
                        int uretimSiparisNo = Convert.ToInt32(UretimFisNo);
                        string KalemKodu = dr["Ürün Kodu"].ToString();
                        int SatirNo = Convert.ToInt32(dr["SiraNo"]);
                        var partino = olustulacakPartilers.Where(x => x.KalemNo == KalemKodu).ToList();
                        double miktar = Convert.ToDouble(dr["Miktar"]);
                        string depoKodu = dr["Depo"].ToString();

                        if (miktar < 0)
                        {
                            continue;
                        }

                        foreach (var item in partino)
                        {
                            parti.Add(new Parti { PartiNo = item.Parti, PartikMiktar = item.PartiMiktari });
                        }

                        inventoryGenExist.Add(new InventoryGenExist
                        {
                            UretimSiparisi = uretimSiparisNo,
                            SatirNumarasi = SatirNo,
                            Miktar = miktar,
                            Parti = parti.ToArray(),
                            PartiNo = partiNo,
                            RotaKodu = rotaKodu,
                            DepoKodu = depoKodu
                        });

                        parti = new List<Parti>();
                    }

                    if (inventoryGenExist.Count > 0)
                    {
                        belgeOlusacakMi = true;
                        var resp = client.AddInventoryGenExist(inventoryGenExist.ToArray(), Giris.dbName, Giris.mKodValue);
                        if (resp.Value == 0)
                        {
                            isOk = true;
                            CustomMsgBtn.Show(resp.Description, "UYARI", "TAMAM"); 
                        }
                        else
                        {
                            CustomMsgBtn.Show(resp.Description, "UYARI", "TAMAM");
                            return;
                        }
                        //CustomMsgBtn.Show(resp.Description, "UYARI", "TAMAM");// else kısmı yoktu.üretim cıkıs yapılırken hata aldıgında devam ediyordu.else eklendi
                    }
                    List<InventoryGenEntry> inventoryGenEntries = new List<InventoryGenEntry>();
                    foreach (DataRow dr in dtSarfEdiecekMalzemeler.AsEnumerable().Where(x => x.Field<double>("Miktar") < 0))
                    {
                        //if (dr["DocEntry"] != DBNull.Value) //Daah önce yaratılmış olan çıkışların satır numarası gelmediğinden bu kontrol yazıldı.
                        //{
                        //    continue;
                        //}
                        int uretimSiparisNo = Convert.ToInt32(UretimFisNo);
                        string KalemKodu = dr["Ürün Kodu"].ToString();
                        int SatirNo = Convert.ToInt32(dr["SiraNo"]);
                        var partino = olustulacakPartilers.Where(x => x.UretimSiparisNo == UretimFisNo && x.SatirNo == SatirNo).ToList();
                        double miktar = Convert.ToDouble(dr["Miktar"]);
                        string depoKodu = dr["Depo"].ToString();
                        double rayicbedel = 0;

                        #region KALEM ÜZERİNDE RAYİÇ BDEL VAR İSE ÜRETİMDEN GİRİŞ SATIRINDAKİ BİRİM FİYATA GÖNDERİR
                        SqlDataAdapter sda_RayicBedel = new SqlDataAdapter(cmd);
                        DataTable dt_RayicBedel = new DataTable();
                        string rayicBedelSql = "";

                        rayicBedelSql = "Select \"U_RayicBedel\" from \"OITM\" where \"ItemCode\" = '" + KalemKodu + "'";

                        cmd = new SqlCommand(rayicBedelSql, Connection.sql);

                        if (Connection.sql.State != ConnectionState.Open)
                        {
                            Connection.sql.Open();
                        }
                        sda_RayicBedel = new SqlDataAdapter(cmd);
                        sda_RayicBedel.Fill(dt_RayicBedel);

                        if (dt_RayicBedel.Rows[0][0] != null && dt_RayicBedel.Rows[0][0].ToString() != "")
                        {
                            rayicbedel = parseNumber.parservalues<double>(dt_RayicBedel.Rows[0][0].ToString());
                        }
                        #endregion
                        int sktgun = 0;
                        #region Kalem Anaverisi üzerinde SKT Gün alanını üretimden giriş çıkış durumlarında geçerlilik sonu alanına basmak için kullanılır
                        SqlDataAdapter sda_SKTGun = new SqlDataAdapter(cmd);
                        DataTable dt_SKTGun = new DataTable();
                        string sKTGunSql = "";

                        sKTGunSql = "Select \"U_SKTGun\" from \"OITM\" where \"ItemCode\" = '" + KalemKodu + "'";

                        cmd = new SqlCommand(sKTGunSql, Connection.sql);

                        if (Connection.sql.State != ConnectionState.Open)
                        {
                            Connection.sql.Open();
                        }
                        sda_SKTGun = new SqlDataAdapter(cmd);
                        sda_SKTGun.Fill(dt_SKTGun);

                        if (dt_SKTGun.Rows[0][0] != null && dt_SKTGun.Rows[0][0].ToString() != "")
                        {
                            sktgun = Convert.ToInt32(dt_SKTGun.Rows[0][0]);
                        }
                        #endregion
                        if (miktar >= 0)
                        {
                            continue;
                        }

                        inventoryGenEntries.Add(new InventoryGenEntry
                        {
                            Parti = partiNo,
                            PartiMiktar = miktar,
                            Miktar = miktar,
                            UretimSiparisi = uretimSiparisNo,
                            SiraNo = SatirNo.ToString(),
                            RotaKodu = rotaKodu,
                            RayicBedel = rayicbedel == -1 ? 0 : rayicbedel,
                            DepoKodu = depoKodu,
                            SKTGun = sktgun,
                            UretimBaslangicTarihi = tarih1
                        });
                    }
                    if (inventoryGenEntries.Count > 0)
                    {
                        belgeOlusacakMi = true;
                        var resp = client.AddInventoryGenEntry(inventoryGenEntries.ToArray(), Giris.dbName, Giris.mKodValue);

                        if (resp.Value != 0 && isOk)
                        {
                            isOk = false;
                        }
                        else if (resp.Value == 0)
                        {
                            isOk = true;
                        }
                        CustomMsgBtn.Show(resp.Description, "UYARI", "TAMAM");

                    }
                    if (isOk)
                    {
                        button2.PerformClick();
                    }

                    if (!belgeOlusacakMi)
                    {
                        CustomMsgBtn.Show("Aktarılacak ürün bulunamadı", "UYARI", "TAMAM");

                    }
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                CustomMsgBtn.Show("İşlem sırasında hata oluştu. Hata Kodu: URTSRF004 " + ex.Message, "UYARI", "TAMAM");
            }
        }

        private class SatirVerisi
        {
            public int sira { get; set; }
            public string urunkodu { get; set; }
            public double sarforan { get; set; }
        }
        private void dtgParti_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    if (dtgParti.Columns[e.ColumnIndex].Name == "Miktar")
            //    {
            //        SayiKlavyesiNew n = new SayiKlavyesiNew(null, dtgParti);
            //        n.ShowDialog();
            //        double kullanilabilirMiktar = 0;
            //        kullanilabilirMiktar = Convert.ToDouble(dtgParti.Rows[e.RowIndex].Cells["KulMiktar"].Value);

            //        //PartiGetir(urunKodu, GirilenMiktar, depo);
            //    }
            //}
        }
    }
}