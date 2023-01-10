namespace MyFirstWebApp.Models
{
	public class CreatePlayerModel
	{
		public string FullName { get; set; }
		public int ShoeSize { get; set; }
		public bool IsActive { get; set; }
		public string? Team { get; set; }
	}
}
