using Microsoft.AspNetCore.Mvc;
using MyCRM.Database;

namespace MyCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly MainDbContext _dbContext;

        public UtilityController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("Migrate")]
        public IActionResult Migrate()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            return Ok("БД обновлена и создана заново");
        }
    }
}