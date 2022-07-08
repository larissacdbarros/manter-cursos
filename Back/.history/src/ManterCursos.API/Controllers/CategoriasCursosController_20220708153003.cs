using System.Collections.Generic;
using System.Threading.Tasks;
using ManterCursos.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManterCursos.API.Controllers
{
    [ApiController]
    [Route("api/categoriasCursos")]
    public class CategoriasCursosController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriasCursosController(DataContext context)
        {
            this._context = context;            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaCurso>>> GetAll()
        {
            return await _context.CategoriasDespesas.ToListAsync();
        }
    }
}