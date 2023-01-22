using System;
using System.Collections.Generic;

namespace AutoSerivce.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Cost { get; set; }

    public string? Description { get; set; }

    public string? MainImagePath { get; set; }

    public bool IsActive { get; set; }

    public int? ManufacturerId { get; set; }

    public virtual ICollection<AttachedProduct> AttachedProductAttachedProductNavigations { get; } = new List<AttachedProduct>();

    public virtual ICollection<AttachedProduct> AttachedProductMainProducts { get; } = new List<AttachedProduct>();

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<ProductPhoto> ProductPhotos { get; } = new List<ProductPhoto>();

    public virtual ICollection<ProductSale> ProductSales { get; } = new List<ProductSale>();
}
