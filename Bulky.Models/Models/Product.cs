﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyBulky.Models
{
	public class Product
	{

		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string ISBN { get; set; }
		[Required]
		public string Author { get; set; }

		[Required]
		[Display(Name = "List Price")]
		[Range(1, 1000)]
		public double Price { get; set; }

		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category Category { get; set; }

		[ValidateNever]
		public string ImgUrl { get; set; }
	}
}
