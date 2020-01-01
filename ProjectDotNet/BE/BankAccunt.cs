using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankAccunt
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }


        public override string ToString()
        {
            return string.Format("BankNumber={0}, BankName={1}, BranchNumber={2}, BranchAddress={3}, BranchCity={4}", BankNumber, BankName, BranchNumber, BranchAddress, BranchCity);
        }
    }
}
