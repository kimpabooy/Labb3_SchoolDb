﻿using System;
using System.Collections.Generic;

namespace Labb3_SchoolDb.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
