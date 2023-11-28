using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesInvoiceProcess.Models;

[Table("salesInvoiceDetails")]
public partial class SalesInvoiceDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("invoiceNo")]
    public int? InvoiceNo { get; set; }

    [Column("itemCode")]
    public int? ItemCode { get; set; }

    [Column("itemEnglishName")]
    [StringLength(255)]
    public string? ItemEnglishName { get; set; }

    [Column("price", TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }

    [Column("totalAmount", TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("vat", TypeName = "decimal(5, 2)")]
    public decimal? Vat { get; set; }

    [Column("totalAmountWithVAT", TypeName = "decimal(18, 2)")]
    public decimal? TotalAmountWithVat { get; set; }

    [ForeignKey("InvoiceNo")]
    [InverseProperty("SalesInvoiceDetails")]
    public virtual SalesInvoice? InvoiceNoNavigation { get; set; }

    [ForeignKey("ItemCode")]
    [InverseProperty("SalesInvoiceDetails")]
    public virtual Item? ItemCodeNavigation { get; set; }
}
