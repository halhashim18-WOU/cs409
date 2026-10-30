using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class ShowType
{
    public int Id { get; set; }

    public string ShowTypeIdentifier { get; set; } = null!;

    public virtual ICollection<Show> Shows { get; set; } = new List<Show>();
}
