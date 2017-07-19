using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Core.Domain
{
    public class BloodType
    {
        public int Id { get; protected set; }
        public string AboType { get; protected set; }
        public char RhType { get; protected set; }
    }
}
