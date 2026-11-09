using System;
using System.Collections.Generic;

namespace HW2.Web.Models;

public partial class Show
{
    public int Id { get; set; }

    public string JustWatchShowId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int ShowTypeId { get; set; }

    public int ReleaseYear { get; set; }

    public int? AgeCertificationId { get; set; }

    public int Runtime { get; set; }

    public int? Seasons { get; set; }

    public string? ImdbId { get; set; }

    public double? ImdbScore { get; set; }

    public double? ImdbVotes { get; set; }

    public double? TmdbPopularity { get; set; }

    public double? TmdbScore { get; set; }

    public virtual AgeCertification? AgeCertification { get; set; }

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public virtual ICollection<GenreAssignment> GenreAssignments { get; set; } = new List<GenreAssignment>();

    public virtual ICollection<ProductionCountryAssignment> ProductionCountryAssignments { get; set; } = new List<ProductionCountryAssignment>();

    public virtual ShowType ShowType { get; set; } = null!;
}
