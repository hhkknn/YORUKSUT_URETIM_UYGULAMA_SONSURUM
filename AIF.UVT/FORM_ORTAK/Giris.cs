using AIF.UVT.DatabaseLayer;
using AIF.UVT.Models;
using AIF.UVT.UVTService;
using Newtonsoft.Json;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AIF.UVT
{
    public partial class Giris : Form
    {
        //cOMMT i

        #region Font İşlemleri

        public int initialWidth;
        public int initialHeight;
        public float initialFontSize;

        #endregion Font İşlemleri

        public Giris()
        {
            #region Update işlemleri

            //mKodValue = "20R5DB";

            //WebClient request = new WebClient();

            //string url = "ftp://ftp.tanas.com.tr/UVT/" + "version.xml";

            //try
            //{
            //    var _assembly = System.Reflection.Assembly
            //        .GetExecutingAssembly().GetName().CodeBase;

            //    var _path = System.IO.Path.GetDirectoryName(_assembly) + "\\";

            //    string configFile = System.IO.Path.Combine(_path, "AIF.UVT_AutoUpdate.exe.config");
            //    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            //    configFile = configFile.Replace("file:\\", "");
            //    configFileMap.ExeConfigFilename = configFile;
            //    System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            //    config.AppSettings.Settings["MusteriKodu"].Value = mKodValue;
            //    config.Save();
            //}
            //catch (Exception)
            //{
            //}

            //request.Credentials = new NetworkCredential("aif@aifteam.com", "SJ^FB5TAKDGq");

            //try
            //{
            //    byte[] newFileData = request.DownloadData(url);
            //    string fileString = System.Text.Encoding.UTF8.GetString(newFileData);

            //    XmlDocument xmlDoc = new XmlDocument();
            //    string _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            //    if (fileString.StartsWith(_byteOrderMarkUtf8))
            //    {
            //        fileString = fileString.Remove(0, _byteOrderMarkUtf8.Length);
            //    }
            //    xmlDoc.LoadXml(fileString);
            //    XmlNodeList parentNode = xmlDoc.GetElementsByTagName("version");

            //    var aaa = parentNode[0].InnerText;
            //    //url = "ftp://ftp.tanas.com.tr/UVT/" + "AIF.UVT.exe";
            //    //request.DownloadFile(url, @"C:\KargoLog\" + "AIF.UVT.exe");

            //    if (exeVersion != aaa)
            //    {
            //        if (MessageBox.Show("Uygulamanın güncel versiyonu yayınlanmıştır, Yüklemek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        {
            //            //this.Close();
            //            //Dispose();
            //            //Application.Exit();
            //        }
            //        else
            //        {
            //            this.Close();
            //            Dispose();
            //            Application.Exit();
            //            System.Diagnostics.Process.Start(Application.StartupPath + "\\AIF.UVT_AutoUpdate.exe");
            //        }
            //        //    System.Threading.Thread t = new System.Threading.Thread(
            //        //new System.Threading.ThreadStart(updatexe)

            //        //);
            //        //    t.Start();
            //    }
            //}
            //catch (WebException ex)
            //{
            //}

            //txtUserName.Text = Properties.Settings.Default["username"].ToString();
            ////txtPassword.Text = Properties.Settings.Default["password"].ToString();
            //if (txtUserName.Text.Count() > 0)
            //    chkBeniHatirla.Checked = true;

            #endregion Update işlemleri

            InitializeComponent();

            #region Font işlemleri

            AutoScaleMode = AutoScaleMode.None;

            initialWidth = Width;
            initialHeight = Height;

            initialFontSize = label1.Font.Size;
            label1.Resize += Form_Resize;

            initialFontSize = label2.Font.Size;
            label2.Resize += Form_Resize;

            initialFontSize = textBox1.Font.Size;
            textBox1.Resize += Form_Resize;

            initialFontSize = textBox2.Font.Size;
            textBox2.Resize += Form_Resize;

            initialFontSize = btnGiris.Font.Size;
            btnGiris.Resize += Form_Resize;

            initialFontSize = btnIptal.Font.Size;
            btnIptal.Resize += Form_Resize;

            #endregion Font işlemleri
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            #region Font İşlemleri

            SuspendLayout();
            float proportionalNewWidth = (float)Width / initialWidth;
            float proportionalNewHeight = (float)Height / initialHeight;

            label1.Font = new Font(label1.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                label1.Font.Style);

            label2.Font = new Font(label2.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                label2.Font.Style);

            textBox1.Font = new Font(textBox1.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                textBox1.Font.Style);

            textBox2.Font = new Font(textBox2.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                textBox2.Font.Style);

            btnGiris.Font = new Font(btnGiris.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                btnGiris.Font.Style);

            btnIptal.Font = new Font(btnIptal.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                btnIptal.Font.Style);

            label3.Font = new Font(label3.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                label3.Font.Style);

            cmbCompany.Font = new Font(cmbCompany.Font.FontFamily, initialFontSize *
                (proportionalNewWidth > proportionalNewHeight ? proportionalNewHeight : proportionalNewWidth),
                cmbCompany.Font.Style);

            ResumeLayout();

            #endregion Font İşlemleri
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

        //public static SAPbobsCOM.Company oCompany;

        public static string exeVersion = "1.0.0.23";
        public static string mKodValue = System.Configuration.ConfigurationManager.AppSettings["MusteriKodu"];
        private List<ComboValues> comboValues = new List<ComboValues>();
        public static string value = "";
        public static SqlConnection sqlConnection_Uvt = null;
        public static string dbName = "";
        public static string id = "";

        //Version myVersion;
        private void Giris_Load(object sender, EventArgs e)
        {
            //mKodValue = "10B1C4";
            mKodValue = "20R5DB";

            //if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            //}

            //lblVersion.Text = String.Concat("V" + myVersion);

            //lblVersion.Text = System.Reflection.Assembly.GetAssembly.myVersion.ToString();
            lblVersion.Text = "V" + exeVersion;

            textBox2.Focus();

            Library.Helper n = new Library.Helper();
            n.SetAllControlsFont(Controls);

            #region MKOD İle Background Değişimi

            var lastOpenedForm = Application.OpenForms.Cast<Form>().Last();

            if (mKodValue == "10B1C4")
            {
                lastOpenedForm.BackgroundImage = Properties.Resources.OTAT_UVT_ArkaPlanV3;
            }
            else if (mKodValue == "20R5DB")
            {
                lastOpenedForm.BackgroundImage = Properties.Resources.YORUK_UVT_ArkaPlanv2;
            }

            #endregion MKOD İle Background Değişimi

            UVTService.UVTServiceSoapClient UVTServiceSoapClient = new UVTService.UVTServiceSoapClient();
            Response response = new Response();
            response = UVTServiceSoapClient.GetCompanyList("", mKodValue);
            if (response.List.Rows.Count > 0)
            {
                cmbCompany.DataSource = response.List;
                cmbCompany.DisplayMember = "cmpName";
                cmbCompany.ValueMember = "dbName";
                cmbCompany.Enabled = true;
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                UVTServiceSoapClient UVTServiceSoapClient = new UVTServiceSoapClient();
                string db = cmbCompany.SelectedValue.ToString();

                sqlConnection_Uvt = new SqlConnection(UVTServiceSoapClient.getConnectionAsString(db, mKodValue));

                if (db == "")
                {
                    CustomMsgBtn.Show("Şirket seçimi yapmadan işleme devam edilemez.", "UYARI", "TAMAM");
                    return;
                }

                #region Databaseden Kullanıcı Okuma

                //SqlCommand cmdw = new SqlCommand("select \"U_UVTKod\" as Kod, \"U_UVTPass\" as Pass,\"firstName\" as Ad,\"lastName\" as Soyad from OHEM where ISNULL(\"U_UVTKod\",'')<>''", Giris.sqlConnection_Uvt);

                //Giris.sqlConnection_UvtStringVal = cmbCompany.SelectedValue.ToString();

                SqlCommand cmd = null;

                if (mKodValue == "10B1C4") //2022.03.29 chn
                {
                    cmd = new SqlCommand("select  Cast(\"Code\" as varchar(10)) as \"Kod\",\"ExtEmpNo\" as \"Pass\",\"firstName\" as Ad,\"lastName\" as Soyad from OHEM", sqlConnection_Uvt);
                }
                if (mKodValue == "20R5DB")
                {
                    cmd = new SqlCommand("select  Cast(\"empID\" as varchar(10)) as \"Kod\",\"ExtEmpNo\" as \"Pass\",\"firstName\" as Ad,\"lastName\" as Soyad from OHEM", sqlConnection_Uvt);
                }

                if (sqlConnection_Uvt.State == ConnectionState.Closed)
                {
                    sqlConnection_Uvt.Open();
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sqlConnection_Uvt.Close();

                #endregion Databaseden Kullanıcı Okuma

                string kullaniciadi = textBox2.Text;
                string kullaniciParolasi = textBox1.Text;
                if (textBox2.Text == "")
                {
                    CustomMsgBtn.Show("Kullanıcı Adı boş bırakılamaz.", "UYARI", "TAMAM");
                    return;
                }

                if (kullaniciParolasi == "")
                {
                    CustomMsgBtn.Show("Kullanıcı Parolası boş bırakılamaz.", "UYARI", "TAMAM");
                    return;
                }

                #region Kullanıcı Doğrulama

                if (textBox2.Text != "" && kullaniciParolasi != "")
                {
                    var userExist = dt.Select("Kod = '" + kullaniciadi + "'").Count();

                    if (userExist == 0)
                    {
                        CustomMsgBtn.Show("Kullanıcı Bulunamadı.", "UYARI", "TAMAM");
                        return;
                    }
                    else
                    {
                        var user = (from cst in dt.AsEnumerable()
                                    where cst.Field<string>("Kod") == kullaniciadi && cst.Field<string>("Pass") == kullaniciParolasi
                                    select new
                                    {
                                        firstNameLastName = cst.Field<string>("Ad") + " " + cst.Field<string>("Soyad")
                                    }).ToList();

                        if (user.Count == 0)
                        {
                            CustomMsgBtn.Show("Kullanıcı adı veya şifre yanlış.", "UYARI", "TAMAM");
                            return;
                        }

                        //progressbar = true;
                        //ProgressBar prgress = new ProgressBar(ListeleData);
                        //prgress.ShowDialog(this);
                        dbName = cmbCompany.SelectedValue.ToString();
                        id = textBox2.Text;
                        try
                        {
                            var connectresp = UVTServiceSoapClient.Login("", "", dbName, mKodValue);

                            if (connectresp.Value != 0)
                            {
                                CustomMsgBtn.Show(connectresp.Description, "UYARI", "TAMAM");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            CustomMsgBtn.Show("Hata." + ex.Message, "UYARI", "TAMAM");
                            return;
                        }

                        textBox1.Text = "";
                        textBox2.Text = "";
                        AtanmisIsler frm = new AtanmisIsler(user.Select(x => x.firstNameLastName).Count() > 0 ? user.Select(x => x.firstNameLastName).First() : "", this, kullaniciadi, Width, Height);
                        //progressbar = false;
                        //prgress.Close();
                        frm.Show();
                        Hide();
                    }
                }

                #endregion Kullanıcı Doğrulama

                cmd = new SqlCommand("select \"U_UrtPrtSekli\" as \"UrtPrtSekli\" from \"@AIF_UVT_PARAM\"", Connection.sql);

                if (Connection.sql.State == ConnectionState.Closed)
                {
                    Connection.sql.Open();
                }

                sda = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);

                if (dt2.Rows.Count > 0)
                {
                    UretimPartilendirmeSekli = dt2.Rows[0][0].ToString();
                }

                Connection.sql.Close();
            }
            catch (Exception ex)
            {
                CustomMsgBtn.Show("Giriş yapılırken hata oluştu. Hata Kodu GRS001 " + ex.Message, "UYARI", "TAMAM");
            }
        }

        public static string UretimPartilendirmeSekli = "";

        private void btnIptal_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void SetAllControlsFont(Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl.Controls != null)
                    SetAllControlsFont(ctrl.Controls);

                if (ctrl.AccessibilityObject.Role == AccessibleRole.StaticText)
                    ctrl.Font = new Font("Bahnschrift SemiCondensed", ctrl.Font.Size - 4, FontStyle.Bold);
                else
                    ctrl.Font = new Font("Bahnschrift SemiCondensed", ctrl.Font.Size - 2, FontStyle.Bold);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //START FORM GENİŞLİK YÜKSEKLİK
            float forGen = Width;
            float forYuk = Height;

            CustomMsgBtn.Show("Genişlik" + forGen + "Yükseklik" + forYuk, "UYARI", "TAMAM");

            //END FORM GENİŞLİK YÜKSEKLİK
        }

        private void cmbCompany_DropDownClosed(object sender, EventArgs e)
        {
            textBox2.Select();
        }
    }
}