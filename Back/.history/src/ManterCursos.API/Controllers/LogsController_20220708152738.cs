using ManterCursos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly DataContext _context;

        public LogsController(DataContext context)
        {
            this._context = context; 
        }
    }
}