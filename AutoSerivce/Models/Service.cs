using AutoSerivce.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace AutoSerivce.Models;

public partial class Service : ViewModel
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

    //Изображение
    //public virtual string? ImagePath { get { return System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{MainImagePath}"); }  }

    private string _imagePath;
    [NotMapped]
    public virtual string? ImagePath
    {
        get
        {
            if (File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{MainImagePath}")))
            {
                _imagePath = System.IO.Path.Combine(Environment.CurrentDirectory, $"Услуги автосервиса/{MainImagePath}");
                return _imagePath;
            }
            else
            {
                MainImagePath = "picture.jpg";
                return null;
            }
        }

        set => Set(ref _imagePath, value);
    }
}
