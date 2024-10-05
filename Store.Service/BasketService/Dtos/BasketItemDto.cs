﻿using System.ComponentModel.DataAnnotations;

namespace Store.Service.BasketService.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be between 1 to 10 pieces")]
        public int Quantity { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public string TypeName { get; set; }
    }
}