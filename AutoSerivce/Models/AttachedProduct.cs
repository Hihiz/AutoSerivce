using System;
using System.Collections.Generic;

namespace AutoSerivce.Models;

public partial class AttachedProduct
{
    public int Id { get; set; }

    public int MainProductId { get; set; }

    public int AttachedProductId { get; set; }

    public virtual Product AttachedProductNavigation { get; set; } = null!;

    public virtual Product MainProduct { get; set; } = null!;
}
