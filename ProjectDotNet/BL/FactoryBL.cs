using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public namespace BL
{
    class FactoryBL
    {
        static IBL bl = null;
        public static IBL GetBL()
        {
            if (bl == null)
                bl = new BL_Imp();
            return bl;
        }
    }
}
