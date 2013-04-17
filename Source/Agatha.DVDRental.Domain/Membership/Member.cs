using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agatha.DVDRental.Domain.Membership
{
    public class Member
    {
        public Member(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
