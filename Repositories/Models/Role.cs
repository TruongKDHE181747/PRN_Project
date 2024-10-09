﻿using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
