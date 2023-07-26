using System;
using System.Collections.Generic;

namespace UserDataApplication.Models
{
    public partial class City080723
    {
        public City080723()
        {
            UserData080723s = new HashSet<UserData080723>();
        }

        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int? StateId { get; set; }

        public virtual State080723? State { get; set; }
        public virtual ICollection<UserData080723> UserData080723s { get; set; }
    }
}
