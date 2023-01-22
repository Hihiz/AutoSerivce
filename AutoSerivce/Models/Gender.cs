using System;
using System.Collections.Generic;

namespace AutoSerivce.Models;

public partial class Gender
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();
}
