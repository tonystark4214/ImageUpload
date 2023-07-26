using System;
using System.Collections.Generic;

namespace UserDataApplication.Models
{
    public partial class UserData080723
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string? ZipCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public string? ImgLoc { get; set; }

        public virtual City080723? City { get; set; }
        public virtual Country080723? Country { get; set; }
        public virtual State080723? State { get; set; }
    }
}
