using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBulky.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		[Required]
		[MaxLength(20)]
		[DisplayName("Category Name")]
		public string Name { get; set; }
	}
}
