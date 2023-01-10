using System.ComponentModel.DataAnnotations;

namespace MyFirstApiApp.Entities
{
	public class Player
	{
		[Key]
		public int Id { get; set; }

		[StringLength(50)]
		public string FullName { get; set; }

		public int ShoeSize { get; set; }
		public bool IsActive { get; set; }
		public string? Team { get; set; }

	}
}
