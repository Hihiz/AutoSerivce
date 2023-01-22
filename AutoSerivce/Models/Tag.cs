using System;
using System.Collections.Generic;

namespace AutoSerivce.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Color { get; set; } = null!;

    public virtual ICollection<TagOfClient> TagOfClients { get; } = new List<TagOfClient>();
}
