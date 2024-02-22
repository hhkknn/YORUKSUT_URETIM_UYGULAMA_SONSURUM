using AIF.UVT.DatabaseLayer;
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
    public partial class AtanmisIsler : Form
    {
        private Giris baseForm = null;

        //font start
        public int initialWidth;

        public int initialHeight;
        public float initialFontSize;
        //font end.

        public AtanmisIsler(string kullaniciKodu, Giris giris, string _KullaniciId, int _width, int _height)
        {
            kadi = kullaniciKodu;
            baseForm = giris;
            KullaniciId = _KullaniciId;

            InitializeComponent();

            //font start
            AutoScaleMode = AutoScaleMode.None;

            initialWidth = _width;
            initialHeight = _height;

            initialFontSize = label1.Font.Size;
            label1.Resize += Form_Resize;

            //initialFontSize = label2.Font.Size;
            //label2.Resize += Form_Resize;

            //initialFontSize = lblHosGeldiniz.Font.Size;
            //lblHosGeldiniz.Resize += Form_Resize;

            //initialFontSize = dateTimePicker1.Font.Size;
            //dateTimePicker1.Resize += Form_Resize;

            //initialFontSize = txtSaat.Font.Size;
            //txtSaat.Resize += Form_Resize;

            //initialFontSize = button4.Font.Size;
            //button4.Resize += Form_Resize;

            //initialFontSize = button5.Font.Size;
            //button5.Resize += Form_Resize;

            //font end
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

            lblHosGeldiniz.Font = new Font(lblHosGeldiniz.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               lblHosGeldiniz.Font.Style);

            dateTimePicker1.Font = new Font(dateTimePicker1.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               dateTimePicker1.Font.Style);

            dateTimePicker2.Font = new Font(dateTimePicker2.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               dateTimePicker2.Font.Style);

            txtSaat.Font = new Font(txtSaat.Font.FontFamily, initialFontSize *
               (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
               txtSaat.Font.Style);

            //button4.Font = new Font(button4.Font.FontFamily, initialFontSize *
            //    (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
            //    button4.Font.Style);

            button5.Font = new Font(button5.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                button5.Font.Style);

            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                richTextBox1.Font.Style);

            btnOncekiGun.Font = new Font(btnOncekiGun.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                btnOncekiGun.Font.Style);

            btnSonrakiGun.Font = new Font(btnSonrakiGun.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                btnSonrakiGun.Font.Style);

            btnBugun.Font = new Font(btnBugun.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                btnBugun.Font.Style);
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

        private string kadi = "";
        private string KullaniciId = "";
        private int width = 0;
        private int height = 0;

        private void AtanmisIsler_Load(object sender, EventArgs e)
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
                //return;
                Library.Helper n = new Library.Helper();
                n.SetAllControlsFont(Controls);
                //lblHosGeldiniz.Text = lblHosGeldiniz.Text + " " + kadi;
                //lblHosGeldiniz.Font = new Font("Arial", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
                txtSaat.Text = DateTime.Now.ToString("HH:mm");

                if (baslangicTarihi != "" && bitisTarihi != "")
                {
                    IstasyonlariListele(baslangicTarihi, bitisTarihi);

                    DateTime _dt = new DateTime(Convert.ToInt32(baslangicTarihi.Substring(0, 4)), Convert.ToInt32(baslangicTarihi.Substring(4, 2)), Convert.ToInt32(baslangicTarihi.Substring(6, 2)));
                    dateTimePicker1.Value = _dt;

                    _dt = new DateTime(Convert.ToInt32(bitisTarihi.Substring(0, 4)), Convert.ToInt32(bitisTarihi.Substring(4, 2)), Convert.ToInt32(bitisTarihi.Substring(6, 2)));

                    dateTimePicker2.Value = _dt;
                }
                else
                {
                    IstasyonlariListele(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                CustomMsgBtn.Show("Atanmış İşler ekranı açılırken hata oluştu. AT001 " + ex.Message, "UYARI", "TAMAM");
            }
        }

        public static bool Joker = false;

        private void IstasyonlariListele(string tarih1, string tarih2)
        {
            try
            {
                Joker = false;

                SqlCommand cmd = null;
                DataTable dtbtn = new DataTable();
                dtbtn.Columns.Add("Islemler");

                //string sqlbase = "Select DISTINCT (select T1.Descr from CUFD as T0 INNER JOIN UFD1 as T1 ON T0.FieldID = T1.FieldID where T0.TableID = 'OWOR' and T1.TableID = 'OWOR' and T1.FldValue = '{0}') as Istasyonlar,'{1}' as Kod from \"@AIF_GUNPERSPLAN\" as T0 INNER JOIN \"@AIF_GUNPERSPLAN1\" as T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"U_Tarih\" >= '" + tarih1 + "' and T0.\"U_Tarih\" <= '" + tarih2 + "' and T1.\"U_PersonelNo\" = '" + KullaniciId + "' and \"{2}\" = 'Y'";
                //string sql = "";
                //string basefield = "U_IST00";
                //string field = "";
                //for (int i = 1; i <= 8; i++)
                //{
                //    field = basefield + i;
                //    sql += string.Format(sqlbase, field.Replace("U_", ""), field.Replace("U_", ""), field);
                //    if (i != 8)
                //    {
                //        sql += " UNION ALL ";
                //    }
                //}

                DateTime dttime = new DateTime(Convert.ToInt32(tarih1.Substring(0, 4)), Convert.ToInt32(tarih1.Substring(4, 2)), Convert.ToInt32(tarih1.Substring(6, 2)));

                string gun = dttime.Day.ToString();
                string ay = dttime.Month.ToString().PadLeft(2, '0');
                string yil = dttime.Year.ToString();
                string colonparametre = "U_Gun" + gun;

                #region Günlük Personel Planlama 2 ekranı

                string sql = "";

                //sql = "Select T4.\"" + colonparametre + "\",\"U_Bolum1\",(Select \"U_BolumAdi\" from \"@AIF_BOLUMLER\" as T5 where T5.\"U_BolumKodu\" = T4.\"U_Bolum1\") as \"Bolum1Adi\",\"U_Bolum2\",(Select \"U_BolumAdi\" from \"@AIF_BOLUMLER\" as T5 where T5.\"U_BolumKodu\" = T4.\"U_Bolum2\") as \"Bolum2Adi\",\"U_Bolum3\",(Select \"U_BolumAdi\" from \"@AIF_BOLUMLER\" as T5 where T5.\"U_BolumKodu\" = T4.\"U_Bolum3\") as \"Bolum3Adi\" from \"@AIF_GUNLUKPERSPLAN\" as T3 INNER JOIN \"@AIF_GUNLUKPERSPLAN1\" as T4 ON T3.\"DocEntry\" = T4.\"DocEntry\" where T4.\"U_PersonelNo\" = '" + KullaniciId + "' and T3.\"U_Aylar\" = '" + ay + "' and T3.\"U_Yil\" = '" + yil + "' ";

                #endregion Günlük Personel Planlama 2 ekranı

                #region Günlük Personel Planlama 3 ekranı

                sql = "Select T4.\"U_Durum\",\"U_Bolum1\",(Select \"U_BolumAdi\" from \"@AIF_BOLUMLER\" as T5 where T5.\"U_BolumKodu\" = T4.\"U_Bolum1\") as \"Bolum1Adi\",\"U_Bolum2\",(Select \"U_BolumAdi\" from \"@AIF_BOLUMLER\" as T5 where T5.\"U_BolumKodu\" = T4.\"U_Bolum2\") as \"Bolum2Adi\",\"U_Bolum3\",(Select \"U_BolumAdi\" from \"@AIF_BOLUMLER\" as T5 where T5.\"U_BolumKodu\" = T4.\"U_Bolum3\") as \"Bolum3Adi\" from \"@AIF_GUNLUKPLAN\" as T3 INNER JOIN \"@AIF_GUNLUKPLAN1\" as T4 ON T3.\"DocEntry\" = T4.\"DocEntry\" where T4.\"U_PersonelNo\" = '" + KullaniciId + "' and Convert(varchar,T3.U_Tarih,112) = '" + dttime.ToString("yyyyMMdd") + "' ";

                #endregion Günlük Personel Planlama 3 ekranı

                cmd = new SqlCommand(sql, Connection.sql);

                if (Connection.sql.State != ConnectionState.Open)
                    Connection.sql.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string calisiyorMu = dt.Rows[0][0].ToString();

                    DataTable dtIstasyonlar = new DataTable();
                    dtIstasyonlar.Columns.Add("Kod", typeof(string));
                    dtIstasyonlar.Columns.Add("Istasyonlar", typeof(string));

                    if (dt.Rows[0]["U_Bolum1"].ToString() == "JOKER" || dt.Rows[0]["U_Bolum2"].ToString() == "JOKER" || dt.Rows[0]["U_Bolum3"].ToString() == "JOKER")
                    {
                        Joker = true;
                        sql = "select T1.FldValue,T1.Descr from CUFD as T0 INNER JOIN UFD1 as T1 ON T0.FieldID = T1.FieldID where T0.TableID = 'OWOR' and T1.TableID = 'OWOR' and T0.AliasID = 'ISTASYON'";

                        cmd = new SqlCommand(sql, Connection.sql);
                        sda = new SqlDataAdapter(cmd);
                        DataTable dtJoker = new DataTable();
                        sda.Fill(dtJoker);

                        foreach (DataRow item in dtJoker.Rows)
                        {
                            DataRow dr = dtIstasyonlar.NewRow();
                            dr["Kod"] = item["FldValue"].ToString();
                            dr["Istasyonlar"] = item["Descr"].ToString();
                            dtIstasyonlar.Rows.Add(dr);
                        }

                        dataGridView1.DataSource = dtIstasyonlar;

                        //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        //btn = new DataGridViewButtonColumn();
                        //btn.HeaderText = "";
                        //btn.Text = "Seç";
                        //btn.Name = "btn1";
                        //btn.UseColumnTextForButtonValue = true;
                        //dataGridView1.Columns.Add(btn);

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
                            {
                                if (dataGridView1.Columns[i].Name == "btn1")
                                    dataGridView1.Columns[i].Width = 120;
                                else
                                    dataGridView1.Columns[i].Width = 480;
                            }

                            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                            {
                                dataGridView1.Rows[i].Height = 65;

                                if (i % 2 == 0)
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                                else
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;

                                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                            }
                        }
                    }
                    else if (calisiyorMu == "X")
                    {
                        #region dtg button

                        if (dt.Rows[0]["U_Bolum1"].ToString() != "")
                        {
                            DataRow dr = dtIstasyonlar.NewRow();
                            dr["Kod"] = dt.Rows[0]["U_Bolum1"].ToString();
                            dr["Istasyonlar"] = dt.Rows[0]["Bolum1Adi"].ToString();
                            dtIstasyonlar.Rows.Add(dr);
                        }

                        if (dt.Rows[0]["U_Bolum2"].ToString() != "")
                        {
                            DataRow dr = dtIstasyonlar.NewRow();
                            dr["Kod"] = dt.Rows[0]["U_Bolum2"].ToString();
                            dr["Istasyonlar"] = dt.Rows[0]["Bolum2Adi"].ToString();
                            dtIstasyonlar.Rows.Add(dr);
                        }

                        if (dt.Rows[0]["U_Bolum3"].ToString() != "")
                        {
                            DataRow dr = dtIstasyonlar.NewRow();
                            dr["Kod"] = dt.Rows[0]["U_Bolum3"].ToString();
                            dr["Istasyonlar"] = dt.Rows[0]["Bolum3Adi"].ToString();
                            dtIstasyonlar.Rows.Add(dr);
                        }

                        dataGridView1.DataSource = dtIstasyonlar;

                        //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        //btn = new DataGridViewButtonColumn();
                        //btn.HeaderText = "";
                        //btn.Text = "Seç";
                        //btn.Name = "btn1";
                        //btn.UseColumnTextForButtonValue = true;
                        //dataGridView1.Columns.Add(btn);

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dataGridView1.Columns.Count - 1; i++)
                            {
                                if (dataGridView1.Columns[i].Name == "btn1")
                                    dataGridView1.Columns[i].Width = 120;
                                else
                                    dataGridView1.Columns[i].Width = 480;
                            }

                            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                            {
                                dataGridView1.Rows[i].Height = 65;

                                if (i % 2 == 0)
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                                else
                                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DimGray;

                                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                            }
                        }

                        #endregion dtg button
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }

                    Connection.sql.Close();
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                    dataGridView1.Font = new System.Drawing.Font("Bahnschrift", 16F, FontStyle.Bold);

                    //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
                    //dataGridView1.EnableHeadersVisualStyles = false;
                    //dataGridView1.DefaultCellStyle.BackColor = Color.LightGray;

                    dataGridView1.ClearSelection();

                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Columns["Kod"].Visible = false;
                    }
                    dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null;
            }
        }

        private void AtanmisIsler_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //Giris giris = new Giris();
                //giris.Show();
            }
            catch (Exception)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            BanaAitİsler n = new BanaAitİsler("", KullaniciId, 0, initialWidth, initialHeight, baslangicTarihi);
            n.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            GunlukTemizlik n = new GunlukTemizlik(initialWidth, initialHeight);
            n.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            Close();
        }

        public static string baslangicTarihi = "";
        public static string bitisTarihi = "";

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    baslangicTarihi = dateTimePicker1.Value.ToString("yyyyMMdd");
                    bitisTarihi = dateTimePicker2.Value.ToString("yyyyMMdd");
                    string val = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    BanaAitİsler n = new BanaAitİsler(val, KullaniciId, 0, initialWidth, initialHeight, _tarih1: baslangicTarihi);
                    n.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                CustomMsgBtn.Show("İşlem sırasında hata oluştu. Hata Kodu : AT003 " + ex.Message, "UYARI", "TAMAM");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            IstasyonlariListele(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"));
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            IstasyonlariListele(dateTimePicker1.Value.ToString("yyyyMMdd"), dateTimePicker2.Value.ToString("yyyyMMdd"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //START FORM GENİŞLİK YÜKSEKLİK
            float forGen = Width;
            float forYuk = Height;

            CustomMsgBtn.Show("Genişlik" + forGen + "Yükseklik" + forYuk, "UYARI", "TAMAM");
            //END FORM GENİŞLİK YÜKSEKLİK
        }

        private void btnOncekiGun_Click(object sender, EventArgs e)
        {
            var datetime = Convert.ToDateTime(dateTimePicker1.Value);

            dateTimePicker1.Value = datetime.AddDays(-1);
        }

        private void btnSonrakiGun_Click(object sender, EventArgs e)
        {
            var datetime = Convert.ToDateTime(dateTimePicker1.Value);

            dateTimePicker1.Value = datetime.AddDays(+1);
        }

        private void btnBugun_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}