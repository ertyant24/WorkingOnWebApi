using MFramework.Services.FakeData;
using Microsoft.EntityFrameworkCore;

namespace MyFirstApiApp.Entities
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			if (Database.CanConnect())
			{
				if (Players.Any() == false)
				{
					for (int i = 0; i < 10; i++)
					{
						Player player = new Player();
						player.FullName = NameData.GetFullName();
						player.IsActive = BooleanData.GetBoolean();
						player.ShoeSize = NumberData.GetNumber();
						player.Team = TextData.GetSentence();

						Players.Add(player);
					}

					SaveChanges();
				}

			}

		}

		public DbSet<Player> Players { get; set; }

	}
}
