using System;
using System.Collections.Generic;
using System.Text;
using BloodDonors.Core.Extensions;
using System.Text.RegularExpressions;

namespace BloodDonors.Core.Domain
{
    public class Personnel
    {
        public string Pesel { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Name { get; protected set; }

        public Personnel(string pesel, string password, string salt,string name)
        {
            SetPesel(pesel);
            SetPassword(password, salt);
            SetName(name);
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
            if (password.Length < 5)
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
    }
}
