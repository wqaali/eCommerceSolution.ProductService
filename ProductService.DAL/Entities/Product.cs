﻿using System.ComponentModel.DataAnnotations;

namespace ProductService.DAL.Entities;

public class Product
{
  [Key]
  public Guid ProductID { get; set; }
  public string ProductName { get; set; }
  public string Category { get; set; }
  public decimal? UnitPrice { get; set; }
  public int? QuantityInStock { get; set; }
}
