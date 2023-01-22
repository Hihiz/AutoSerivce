using System;
using System.Collections.Generic;

namespace AutoSerivce.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public string DurationWork { get; set; } = null!;

    public string? Description { get; set; }

    public double? Discount { get; set; }

    public string? MainImagePath { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; } = new List<ClientService>();

    public virtual ICollection<ServicePhoto> ServicePhotos { get; } = new List<ServicePhoto>();
}
