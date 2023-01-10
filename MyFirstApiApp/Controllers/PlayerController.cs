using Microsoft.AspNetCore.Mvc;
using MyFirstApiApp.Entities;
using MyFirstApiApp.Models;

namespace MyFirstApiApp.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PlayerController : Controller
	{
		private DatabaseContext _databaseContext;

		public PlayerController(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		[HttpGet]
		public IActionResult List()
		{
			return Ok(_databaseContext.Players.ToList());
		}

		[HttpGet("{count}/{order}")]

		public IActionResult List(int count, string order)
		{
			List<Player> plyers = null;

			if (order == "asc")
			{
				plyers = _databaseContext.Players.OrderBy(x => x.Id).Take(count).ToList();
			}
			else if (order == "desc")
			{
				plyers = _databaseContext.Players.OrderByDescending(x => x.Id).Take(count).ToList();
			}
			else
			{
				plyers = _databaseContext.Players.Take(count).ToList();
			}

			return Ok(plyers);
		}

		[HttpGet("{Id}")]
		public IActionResult GetById(int Id)
		{
			Player play = _databaseContext.Players.Find(Id);

			if (play == null)
			{
				return NotFound(Id);
			}

			return Ok(play);
		}

		[HttpPost]
		public IActionResult Create(PlayerCreateModel model)
		{
			if (model.FullName == "Erto")
			{
				return BadRequest(model);
			}

			Player player = new Player()
			{
				FullName = model.FullName,
				ShoeSize = model.ShoeSize,
				IsActive = model.IsActive,
				Team = model.Team,
			};

			_databaseContext.Players.Add(player);
			_databaseContext.SaveChanges();

			return Created("", model);
		}

		[HttpPut("{Id}")]
		public IActionResult Edit([FromRoute] int Id, [FromBody] PlayerUpdateModel model)
		{
			Player players = _databaseContext.Players.Find(Id);

			if (players == null)
			{
				return NotFound();
			}

			players.FullName = model.FullName;
			players.ShoeSize = model.ShoeSize;
			players.IsActive = model.IsActive;
			players.Team = model.Team;

			_databaseContext.SaveChanges();

			return Ok(players);
		}

		[HttpDelete("{Id}")]
		public IActionResult Delete([FromRoute]int Id)
		{
			Player players = _databaseContext.Players.Find(Id);

			if(players == null)
			{
				return NotFound();
			}

			_databaseContext.Players.Remove(players);
			_databaseContext.SaveChanges();

			return Ok();
		}

	}
}
