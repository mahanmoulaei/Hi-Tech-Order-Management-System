using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FinalProject.VALIDATION
{
    public static class Validator
    {
        public static bool IsValidId(string input)
        {
            if (!(Regex.IsMatch(input, @"^\d{4}$")))
            {
                return false;
            }
            return true;
        }

        //Overloaded methods : Two methods have the same name but
        //different signatures 
        public static bool IsValidId(string input, int size)
        {
            if (!(Regex.IsMatch(input, @"^\d{" + size + "}$")))
            {
                return false;
            }
            return true;
        }

        public static bool IsValidName(string input)
        {
            if (input == "")
            {
                return false;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (!(Char.IsLetter(input[i])) && !(Char.IsWhiteSpace(input[i])))
                {
                    return false;
                }

            }

            return true;

        }

        public static bool IsEmpty(string input)
        {
            if (input == "")
            {
                return true;
            }

            return false;


        }

        public static bool IsValidEmail(string email, String targetName)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                MessageBox.Show("Invalid " + targetName, "Error");
                return false;
            }
        }
    }
}
