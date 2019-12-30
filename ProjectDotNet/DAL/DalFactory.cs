using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class DalFactory
    {
        static DalImp dal = null;
        public static DalImp GetDal()
        {
            if (dal == null)
                dal = new DalImp();
            return dal;
        }
    }
}
