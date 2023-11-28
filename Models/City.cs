using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesInvoiceProcess.Models;

[Table("cities")]
public partial class City
{
    [Key]
    [Column("cityCode")]
    public int CityCode { get; set; }

    [Column("countryCode")]
    public int? CountryCode { get; set; }

    [Column("cityEnglishName")]
    [StringLength(255)]
    public string? CityEnglishName { get; set; }

    [Column("cityArabicName")]
    [StringLength(255)]
    public string? CityArabicName { get; set; }

    [ForeignKey("CountryCode")]
    [InverseProperty("Cities")]
    public virtual Country? CountryCodeNavigation { get; set; }

    [InverseProperty("CityCodeNavigation")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
