using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Azure.Identity;

namespace StudentManagementSystem.MVVM.Model
{
    public class SecurityMethods
    {
        

        public static string HashSha256(string password)
        {

            using (SHA256 Hash = SHA256.Create())
            {
                // convert the password to a byte array
                byte[] bytes = Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for(int i=0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                // returns 64 characters since each hex digit represents 4bit pattern
                return builder.ToString();
            }
        }


        #region ValidationCheckers 

        private static bool ContainWhitespaceInBetween(string input)
        {
            if (input is not null)
            {
                input.Trim();
                if (input.Any(char.IsWhiteSpace))
                    return false;
            }
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !ContainWhitespaceInBetween(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || !ContainWhitespaceInBetween(password))
                return false;

            Regex rx = new Regex(@"[!@#$%^&*()_+\-=\[\]{};:,.<>\?]");



            if (password.Any(char.IsUpper) && password.Any(char.IsLower)
                && password.Any(char.IsNumber) && rx.IsMatch(password)
                && password.Length > 7 && password.Length < 33)
                    return true;
                
            return false;
            
        }

        public static bool IsValidLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login) || !ContainWhitespaceInBetween(login))
                return false;
            else if (login.Length > 5 && login.Length < 51)
                return true;

            return false;
        }

        public static bool IsValidPhoneNumber(string PhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                return true;

            if (PhoneNumber.Length == 9 && PhoneNumber.All(char.IsNumber))
                return true;

            return false;
        }

        public static bool IsValidTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || !ContainWhitespaceInBetween(title))
                return false;

            title.ToLower().Trim();

            if (title == "proffesor" || title == "doctor" || title == "master" || title == "bachelor" || title == "dean")
                return true;

            return false;
        }
        
        public static string[] TrimAll(params string[] input)
        {
            string[] result = new string[input.Length];
            if (input is null || input.Length == 0)
                throw new ArgumentException("input can't be null");
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    result[i] = input[i].Trim();
                }
                return result;
            }
        }

        public static bool IsValidGrade(int grade)
        {
            if (grade == 1 || grade == 2 || grade == 3 || grade == 4 || grade == 5 || grade == 6)
                return true;
            else
                return false;
        }


        #endregion  
    }
}
