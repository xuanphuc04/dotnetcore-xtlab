﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace L57Scaffold.Models;

[Table("Cungcap")]
public partial class Cungcap
{
    [Key]
    [Column("CungcapID")]
    public int CungcapId { get; set; }

    [StringLength(255)]
    public string? Tendaydu { get; set; }

    [StringLength(255)]
    public string? TenLienhe { get; set; }

    [StringLength(255)]
    public string? Diachi { get; set; }

    [StringLength(255)]
    public string? Thanhpho { get; set; }

    [StringLength(255)]
    public string? MaBuudien { get; set; }

    [StringLength(255)]
    public string? Quocgia { get; set; }

    [StringLength(255)]
    public string? Dienthoai { get; set; }
}
