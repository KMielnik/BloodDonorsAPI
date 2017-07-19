using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Core.Domain
{
    public class Personnel
    {
        public string Pesel { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Name { get; protected set; }
    }
}
