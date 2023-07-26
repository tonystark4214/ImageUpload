using System;
using System.Collections.Generic;

namespace UserDataApplication.Models
{
    public partial class Country080723
    {
        public Country080723()
        {
            State080723s = new HashSet<State080723>();
            UserData080723s = new HashSet<UserData080723>();
        }

        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        public virtual ICollection<State080723> State080723s { get; set; }
        public virtual ICollection<UserData080723> UserData080723s { get; set; }
    }
}
