using ManterCursos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/cursos")]
    public class CursosController: ControllerBase
    {
        private readonly DataContext _context;
    }
}