﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Category
    {

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Title { get; set; }

    }
}
