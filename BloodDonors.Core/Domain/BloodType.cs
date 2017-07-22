using System;
using BloodDonors.Core.Extensions;

namespace BloodDonors.Core.Domain
{
    public class BloodType
    {
        public string AboType { get; protected set; }
        public char RhType { get; protected set; }

        public BloodType(string aboType, char rhType )
        {
            SetAboType(aboType);
            SetRhType(rhType);
        }

        private void SetAboType(string aboType)
        {
            if (aboType.Empty())
                throw new Exception($"{nameof(aboType)} can't be empty.");
            AboType = aboType.ToUpper();
        }

        private void SetRhType(char rhType)
        {
            if (rhType != '+' && rhType != '-')
                throw new Exception($"Invalid {nameof(rhType)}.");
            RhType = rhType;
        }
    }
}
