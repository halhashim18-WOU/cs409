using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class ProductionCountryAssignment
{
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int ProductionCountryId { get; set; }

    public virtual ProductionCountry ProductionCountry { get; set; } = null!;

    public virtual Show Show { get; set; } = null!;
}
