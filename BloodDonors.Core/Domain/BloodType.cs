﻿using System;
using BloodDonors.Core.Extensions;

namespace BloodDonors.Core.Domain
{
    public class BloodType
    {
        public string AboType { get; protected set; }
        public string RhType { get; protected set; }

        public BloodType(string aboType, string rhType )
        {
            SetAboType(aboType);
            SetRhType(rhType);
        }

        public BloodType()
        {
            
        }

        private void SetAboType(string aboType)
        {
            if (aboType.Empty())
                throw new ArgumentNullException($"{nameof(aboType)} can't be empty.");
            AboType = aboType.ToUpper();
        }

        private void SetRhType(string rhType)
        {
            if (rhType != "+" && rhType != "-")
                throw new ArgumentException($"Invalid {nameof(rhType)}.");
            RhType = rhType;
        }
    }
}
