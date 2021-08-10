﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Product
    {
        [Key]
        public int SKU { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public int NumberOfInventory { get; set; }
        public bool IsInStock
        {
            get
            {
                return NumberOfInventory >= 1 ? true : false;
            }
        }

    }
}