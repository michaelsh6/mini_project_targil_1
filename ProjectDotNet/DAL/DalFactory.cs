using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalFactory
    {
        static IDAL dal = null;
        public static IDAL GetDal()
        {
            if (dal == null)
                dal = new DalImp();
            return dal;
        }
    }
}
