using System;
using System.Collections.Generic;

namespace UserDataApplication.Models
{
    public partial class State080723
    {
        public State080723()
        {
            City080723s = new HashSet<City080723>();
            UserData080723s = new HashSet<UserData080723>();
        }

        public int StateId { get; set; }
        public string? StateName { get; set; }
        public int? CountryId { get; set; }

        public virtual Country080723? Country { get; set; }
        public virtual ICollection<City080723> City080723s { get; set; }
        public virtual ICollection<UserData080723> UserData080723s { get; set; }
    }
}
