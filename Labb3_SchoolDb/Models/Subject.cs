using System;
using System.Collections.Generic;

namespace Labb3_SchoolDb.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
