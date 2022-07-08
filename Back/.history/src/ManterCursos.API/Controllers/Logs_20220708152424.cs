using Microsoft.AspNetCore.Mvc;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class Logs : ControllerBase
    {
        private readonly DataContext _context;
    }
}