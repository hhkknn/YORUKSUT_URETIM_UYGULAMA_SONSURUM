using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIF.UVT.DatabaseLayer
{
    public class Connection
    {
        //committ.
        public static string sqlString = "";

        public static string sqlStringVal = "";
        private static SqlConnection _sql;

        public static SqlConnection sql
        {
            get
            {
                {
                    try
                    {
                        //if (_sql == null)
                        //{
                        //_sql = new SqlConnection(@"Server=.;Database=SBODemoTR;User Id=sa;Password=123qwe;");
                        //sqlString = string.Format("Server=192.168.2.51;Database={0};User Id=sa;Password=Yoruk@1234", sqlStringVal);
                        if (Giris.mKodValue == "10B1C4")
                        {
                            sqlString = string.Format("Server=172.55.10.16;Database={0};User Id=sa;Password=Qaz1Wsx2", Giris.dbName);
                        }
                        else if (Giris.mKodValue == "20R5DB")
                        {
                            sqlString = string.Format("Server=192.168.2.53;Database={0};User Id=sa;Password=Yoruk123", Giris.dbName);
                        }
                        _sql = new SqlConnection(sqlString);
                        //}
                        //else
                        //{
                        //    return _sql;
                        //}
                    }
                    catch (Exception)
                    {
                    }

                    return _sql;
                }
            }
        }
    }
}