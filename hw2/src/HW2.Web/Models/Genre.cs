using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string GenreString { get; set; } = null!;

    public virtual ICollection<GenreAssignment> GenreAssignments { get; set; } = new List<GenreAssignment>();
}
