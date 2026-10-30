using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class GenreAssignment
{
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Show Show { get; set; } = null!;
}
