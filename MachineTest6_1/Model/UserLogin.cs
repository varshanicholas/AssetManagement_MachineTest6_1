using System;
using System.Collections.Generic;

namespace MachineTest6_1.Model;

public partial class UserLogin
{
    public int LoginId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public int? RegistrationId { get; set; }

    public virtual UserRegistration? Registration { get; set; }

    public virtual UserRole? Role { get; set; }
}
