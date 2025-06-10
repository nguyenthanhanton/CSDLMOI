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
            conStr = "Data Source = Admin\\SQLEXPRESS06; Initial Catalog = QuanLyLichThucHanh; UID = hieutranminh; PWD = tranminhhieu;";
        }
        public SqlConnection getConnect()
        {
            return new SqlConnection(conStr);
        }
    }
}
