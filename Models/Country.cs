using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesInvoiceProcess.Models;

[Table("countries")]
public partial class Country
{
    [Key]
    [Column("countryCode")]
    public int CountryCode { get; set; }

    [Column("countryEnglishName")]
    [StringLength(255)]
    public string? CountryEnglishName { get; set; }

    [Column("countryArabicName")]
    [StringLength(255)]
    public string? CountryArabicName { get; set; }

    [InverseProperty("CountryCodeNavigation")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    [InverseProperty("CountryCodeNavigation")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
