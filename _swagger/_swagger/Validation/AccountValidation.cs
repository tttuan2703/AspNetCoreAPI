using _swagger.Models;
using System;
using System.Text.RegularExpressions;

namespace _swagger.Validation
{
    public class AccountValidation
    {
        public string checkValidateAccount(AccountViewModel account)
        {
            //Check Fullname not null
            if (account.FullName == null)
                return "Facebook not null";
            //Check number in array FullName \
            if (!Regex.Match(account.Address, @"^[a-zA-Z0-9\s]+$").Success)
                return "Address not contain special characters.";
            //Check age must be at least 18
            DateTime now = DateTime.Now;
            if (now.Year - account.DateOfBirth.Year < 18)
                return "Age must be least at 18.";
            //Check address length least at 20
            if (!Regex.Match(account.Phone, @"^([0-9]{10})$").Success)
                return "Phone not valid.";
            //Check telephone
            if (!Regex.Match(account.Telephone, @"^([0-9]{10})$").Success)
                return "Telephone not valid.";
            //Check Email
            if (!Regex.Match(account.Email, @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$").Success)
                return "Email not valid. Ex:gmail123@gmail.com";
            //Check facebook not null
            if (account.Faceboook == null)
                return "Facebook not null";
            //Check length UserName
            if (!Regex.Match(account.UserName, @"^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$").Success)
                return "UserName contain only contains alphanumeric characters, underscore and dot.(length>=8 and length<=20)\n" +
                        "Ex:Tai_Khoan.123";
            //Check Password
            if (!Regex.Match(account.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,}$").Success)
                return "Password not strong. Password must contain lowercase and uppercase characters, numbers, and special characters, with a length of at least 8\n" +
                        "Ex: Pass@123";
            //Check confirm
            if (!account.ConfirmPassword.Equals(account.Password))
                return "confirm password doesn't match";
            return "";
        }
    }
}
