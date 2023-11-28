using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesInvoiceProcess.Models;

[Table("customers")]
public partial class Customer
{
    [Key]
    [Column("customerCode")]
    public int CustomerCode { get; set; }

    [Column("englishName")]
    [StringLength(255)]
    public string? EnglishName { get; set; }

    [Column("arabicName")]
    [StringLength(255)]
    public string? ArabicName { get; set; }

    [Column("mobileNo")]
    [StringLength(20)]
    public string? MobileNo { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("countryCode")]
    public int? CountryCode { get; set; }

    [Column("cityCode")]
    public int? CityCode { get; set; }

    [Column("addressLine1")]
    [StringLength(255)]
    public string? AddressLine1 { get; set; }

    [Column("addressLine2")]
    [StringLength(255)]
    public string? AddressLine2 { get; set; }

    [Column("addressLine3")]
    [StringLength(255)]
    public string? AddressLine3 { get; set; }

    [ForeignKey("CityCode")]
    [InverseProperty("Customers")]
    public virtual City? CityCodeNavigation { get; set; }

    [ForeignKey("CountryCode")]
    [InverseProperty("Customers")]
    public virtual Country? CountryCodeNavigation { get; set; }

    [InverseProperty("CustomerCodeNavigation")]
    public virtual ICollection<SalesInvoice> SalesInvoices { get; set; } = new List<SalesInvoice>();
}
