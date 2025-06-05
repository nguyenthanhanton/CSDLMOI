using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    class GVBLL
    {
        ChucNang CNGV;
        public GVBLL()
        {
            CNGV = new ChucNang();
        }
        public DataTable GetAllGV()
        {
            return CNGV.GetAllGV();
        }
        public bool ThemGV(tblGV gv)
        {
            return CNGV.ThemGV(gv);
        }
        public bool SuaGV(tblGV gv)
        {
            return CNGV.SuaGV(gv);
        }
        public bool XoaGV(tblGV gv)
        {
            return CNGV.XoaGV(gv);
        }
        public DataTable FindGV(string keyword)
        {
            return CNGV.FindGV(keyword);
        }
    }
}
