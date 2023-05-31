using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace L57Scaffold.Models;

[Table("Donhang")]
public partial class Donhang
{
    [Key]
    [Column("DonhangID")]
    public int DonhangId { get; set; }

    [Column("KhachhangID")]
    public int? KhachhangId { get; set; }

    [Column("NhanvienID")]
    public int? NhanvienId { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Ngaydathang { get; set; }

    [Column("ShipperID")]
    public int? ShipperId { get; set; }
}
