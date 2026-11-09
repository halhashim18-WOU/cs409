using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class Person
{
    public int Id { get; set; }

    public int JustWatchPersonId { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
