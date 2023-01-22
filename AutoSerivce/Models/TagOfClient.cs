using System;
using System.Collections.Generic;

namespace AutoSerivce.Models;

public partial class TagOfClient
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int TagId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
