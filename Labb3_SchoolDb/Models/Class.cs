using System;
using System.Collections.Generic;

namespace Labb3_SchoolDb.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public int? StaffId { get; set; }

    public virtual Staff? Staff { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
