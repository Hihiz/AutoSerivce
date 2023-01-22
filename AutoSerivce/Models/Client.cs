using System;
using System.Collections.Generic;

namespace AutoSerivce.Models;

public partial class Client
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirsName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime? Birthday { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public int GenderCodeId { get; set; }

    public string? PhotoPath { get; set; }

    public virtual ICollection<ClientService> ClientServices { get; } = new List<ClientService>();

    public virtual Gender GenderCode { get; set; } = null!;

    public virtual ICollection<TagOfClient> TagOfClients { get; } = new List<TagOfClient>();
}
