using System;
using System.Collections.Generic;

namespace MachineTest6_1.Model;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
