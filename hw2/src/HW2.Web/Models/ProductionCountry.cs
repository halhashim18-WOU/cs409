using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class ProductionCountry
{
    public int Id { get; set; }

    public string CountryIdentifier { get; set; } = null!;

    public virtual ICollection<ProductionCountryAssignment> ProductionCountryAssignments { get; set; } = new List<ProductionCountryAssignment>();
}
