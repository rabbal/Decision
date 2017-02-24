using System.Net.Mail;
using NTierMvcFramework.Common.Extensions;

namespace NTierMvcFramework.Common.Email
{
    public class EmailAddress
    {
        public EmailAddress(string address)
        {
            Address = address;
        }

        public EmailAddress(string address, string displayName)
        {
            Address = address;
            DisplayName = displayName;
        }

        public string Address { get; set; }
        public string DisplayName { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return DisplayName.IsEmpty() ? Address : "{0} [{1}]".FormatCurrent(DisplayName, Address);
        }

        public MailAddress ToMailAddress()
        {
            return new MailAddress(Address, DisplayName);
        }
    }
}