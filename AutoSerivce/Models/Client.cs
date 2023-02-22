using AutoSerivce.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace AutoSerivce.Models;

public partial class Client : ViewModel
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

    public virtual Gender GenderCode { get; set; } = null!;

    public virtual ICollection<ClientService> ClientServices { get; } = new List<ClientService>();
    public virtual ICollection<TagOfClient> TagOfClients { get; } = new List<TagOfClient>();

    private string _imagePath;

    [NotMapped]
    public virtual string? ImagePath
    {
        get
        {
            if (File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, $"Фото клиентов/{PhotoPath}")))
            {
                _imagePath = System.IO.Path.Combine(Environment.CurrentDirectory, $"Фото клиентов/{PhotoPath}");
                return _imagePath;
            }
            else
            {
                PhotoPath = "picture.jpg";
                return null;
            }
        }

        set => Set(ref _imagePath, value);
    }
}
