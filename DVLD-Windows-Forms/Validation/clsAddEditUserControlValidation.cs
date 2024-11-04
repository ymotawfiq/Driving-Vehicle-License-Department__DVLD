using DVLD_Business_Logic_Tear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net.Mail;

namespace DVLD_Windows_Forms.Validation
{
    internal class clsAddEditUserControlValidation
    {
        private static bool _IsChar(char ch)
        {
            return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
        }

        private static bool _IsCharOrNumber(char ch)
        {
            return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9');
        }

        private static bool _IsNumber(char ch)
        {
            return (ch >= '0' && ch <= '9');
        }

        private static bool _IsValidWord(string word)
        {
            for(int i=0; i<word.Length; i++)
            {
                if (!_IsChar(word[i])) return false;
            }
            return true;
        }

        private static bool _IsValidNationalNo(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (!_IsCharOrNumber(word[i])) return false;
            }
            return true;
        }

        private static bool _IsValidPhoneNumber(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (!_IsNumber(word[i])) return false;
            }
            return true;
        }

        public static bool ValidateName(string fName)
        {
            if (fName.Length < 3 || !_IsValidWord(fName)) return false;
            return true;
        }

        public static bool ValidateNationalNo(string nationalNo, int personID)
        {
            if (nationalNo.Length > 20 || !_IsValidNationalNo(nationalNo) 
                || (!clsPerson.ExistsByNationalNoForPerson(nationalNo, personID)
                && clsPerson.ExistsByNationalNo(nationalNo))
                || nationalNo.Trim() == "" || nationalNo == null) return false;
            return true;
        }

        public static bool ValidatePhone(string phone, int personID)
        {
            if (phone.Length > 20 || !_IsValidPhoneNumber(phone)
                || phone.Trim() == "" || phone == null
                || (!clsPerson.ExistsByPhoneForPerson(phone, personID) 
                && clsPerson.ExistsByPhone(phone))) return false;
            return true;
        }

        public static bool ValidateAddress(string address)
        {
            if (address.Length > 500 || address.Trim()=="") return false;
            return true;
        }


        public static bool ValidateEmail(string email, int personID)
        {
            if (email.Trim() == "") return true;
            if (email.IndexOf("@") <= 0) return false;
            try
            {
                MailAddress address = new MailAddress(email);
                if (address.Address != email || (!clsPerson.ExistsByEmailForPerson(email, personID)
                    && clsPerson.ExistsByEmail(email))) return false;
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

    }
}
