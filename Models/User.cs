using System;
using System.Collections.Generic;

namespace Compare.Models;

public partial class User
{
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        return Email == ((User)obj).Email;
    }

    public override int GetHashCode()
    {
        return Email.GetHashCode();
    }
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Course> Courses { get; } = new List<Course>();
}
