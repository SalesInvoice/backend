using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesInvoiceProcess.Models;

[Table("items")]
public partial class Item
{
    [Key]
    [Column("itemCode")]
    public int ItemCode { get; set; }

    [Column("itemEnglishName")]
    [StringLength(255)]
    public string? ItemEnglishName { get; set; }

    [Column("itemArabicName")]
    [StringLength(255)]
    public string? ItemArabicName { get; set; }

    [Column("price", TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    [Column("vat", TypeName = "decimal(5, 2)")]
    public decimal? Vat { get; set; }

    [InverseProperty("ItemCodeNavigation")]
    public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetails { get; set; } = new List<SalesInvoiceDetail>();
}
