using ManterCursos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/usurios")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public CursosController(DataContext context)
        {
            this._context = context; 
        }
        
    }
}