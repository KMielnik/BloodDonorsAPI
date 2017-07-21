using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BloodDonors.Core.Extensions;
using System.Text.RegularExpressions;

namespace BloodDonors.Core.Domain
{
    public class Donor
    {
        [StringLength(11), MinLength(11)]
        public string Pesel { get; protected set; }
        [StringLength(200), MinLength(5)]
        public string Password { get; protected set; }
        [StringLength(200)]
        public string Salt { get; protected set; }
        public string Name { get; protected set; }
        public DateTime? LastDonated { get; protected set; }
        public BloodType BloodType { get; protected set; }
        public string Mail { get; protected set; }
        public string Phone { get; protected set; }

        public Donor(string pesel,string password,string salt,
            string name, BloodType bloodType,string mail,string phone)
        {
            SetPesel(pesel);
            SetPassword(password, salt);
            SetName(name);  
            
            LastDonated = null;

            SetBloodType(bloodType);
            SetMail(mail);
            SetPhone(phone);
        }

        private void SetPesel(string pesel)
        {
            if (pesel.Empty())
                throw new Exception($"{nameof(pesel)} can't be empty.");
            if (pesel.Length != 11)
                throw new Exception($"Invalid {nameof(pesel)}");

            var regexOnlyDigits = new Regex("^[0-9]+$");
            if (regexOnlyDigits.IsMatch(pesel) == false)
                throw new Exception($"Invalid {nameof(pesel)}");

            Pesel = pesel;
        }

        private void SetPassword(string password, string salt)
        {
            if (password.Empty())
                throw new Exception($"{nameof(password)} can't be empty.");
            if (salt.Empty())
                throw new Exception($"{nameof(salt)} can't be empty.");
            if (password.Length <5)
                throw new Exception($"{nameof(password)} must be at least 5 characters long.");
            Password = password;
            Salt = salt;
        }

        private void SetName(string name)
        {
            if (name.Empty())
                throw new Exception($"{nameof(name)} can't be empty.");
            Name = name;
        }

        private void SetBloodType(BloodType bloodType)
        {
            BloodType = bloodType ?? throw new Exception($"{nameof(bloodType)} can't be null.");
        }
        private void SetMail(string mail)
        {
            if (mail.Empty())
                throw new Exception($"{nameof(mail)} can't be empty.");
            Mail = mail.ToLowerInvariant();
        }
        private void SetPhone(string phone)
        {
            if (phone.Empty())
                throw new Exception($"{nameof(phone)} can't be empty.");

            var regexOnlyDigits = new Regex("^[0-9]+$");
            if (regexOnlyDigits.IsMatch(phone) == false)
                throw new Exception($"Invalid {nameof(phone)}");

            Phone = phone;
        }
    }
}
