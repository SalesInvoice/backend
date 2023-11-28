using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SalesInvoiceProcess.Models;

[Table("salesInvoices")]
public partial class SalesInvoice
{
    [Key]
    [Column("invoiceNo")]
    public int InvoiceNo { get; set; }

    [Column("invoiceDate", TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [Column("customerCode")]
    public int? CustomerCode { get; set; }

    [Column("englishName")]
    [StringLength(255)]
    public string? EnglishName { get; set; }

    [Column("remarks")]
    [StringLength(255)]
    public string? Remarks { get; set; }

    [Column("totalSalesInvoiceAmount", TypeName = "decimal(18, 2)")]
    public decimal? TotalSalesInvoiceAmount { get; set; }

    [Column("totalVATAmount", TypeName = "decimal(18, 2)")]
    public decimal? TotalVatamount { get; set; }

    [Column("totalSalesInvoiceAmountWithVATAmount", TypeName = "decimal(18, 2)")]
    public decimal? TotalSalesInvoiceAmountWithVatamount { get; set; }

    [ForeignKey("CustomerCode")]
    [InverseProperty("SalesInvoices")]
    public virtual Customer? CustomerCodeNavigation { get; set; }

    [InverseProperty("InvoiceNoNavigation")]
    public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetails { get; set; } = new List<SalesInvoiceDetail>();
}
