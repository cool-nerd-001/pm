﻿
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace ProductMicroservice.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product
    {
        
        [Key]
        public Guid PId { get; set; }

        
        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }


        
        [Required]
        [MaxLength(15)]
        public string? Brand { get; set; }

        
        [Required]
        [MaxLength(15)]
        public string? Category { get; set; }


        
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price Field is Required Or Price Should be a Positive Number")]
        public int Price { get; set; }


        
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Stock Field is Required Or Stock Should be a Positive Number")]
        public int Stock { get; set; }


        
        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        
        [Required]
        [Url]
        public string? Image {get; set; }
  
    }
}
