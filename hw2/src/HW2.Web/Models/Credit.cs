using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class Credit
{
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int PersonId { get; set; }

    public int RoleId { get; set; }

    public string? CharacterName { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual Show Show { get; set; } = null!;
}
