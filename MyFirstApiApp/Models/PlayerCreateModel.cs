using System.ComponentModel.DataAnnotations;

namespace MyFirstApiApp.Models
{
	public class PlayerCreateModel
	{
		[StringLength(30)]
		public string FullName { get; set; }
		public int ShoeSize { get; set; }
		public bool IsActive { get; set; }
		public string Team { get; set; }
	}
}
