using ManterCursos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/categoriasCursos")]
    public class CategoriasCursos : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriasCursos(Parameters)
        {
            
        }
    }
}