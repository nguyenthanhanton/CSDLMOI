using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    class DataConnect
    {

        string conStr;
        public DataConnect()
        {
            conStr = "server=DESKTOP-8SM0K2C;" +
                                                          "uid=nbich;pwd=tranthingocbich1412;" +
                                                          "database=QuanLyLichThucHanh";
        }
        public SqlConnection getConnect()
        {
            return new SqlConnection(conStr);
        }
    }
}
