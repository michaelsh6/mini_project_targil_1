using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Host
    {
        public int HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string phoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankAccunt BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set;}

        public override string ToString()
        {
            return string.Format("HostKey={0}, PrivateName={1}, FamilyName={2}, phoneNumber={3}, MailAddress={4},BankAccountNumber={5},CollectionClearance={6}", HostKey, PrivateName, FamilyName, phoneNumber, MailAddress, BankAccountNumber, CollectionClearance) + this.BankBranchDetails.ToString();
        }

    }
}
