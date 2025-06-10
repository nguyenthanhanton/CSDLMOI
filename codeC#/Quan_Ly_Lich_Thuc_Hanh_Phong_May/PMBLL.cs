using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Lich_Thuc_Hanh_Phong_May
{
    class PMBLL
    {
        ChucNang CNPM;
        public PMBLL()
        {
            CNPM = new ChucNang();
        }
        public DataTable GetAllPM()
        {
            return CNPM.GetAllPM();
        }
        public bool ThemPM(tblPM pm)
        {
            return CNPM.ThemPM(pm);
        }
        public bool SuaPM(tblPM pm)
        {
            return CNPM.SuaPM(pm);
        }
        public bool XoaPM(tblPM pm)
        {
            return CNPM.XoaPM(pm);
        }
        public DataTable FindPM(string keyword)
        {
            return CNPM.FindPM(keyword);
        }
    }
}
